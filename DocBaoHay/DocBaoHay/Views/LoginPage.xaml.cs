using DocBaoHay.Models;
using DocBaoHay.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string matKhau = MatKhauEntry.Text;

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(matKhau))
            {
                await DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin đăng nhập", "OK");
                return;
            }

            HttpClient http = new HttpClient();
            var nd_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/nguoi-dung/dang-nhap?email=" + email + "&&matKhau=" + matKhau);
            var nd = JsonConvert.DeserializeObject<NguoiDung>(nd_str);

            if (nd != null) {
                await DisplayAlert("Thông báo", "Đăng nhập thành công!", "OK");
                NguoiDung.nguoiDung = nd;
            } else
            {
                await DisplayAlert("Thông báo", "Email hoặc Mật khẩu không đúng! Vui lòng thử lại", "OK");
                MatKhauEntry.Text = string.Empty;
            }
        }
    }
}