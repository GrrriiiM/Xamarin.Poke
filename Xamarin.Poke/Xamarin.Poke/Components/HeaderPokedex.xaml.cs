using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Poke.Components
{
    public partial class HeaderPokedex : ContentView
    {
        public HeaderPokedex()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ScrollYPositionProperty = BindableProperty.Create(
            nameof(ScrollYPosition),
            typeof(double),
            typeof(HeaderPokedex), 
            0D);

        public double ScrollYPosition
        {
            get => (double)GetValue(ScrollYPositionProperty);
            set => SetValue(ScrollYPositionProperty, value);
        }

        private double LastY = 0;
        void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            
            if (e.StatusType == GestureStatus.Running)
            {
                if (this.HeightRequest + (e.TotalY) >= 200D)
                {
                    this.HeightRequest = 200D + (e.TotalY);
                }
                this.LastY = e.TotalY - this.LastY;;
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                if (this.LastY > 30)
                {
                    this.expand(e.TotalY);
                }
                else if (this.LastY< - 30)
                {
                    this.collapse(this.LastY); 
                }
            }
            else if (e.StatusType == GestureStatus.Started)
            {
                this.LastY = 0;
            }
        }

        private async Task expand(double size)
        {
            var th = (double)App.Current.Resources["screenDensityHeight"] - (double)App.Current.Resources["headerExpandedHeight"];
            var h = th - size;
            var time = (h / th) * 300;

            await this.LayoutTo(new Rectangle(this.X, this.Y, this.Width, (double)App.Current.Resources["screenDensityHeight"]), (uint)time, Easing.SinOut);

        }

        private async Task collapse(double size)
        {
            var th = (double)App.Current.Resources["screenDensityHeight"] - (double)App.Current.Resources["headerExpandedHeight"];
            var h = th - size;
            var time = (h / th) * 300;

            await this.LayoutTo(new Rectangle(this.X, this.Y, this.Width, (double)App.Current.Resources["headerExpandedHeight"]), (uint)time, Easing.SinOut);

        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (this.Height != (double)App.Current.Resources["screenDensityHeight"])
            {
                this.expand(0);
            }
            else
            {
                this.collapse(0);
            }
        }

    }
}
