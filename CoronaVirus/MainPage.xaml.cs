using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace CoronaVirus
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetTotalCases();
            BindingContext = this;
        }

        // a function to get Corona virus data from the API (total numbers) and then display it
        private async void GetTotalCases()
        {
            // send API request and get response
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://corona.lmao.ninja/all");

            // check for response success here first!!

            TotalData total = JsonConvert.DeserializeObject<TotalData>(response);            
            // set display labels with the data received from API call
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
