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
    public partial class AddNewsPage : ContentPage
    {
        List<ChuDe> ChuDeList;
        List<TacGia> TacGiaList;
        public AddNewsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData ()
        {
            HttpClient http1 = new HttpClient();

            string url1 = "http://192.168.56.1/docbaohay/api/chu-de";
            var ChuDeList_str = await http1.GetStringAsync(url1);

            ChuDeList = JsonConvert.DeserializeObject<List<ChuDe>>(ChuDeList_str);
            ChuDePicker.ItemsSource = ChuDeList;
            ChuDePicker.ItemDisplayBinding = new Binding("Ten");

            string url2 = "http://192.168.56.1/docbaohay/api/tac-gia";
            var TacGiaList_str = await http1.GetStringAsync(url2);

            TacGiaList = JsonConvert.DeserializeObject<List<TacGia>>(TacGiaList_str);
            TacGiaPicker.ItemsSource= TacGiaList;
            TacGiaPicker.ItemDisplayBinding = new Binding("Ten");
        }

        private async void AddContentBtn_Clicked(object sender, EventArgs e)
        {
            string tieuDe = TieuDe.Text;
            string thumbnail = Thumbnail.Text;
            string moTa = MoTa.Text;
            int tacGia = TacGiaList[TacGiaPicker.SelectedIndex].Id;
            int chuDe = ChuDeList[ChuDePicker.SelectedIndex].Id;

            BaiBao baiBao = new BaiBao
            {
                TieuDe = tieuDe,
                Thumbnail = thumbnail,
                MoTa = moTa,
                TacGia = tacGia,
                ChuDe = chuDe,
            };
            HttpClient http = new HttpClient();
            string bbJson = JsonConvert.SerializeObject(baiBao);
            StringContent httpContent = new StringContent(bbJson, Encoding.UTF8, "application/json");
            HttpResponseMessage ketQua = await http.PostAsync("http://192.168.56.1/docbaohay/api/bai-bao", httpContent);
            var ketQuaTV = await ketQua.Content.ReadAsStringAsync();
            int result = int.Parse(JsonConvert.DeserializeObject<int>(ketQuaTV).ToString());

            if (result == 1)
            {
                await DisplayAlert("Thông báo", "Thêm bài báo thành công! Hãy thêm nội dung cho bài báo", "OK");
                await Navigation.PushAsync(new AddContentPage());
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }
    }
}