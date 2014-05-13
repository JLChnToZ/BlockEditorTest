using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BlockEditorTest {
    public static class FormExtensionFunctions {
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        public static void DragWindow(this Form form) {
            if (form.InvokeRequired) {
                form.Invoke(new Action<Form>(DragWindow), form);
                return;
            }
            ReleaseCapture();
            SendMessage(form.Handle, 0xA1, 0x2, 0);
        }
    }
}
