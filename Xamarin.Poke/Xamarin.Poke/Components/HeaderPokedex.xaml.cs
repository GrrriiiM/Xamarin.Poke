using System;
using System.Collections.Generic;
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


        void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                if (this.HeightRequest + e.TotalY >= 200D)
                {
                    this.HeightRequest = 200D + e.TotalY;
                }
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                this.HeightRequest = 200D;
            }
        }

    }
}
