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
    public partial class CountryDataPage : ContentPage
    {
        public CountryDataPage()
        {
            InitializeComponent();
        }

        // sends a GET request to the API for country corona virus data 
        // returns data on the country entered in the page's entry field 
        private async void Get_Country_Data(object sender, EventArgs e)
        {
            var country = country_entry.Text;

            // send API request and get response
            HttpClient client = new HttpClient();
            try
            {
                var json = await client.GetStringAsync("https://corona.lmao.ninja/v2/countries" + "/" + country);
                CountryData data = JsonConvert.DeserializeObject<CountryData>(json);

                // API returns an 'updated' field with the UNIX TIME of last update received 
                // create a new DateTime object to represent 1/1/1970
                DateTime lastUpdate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

                // add the unix time elapsed since 1/1/1970 and convert to current local time
                lastUpdate = lastUpdate.AddMilliseconds(data.updated).ToLocalTime();

                // set country data display labels with COVID-19 data returned from API call
                name.Text = $"* {data.country.ToString()} Country Totals *";
                updated.Text= $"Updated: {lastUpdate.ToString()}";
                cases.Text = $"# of Cases: {data.cases.ToString()}";
                todaycases.Text = $"# of Cases Today: {data.todayCases.ToString()}";
                deaths.Text = $"# of Deaths: {data.deaths.ToString()}";
                todaydeaths.Text = $"# of Deaths Today: {data.todayDeaths.ToString()}";
                active.Text = $"# of Active: {data.active.ToString()}";
                critical.Text = $"# of Critical: {data.critical.ToString()}";
                recovered.Text = $"# of Recoveries: {data.recovered.ToString()}";
            }

            // NEED TO HANDLE THE EXCEPTION HERE FOR OTHER THAN 200 HTTP STATUS CODES
            catch
            {
                await DisplayAlert("Not Found!", ($"no data found for \"{country}\""), "CANCEL");
                country_entry.Text = "";
            }
        }

        // Called on page being navigated to
        private void On_Page_Appearing(object sender, EventArgs e)
        {

        }

        // Called on page being navigated away from
        private void On_Page_Disappearing(object sender, EventArgs e)
        {
            // clear the entry field
            country_entry.Text = "";

            // clear all display labels 
            name.Text = "";
            updated.Text = "";
            cases.Text = "";
            todaycases.Text = "";
            deaths.Text = "";
            todaydeaths.Text = "";
            active.Text = "";
            recovered.Text = "";            
            critical.Text = "";
        }

    }
}