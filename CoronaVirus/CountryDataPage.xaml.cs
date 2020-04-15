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

        private void Country_entry_Completed(object sender, EventArgs e)
        {
            var searchCountry = country_entry.Text;
            DisplayAlert(($"{searchCountry}"), "is the country you entered", "OK");
        }

        // GET corona virus data of all countries from the API 
        // displays data sorted by highest to lowest number of cases
        private async void SortByCountry()
        {
            // send API request and get response
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync("https://corona.lmao.ninja/countries");
            CountryDataList data = JsonConvert.DeserializeObject<CountryDataList>(json);

            // set the list view item source to display each country's info
            //listView_Country.ItemSource = data.countries;
        }
    }
}