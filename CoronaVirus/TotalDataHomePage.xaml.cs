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
            var json = await client.GetStringAsync("https://corona.lmao.ninja/all");
            TotalData totals = JsonConvert.DeserializeObject<TotalData>(json);

            // create a new DateTime object to represent 1/1/1970
            DateTime timenow = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            
            // the API call returns an 'updated' field with the unix time of last update 
            // add the unix time elapsed since 1/1/1970 to get current local time
            timenow = timenow.AddMilliseconds(totals.updated).ToLocalTime();
            
            // set display labels with the data received 
            caselabel.Text = ($"# of Cases: {totals.cases.ToString()}");
            deathslabel.Text = ($"# of Deaths: {totals.deaths.ToString()}");
            recoveredlabel.Text = ($"# of Recovered: {totals.recovered.ToString()}");
            lastupdatedlabel.Text = ($"(Last Updated: {timenow})");
        }     
    }
}