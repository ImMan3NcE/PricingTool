using PricingTool.MVVM.ViewModels;


namespace PricingTool.MVVM.Views;

public partial class ProjectDataView : ContentPage
{
    ProjectDataViewModel projectDataViewModel = new ProjectDataViewModel();

    string ldcPath = "";
    string lpaPath = "";
    string lplPath = "";
    string lacPath = "";
    string travePath = "";
    string ltuPath = "";
    string lkkPath = "";

    public ProjectDataView()
    {
        InitializeComponent();
        BindingContext = new ProjectDataViewModel();

    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (sender == switchLDC)
        {
            // Jeœli zmieni³ siê switchLDC
            if (switchLDC.IsToggled == true)
            {
                entryLDC.IsEnabled = true;
                entryLDC.Placeholder = "Wklej œcie¿kê do pliku LDC!";
            }
            else
            {
                entryLDC.IsEnabled = false;
                entryLDC.Text = string.Empty;
                entryLDC.Placeholder = "Zablokowano LDC!";
            }
        }
        else if (sender == switchLPA)
        {
            if (switchLPA.IsToggled == true)
            {
                entryLPA.IsEnabled = true;
                entryLPA.Placeholder = "Wklej œcie¿kê do pliku LPA!";
            }
            else
            {
                entryLPA.IsEnabled = false;
                entryLPA.Text = string.Empty;
                entryLPA.Placeholder = "Zablokowano LPA!";
            }
        }
        else if (sender == switchLPL)
        {
            if (switchLPL.IsToggled == true)
            {
                entryLPL.IsEnabled = true;
                entryLPL.Placeholder = "Wklej œcie¿kê do pliku LPL!";
            }
            else
            {
                entryLPL.IsEnabled = false;
                entryLPL.Text = string.Empty;
                entryLPL.Placeholder = "Zablokowano LPL!";
            }
        }
        else if (sender == switchLAC)
        {
            if (switchLAC.IsToggled == true)
            {
                entryLAC.IsEnabled = true;
                entryLAC.Placeholder = "Wklej œcie¿kê do pliku LAC!";
            }
            else
            {
                entryLAC.IsEnabled = false;
                entryLAC.Text = string.Empty;
                entryLAC.Placeholder = "Zablokowano LAC!";

            }
        }
        else if (sender == switchTrave)
        {
            if (switchTrave.IsToggled == true)
            {
                entryTrave.IsEnabled = true;
                entryTrave.Placeholder = "Wklej œcie¿kê do pliku Trave!";
            }
            else
            {
                entryTrave.IsEnabled = false;
                entryTrave.Text = string.Empty;
                entryTrave.Placeholder = "Zablokowano Trave!";
            }
        }
        else if (sender == switchLTU)
        {
            if (switchLTU.IsToggled == true)
            {
                entryLTU.IsEnabled = true;
                entryLTU.Placeholder = "Wklej œcie¿kê do pliku LTU!";
            }
            else
            {
                entryLTU.IsEnabled = false;
                entryLTU.Text = string.Empty;
                entryLTU.Placeholder = "Zablokowano LTU!";
            }
        }
        else if (sender == switchLKK)
        {
            if (switchLKK.IsToggled == true)
            {
                entryLKK.IsEnabled = true;
                entryLKK.Placeholder = "Wklej œcie¿kê do pliku LKK!";
            }
            else
            {
                entryLKK.IsEnabled = false;
                entryLKK.Text = string.Empty;
                entryLKK.Placeholder = "Zablokowano LKK!";
            }
        }
    }





