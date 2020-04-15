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
 
    public partial class TotalDataHomePage : ContentPage
    {
        public TotalDataHomePage()
        {
            InitializeComponent();
            CheckConnection();
            StartTimer();
            BindingContext = this;
        }

        private async void CheckConnection()
        {
            // check for internet connection and alert user if no connection exists
            if (CrossConnectivity.Current.IsConnected)
            {
                GetTotalCases();
            }
            else
            {
                await DisplayAlert("Internet Not Connected!", "Please establish internet connection to continue", "OK");
            }

        }

        // a function to display the current time and number of days since first case was reported
        private void StartTimer()
        {
            // set the current time in the time label
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
               displayed_time.Text = DateTime.Now.ToString("HH.mm:ss"));

                // display the number of days since first case of CoronaVirus was reported (1/22/2020)
                DateTime startDate = new System.DateTime(2020, 1, 22, 12, 0, 0);
                System.TimeSpan days = DateTime.Now.Date - startDate;
                displayed_days.Text = days.TotalDays.ToString();
                return true;
            });
        }

        // a function to get Corona virus data from the API (total numbers) and then display it
        private async void GetTotalCases()
        {
            // create new http client to handle the request
            HttpClient client = new HttpClient();

            // send GET request and store response in TotalData object
            var response = await client.GetStringAsync("https://corona.lmao.ninja/all");
            TotalData total = JsonConvert.DeserializeObject<TotalData>(response);

            // set display labels with the data received 
            caselabel.Text = ($"# of Cases: {total.cases.ToString()}");
            deathslabel.Text = ($"# of Deaths: {total.deaths.ToString()}");
            recoveredlabel.Text = ($"# of Recovered: {total.recovered.ToString()}");
            lastupdatedlabel.Text = ($"Last Updated: {total.updated.ToString()}");
        }


        /* BUTTON CLICK EVENT OR MENU ITEM - WILL GO TO A NEW PAGE */

        // a function to get Corona virus data from the API (state data) and then 
        // display the state data sorted by highest to lowest number of cases
        private async void SortByCountry()
        {
            // send API request and get response
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://corona.lmao.ninja/states");
            CountryDataList data = JsonConvert.DeserializeObject<CountryDataList>(response);

            // set the list view item source to display each country's info
            //listView_Country.ItemSource = data.countries;
        }


        /* BUTTON CLICK EVENT OR MENU ITEM - WILL GO TO A NEW PAGE */

        // a function to get Corona virus data from the API (country data) and then display
        // the country data sorted by highest to lowest number of cases
        private async void SortByState()
        {
            // send API request and get response
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://corona.lmao.ninja/countries");
            StateDataList data = JsonConvert.DeserializeObject<StateDataList>(response);

            // set the list view item source to display each state's info 
            //listView_State.ItemSource = data.states;
        }
    }
}