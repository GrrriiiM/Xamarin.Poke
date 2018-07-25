//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xamarin.Forms;

//namespace Xamarin.Poke.Behaviors
//{
//    public class ColapseBehavior : Behavior<View>
//    {
//        public static readonly BindableProperty ReferenceAnchorHeightProperty =
//        BindableProperty.Create(nameof(ReferenceAnchorHeight), typeof(double), typeof(ColapseBehavior), 
//            defaultBindingMode: BindingMode.TwoWay,
//            propertyChanged: (b, o, n) => {
//                var a = "";
//            });

//        public double ReferenceAnchorHeight
//        {
//            get => (double)GetValue(ReferenceAnchorHeightProperty);
//            set => SetValue(ReferenceAnchorHeightProperty, value);
//        }


//        public static readonly BindableProperty InitialHeightProperty =
//        BindableProperty.Create(nameof(InitialHeight), typeof(double), typeof(ColapseBehavior));

//        public double InitialHeight
//        {
//            get => (double)GetValue(InitialHeightProperty);
//            set => SetValue(InitialHeightProperty, value);
//        }


//        public static readonly BindableProperty MaxHeightProperty =
//        BindableProperty.Create(nameof(MaxHeight), typeof(double), typeof(ColapseBehavior));

//        public double MaxHeight
//        {
//            get => (double)GetValue(MaxHeightProperty);
//            set => SetValue(MaxHeightProperty, value);
//        }

//        public static readonly BindableProperty MinHeightProperty =
//        BindableProperty.Create(nameof(MinHeight), typeof(double), typeof(ColapseBehavior));

//        public double MinHeight
//        {
//            get => (double)GetValue(MinHeightProperty);
//            set => SetValue(MinHeightProperty, value);
//        }

//    }
//}
