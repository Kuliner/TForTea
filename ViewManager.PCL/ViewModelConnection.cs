using System;
using Windows.UI.Xaml.Controls;

namespace ViewManagement
{
    public class ViewModelConnection
    {
        private ContentControl _viewInstance = null;
        private Type _viewModelType;
        private Type _viewType;

        public ContentControl ViewInstance
        {
            get { return _viewInstance; }
            set { _viewInstance = value; }
        }

        public Type ViewModelType
        {
            get { return _viewModelType; }
        }

        public Type ViewType
        {
            get { return _viewType; }
        }

        public void Set<ViewModel, View>()
        {
            _viewType = typeof(View);
            _viewModelType = typeof(ViewModel);
        }

        public void Set<ViewModel, View>(ContentControl viewInstance)
        {
            _viewType = typeof(View);
            _viewModelType = typeof(ViewModel);
            _viewInstance = viewInstance;
        }
    }
}