using Callisto.Controls;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using TForTea.Managers;
using TForTea.WinRT.Models;
using TForTea.WinRT.Services;
using ViewManagement;
using Windows.Storage;
using Windows.Web.Http;

namespace TForTea.WinRT.ViewModel
{
    public class MainViewModel : ViewModelBaseWrapper
    {
        private DatabaseManager databaseManager;
        private RelayCommand goBackCommand;
        private LogManager logManager;
        private Tea selectedTea;
        private bool teaDetailsVisible;
        private ObservableCollection<Tea> teaList = new ObservableCollection<Tea>();
        private TeaProxyService teaProxyService;

        private RelayCommand<ValueChangedEventArgs<double>> valueChangedCommand;

        public MainViewModel(TeaProxyService teaProxyService, DatabaseManager databaseManager, LogManager logManager)
        {
            this.teaProxyService = teaProxyService;
            this.databaseManager = databaseManager;
            this.logManager = logManager;
        }

        public RelayCommand GoBackCommand
        {
            get
            {
                return goBackCommand
                    ?? (goBackCommand = new RelayCommand(
                    () =>
                    {
                        SelectedTea = null;
                        TeaDetailsVisible = false;
                    }));
            }
        }

        public Tea SelectedTea
        {
            get { return selectedTea; }

            set
            {
                selectedTea = value;
                RaisePropertyChanged();

                if (value != null)
                {
                    TeaDetailsVisible = true;
                }
            }
        }

        public bool TeaDetailsVisible
        {
            get { return teaDetailsVisible; }

            set
            {
                teaDetailsVisible = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Tea> TeaList
        {
            get { return this.teaList; }

            set
            {
                teaList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<ValueChangedEventArgs<double>> ValueChangedCommand
        {
            get
            {
                return valueChangedCommand
                    ?? (valueChangedCommand = new RelayCommand<ValueChangedEventArgs<double>>(
                    (val) =>
                    {
                        if (selectedTea == null)
                            return;

                        selectedTea.Rate = val.NewValue;
                        databaseManager.TeaService.Update(SelectedTea);
                    }));
            }
        }

        public async override void NavigatedTo()
        {
            base.NavigatedTo();
            await PopulateDatabase();
        }

        private async Task<string> DownloadImage(Tea tea)
        {
            var destFileName = Path.Combine("Images", +tea.Id + tea.Type + tea.Name + "Image.jpg");
            StorageFile destinationFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(
                  destFileName, CreationCollisionOption.GenerateUniqueName);

            HttpClient client = new HttpClient();
            var buffer = await client.GetBufferAsync(new Uri(tea.Image));
            await Windows.Storage.FileIO.WriteBufferAsync(destinationFile, buffer);
            return Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, destFileName);
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
                        var teaInDb = this.databaseManager.TeaService.Get(tea.Guid);
                        if (teaInDb == null)
                        {
                            try
                            {
                                tea.Rate = 0;
                                string destFileName = await this.DownloadImage(tea);
                                tea.Image = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, destFileName);
                                this.databaseManager.TeaService.Add(tea);
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