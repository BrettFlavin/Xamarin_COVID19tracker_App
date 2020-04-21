using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Microcharts;
using SkiaSharp;

namespace CoronaVirus
{
     
    public partial class TotalDataHomePage : ContentPage
    {

        public TotalDataHomePage()
        {
            InitializeComponent();
        }

        // function executes on startup to display the current time and number of days since first case was reported
        private void StartClock()
        {
            // set the current time in the time label
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // get current time when main thread starts
                Device.BeginInvokeOnMainThread(() =>
                timelabel.Text = DateTime.Now.ToString("HH.mm:ss"));

                // display number of days since first case of Covid-19 was reported (using 1/22/2020)
                DateTime startDate = new System.DateTime(2020, 1, 22, 12, 0, 0);
                System.TimeSpan days = DateTime.Now.Date - startDate;

                // display the days since 1st COVID-19 case was reported
                dayslabel.Text = days.TotalDays.ToString();

                return true;
            });
        }

        // function executes on startup to verify internet connection is active
        private async void CheckConnection()
        {
            // check for internet connection and alert user if no connection exists
            if (CrossConnectivity.Current.IsConnected)
            {
                GetGraphData();
                GetTotalCases();                
            }
            else
            {
                await DisplayAlert("Internet Not Connected!", "Please establish internet connection to continue", "OK");
            }
        }

        // function executes once internet connection is verified to call API, get totals, and display them
        // API: NOVEL COVID-19 / BASE URL: https://corona.lmao.ninja/
        private async void GetTotalCases()
        {
            // create new http client to handle the request
            HttpClient client = new HttpClient();

            // send GET request to return totals for all countries and store response in TotalData object
            var totals_json = await client.GetStringAsync("https://corona.lmao.ninja/v2/all");

            // deserialize the json response to a TotalData object 
            TotalData totals = JsonConvert.DeserializeObject<TotalData>(totals_json);

            // API returns an 'updated' field with the UNIX TIME of last update received 
            // create a new DateTime object to represent 1/1/1970
            DateTime lastUpdate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            // add the unix time elapsed since 1/1/1970 and convert to current local time
            lastUpdate = lastUpdate.AddMilliseconds(totals.updated).ToLocalTime();

            // set the time label to display current local time
            updatelabel.Text = $"Last Update: {lastUpdate.ToString()}";

            // set total data display labels with COVID-19 data returned from API call
            caseslabel.Text = $"Total # of Cases: {totals.cases.ToString()}";
            todaycaseslabel.Text = $"Total # of Cases Today: {totals.todayCases.ToString()}";
            deathslabel.Text = $"Total # of Deaths: {totals.deaths.ToString()}";
            todaydeathslabel.Text = $"Total # of Deaths Today: {totals.todayDeaths.ToString()}";
            recoveredlabel.Text = $"Total # of Recovered: {totals.recovered.ToString()}";
            activelabel.Text = $"Total # of Active: {totals.active.ToString()}";
            criticallabel.Text = $"Total # of Critical: {totals.critical.ToString()}";
            countriesaffectedlabel.Text = $"Total # of Countries Affected: {totals.affectedCountries.ToString()}";

            // make the view visible
            totalscroll.IsVisible = true;
        }

        // function executes once internet connection is verified to call API and get data for Chart
        // API: NOVEL COVID-19 / BASE URL: https://corona.lmao.ninja/
        private async void GetGraphData()
        {
            // create new http client to handle the request
            HttpClient client = new HttpClient();

            // send GET request to get graph data for last 7 days and store response in TotalData object
            var json = await client.GetStringAsync("https://corona.lmao.ninja/v2/historical/all?lastdays=7");
            
            // deserialize the json response to a Graph object 
            Graph data = JsonConvert.DeserializeObject<Graph>(json);  

            /* SET DATA FOR CASES CHART HERE */
            // a dictionary containing a key-value pair to represent date and number of cases 
            var cases = data.cases;
            // set XAML Chart property and entries 
            cases_chart.Chart = new LineChart { Entries = CreateChartEntries(cases, "#000000") };

            /* SET DATA FOR DEATHS CHART HERE */
            // a dictionary containing a key-value pair to represent date and number of deaths 
            var deaths = data.deaths;
            // set XAML Chart property and entries 
            deaths_chart.Chart = new LineChart { Entries = CreateChartEntries(deaths, "#FF0000") };

            /* SET DATA FOR RECOVERIES CHART HERE */
            // a dictionary containing a key-value pair to represent date and number of recoveries 
            var recoveries = data.recovered;
            // set XAML Chart property and entries 
            recoveries_chart.Chart = new LineChart { Entries = CreateChartEntries(recoveries, "#04FA18") };         
        }

        // a function that returns a list of microchart entries 
        // params: theCases - a dictionary that is holding the key/value pair
        //         colorHexString - a hex string color to set that data of chart 
        List<Microcharts.Entry> CreateChartEntries(Dictionary<string, float> theCases, string colorHexString)
        {
            // make a list of microchart entries from returned json 
            List<Microcharts.Entry> GraphDataList = new List<Microcharts.Entry>();
            foreach (KeyValuePair<String, float> item in theCases)
            {
                GraphDataList.Add(new Microcharts.Entry(item.Value)
                {
                    Color = SKColor.Parse(colorHexString),
                    Label = item.Key,
                    ValueLabel = item.Value.ToString()
                });
            }
            // set XAML Chart property and entries 
            return GraphDataList;
        }

        // function to clear all displayed API data from page
        private void ClearData()
        {
            // clear time and days labels
            timelabel.Text = "";
            dayslabel.Text = "";

            // clear all other data display labels 
            updatelabel.Text = "";
            caseslabel.Text = "";
            todaycaseslabel.Text = "";
            deathslabel.Text = "";
            todaydeathslabel.Text = "";
            recoveredlabel.Text = "";
            activelabel.Text = "";
            criticallabel.Text = "";
            countriesaffectedlabel.Text = "";

            // hide the scrollview 
            totalscroll.IsVisible = false;
        }

        // Executes when page appears 
        private void On_PageAppearing(object sender, EventArgs e)
        {           
            StartClock();
            CheckConnection();            
        }

        // Executes when page disappears 
        private void On_PageDisappearing(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}