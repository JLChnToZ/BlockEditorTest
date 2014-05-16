using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace BlockEditorTest {
    enum FileType {
        XML = 0,
        JS = 1,
    }

    class MainForm : Form {

        WebView webView;
        FormControlObject ctrl;

        bool applyTurbo = false;

        Action prepared;

        public string FilePath { get; set; }
        public FileType fileType { get; set; }

        public MainForm() {
            ClientSize = new Size(640, 480);
            ShowIcon = false;

            FilePath = "";
            fileType = FileType.XML;

            Menu = new MainMenu();
            CreateMenu();

            BrowserSettings browserSettings = new BrowserSettings() {
                HistoryDisabled = true,
                FixedFontFamily = FontFamily.GenericMonospace.Name,
                DefaultFontSize = 12,
                DefaultFixedFontSize = 8
            };

            webView = new WebView("http://internal/res/index.html", browserSettings) {
                Dock = DockStyle.Fill,
                RequestHandler = new ManifestResourceHandler() {
                    culture = Thread.CurrentThread.CurrentCulture
                },
                LifeSpanHandler = new ExternalLifeSpanHandler(),
                JsDialogHandler = new WebViewDialogHandler(this)
            };
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
            MenuItem fileMenu = new MenuItem(strings.m_file);
            fileMenu.MenuItems.Add(strings.m_file_new, (s, e) => NewFile());
            fileMenu.MenuItems.Add(strings.m_file_open, (s, e) => OpenFile());
            fileMenu.MenuItems.Add(strings.m_file_import, (s, e) => OpenFile(false));
            fileMenu.MenuItems.Add(strings.m_file_save, (s, e) => {
                if (FilePath.Length > 0)
                    SaveFile();
                else
                    SaveAs(s, e);
            });
            fileMenu.MenuItems.Add(strings.m_file_saveas, SaveAs);
            fileMenu.MenuItems.Add("-");
            fileMenu.MenuItems.Add(strings.m_file_exit, (s, e) => {
                Close();
            });
            Menu.MenuItems.Add(fileMenu);

            MenuItem testMenu = new MenuItem(strings.m_test);
            testMenu.MenuItems.Add(strings.m_test_run, (s, e) => {
                prepareSaveFile(new Action(() => {
                    TestForm testing = new TestForm();
                    testing.Show();
                    testing.TurboMode = applyTurbo;
                    testing.RunScript(OutputData(FileType.JS));
                }));
            });
            testMenu.MenuItems.Add(strings.m_test_turbo, (s, e) => ((MenuItem)s).Checked = applyTurbo = !((MenuItem)s).Checked);
            Menu.MenuItems.Add(testMenu);

            MenuItem helpMenu = new MenuItem(strings.m_help);
            helpMenu.MenuItems.Add(strings.m_help_about, (s, e) => {
                new AboutForm().ShowDialog(this);
            });
            Menu.MenuItems.Add(helpMenu);
        }

        void NewFile() {
            webView.ExecuteScript("resetAll();");
        }

        void OpenFile(bool clearFirst = true) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strings.f_filetypes;
            dlg.FileName = FilePath;
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    FilePath = dlg.FileName;
                    if (dlg.FileName.ToLower().EndsWith(".js"))
                        fileType = FileType.JS;
                    else
                        fileType = FileType.XML;
                    LoadFile(clearFirst);
                } catch (Exception ex) {
                    showErrorDialog(ex.Message);
                }
            }
        }

        void LoadFile(bool clearFirst = true) {
            string f = File.ReadAllText(FilePath);
            f = f.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\"", "\\\"").Replace("\'", "\\\'");
            if (fileType == FileType.JS)
                webView.ExecuteScript(string.Format((clearFirst ? "resetAll();" : "") + "loadJSFile(\"{0}\");", f));
            else
                webView.ExecuteScript(string.Format((clearFirst ? "resetAll();" : "") + "loadXMLFile(\"{0}\");", f));
        }

        void SaveFile() {
            prepareSaveFile(new Action(() => {
                try {
                    File.WriteAllText(FilePath, OutputData(fileType));
                } catch (Exception ex) {
                    showErrorDialog(ex.Message);
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
            dlg.Filter = strings.f_filetypes;
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

        void OnDataArrive(object sender, EventArgs e) {
            if (InvokeRequired)
                Invoke(prepared);
            else
                prepared();
        }

        void showErrorDialog(string message) {
            PromptDialog dlg = new PromptDialog();
            dlg.PromptText = message;
            dlg.isOKCancel = false;
            dlg.isPrompt = false;
            dlg.messageBoxIcon = MessageBoxIcon.Error;
            dlg.ShowDialog(this);
        }
    }
}
