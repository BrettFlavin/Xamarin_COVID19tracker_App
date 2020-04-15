using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}