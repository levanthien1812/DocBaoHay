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
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

        private async void SignUpBtn_Clicked(object sender, EventArgs e)
        {
			string hoTen = HoTenEntry.Text;
			string tenDangNhap = TenDangNhapEntry.Text;
			string email = EmailEntry.Text;
			string matKhau = MatKhauEntry.Text;
			string matKhauXN = MatKhauXacNhanEntry.Text;
			if (matKhau != matKhauXN)
			{
				await DisplayAlert("Thông báo", "Mật khẩu xác nhận không đúng! Vui lòng nhập lại.", "OK");
				MatKhauXacNhanEntry.Text = "";
				return;
			}
			NguoiDung nd = new NguoiDung
			{
				HoTen = hoTen,
				TenDangNhap = tenDangNhap,
				Email = email,
				MatKhau= matKhau
			};

			// Used for sending HTTP requests/responses based on URL
			HttpClient http = new HttpClient();
			// Convert something to JSON
			string ndJson = JsonConvert.SerializeObject(nd);
			// Provide HTTP content
			StringContent httpContent = new StringContent(ndJson, Encoding.UTF8, "application/json");
			// HTTP response includes DATA and MESSAGE
			HttpResponseMessage ketQua = await http.PostAsync("http://192.168.56.1/docbaohay/api/nguoi-dung/dang-ky", httpContent);
			// Convert HTTP response's content to string
			var ketQuaTV = await ketQua.Content.ReadAsStringAsync();
			//Convert JSON to something
			nd = JsonConvert.DeserializeObject<NguoiDung>(ketQuaTV);

			if (nd != null)
			{
				await DisplayAlert("Thông báo", "Đăng ký thành công, vui lòng đăng nhập để tiếp tục.", "OK");
				await Navigation.PushAsync(new LoginPage());
			} else
			{
				await DisplayAlert("Thông báo", "Tên đăng nhập hoặc email đã tồn tại! Vui lòng thử lại", "OK");
			}
        }
    }
}