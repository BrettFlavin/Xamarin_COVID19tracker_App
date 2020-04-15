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
                var json = await client.GetStringAsync("https://corona.lmao.ninja/countries" + "/" + country);
                CountryData data = JsonConvert.DeserializeObject<CountryData>(json);

                // set display labels with the data received 
                name.Text = ($"Country: {data.country.ToString()}");
                cases.Text = ($"# of Cases: {data.cases.ToString()}");
                deaths.Text = ($"# of Deaths: {data.deaths.ToString()}");
                recovered.Text = ($"# of Recoveries: {data.recovered.ToString()}");
                active.Text = ($"# of Active: {data.active.ToString()}");
                critical.Text = ($"# of Critical: {data.critical.ToString()}");
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
            cases.Text = "";
            deaths.Text = "";
            recovered.Text = "";
            active.Text = "";
            critical.Text = "";
        }

    }
}