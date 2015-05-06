using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using WishProject.Models;
using System.Collections.ObjectModel;

namespace StockPricePhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // URI for RESTful service (implemented using Web API)
        private const String serviceURI = "http://wishproject.azurewebsites.net/";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click_DisplayPrices(object sender, RoutedEventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serviceURI);                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../api/stock
                    HttpResponseMessage response = await client.GetAsync("Christmas");

                    // continue
                    if (response.IsSuccessStatusCode)                                                  
                    {
 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<ChristmasUser>>();

                        priceList.ItemsSource = new ObservableCollection<ChristmasUser>(listings);
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (Exception)
            {
                //;
            }
        }
    }
}