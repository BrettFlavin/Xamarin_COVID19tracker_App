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

        // sends a GET request to the API for state corona virus data 
        // returns data on the state entered in the page's entry field 
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
                name.Text = ($"* {data.state.ToString()} State Totals *");
                cases.Text = ($"# of Cases: {data.cases.ToString()}");
                todaycases.Text = ($"# of Cases Today: {data.todayCases.ToString()}");
                deaths.Text = ($"# of Deaths: {data.deaths.ToString()}");
                todaydeaths.Text = ($"# of Deaths Today: {data.todayDeaths.ToString()}");
                active.Text = ($"# of Active: {data.active.ToString()}");
            }
            catch
            {
                await DisplayAlert("Not Found!", ($"no data found for \"{state}\" "), "CANCEL");
                state_entry.Text = "";
            }
        }

        // Executes when page appears 
        private void On_Page_Appearing(object sender, EventArgs e)
        {
            
        }

        // Executes when page disappears 
        private void On_Page_Disappearing(object sender, EventArgs e)
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
        }
    }
}