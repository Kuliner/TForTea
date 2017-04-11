namespace TForTea.Views
{
    using Callisto.Controls;
    using System;
    using TForTea.Models;
    using TForTea.ViewModels;
    using Toolkit.Prism.Mvvm;

    public sealed partial class MainPage : MvvmPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public MainPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainPageViewModel;
            }
        }

        private void Rating_ValueChanged(object sender, EventArgs e)
        {
            var tea = (Tea)((Rating)sender).DataContext;
            tea.Rate = ((Rating)sender).Value;
            this.ViewModel.RatingTapped(tea);
        }
    }
}