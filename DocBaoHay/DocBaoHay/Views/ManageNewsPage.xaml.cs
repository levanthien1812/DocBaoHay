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
    public partial class ManageNewsPage : ContentPage
    {
        public ManageNewsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient http1 = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/bai-bao";
            var BaiBaoList_str = await http1.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewsPage());
        }

        private async void ManageRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }
    }
}