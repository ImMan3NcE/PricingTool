namespace PricingTool.MVVM.Views;

public partial class PopUpChecklistProject 
{
	public PopUpChecklistProject()
	{
		InitializeComponent();
		SettingsOfPopup();
	}

	public void SettingsOfPopup()
	{
        VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Start;
        //HorizontalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Start;
		Size = new Size(700, 690);
		Color = Colors.Transparent;
		CanBeDismissedByTappingOutsideOfPopup = false;
		

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}