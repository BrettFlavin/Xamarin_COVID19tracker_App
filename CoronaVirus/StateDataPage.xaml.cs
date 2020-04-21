using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace CoronaVirus
{
    public partial class StateDataPage : ContentPage
    {
        public StateDataPage()
        {
            InitializeComponent();
        }

        // function executes when user clicks enter on Entry field - calls the API, gets data, sets the page's display labels
        // API: NOVEL COVID-19 
        // BASE URL: https://corona.lmao.ninja/
        private async void Get_State_Data(object sender, EventArgs e)
        {
            var state = state_entry.Text;

            // send API request and get response
            HttpClient client = new HttpClient();
            try
            {
                var json = await client.GetStringAsync("https://corona.lmao.ninja/v2/states" + "/" + state);
                StateData data = JsonConvert.DeserializeObject<StateData>(json);

                // set state data display labels with COVID-19 data returned from API call
                name.Text = ($"{data.state.ToString()} State Totals");
                cases.Text = ($"# of Cases: {data.cases.ToString()}");
                todaycases.Text = ($"# of Cases Today: {data.todayCases.ToString()}");
                deaths.Text = ($"# of Deaths: {data.deaths.ToString()}");
                todaydeaths.Text = ($"# of Deaths Today: {data.todayDeaths.ToString()}");
                active.Text = ($"# of Active: {data.active.ToString()}");

                // make the view visible
                statescroll.IsVisible = true;
            }
            catch
            {
                await DisplayAlert("Not Found!", ($"no data found for \"{state}\" "), "CANCEL");
                state_entry.Text = "";
            }
        }

        // function to clear all displayed API data from page
        private void ClearData()
        {
            // clear the entry field
            state_entry.Text = "";

            // clear all display labels 
            name.Text = "";
            cases.Text = "";
            todaycases.Text = "";
            deaths.Text = "";
            todaydeaths.Text = "";
            active.Text = "";

            // hide the scrollview
            statescroll.IsVisible = false;
        }

        // event fires on button click to get and display a list of all state's data from API
        private void GetStatesButton_Clicked(object sender, EventArgs e)
        {

        }

        // event fires on button click to clear all API data from page
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            ClearData();
        }

        // Executes when page appears 
        private void On_Page_Appearing(object sender, EventArgs e)
        {

        }

        // Executes when page disappears 
        private void On_Page_Disappearing(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}