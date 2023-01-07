using DocBaoHay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalInfoPage : ContentPage
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
            InitializeData();
        }

        void InitializeData()
        {
            if (NguoiDung.nguoiDung != null)
            {
                HoTenEntry.Text = NguoiDung.nguoiDung.HoTen.ToString();
                TenDangNhapEntry.Text = NguoiDung.nguoiDung.TenDangNhap.ToString();
                EmailEntry.Text = NguoiDung.nguoiDung.Email.ToString();
                MatKhauEntry.Text = NguoiDung.nguoiDung.MatKhau.ToString();
            }
        }

        private async void UpdateInfoBtn_Clicked(object sender, EventArgs e)
        {
            string hoTen = HoTenEntry.Text;
            string tenDangNhap = TenDangNhapEntry.Text;
            string email = EmailEntry.Text;
            string matKhau = MatKhauEntry.Text;

            NguoiDung nd = new NguoiDung
            {
                Id = NguoiDung.nguoiDung.Id,
                HoTen = hoTen,
                Email = email,
                MatKhau = matKhau,
                TenDangNhap = tenDangNhap
            };

            HttpClient http = new HttpClient();
            string ndJson = JsonConvert.SerializeObject(nd);
            StringContent httpContent = new StringContent(ndJson, Encoding.UTF8, "application/json");
            HttpResponseMessage ketQua = await http.PostAsync("http://192.168.56.1/docbaohay/api/nguoi-dung/cap-nhat", httpContent);
            int ketQuaTV = int.Parse(await ketQua.Content.ReadAsStringAsync());
            lb.Text = ketQuaTV.ToString();
            if (ketQuaTV == 1)
            {
                if (email != NguoiDung.nguoiDung.Email || matKhau != NguoiDung.nguoiDung.MatKhau)
                {
                    await DisplayAlert("Thông báo", "Bạn đã thay đổi email hoặc/và mật khẩu! Vui lòng đăng nhập lại", "OK");
                    NguoiDung.nguoiDung = null;
                    await Navigation.PushAsync(new LoginPage());
                    return;
                }
                await DisplayAlert("Thông báo", "Cập nhật thông tin thành công!", "OK");
            } else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }
    }
}