using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace CoronaVirus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainTabbedPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        // event fires when internet connectivity changes
        // displays an alert and enables or disables buttons
        async private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            // alert internet connected and enable buttons
            if (e.IsConnected)
            {
                await MainPage.DisplayAlert("Internet Connected", "Enjoy the app!", "OK");
            }
            // alert internet disconnected and disable buttons
            else
            {
                await MainPage.DisplayAlert("*** Warning ***", "Internet Disconnected! - Please connect to Wi-Fi to continue!", "OK");               
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
