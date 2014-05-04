using System;
using System.Windows.Forms;

namespace BlockEditorTest {
    class FormControlObject {

        private MainForm parent;

        public string jsdata = "", xmldata = "";

        public FormControlObject(MainForm parent) {
            this.parent = parent;
        }

        public void Drag() {
            parent.DragWindow();
        }

        public void setJSData(string data) {
            jsdata = data;
        }

        public void setXMLData(string data) {
            xmldata = data;
        }
    }
}
