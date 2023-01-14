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
    public partial class AddAuthorPage : ContentPage
    {
        public AddAuthorPage()
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

            TacGia tacGia = new TacGia
            {
                Ten = ten,
                Hinh = hinh
            };

            HttpClient http = new HttpClient();
            string tgJson = JsonConvert.SerializeObject(tacGia);
            StringContent httpContent = new StringContent(tgJson, Encoding.UTF8, "application/json");
            HttpResponseMessage ketQua = await http.PostAsync("http://192.168.56.1/docbaohay/api/tac-gia", httpContent);
            var ketQuaTV = await ketQua.Content.ReadAsStringAsync();
            int result = int.Parse(JsonConvert.DeserializeObject<int>(ketQuaTV).ToString());

            if (result == 1)
            {
                await DisplayAlert("Thông báo", "Thêm tác giả thành công", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }
    }
}