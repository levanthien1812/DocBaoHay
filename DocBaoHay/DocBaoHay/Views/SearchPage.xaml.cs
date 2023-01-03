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
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchEntry != null)
            {
                string tuKhoa = SearchEntry.Text;
                HttpClient http = new HttpClient();
                string url1 = "http://192.168.56.1/docbaohay/api/tac-gia/tim-kiem/?tuKhoa=" + tuKhoa;
                var result1_str = await http.GetStringAsync(url1);
                var result1 = JsonConvert.DeserializeObject<List<TacGia>>(result1_str);
                if (result1 != null)
                {
                    DeXuatTacGiaCV.ItemsSource = result1; 
                }
                string url2 = "http://192.168.56.1/docbaohay/api/bai-bao/tim-kiem/?tuKhoa=" + tuKhoa;
                var result2_str = await http.GetStringAsync(url2);
                var result2 = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(result2_str);
                if (result2 != null)
                {
                    DeXuatBaiBaoLV.ItemsSource = result2;
                }
            }
        }

        private void DeXuatTacGiaCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeXuatBaiBaoLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }
    }
}