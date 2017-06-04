using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFImageLoader
{
    class DownloaderV2 : IWebsiteImagesDownloader
    {
        private readonly string _folder;
        private readonly int _threadCount;
        private readonly object _lock;

        private WebBrowser _browser;
        private List<string> _imagesUrls;
        private bool _isSuspended;
        private int _progress;
        private int _total;

        public event WebsiteImagesDownloaderProgressEventHandler OnProgress;
        public event WebsiteImagesDownloaderEventHandler OnFinished;

        public string Address { get; }

        public DownloaderV2(string address, string folder, int threadCount)
        {
            Address = address;
            _folder = folder;
            _threadCount = threadCount;
            _lock = new object();
        }

        public void Start()
        {
            _browser = new WebBrowser();
            _browser.DocumentCompleted += _browser_DocumentCompleted;
            _browser.Navigate(Address);
        }

        private async void _browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = (WebBrowser)sender;
            _imagesUrls = GetImagesUrls(browser.Document).ToList();
            _total = _imagesUrls.Count;
            var tasks = new Task[_threadCount];
            for (int i = 0; i < _threadCount; i++)
            {
                var task = Task.Run(async () =>
                {
                    string url;
                    while (TryEnqueueUrl(out url))
                    {
                        await Task.Delay(200);
                        var filename = Path.Combine(_folder, Guid.NewGuid() + ".bmp");
                        var client = new WebClient();
                        File.WriteAllBytes(filename, client.DownloadData(url));
                        IncProgerss();
                        while (_isSuspended)
                            await Task.Delay(1000);
                    }
                });
                tasks[i] = task;
            }
            await Task.WhenAll(tasks);
            OnFinished?.Invoke(this);
        }

        private void IncProgerss()
        {
            OnProgress?.Invoke(this, ++_progress, _total);
        }

        public void Suspend()
        {
            _isSuspended = true;
        }

        public void Resume()
        {
            _isSuspended = false;
        }

        private bool TryEnqueueUrl(out string url)
        {
            lock (_lock)
            {
                url = _imagesUrls.FirstOrDefault();
                if (url != null)
                    _imagesUrls.Remove(url);
                return url != null;
            }
        }

        private static IEnumerable<string> GetImagesUrls(HtmlDocument document)
        {
            return document.Images.OfType<HtmlElement>().Select(image =>
            {
                var src = image.GetAttribute("src").TrimEnd('/');
                if (!Uri.IsWellFormedUriString(src, UriKind.Absolute))
                    src = string.Concat(document.Url.AbsoluteUri, "/", src);
                return src;
            });
        }
    }
}
