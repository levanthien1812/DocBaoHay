using DocBaoHay.Models;
using DocBaoHay.Services;
using DocBaoHay.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocBaoHay
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            NguoiDung.nguoiDung = null;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
