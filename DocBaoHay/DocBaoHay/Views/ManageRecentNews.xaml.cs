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
    public partial class ManageRecentNews : ContentPage
    {
        public ManageRecentNews()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient httpClient = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/bao-da-doc";
            var BaiBaoList_str = await httpClient.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;

            if (BaiBaoList.Count == 0)
            {
                ThongBao.IsVisible = true;
                ThongBao.Text = "Bạn không xem tin nào gần đây.";
                DeleteAllBtn.IsVisible= false;
            } else
            {
                ThongBao.IsVisible = false;
                ThongBao.Text = string.Empty;
                DeleteAllBtn.IsVisible = true;
            }
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }

        private async void DeleteAllBtn_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Thông báo", "Bạn có chắc chắn muốn xóa tất cả các bài báo đã xem?", "OK", "Hủy");
            if (choose == false) return;

            HttpClient httpClient = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/bao-da-doc/xoa";
            await httpClient.DeleteAsync(url);
            await DisplayAlert("Thông báo", "Xóa thành công", "OK");
            InitializeData();
        }

        private async void ManageRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }
    }
}