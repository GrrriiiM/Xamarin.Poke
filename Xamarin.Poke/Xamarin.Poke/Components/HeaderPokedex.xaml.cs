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
    }
}
