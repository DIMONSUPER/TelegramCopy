namespace TelegramCopy;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

    #region -- Overrides --

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.MinimumWidth = Constants.MINIMUM_WINDOW_WIDTH;
        window.MinimumHeight = Constants.MINIMUM_WINDOW_HEIGHT;

        return window;
    }

    #endregion
}

