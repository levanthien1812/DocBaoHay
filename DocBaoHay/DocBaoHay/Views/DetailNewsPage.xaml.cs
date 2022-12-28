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
	public partial class DetailNewsPage : ContentPage
	{
		public DetailNewsPage ()
		{
			InitializeComponent ();
		}

		public DetailNewsPage (BaiBao baiBao)
		{
			InitializeComponent ();
		}
	}
}