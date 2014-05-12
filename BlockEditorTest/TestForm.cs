using System;
using System.Windows.Forms;
using Jurassic;

namespace BlockEditorTest {
    public partial class TestForm : Form {

        private ScriptEngine _engine;

        public ScriptEngine Engine {
            get { return _engine; }
            set { _engine = value; }
        }

        public TestForm() {
            InitializeComponent();
            _engine = new ScriptEngine();
            initScriptEngine();
        }

        private void initScriptEngine() {
            _engine.SetGlobalFunction("print", new Action<string>((s) => outputString(FixStringLineWrap(s))));
            _engine.SetGlobalFunction("printLine", new Action<string>((s) => outputString(FixStringLineWrap(s) + "\r\n")));
            _engine.SetGlobalFunction("prompt", new Func<string, string, string>((m, d) => {
                PromptDialog dlg = new PromptDialog();
                dlg.PromptText = m;
                dlg.Value = d;
                bool result = dlg.ShowDialog(this) == DialogResult.OK;
                if (result)
                    return dlg.Value;
                else
                    return d;
            }));
        }

        private void outputString(string s) {
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
            } catch (JavaScriptException ex) {
                outputString(string.Format("\r\n於第{1}行發生錯誤：{0}", ex.Message, ex.LineNumber));
            } finally {
                outputString("\r\n程式結束。\r\n");
            }
        }
    }
}
