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
		BaiBao _baiBao;
		public DetailNewsPage ()
		{
			InitializeComponent ();
		}

		public DetailNewsPage (BaiBao_ChuDe baiBao)
		{
			InitializeComponent ();
			InitializeData(baiBao);
			CreateRead(baiBao);
			Title = baiBao.TieuDe;
		}

		private async void InitializeData(BaiBao_ChuDe baiBao)
		{
			HttpClient http = new HttpClient();
			var baiBao_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/bai-bao/" + baiBao.Id);
			var baiBao_obj = JsonConvert.DeserializeObject<List<BaiBao>>(baiBao_str)[0];
			FollowBtn.CommandParameter = baiBao.TacGiaId;
			SaveBtn.CommandParameter = baiBao.Id;
			AuthorImg.Source = baiBao.TacGiaHinh;
			BaiBaoTitle.Text = baiBao.TieuDe;
			BaiBaoTime.Text = baiBao.KhoangTG;
			BaiBaoDescription.Text = baiBao_obj.MoTa;

			var doanVan_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/bai-bao/" + baiBao.Id + "/doan-van");
            var doanVan = JsonConvert.DeserializeObject<List<DoanVan>>(doanVan_str);

			for (int i = 0; i < doanVan.Count; i++)
			{
				if (doanVan[i].Loai == 1)
				{
					Label lb = new Label();
					lb.FontFamily = "RobotoSlab";
                    lb.FontSize = 18;
					lb.Margin = new Thickness(0, 5, 0, 0);
					lb.Text = doanVan[i].NoiDung;
					MainSL.Children.Add(lb);
				} 
				if (doanVan[i].Loai == 2)
				{
					Image img = new Image();
                    img.Margin = new Thickness(0, 5, 0, 0);
					img.WidthRequest = 400;
                    img.Source = doanVan[i].NoiDung.ToString();
					MainSL.Children.Add(img);
				}
                if (doanVan[i].Loai == 3)
				{
                    Label lb = new Label();
                    lb.FontSize = 19;
                    lb.FontFamily = "RobotoSlab";
                    lb.Margin = new Thickness(0, 5, 0, 0);
					lb.FontAttributes= FontAttributes.Bold;
                    lb.Text = doanVan[i].NoiDung;
                    MainSL.Children.Add(lb);
                }

            }

            if (NguoiDung.nguoiDung != null)
			{
				// Kiểm tra theo dõi
				string url1 = "http://192.168.56.1/docbaohay/api/tac-gia/kiem-tra-theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&tacGiaId=" + baiBao.TacGiaId;
				int ketQua1 = int.Parse(await http.GetStringAsync(url1));
				if (ketQua1 == 1)
				{
					FollowBtn.Text = "Đã theo dõi";
					FollowBtn.TextColor = Color.Green;
					FollowBtn.IsEnabled= false;
				}

				// Kiểm tra lưu
                string url2 = "http://192.168.56.1/docbaohay/api/bai-bao/kiem-tra-luu?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&baiBaoId=" + baiBao.Id;
                int ketQua2 = int.Parse(await http.GetStringAsync(url2));
                if (ketQua2 == 1)
                {
                    SaveBtn.Text = "Đã lưu";
                    SaveBtn.TextColor = Color.Green;
					SaveBtn.IsEnabled= false;
                }
            }
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
				return;
			}

			HttpClient http = new HttpClient();
			string url = "http://192.168.56.1/docbaohay/api/tac-gia/theo-doi?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&tacGiaId=" + tacGiaId;
            HttpResponseMessage ketQuaRes = await http.PostAsync(url, null);
			
			int ketQua = int.Parse(await ketQuaRes.Content.ReadAsStringAsync());
			if (ketQua == 1)
			{
                FollowBtn.Text = "Đã theo dõi";
				FollowBtn.TextColor = Color.Green;
				FollowBtn.IsEnabled = false;
            } else
			{
				await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
			}
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            int baiBaoId = int.Parse(SaveBtn.CommandParameter.ToString());

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
            string url = "http://192.168.56.1/docbaohay/api/bai-bao/luu?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&baiBaoId=" + baiBaoId;
            HttpResponseMessage ketQuaRes = await http.PostAsync(url, null);

            int ketQua = int.Parse(await ketQuaRes.Content.ReadAsStringAsync());
            if (ketQua == 1)
            {
                SaveBtn.Text = "Đã lưu";
                SaveBtn.TextColor = Color.Green;
                SaveBtn.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }

		private async void CreateRead(BaiBao_ChuDe baiBao)
		{
			if (NguoiDung.nguoiDung == null) return;
			HttpClient http = new HttpClient();
			string url = "http://192.168.56.1/docbaohay/api/bai-bao/doc?nguoiDungId=" + NguoiDung.nguoiDung.Id + "&&baiBaoId=" + baiBao.Id;
			await http.PostAsync(url, null);
        }
    }
}