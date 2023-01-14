using DocBaoHay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient http1 = new HttpClient();

            string url1 = "http://192.168.56.1/docbaohay/api/bai-bao/tu-khoa";
            var TuKhoaList_str = await http1.GetStringAsync(url1);

            var TuKhoaList = JsonConvert.DeserializeObject<List<TuKhoaTimKiem>>(TuKhoaList_str);
             
            foreach (var item in TuKhoaList )
            {
                Button btn = new Button
                {
                    Text = item.TuKhoa,
                    BackgroundColor = Color.LightGray,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Start,
                };
                KeyWordsSL.Children.Add(btn);
            }

            string url2 = "http://192.168.56.1/docbaohay/api/bai-bao";
            var BaiBaoList_str = await http1.GetStringAsync(url2);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            DeXuatBaiBaoLV.ItemsSource= BaiBaoList;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFrame.BorderColor = Color.FromHex("#2e98c4");
        }

        private void DeXuatBaiBaoLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }

        private async void SearchBtn_Clicked(object sender, EventArgs e)
        {
            DeXuatTuKhoa.IsVisible = false;
            label2.Text = "Các bài báo tìm kiếm được";
            if (SearchEntry.Text != "")
            {
                string tuKhoa = SearchEntry.Text;
                HttpClient http = new HttpClient();
                string url2 = "http://192.168.56.1/docbaohay/api/bai-bao/tim-kiem/?tuKhoa=" + tuKhoa;
                var result2_str = await http.GetStringAsync(url2);
                var result2 = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(result2_str);
                if (result2 != null)
                {
                    DeXuatBaiBaoLV.ItemsSource = result2;
                }
            }
        }
    }
}