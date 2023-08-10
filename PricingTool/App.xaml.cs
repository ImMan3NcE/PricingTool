
using PricingTool.MVVM.Views;

namespace PricingTool;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new ProjectDataView());
    }
}
