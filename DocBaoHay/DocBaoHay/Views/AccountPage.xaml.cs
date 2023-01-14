using DocBaoHay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

         protected override void OnAppearing()
        {
            if (NguoiDung.nguoiDung != null)
            {
                NotSignedInSL.IsVisible = false;
                SignedInSL.IsVisible = true;
                if (NguoiDung.nguoiDung.QuanTriVien == true)
                {
                    AdminSL.IsVisible = true;
                    SignedInBtns.IsVisible = false;
                } else
                {
                    AdminSL.IsVisible = false;
                    SignedInBtns.IsVisible = true;
                }
                initializeSignedIn();
            }
            else
            {
                NotSignedInSL.IsVisible = true;
                SignedInSL.IsVisible = false;
            }
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void initializeSignedIn()
        {
            UserImg.Source = "user_default_logo.jpg";
            UserUsername.Text = NguoiDung.nguoiDung.TenDangNhap.ToString();
            UserFullname.Text = NguoiDung.nguoiDung.HoTen.ToString();
        }

        private void SavedBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageSavedNews());
        }

        private void FollowedBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageFollowPage());
        }

        private void RecentBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageRecentNews());
        }

        private async void UpdateInfoBtn_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Xác nhận", "Vui lòng nhập mật khẩu để tiếp tục", "OK", "Hủy", "Mật khẩu của bạn");
            if (result == "") return;
            if (result != NguoiDung.nguoiDung.MatKhau)
            {
                bool choose = await DisplayAlert("Thông báo", "Bạn đã nhập sai mật khẩu! Vui lòng thử lại", "OK", "Hủy");
                if (choose) UpdateInfoBtn_Clicked(sender, EventArgs.Empty);
                return;
            }
            await Navigation.PushAsync(new PersonalInfoPage());
        }

        private async void LogOutBtn_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Thông báo", "Bạn có chắc chắn muốn đăng xuất", "OK", "Hủy");
            if (choose == false) return;
            NguoiDung.nguoiDung = null;
            OnAppearing();
        }

        private void ManageNewsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageNewsPage());
        }

        private void ManageTopicsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageTopicsPage());
        }

        private void ManageAuthorsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageAuthorsPage());
        }
    }
}