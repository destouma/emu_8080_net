using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emu8080XamarinForms
{
    public partial class App : Application
    {
        public static IByteFile ByteFile { get; private set; }

        public static void Init(IByteFile byteFile)
        {
            App.ByteFile = byteFile;
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
