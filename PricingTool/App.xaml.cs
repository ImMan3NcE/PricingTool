
using PricingTool.MVVM.Views;

namespace PricingTool;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new ProjectDataView());
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 1400;
        const int newHeight = 700;


        window.X = 500;    
        window.Y = 200;    

        window.Width = newWidth;
        window.Height = newHeight;

        window.MinimumHeight = newHeight;
        window.MinimumWidth = newWidth;

        return window;

    }
}
