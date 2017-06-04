using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WFImageLoader
{
    delegate void FormVmProgressEventHandler(
        FormVm sender, int siteProgress, int siteTotal, int totalProgress, int totalTotal);

    delegate void FormVmStatusEventHandler(FormVm sender, string status);

    class FormVm
    {
        private readonly string _folder;
        private int _done;
        private IWebsiteImagesDownloader _downloader;

        public event FormVmProgressEventHandler OnProgerss;
        public event FormVmStatusEventHandler OnStatusChanged;

        public List<string> Queue { get; }

        public FormVm(string folder)
        {
            _folder = folder;
            Queue = new List<string>();
        }

        public void AddAddress(string address)
        {
            Queue.Add(address);
            LaunchDownloader();
        }

        public void RemoveAddress(string address)
        {
            Queue.Remove(address);
        }

        public void Suspend()
        {
            if (_downloader != null)
            {
                _downloader.Suspend();
                OnStatusChanged?.Invoke(this, _downloader.Address + " suspended");
            }
        }

        public void Resume()
        {
            if (_downloader != null)
            {
                _downloader.Resume();
                OnStatusChanged?.Invoke(this, _downloader.Address + " in progress");
            }
        }

        private void LaunchDownloader()
        {
            if (_downloader != null)
                return;
            string url;
            if (TryDequeueAddress(out url))
            {
                OnStatusChanged?.Invoke(this, url + " in progress");
                _downloader = new DownloaderV2(url, _folder, 4);
                _downloader.OnProgress += _downloader_OnProgress;
                _downloader.OnFinished += _downloader_OnFinished;
                _downloader.Start();
            }
            else
            {
                _downloader = null;
            }
        }

        private void _downloader_OnFinished(IWebsiteImagesDownloader sender)
        {
            _downloader.OnProgress -= _downloader_OnProgress;
            _downloader = null;
            _done++;
            LaunchDownloader();
        }

        private void _downloader_OnProgress(IWebsiteImagesDownloader sender, int progress, int total)
        {
            OnProgerss?.Invoke(this, progress, total, _done, _done + Queue.Count);
            Trace.WriteLine($"{progress}/{total}");
        }

        private bool TryDequeueAddress(out string url)
        {
            url = Queue.FirstOrDefault();
            if (url != null)
                Queue.Remove(url);
            return url != null;
        }
    }
}
