using DocBaoHay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageTopicsPage : ContentPage
    {
        public ManageTopicsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            HttpClient http = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/chu-de";
            var ChuDeList_str = await http.GetStringAsync(url);

            var ChuDeList = JsonConvert.DeserializeObject<List<ChuDe>>(ChuDeList_str);
            NewsLV.ItemsSource = ChuDeList;
        }

        private async void ManageRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTopicPage());
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Cảnh báo", "Xóa một chủ đề cũng sẽ xóa luôn các bài báo liên quan và hủy theo dõi chủ đề của người dùng. Thao tác này không thể hoàn tác. Bạn có chắc chắn muôn xóa?", "OK", "Hủy");
            if (choose)
            {
                HttpClient http = new HttpClient();
                string url = "http://192.168.56.1/docbaohay/api/chu-de/" + ((ImageButton)sender).CommandParameter;
                await http.DeleteAsync(url);
                await DisplayAlert("Thông báo", "Xóa chủ đề thành công", "OK");
            }
            InitializeData();
        }

        protected override void OnAppearing()
        {
            InitializeData();
        }
    }
}