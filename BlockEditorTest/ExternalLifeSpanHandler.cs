using System;
using System.Diagnostics;
using CefSharp;

namespace BlockEditorTest {
    class ExternalLifeSpanHandler: ILifeSpanHandler {

        public void OnBeforeClose(IWebBrowser browser) { }

        public bool OnBeforePopup(IWebBrowser browser, string url, ref int x, ref int y, ref int width, ref int height) {
            try {
                Process.Start(url);
            } catch { }
            return true;
        }

    }
}
