using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using CefSharp;
using CefSharp.WinForms;

namespace BlockEditorTest {
    enum FileType {
        XML = 0,
        JS = 1,
    }

    class MainForm : Form {
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        WebView webView;
        FormControlObject ctrl;

        private Action prepared;

        public string FilePath { get; set; }
        public FileType fileType { get; set; }

        public MainForm() {
            ClientSize = new Size(640, 480);
            ShowIcon = false;

            FilePath = "";
            fileType = FileType.XML;

            Menu = new MainMenu();
            CreateMenu();

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FixedFontFamily = FontFamily.GenericMonospace.Name;

            webView = new WebView("http://internal/res/index.html", browserSettings);
            webView.Dock = DockStyle.Fill;
            webView.RequestHandler = new ManifestResourceHandler();
            webView.LifeSpanHandler = new ExternalLifeSpanHandler();
            webView.JsDialogHandler = new WebViewDialogHandler(this);
            webView.PropertyChanged += WebViewTitleChanged;

            this.Controls.Add(webView);

            CEF.RegisterJsObject("win", ctrl = new FormControlObject(this));
            ctrl.OnDataArrive += OnDataArrive;
        }

        public MainForm(string FilePath)
            : this() {
            this.FilePath = FilePath;
            if (FilePath.ToLower().EndsWith(".js"))
                fileType = FileType.JS;
            LoadFile();
        }

        void CreateMenu() {
            MenuItem fileMenu = new MenuItem("檔案 (&F)");
            fileMenu.MenuItems.Add("新增 (&N)", (s, e) => NewFile());
            fileMenu.MenuItems.Add("開啟 (&O)", (s, e) => OpenFile());
            fileMenu.MenuItems.Add("儲存 (&S)", (s, e) => {
                if (FilePath.Length > 0)
                    SaveFile();
                else
                    SaveAs(s, e);
            });
            fileMenu.MenuItems.Add("另存為 (&A)", SaveAs);
            fileMenu.MenuItems.Add("-");
            fileMenu.MenuItems.Add("離開 (&E)", (s, e) => {
                Close();
            });
            Menu.MenuItems.Add(fileMenu);

            MenuItem testMenu = new MenuItem("測試 (&T)");
            testMenu.MenuItems.Add("執行 (&R)", (s, e) => {
                prepareSaveFile(new Action(() => {
                    TestForm testing = new TestForm();
                    testing.Show();
                    testing.RunScript(OutputData(FileType.JS));
                }));
            });
            Menu.MenuItems.Add(testMenu);
        }

        void NewFile() {
            webView.ExecuteScript("resetAll();");
        }

        void OpenFile() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "拼圖格式 (*.blockly)|*.xml|JavaScript 腳本 (*.js)|*.js|所有檔案 (*.*)|*.*";
            dlg.FileName = FilePath;
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    FilePath = dlg.FileName;
                    if (dlg.FileName.ToLower().EndsWith(".js"))
                        fileType = FileType.JS;
                    else
                        fileType = FileType.XML;
                    LoadFile();
                } catch (Exception ex) {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void LoadFile() {
            string f = File.ReadAllText(FilePath);
            f = f.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\"", "\\\"").Replace("\'", "\\\'");
            if (fileType == FileType.JS)
                webView.ExecuteScript(string.Format("resetAll();loadJSFile(\"{0}\");", f));
            else
                webView.ExecuteScript(string.Format("resetAll();loadXMLFile(\"{0}\");", f));
        }

        void SaveFile() {
            prepareSaveFile(new Action(() => {
                try {
                    File.WriteAllText(FilePath, OutputData(fileType));
                } catch (Exception ex) {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        string OutputData(FileType fileType) {
            if (fileType == FileType.JS)
                return ctrl.jsdata;
            return ctrl.xmldata;
        }

        void SaveAs(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "拼圖格式 (*.blockly)|*.xml|JavaScript 腳本 (*.js)|*.js|所有檔案 (*.*)|*.*";
            dlg.FileName = FilePath;
            if (dlg.ShowDialog() == DialogResult.OK) {
                FilePath = dlg.FileName;
                if (dlg.FilterIndex == 2)
                    fileType = FileType.JS;
                else
                    fileType = FileType.XML;
                SaveFile();
            }
        }

        void WebViewTitleChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName != "Title") return;
            if (InvokeRequired) {
                Invoke(new PropertyChangedEventHandler(WebViewTitleChanged), sender, e);
                return;
            }
            Text = webView.Title;
        }

        void prepareSaveFile(Action prepared) {
            this.prepared = prepared;
            webView.ExecuteScript("saveFile();");
        }

        internal void DragWindow() {
            if (InvokeRequired) {
                Invoke(new Action(DragWindow));
                return;
            }
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        void OnDataArrive(object sender, EventArgs e) {
            if (InvokeRequired)
                Invoke(prepared);
            else
                prepared();
        }
    }
}
