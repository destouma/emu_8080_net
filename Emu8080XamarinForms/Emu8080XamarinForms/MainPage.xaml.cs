using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Emu8080XamarinForms
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;
        Image imageToShow;
        ByteArrayToImageSourceConverter ByteArrayToImage;

        public MainPage()
        {
            InitializeComponent();
            ByteArrayToImage = new ByteArrayToImageSourceConverter();

            BindingContext = viewModel = new MainPageViewModel();
            viewModel.startEmulator();
        }

    }
}
