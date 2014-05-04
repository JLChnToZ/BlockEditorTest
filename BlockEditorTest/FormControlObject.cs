using System;
using System.Windows.Forms;

namespace BlockEditorTest {
    class FormControlObject {

        public EventHandler OnDataArrive;

        private MainForm parent;

        public string jsdata = "", xmldata = "";

        public FormControlObject(MainForm parent) {
            this.parent = parent;
        }

        public void Drag() {
            parent.DragWindow();
        }

        public void setData(string JSData, string XMLData) {
            jsdata = JSData;
            xmldata = XMLData;
            if (OnDataArrive != null)
                OnDataArrive(this, new EventArgs());
        }
    }
}