    private void Button_Clicked(object sender, EventArgs e)
    {
        projectDataViewModel.KillExcel();
        lblPercentages.Text = "START";

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (lblPercentages.Text.ToLower() == "start")
        {
            bool isValid = await DataValidation();

            if (isValid)
            {
                GenerateFile();
            }
        }

    }
    public async Task<bool> DataValidation()
    {
        string infMsg = "Pole nie mo¿e byæ puste, wygaœ je przyciskiem po prawej stronie!";
        string infMsg2 = "Plik nie istnieje!";
        string infCncl = "Zamknij";



        if (entryMainPath.Text == null || entryMainName.Text == null)
        {
            await DisplayAlert("Brak danych!", "Brak scie¿ki do pliku docelowego lub nazwy docelowej.\nWprowadŸ dane!", infCncl);
            return false;
        }
        if (!Directory.Exists(entryMainPath.Text))
        {
            await DisplayAlert("Œcie¿ka nie istnieje!", $"Poni¿sza œcie¿ka nie istnieje. WprowadŸ poprawn¹ œcie¿kê.\n{entryMainPath.Text}", infCncl);
            return false;
        }


        if (switchLDC.IsToggled == true && string.IsNullOrEmpty(entryLDC.Text))
        {
            await DisplayAlert("B³¹d LDC!", infMsg, infCncl);
            return false;
        }
        else
        {
            ldcPath = entryLDC.Text.Replace("\"", "");
            if (switchLDC.IsToggled == true && !CheckIfFileExists(lpaPath))
            {
                await DisplayAlert("B³¹d LDC!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLPA.IsToggled == true && string.IsNullOrEmpty(entryLPA.Text))
        {
            await DisplayAlert("B³¹d LPA!", infMsg, infCncl);
            return false;
        }
        else
        {
            lpaPath = entryLPA.Text.Replace("\"", "");
            if (switchLPA.IsToggled == true && !CheckIfFileExists(lpaPath))
            {
                await DisplayAlert("B³¹d LPA!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLPL.IsToggled == true && string.IsNullOrEmpty(entryLPL.Text))
        {
            await DisplayAlert("B³¹d LPL!", infMsg, infCncl);
            return false;
        }
        else
        {
            lplPath = entryLPL.Text.Replace("\"", "");
            if (switchLPL.IsToggled == true && !CheckIfFileExists(lplPath))
            {
                await DisplayAlert("B³¹d LPL!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLAC.IsToggled == true && string.IsNullOrEmpty(entryLAC.Text))
        {
            await DisplayAlert("B³¹d LAC!", infMsg, infCncl);
            return false;
        }
        else
        {
            lacPath = entryLAC.Text.Replace("\"", "");
            if (switchLAC.IsToggled == true && !CheckIfFileExists(lacPath))
            {
                await DisplayAlert("B³¹d LAC!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchTrave.IsToggled == true && string.IsNullOrEmpty(entryTrave.Text))
        {
            await DisplayAlert("B³¹d Trave!", infMsg, infCncl);
            return false;
        }
        else
        {
            travePath = entryTrave.Text.Replace("\"", "");
            if (switchTrave.IsToggled == true && !CheckIfFileExists(travePath))
            {
                await DisplayAlert("B³¹d Trave!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLTU.IsToggled == true && string.IsNullOrEmpty(entryLTU.Text))
        {
            await DisplayAlert("B³¹d LTU!", infMsg, infCncl);
            return false;
        }
        else
        {
            ltuPath = entryLTU.Text.Replace("\"", "");
            if (switchLTU.IsToggled == true && !CheckIfFileExists(ltuPath))
            {
                await DisplayAlert("B³¹d LTU!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLKK.IsToggled == true && string.IsNullOrEmpty(entryLKK.Text))
        {
            await DisplayAlert("B³¹d LKK!", infMsg, infCncl);
            return false;
        }
        else
        {
            lkkPath = entryLKK.Text.Replace("\"", "");
            if (switchLKK.IsToggled == true && !CheckIfFileExists(lkkPath))
            {
                await DisplayAlert("B³¹d LKK!", infMsg2, infCncl);
                return false;
            }

        }
        return true;
    }

    public bool CheckIfFileExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async void GenerateFile()
    {
        if (!string.IsNullOrEmpty(ldcPath))
            projectDataViewModel.TransformLDC(ldcPath);
        lblPercentages.Text = "15 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(lpaPath))
            projectDataViewModel.TransformLPA(lpaPath);
        lblPercentages.Text = "30 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLPL.Text))
            projectDataViewModel.TransformLPL(lplPath);
        lblPercentages.Text = "45 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLAC.Text))
            projectDataViewModel.TransformLAC(lacPath);
        lblPercentages.Text = "60 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryTrave.Text))
            projectDataViewModel.TransformTrave(travePath);
        lblPercentages.Text = "75 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLTU.Text))
            projectDataViewModel.TransformLTU(ltuPath);
        lblPercentages.Text = "85 %";
        await Task.Delay(100);
        if (!string.IsNullOrEmpty(entryLKK.Text))
            projectDataViewModel.TransformLKK(lkkPath);
        lblPercentages.Text = "95 %";
        await Task.Delay(100);

        projectDataViewModel.NewFileExcel((entryMainPath.Text.Replace("\"", "") + "\\" + entryMainName.Text.Replace("\"", "")+".xlsx"));
        lblPercentages.Text = "Done!";
    }
}