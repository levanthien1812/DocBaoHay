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
	public partial class DetailNewsPage : ContentPage
	{
		public DetailNewsPage ()
		{
			InitializeComponent ();
		}

		public DetailNewsPage (BaiBao_ChuDe baiBao)
		{
			InitializeComponent ();
			InitializeData(baiBao);
		}

		private void InitializeData(BaiBao_ChuDe baiBao)
		{
			HttpClient http = new HttpClient();
			//var baiBao_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/bai-bao/" + baiBao.Id);
			//var baiBao_obj = JsonConvert.DeserializeObject<BaiBao>(baiBao_str);
			FollowBtn.CommandParameter = baiBao.TacGiaId;
			AuthorImg.Source = baiBao.TacGiaHinh;
			BaiBaoTitle.Text = baiBao.TieuDe;
			BaiBaoTime.Text = baiBao.KhoangTG;
		}

        private async void FollowBtn_Clicked(object sender, EventArgs e)
        {
			int tacGiaId = int.Parse(FollowBtn.CommandParameter.ToString());

			if (NguoiDung.nguoiDung == null)
			{
				bool choose = await DisplayAlert("Thông báo", "Cần đăng nhập để thực hiện", "OK", "Hủy");
				if (choose == true)
				{
					await Navigation.PushAsync(new LoginPage());
					return;
				} 
			}

			HttpClient http = new HttpClient();
			string url = "http://192.168.56.1/docbaohay/api/tac-gia/theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&tacGiaId=" + tacGiaId;
            HttpResponseMessage ketQuaRes = await http.PostAsync(url, null);
			
			int ketQua = int.Parse(await ketQuaRes.Content.ReadAsStringAsync());
			if (ketQua == 1)
			{
                FollowBtn.Text = "Đã theo dõi";
            } else
			{
				await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
				
			}
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
			SaveBtn.Text = "Đã lưu";
        }
    }
}