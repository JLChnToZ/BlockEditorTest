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
            _engine.SetGlobalFunction("print", new Action<string>((s) => {
                textOutput.AppendText(FixStringLineWrap(s));
                textOutput.Select(textOutput.TextLength, 0);
                textOutput.ScrollToCaret();
            }));
            _engine.SetGlobalFunction("printLine", new Action<string>((s) => {
                textOutput.AppendText(FixStringLineWrap(s) + "\r\n");
                textOutput.Select(textOutput.TextLength, 0);
                textOutput.ScrollToCaret();
            }));
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

        private static string FixStringLineWrap(string src) {
            return src.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        public void RunScript(string Script) {
            _engine.Execute(Script);
        }
    }
}
