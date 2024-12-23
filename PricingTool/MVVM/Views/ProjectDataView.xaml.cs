using CommunityToolkit.Maui.Views;
using PricingTool.MVVM.Models;
using PricingTool.MVVM.ViewModels;
using System.Collections.ObjectModel;

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
    ObservableCollection<string> collectionItems = new ObservableCollection<string>();
    public ProjectDataView()
    {
        InitializeComponent();
        BindingContext = new ProjectDataViewModel();

    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (sender == switchLDC)
        {
            // Je�li zmieni� si� switchLDC
            if (switchLDC.IsToggled == true)
            {
                entryLDC.IsEnabled = true;
                entryLDC.Placeholder = "Wklej �cie�k� do pliku LDC!";
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
                entryLPA.Placeholder = "Wklej �cie�k� do pliku LPA!";
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
                entryLPL.Placeholder = "Wklej �cie�k� do pliku LPL!";
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
                entryLAC.Placeholder = "Wklej �cie�k� do pliku LAC!";
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
                entryTrave.Placeholder = "Wklej �cie�k� do pliku Trave!";
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
                entryLTU.Placeholder = "Wklej �cie�k� do pliku LTU!";
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
                entryLKK.Placeholder = "Wklej �cie�k� do pliku LKK!";
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
        collectionItems.Clear();
        AddLabelResult("Zresetowano.");
        

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        collectionItems.Clear();
        if (lblPercentages.Text.ToLower() == "start")
        {
            bool isValid = await DataValidation();

            if (isValid)
            {
                GenerateFile();
            }
        }
        ScreenSave();
    }
    public async Task<bool> DataValidation()
    {
        string infMsg = "Pole nie mo�e by� puste, wyga� je przyciskiem po prawej stronie!";
        string infMsg2 = "Plik nie istnieje!";
        string infCncl = "Zamknij";



        if (entryMainPath.Text == null || entryMainName.Text == null)
        {
            await DisplayAlert("Brak danych!", "Brak scie�ki do pliku docelowego lub nazwy docelowej.\nWprowad� dane!", infCncl);
            return false;
        }
        if (!Directory.Exists(entryMainPath.Text))
        {
            await DisplayAlert("�cie�ka nie istnieje!", $"Poni�sza �cie�ka nie istnieje. Wprowad� poprawn� �cie�k�.\n{entryMainPath.Text}", infCncl);
            return false;
        }


        if (switchLDC.IsToggled == true && string.IsNullOrEmpty(entryLDC.Text))
        {
            await DisplayAlert("B��d LDC!", infMsg, infCncl);
            return false;
        }
        else
        {
            ldcPath = entryLDC.Text.Replace("\"", "");
            if (switchLDC.IsToggled == true && !CheckIfFileExists(lpaPath))
            {
                await DisplayAlert("B��d LDC!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLPA.IsToggled == true && string.IsNullOrEmpty(entryLPA.Text))
        {
            await DisplayAlert("B��d LPA!", infMsg, infCncl);
            return false;
        }
        else
        {
            lpaPath = entryLPA.Text.Replace("\"", "");
            if (switchLPA.IsToggled == true && !CheckIfFileExists(lpaPath))
            {
                await DisplayAlert("B��d LPA!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLPL.IsToggled == true && string.IsNullOrEmpty(entryLPL.Text))
        {
            await DisplayAlert("B��d LPL!", infMsg, infCncl);
            return false;
        }
        else
        {
            lplPath = entryLPL.Text.Replace("\"", "");
            if (switchLPL.IsToggled == true && !CheckIfFileExists(lplPath))
            {
                await DisplayAlert("B��d LPL!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLAC.IsToggled == true && string.IsNullOrEmpty(entryLAC.Text))
        {
            await DisplayAlert("B��d LAC!", infMsg, infCncl);
            return false;
        }
        else
        {
            lacPath = entryLAC.Text.Replace("\"", "");
            if (switchLAC.IsToggled == true && !CheckIfFileExists(lacPath))
            {
                await DisplayAlert("B��d LAC!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchTrave.IsToggled == true && string.IsNullOrEmpty(entryTrave.Text))
        {
            await DisplayAlert("B��d Trave!", infMsg, infCncl);
            return false;
        }
        else
        {
            travePath = entryTrave.Text.Replace("\"", "");
            if (switchTrave.IsToggled == true && !CheckIfFileExists(travePath))
            {
                await DisplayAlert("B��d Trave!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLTU.IsToggled == true && string.IsNullOrEmpty(entryLTU.Text))
        {
            await DisplayAlert("B��d LTU!", infMsg, infCncl);
            return false;
        }
        else
        {
            ltuPath = entryLTU.Text.Replace("\"", "");
            if (switchLTU.IsToggled == true && !CheckIfFileExists(ltuPath))
            {
                await DisplayAlert("B��d LTU!", infMsg2, infCncl);
                return false;
            }
        }
        if (switchLKK.IsToggled == true && string.IsNullOrEmpty(entryLKK.Text))
        {
            await DisplayAlert("B��d LKK!", infMsg, infCncl);
            return false;
        }
        else
        {
            lkkPath = entryLKK.Text.Replace("\"", "");
            if (switchLKK.IsToggled == true && !CheckIfFileExists(lkkPath))
            {
                await DisplayAlert("B��d LKK!", infMsg2, infCncl);
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
        {
            projectDataViewModel.TransformLDC(ldcPath);
            AddLabelResult("Przygotowano LDC!");
        }
            
        lblPercentages.Text = "15 %";

        await Task.Delay(100);

        if (!string.IsNullOrEmpty(lpaPath))
        {
            projectDataViewModel.TransformLPA(lpaPath);
            AddLabelResult("Przygotowano LPA!");
        }
            
        lblPercentages.Text = "30 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLPL.Text))
        {
            projectDataViewModel.TransformLPL(lplPath);
            AddLabelResult("Przygotowano LPL!");
        }
            
        lblPercentages.Text = "45 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLAC.Text))
        {
            projectDataViewModel.TransformLAC(lacPath);
            AddLabelResult("Przygotowano LAC!");
        }
            
        lblPercentages.Text = "60 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryTrave.Text))
        {
            projectDataViewModel.TransformTrave(travePath);
            AddLabelResult("Przygotowano TRAVE!");
        }
            
        lblPercentages.Text = "75 %";
        await Task.Delay(100);

        if (!string.IsNullOrEmpty(entryLTU.Text))
        {
            projectDataViewModel.TransformLTU(ltuPath);
            AddLabelResult("Przygotowano LTU!");
        }
            
        lblPercentages.Text = "85 %";
        await Task.Delay(100);
        if (!string.IsNullOrEmpty(entryLKK.Text))
        {
            projectDataViewModel.TransformLKK(lkkPath);
            AddLabelResult("Przygotowano LKK!");
        }
            
        lblPercentages.Text = "95 %";
        await Task.Delay(100);

        projectDataViewModel.NewFileExcel((entryMainPath.Text.Replace("\"", "") + "\\" + entryMainName.Text.Replace("\"", "") + ".xlsx"));
        lblPercentages.Text = "Done!";
        AddLabelResult($"Przygotowano plik {entryMainName.Text}!");

    }

    public void AddLabelResult(string stepOfPricing)
    {

        collectionItems.Insert(0,stepOfPricing);
        LoadCollectionView();

    }

    public void LoadCollectionView()
    {
        
        CollectionView collectionView = new CollectionView
        {
            ItemsSource = collectionItems,

            ItemTemplate = new DataTemplate(() =>
            {


                Label label = new Label
                {
                    //Style = (Style)Resources["LblResultPricing"],
                    //VerticalOptions = LayoutOptions.End,
                    //HorizontalOptions = LayoutOptions.End,
                    //HorizontalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(10, 0, 20, 0),
                    FontAttributes = FontAttributes.Italic,
                    TextColor = Color.FromArgb("#18446F"),
                    //BackgroundColor = Color.FromRgb(255, 99, 71)

                };
                label.SetBinding(Label.TextProperty, new Binding("."));

                //vslResultReport.Children.Add(label);
                return label;

            }),
            

        };

        clvResultReport.ItemsSource = collectionView.ItemsSource;
        clvResultReport.ItemTemplate = collectionView.ItemTemplate;
        
        //clvResultReport.ScrollTo(collectionItems.Last(), ScrollToPosition.Start,ScrollToPosition.MakeVisible, true) ;
    }

    public async void ScreenSave()
    {
        var result = await GridScreenSave.CaptureAsync();
        using MemoryStream memoryStream = new MemoryStream();

        await result.CopyToAsync(memoryStream);

        string fullPath = Path.Combine(entryMainPath.Text.Replace("\"", "") + "\\" + entryMainName.Text.Replace("\"", "") + ".png");

        File.WriteAllBytes(fullPath, memoryStream.ToArray());



        await Share.Default.RequestAsync(new ShareFileRequest
        {
            File = new ShareFile(fullPath),


        });
    }

    private void CheckListPopUp(object sender, EventArgs e)
    {
        this.ShowPopup(new PopUpChecklistProject());
    }
}