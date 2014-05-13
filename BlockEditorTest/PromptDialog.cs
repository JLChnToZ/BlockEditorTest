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

        private MessageBoxIcon _icon = MessageBoxIcon.None;

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

        public bool isPrompt {
            get {
                return textInput.Visible;
            }
            set {
                textInput.Visible = value;
            }
        }

        public bool isOKCancel {
            get {
                return btnOK.Visible;
            }
            set {
                btnOK.Visible = value;
                AcceptButton = btnOK.Visible ? btnOK : btnCancel;
            }
        }

        public MessageBoxIcon messageBoxIcon {
            get {
                return _icon;
            }
            set {
                _icon = value;
                switch (_icon) {
                    case MessageBoxIcon.Asterisk:
                        BackgroundImage = SystemIcons.Asterisk.ToBitmap();
                        break;
                    case MessageBoxIcon.Error:
                        BackgroundImage = SystemIcons.Error.ToBitmap();
                        break;
                    case MessageBoxIcon.Exclamation:
                        BackgroundImage = SystemIcons.Exclamation.ToBitmap();
                        break;
                    case MessageBoxIcon.Question:
                        BackgroundImage = SystemIcons.Question.ToBitmap();
                        break;
                    case MessageBoxIcon.None:
                        BackgroundImage = null;
                        break;
                }
            }
        }

        public PromptDialog() {
            InitializeComponent();
            this.textInput.MaxLength = int.MaxValue;
            this.labelPrompt.Font = SystemFonts.MessageBoxFont;
            this.panel.BackColor = Color.FromArgb(128, SystemColors.Window);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            e.Graphics.Clear(BackColor);
            if (BackgroundImage == null) return;
            var rc = new Rectangle(
                panel.Padding.Left,
                this.ClientSize.Height - BackgroundImage.Height * 2 - panel.Padding.Bottom,
                BackgroundImage.Width * 2, BackgroundImage.Height * 2);
            e.Graphics.DrawImage(BackgroundImage, rc);
        }

        void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = btnOK.Visible ? DialogResult.Cancel : DialogResult.OK;
            Close();
        }

        void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        void panel_MouseDown(object sender, MouseEventArgs e) {
            this.DragWindow();
        }
    }
}
