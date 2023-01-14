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
	public partial class NewsByTopic : ContentPage
	{
        ChuDe _chuDe;
		public NewsByTopic ()
		{
			InitializeComponent ();
		}
		public NewsByTopic (ChuDe chude)
		{
			InitializeComponent ();
            _chuDe= chude;
			Title = "Tin " + chude.Ten;
            InitializeData(chude.Id);
        }

        async void InitializeData(int chudeId)
        {
            HttpClient http1 = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/bai-bao?ChuDe=" + chudeId;
            var BaiBaoList_str = await http1.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;
            FollowBtn.CommandParameter = chudeId;

            if (NguoiDung.nguoiDung != null)
            {
                HttpClient http2 = new HttpClient();
                string check_url = "http://192.168.56.1/docbaohay/api/chu-de/kiem-tra-theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&chuDeId=" + chudeId;
                int ketQua = int.Parse(await http2.GetStringAsync(check_url));
                if (ketQua == 1)
                {
                    FollowBtn.Text = "Đã theo dõi";
                }
            }
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }

        private async void FollowBtn_Clicked(object sender, EventArgs e)
        {
            int chuDeId = int.Parse(FollowBtn.CommandParameter.ToString());

            if (NguoiDung.nguoiDung == null)
            {
                bool choose = await DisplayAlert("Thông báo", "Cần đăng nhập để thực hiện", "OK", "Hủy");
                if (choose == true)
                {
                    await Navigation.PushAsync(new LoginPage());
                    return;
                }
                return;
            }

            HttpClient http = new HttpClient();
            string url = "http://192.168.56.1/docbaohay/api/chu-de/theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&chuDeId=" + chuDeId;
            HttpResponseMessage ketQuaRes = await http.PostAsync(url, null);

            int ketQua = int.Parse(await ketQuaRes.Content.ReadAsStringAsync());
            if (ketQua == 1)
            {
                FollowBtn.Text = "Đã theo dõi";
                FollowBtn.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }

        protected override void OnAppearing()
        {
            InitializeData(_chuDe.Id);
        }
    }
}