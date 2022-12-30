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

        }

        private void FollowedBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageFollowPage());
        }

        private void RecentBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageRecentNews());
        }

        private void UpdateInfoBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}