using System.Windows;

namespace Common.Mvvm
{
    public abstract class BaseViewModel<TView> : PropertyChangedBase, IBaseViewModel
        where TView : FrameworkElement
    {
        private TView _view;

        public TView View
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged(nameof(View));
            }
        }

        protected abstract TView CreateView();

        public virtual void ActivateViewModel()
        {
            View = CreateView();
        }

        public virtual void DeactivateViewModel()
        {
            View = null;
        }
    }
}
