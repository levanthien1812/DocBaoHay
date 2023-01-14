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
    public partial class CompositeNewsPage : ContentPage
    {
        public CompositeNewsPage()
        {
            InitializeComponent();
            InitializeData();
        }

        async void InitializeData ()
        {
            HttpClient httpClient = new HttpClient();

            var ChuDeList_str = await httpClient.GetStringAsync("http://192.168.56.1/docbaohay/api/chu-de");

            var ChuDeList = JsonConvert.DeserializeObject<List<ChuDe>>(ChuDeList_str);
            foreach(ChuDe cd in ChuDeList )
            {
                Label lb = new Label
                {
                    Text = cd.Ten
                };
                var BaiBaoList_str = await httpClient.GetStringAsync("http://192.168.56.1/docbaohay/api/bai-bao?chu-de=" + cd.Id);
                var BaiBaoList = JsonConvert.DeserializeObject<List<BaiBao_ChuDe>>(BaiBaoList_str);
                ListView lv = new ListView
                {
                    ItemsSource = BaiBaoList
                };

                MainSL.Children.Add(lb);
                MainSL.Children.Add(lv);
            }
        }
    }
}