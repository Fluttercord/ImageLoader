using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ThreadState = System.Threading.ThreadState;

namespace WFImageLoader
{
    public delegate void WebsiteImagesDownloaderProgressHandler(WebsiteImagesDownloader sender, int current, int total);

    public class WebsiteImagesDownloader : IDisposable
    {
        private readonly string _outputFolder;
        private readonly object _lock = new object();
        private WebBrowser _browser;
        private Queue<string> _imagesUrls;
        private List<string> _inProggress;
        private int _total;
        private List<Thread> _threads;
        private bool _started;
        private bool _isSuspended;

        public event WebsiteImagesDownloaderProgressHandler OnProgress;

        public string Address { get; }

        public WebsiteImagesDownloader(string address, string outputFolder)
        {
            Address = address;
            _outputFolder = outputFolder;
        }

        public void Start()
        {
            if (_started)
                return;
            _started = true;
            _browser = new WebBrowser();
            _browser.DocumentCompleted += _browser_DocumentCompleted;
            _browser.Navigate(Address);
        }

        private void _browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = (WebBrowser)sender;
            _imagesUrls = new Queue<string>(GetImagesUrls(browser.Document));
            _total = _imagesUrls.Count;
            _inProggress = new List<string>();
            _threads = new List<Thread>();
            for (int i = 0; i < 3; i++)
                LaunchThread();
        }

        private void LaunchThread()
        {
            if (_isSuspended)
                return;
            var newThread = new Thread(new ThreadStart(delegate
            {
                var imageUrl = GetImagesUrl();
                if (imageUrl == null)
                    return;
                var client = new WebClient();
                var filename = Path.Combine(_outputFolder, Guid.NewGuid().ToString() + ".bmp");
                Trace.WriteLine(Thread.CurrentThread.ManagedThreadId + " - " + imageUrl);

                Thread.Sleep(5000);

                File.WriteAllBytes(filename, client.DownloadData(imageUrl));
                IncreaseProgress();
                CheckThreads();
                Release(imageUrl);
            }));
            _threads.Add(newThread);
            newThread.Start();
        }

        private void Release(string image)
        {
            lock (_lock)
            {
                _inProggress.Remove(image);
            }
        }

        private void CheckThreads()
        {
            lock (_lock)
            {
                var threadsToRemove = _threads.Where(t => t.ThreadState != ThreadState.Running).ToList();
                threadsToRemove.ForEach(t =>
                {
                    _threads.Remove(t);
                    LaunchThread();
                });
            }
        }

        private string GetImagesUrl()
        {
            lock (_lock)
            {
                var url = _imagesUrls.Count == 0 ? null : _imagesUrls.Dequeue();
                if (url != null)
                    _inProggress.Add(url);
                return url;
            }
        }

        public void IncreaseProgress()
        {
            OnProgress?.Invoke(this, _total - _imagesUrls.Count, _total);
        }

        public void Suspend()
        {
            if (!_started || _imagesUrls == null || _imagesUrls.Count == 0)
                return;
            _isSuspended = true;
            _threads.ForEach(t => t.Abort());
            _threads.Clear();
            _inProggress.ForEach(url => _imagesUrls.Enqueue(url));
            _inProggress.Clear();
        }


        public void Resume()
        {
            if (!_started || _imagesUrls == null || _imagesUrls.Count == 0)
                return;
            _isSuspended = false;
            for (int i = 0; i < 3; i++)
                LaunchThread();
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

        public void Dispose()
        {
            _browser?.Dispose();
        }
    }
}
