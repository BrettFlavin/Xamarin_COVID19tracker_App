using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}