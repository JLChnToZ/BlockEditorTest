using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CefSharp;

namespace BlockEditorTest {
    public class ManifestResourceHandler : IRequestHandler {
        public const string manifestProtocol = "http://internal/";

        public EventHandler<ContentReceivedEventArgs> PostContentReceived;

        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port,
            string realm, string scheme, ref string username, ref string password) {
            return false;
        }

        public bool GetDownloadHandler(IWebBrowser browser, string mimeType,
            string fileName, long contentLength, ref IDownloadHandler handler) {
            return false;
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request,
            NavigationType naigationvType, bool isRedirect) {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse) {
            Assembly asm = Assembly.GetExecutingAssembly();
            string MIME = "application/octet-stream";
            string requestURL = requestResponse.Request.Url, _lower = requestURL.ToLower();
            if ((_lower == manifestProtocol + "postdata" || _lower.StartsWith(manifestProtocol + "postdata?"))
                && PostContentReceived != null)
                PostContentReceived(browser, new ContentReceivedEventArgs(requestResponse.Request.Body));
            if (requestURL.Contains('?'))
                requestURL = requestURL.Substring(0, requestURL.IndexOf('?'));
            _lower = requestURL.ToLower();
            if (_lower.StartsWith(manifestProtocol)) {
                if (_lower.EndsWith(".html") || _lower.EndsWith(".htm"))
                    MIME = "text/html";
                else if (_lower.EndsWith(".css"))
                    MIME = "text/css";
                else if (_lower.EndsWith(".js"))
                    MIME = "text/javascript";
                else if (_lower.EndsWith(".txt"))
                    MIME = "text/plain";
                requestURL = requestURL.Substring(manifestProtocol.Length);
                requestURL = requestURL.Replace('/', '.').Replace(' ', '_');
                try {
                    requestResponse.RespondWith(asm.GetManifestResourceStream(asm.GetName().Name + "." + requestURL), MIME);
                } catch { }
            }
            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText,
            string mimeType, System.Net.WebHeaderCollection headers) { }
    }

    public class ContentReceivedEventArgs : EventArgs {

        private string _content;

        public string Content { get { return _content; } }

        internal ContentReceivedEventArgs(string Content) {
            _content = Content;
        }
    }
}
