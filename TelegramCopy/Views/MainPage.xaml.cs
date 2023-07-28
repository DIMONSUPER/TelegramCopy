using TelegramCopy.ViewModels;

namespace TelegramCopy.Views;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
    }

    #region -- Overrides --

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (_viewModel is not null)
        {
            _viewModel.ScrollToLastMessage -= OnScrollToLastRequested;
        }

        if (BindingContext is MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ScrollToLastMessage += OnScrollToLastRequested;
        }
    }

    private void OnScrollToLastRequested(object sender, bool isAnimated)
    {
        var groups = _viewModel?.SelectedChat?.MessageGroups;

        if (groups is not null)
        {
            MainThread.BeginInvokeOnMainThread(() => collectionView.ScrollTo(groups[^1].Count - 1, groups.Count - 1, ScrollToPosition.Start, isAnimated));
        }
    }

    #endregion
}


