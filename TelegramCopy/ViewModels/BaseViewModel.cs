using CommunityToolkit.Mvvm.ComponentModel;

namespace TelegramCopy.ViewModels
{
    public class BaseViewModel : ObservableObject, IInitialize, IPageLifecycleAware
    {
		public BaseViewModel()
		{
		}

        #region -- IInitialize implementation --

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IPageLifecycleAware implementation --

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion
    }
}

