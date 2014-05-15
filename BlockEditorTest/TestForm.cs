using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jurassic;

namespace BlockEditorTest {
    public partial class TestForm : Form {

        private StringBuilder _turboBuffer;
        private ScriptEngine _engine;
        private bool _turbo;

        public ScriptEngine Engine {
            get { return _engine; }
            set { _engine = value; }
        }

        public bool TurboMode {
            get { return _turbo; }
            set {
                if (!value) popTurboString();
                _turbo = value;
            }
        }

        public TestForm() {
            InitializeComponent();

            textOutput.Font = new Font(FontFamily.GenericMonospace, 12F);
            textOutput.MaxLength = int.MaxValue;

            _engine = new ScriptEngine();
            _turboBuffer = new StringBuilder();

            initScriptEngine();
        }

        private void initScriptEngine() {
            _engine.SetGlobalFunction("print", new Action<string>((s) => outputString(FixStringLineWrap(s))));
            _engine.SetGlobalFunction("printLine", new Action<string>((s) => outputString(FixStringLineWrap(s) + "\r\n")));
            _engine.SetGlobalFunction("prompt", new Func<string, string, string>((m, d) => {
                popTurboString();
                PromptDialog dlg = new PromptDialog();
                dlg.PromptText = m;
                dlg.Value = d;
                dlg.messageBoxIcon = MessageBoxIcon.Question;
                return dlg.ShowDialog(this) == DialogResult.OK ? dlg.Value : d;
            }));
        }

        private void outputString(string s) {
            if (_turbo) _turboBuffer.Append(s);
            else appendAndScroll(s);
        }

        private void popTurboString() {
            if (!_turbo) return;
            appendAndScroll(_turboBuffer.ToString());
            _turboBuffer.Clear();
        }

        private void appendAndScroll(string s) {
            textOutput.AppendText(s);
            textOutput.Select(textOutput.TextLength, 0);
            textOutput.ScrollToCaret();
        }

        private static string FixStringLineWrap(string src) {
            return src.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        public void RunScript(string Script) {
            try {
                _engine.Execute(Script);
                popTurboString();
            } catch (JavaScriptException ex) {
                popTurboString();
                outputString(string.Format("\r\n於第{1}行發生錯誤：{0}", ex.Message, ex.LineNumber));
            } finally {
                outputString("\r\n程式結束。\r\n");
            }
        }
    }
}
