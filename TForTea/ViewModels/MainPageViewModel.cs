namespace TForTea.ViewModels
{
    using Models;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using TForTea.Managers;
    using TForTea.Services;
    using Windows.Networking.BackgroundTransfer;

    public class MainPageViewModel : ViewModelBase
    {
        private DatabaseManager dbManager;
        private LogManager logManager;
        private bool ready;
        private ObservableCollection<Tea> teaList = new ObservableCollection<Tea>();
        private TeaProxyService teaProxyService;

        public MainPageViewModel(TeaProxyService teaProxyService, DatabaseManager dbManager, LogManager logManager)
        {
            this.teaProxyService = teaProxyService;
            this.dbManager = dbManager;
            this.logManager = logManager;
        }

        public ObservableCollection<Tea> TeaList
        {
            get { return this.teaList; }
            set { this.SetProperty(ref this.teaList, value); }
        }

        public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            await this.PopulateDatabase();
        }

        internal void RatingTapped(Tea tea)
        {
            this.dbManager.TeaService.Update(tea);
        }

        private async Task<string> DownloadImage(Tea tea)
        {
            var destFileName = Path.Combine("Images", +tea.Id + tea.Type + tea.Name + "Image.jpg");
            int itt = 1;

            while (File.Exists(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, destFileName)))
            {
                destFileName = Path.Combine("Images", +tea.Id + tea.Type + itt + "Image.jpg");
                itt++;
            }

            BackgroundDownloader downloader = new BackgroundDownloader();
            DownloadOperation download = downloader.CreateDownload(new Uri(tea.Image), await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(destFileName));
            await download.StartAsync();
            return destFileName;
        }

        private async Task PopulateDatabase()
        {
            try
            {
                var teas = await this.teaProxyService.GetTeaList();
                if (teas != null)
                {
                    foreach (var tea in teas)
                    {
                        var teaInDb = this.dbManager.TeaService.Get(tea.Guid);
                        if (teaInDb == null)
                        {
                            try
                            {
                                tea.Rate = 0;
                                string destFileName = await this.DownloadImage(tea);
                                tea.Image = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, destFileName);
                                this.dbManager.TeaService.Add(tea);
                                this.TeaList.Add(tea);
                            }
                            catch (Exception ex)
                            {
                                this.logManager.Logger.Error(ex.StackTrace);
                            }
                        }
                        else
                        {
                            this.TeaList.Add(teaInDb);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
            }
        }
    }
}