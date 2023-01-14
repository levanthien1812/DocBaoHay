using DocBaoHay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotNewsPage : ContentPage
    {
        public HotNewsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient http1 = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/bai-bao/hot";
            var BaiBaoList_str = await http1.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;
        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage());
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }

        private async void HotNewsRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }

        protected override void OnAppearing()
        {
            InitializeData();
        }
    }
}