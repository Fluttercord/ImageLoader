namespace WFImageLoader
{
    delegate void WebsiteImagesDownloaderProgressEventHandler(IWebsiteImagesDownloader sender, int progress, int total);

    delegate void WebsiteImagesDownloaderEventHandler(IWebsiteImagesDownloader sender);

    interface IWebsiteImagesDownloader
    {
        event WebsiteImagesDownloaderProgressEventHandler OnProgress;
        event WebsiteImagesDownloaderEventHandler OnFinished;

        string Address { get; }

        void Start();
        void Suspend();
        void Resume();
    }
}
