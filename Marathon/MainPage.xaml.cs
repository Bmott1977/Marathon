using Marathon.Models;
using Newtonsoft.Json;
namespace Marathon;

public partial class MainPage : ContentPage
{
    private RaceCollection RaceObject;
    public MainPage()
    {
        InitializeComponent();
        FillPicker();
    }

    public void FillPicker()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var Response = client.GetAsync("races/").Result;
        var wsJson = Response.Content.ReadAsStringAsync().Result;

        RaceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson);
        pkrRace.ItemsSource = RaceObject.races;
    }

    private void PkrRace_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var RaceID = RaceObject.races[pkrRace.SelectedIndex].id;
        
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var Response = client.GetAsync("results/" + RaceID).Result;
        var wsJson = Response.Content.ReadAsStringAsync().Result;
        var ResultObject = JsonConvert.DeserializeObject<ResultCollection>(wsJson);

        var CellTemplate = new DataTemplate(typeof(TextCell));
        CellTemplate.SetBinding(TextCell.TextProperty, "name");
        CellTemplate.SetBinding(TextCell.DetailProperty, "detail");

        lstResults.ItemTemplate = CellTemplate;
        lstResults.ItemsSource = ResultObject.results;
    }
}