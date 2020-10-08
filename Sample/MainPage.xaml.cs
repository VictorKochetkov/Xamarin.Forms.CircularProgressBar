using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Controls;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                AddProgress(bar1, 10);
                return true;
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                AddProgress(bar2, 10);
                return true;
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                AddProgress(bar3, 10);
                return true;
            });
        }

        void AddProgress(CircularProgressBar view, double add)
        {
            var value = view.Progress + add;

            if (value > 100)
                value = 0;

            view.Progress = value;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
