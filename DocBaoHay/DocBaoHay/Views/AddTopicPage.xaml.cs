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
    public partial class AddTopicPage : ContentPage
    {
        public AddTopicPage()
        {
            InitializeComponent();
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            string ten = Ten.Text;
            string hinh = Hinh.Text;

            if (string.IsNullOrEmpty(ten) || string.IsNullOrWhiteSpace(hinh))
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin!", "OK");
                return;
            }
            
            ChuDe chuDe = new ChuDe
            {
                Ten = ten,
                Hinh = hinh
            };

            HttpClient http = new HttpClient();
            string cdJson = JsonConvert.SerializeObject(chuDe);
            StringContent httpContent = new StringContent(cdJson, Encoding.UTF8, "application/json");
            HttpResponseMessage ketQua = await http.PostAsync("http://192.168.56.1/docbaohay/api/chu-de", httpContent);
            var ketQuaTV = await ketQua.Content.ReadAsStringAsync();
            int result = int.Parse(JsonConvert.DeserializeObject<int>(ketQuaTV).ToString());

            if (result == 1)
            {
                await DisplayAlert("Thông báo", "Thêm chủ đề thành công", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }
    }
}