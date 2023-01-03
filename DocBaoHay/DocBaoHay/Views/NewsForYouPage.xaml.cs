﻿using DocBaoHay.Models;
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
    public partial class NewsForYouPage : ContentPage
    {
        public NewsForYouPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData()
        {
            if (NguoiDung.nguoiDung == null) return;

            HttpClient httpClient = new HttpClient();

            string url = "http://192.168.56.1/docbaohay/api/bai-bao/cho-ban?NguoiDungId=" + NguoiDung.nguoiDung.Id;
            var BaiBaoList_str = await httpClient.GetStringAsync(url);

            var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
            NewsLV.ItemsSource = BaiBaoList;
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BaiBao_ChuDe baiBao = e.SelectedItem as BaiBao_ChuDe;
            Navigation.PushAsync(new DetailNewsPage(baiBao));
        }

        private async void ManageRV_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ((RefreshView)sender).IsRefreshing = false;
            InitializeData();
        }
    }
}