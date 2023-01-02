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
		public NewsByTopic ()
		{
			InitializeComponent ();
		}
		public NewsByTopic (ChuDe chude)
		{
			InitializeComponent ();
			Title = "Tin " + chude.Ten;
            InitializeData(chude.Id);
        }

        async void InitializeData(int chudeId)
        {
            HttpClient httpClient = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/bai-bao?ChuDe=" + chudeId.ToString();
            var BaiBaoList_str = await httpClient.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;
            FollowBtn.CommandParameter = chudeId;

            if (NguoiDung.nguoiDung != null)
            {
                string check_url = "http://192.168.56.1/docbaohay/api/tac-gia/kiem-tra-theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&chuDeId=" + chudeId;
                int ketQua = int.Parse(await httpClient.GetStringAsync(check_url));
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
    }
}