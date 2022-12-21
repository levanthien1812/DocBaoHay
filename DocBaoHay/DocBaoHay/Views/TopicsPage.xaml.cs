using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using DocBaoHay_WebAPI.Models;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopicsPage : ContentPage
    {
        public TopicsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient httpClient = new HttpClient();

            var ChuDeList = await httpClient.GetStringAsync("http://172.17.29.57/docbaohay/api/chu-de");

            var ChuDeListConverted = JsonConvert.DeserializeObject<List<ChuDe>>(ChuDeList);
            ChuDeCV.ItemsSource = ChuDeListConverted;
        }

        private void ChuDeCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChuDe chuDe = e.CurrentSelection.FirstOrDefault() as ChuDe;
            Navigation.PushAsync(new NewsByTopic(chuDe));
        }
    }
}