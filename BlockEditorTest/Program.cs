using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace BlockEditorTest {
    static class Program {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = "";
            if (args.Length > 0)
                foreach (string arg in args) {
                    if (arg.StartsWith("-lang=")) {
                        Thread t = Thread.CurrentThread;
                        t.CurrentCulture = t.CurrentUICulture = strings.Culture = new CultureInfo(arg.Substring(6));
                    } else if (File.Exists(arg))
                        path = arg;
                }
            if (path != "")
                Application.Run(new MainForm(path));
            else
                Application.Run(new MainForm());
        }
    }
}
