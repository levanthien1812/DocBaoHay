using DocBaoHay_WebAPI.Models;
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
	public partial class NewsByTopic : ContentPage
	{
		public NewsByTopic ()
		{
			InitializeComponent ();
		}
		public NewsByTopic (ChuDe chude)
		{
			InitializeComponent ();
			Title = "Tin " + chude.Ten;
            InitializeData(chude);
        }

        async void InitializeData(ChuDe chude)
        {
            HttpClient httpClient = new HttpClient();

            string url = "http://172.17.29.57/docbaohay/api/bai-bao/" + chude.ID;
            var BaiBaoList = await httpClient.GetStringAsync(url);

            var BaiBaoListConverted = JsonConvert.DeserializeObject<List<BaiBao>>(BaiBaoList);
            NewsLV.ItemsSource = BaiBaoListConverted;
        }

        private void NewsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}