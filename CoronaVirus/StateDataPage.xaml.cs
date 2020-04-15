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

        private void State_entry_Completed(object sender, EventArgs e)
        {
            var searchState = state_entry.Text;
            DisplayAlert(($"{searchState}"), "is the state you entered", "OK");
        }

        // GET corona virus data of all states from the API 
        // displays data sorted by highest to lowest number of cases
        private async void SortByState()
        {
            // send API request and get response
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync("https://corona.lmao.ninja/states");
            StateDataList data = JsonConvert.DeserializeObject<StateDataList>(json);

            // set the list view item source to display each state's info 
            //listView_State.ItemSource = data.states;
        }
    }
}