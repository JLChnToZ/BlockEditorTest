namespace BlockEditorTest {
    partial class PromptDialog {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.labelPrompt = new System.Windows.Forms.Label();
            this.textInput = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.ControlButtonFloatLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.ControlButtonFloatLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPrompt
            // 
            this.labelPrompt.AutoSize = true;
            this.labelPrompt.Location = new System.Drawing.Point(11, 20);
            this.labelPrompt.Name = "labelPrompt";
            this.labelPrompt.Size = new System.Drawing.Size(74, 12);
            this.labelPrompt.TabIndex = 0;
            this.labelPrompt.Text = "{Place Holder}";
            this.labelPrompt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // textInput
            // 
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.Location = new System.Drawing.Point(11, 125);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(176, 22);
            this.textInput.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.ControlButtonFloatLayout);
            this.panel.Controls.Add(this.labelPrompt);
            this.panel.Controls.Add(this.textInput);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(8);
            this.panel.Size = new System.Drawing.Size(200, 160);
            this.panel.TabIndex = 4;
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // ControlButtonFloatLayout
            // 
            this.ControlButtonFloatLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlButtonFloatLayout.BackColor = System.Drawing.Color.Transparent;
            this.ControlButtonFloatLayout.Controls.Add(this.btnOK);
            this.ControlButtonFloatLayout.Controls.Add(this.btnCancel);
            this.ControlButtonFloatLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.ControlButtonFloatLayout.Location = new System.Drawing.Point(0, 0);
            this.ControlButtonFloatLayout.Margin = new System.Windows.Forms.Padding(0);
            this.ControlButtonFloatLayout.Name = "ControlButtonFloatLayout";
            this.ControlButtonFloatLayout.Size = new System.Drawing.Size(198, 20);
            this.ControlButtonFloatLayout.TabIndex = 4;
            this.ControlButtonFloatLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnOK.Image = global::BlockEditorTest.Properties.Resources.tick;
            this.btnOK.Location = new System.Drawing.Point(178, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(20, 20);
            this.btnOK.TabIndex = 2;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCancel.Image = global::BlockEditorTest.Properties.Resources.cross;
            this.btnCancel.Location = new System.Drawing.Point(158, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(20, 20);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PromptDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(200, 160);
            this.Controls.Add(this.panel);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "{Dialog}";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ControlButtonFloatLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPrompt;
        private System.Windows.Forms.TextBox textInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.FlowLayoutPanel ControlButtonFloatLayout;
    }
}