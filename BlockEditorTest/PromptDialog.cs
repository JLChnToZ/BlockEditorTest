using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlockEditorTest {
    public partial class PromptDialog : Form {

        public string PromptText {
            get {
                return labelPrompt.Text;
            }
            set {
                labelPrompt.Text = value;
            }
        }

        public string Value {
            get {
                return textInput.Text;
            }
            set {
                textInput.Text = value;
            }
        }

        public PromptDialog() {
            InitializeComponent();
        }

        private void OnKeyPress(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                    e.Handled = true;
                    break;
            }
        }
    }
}
