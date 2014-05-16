using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlockEditorTest {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            textBox1.Text = Properties.Resources.about;
            Text = strings.f_about;
        }
    }
}
