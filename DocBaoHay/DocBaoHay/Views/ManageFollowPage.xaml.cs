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
    public partial class ManageFollowPage : ContentPage
    {
        public ManageFollowPage()
        {
            InitializeComponent();
            InitializeData();
        }

        public async void InitializeData()
        {
            HttpClient http = new HttpClient();

            string followedAuthors_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/theo-doi/tac-gia");
            var followedAuthors = JsonConvert.DeserializeObject<List<TacGia>>(followedAuthors_str);
            FollowedAuthorsLV.ItemsSource = followedAuthors;

            if (followedAuthors.Count == 0)
            {
                ThongBao1.IsVisible = true;
                ThongBao1.Text = "Bạn chưa theo dõi nguồn báo nào.";
            }
            else
            {
                ThongBao1.IsVisible = false;
                ThongBao1.Text = string.Empty;
            }

            string followedTopics_str = await http.GetStringAsync("http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/theo-doi/chu-de");
            var followedTopics = JsonConvert.DeserializeObject<List<ChuDe>>(followedTopics_str);
            FollowedTopicsLV.ItemsSource = followedTopics;

            if (followedTopics.Count == 0)
            {
                ThongBao2.IsVisible = true;
                ThongBao2.Text = "Bạn chưa theo dõi chủ đề nào.";
            }
            else
            {
                ThongBao2.IsVisible = false;
                ThongBao2.Text = string.Empty;
            }
        }

        private async void UnfollowAuthor_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Thông báo", "Bạn có chắc chắn muốn bỏ theo dõi?", "OK", "Hủy");
            if (choose == false) return;

            HttpClient http = new HttpClient();
            int tacGiaId = int.Parse(((Button)sender).CommandParameter.ToString());
            string unfollowAuthor_str = "http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/theo-doi/tac-gia/" + tacGiaId;
            await http.DeleteAsync(unfollowAuthor_str);
            InitializeData();
        }

        private async void FollowedAuthorsRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }

        private async void UnfollowTopic_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Thông báo", "Bạn có chắc chắn muốn bỏ theo dõi?", "OK", "Hủy");
            if (choose == false) return;

            HttpClient http = new HttpClient();
            int tacGiaId = int.Parse(((Button)sender).CommandParameter.ToString());
            string unfollowTopic_str = "http://192.168.56.1/docbaohay/api/nguoi-dung/" + NguoiDung.nguoiDung.Id + "/theo-doi/chu-de/" + tacGiaId;
            await http.DeleteAsync(unfollowTopic_str);
            InitializeData();
        }

        private async void FollowedTopicsRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }
    }
}