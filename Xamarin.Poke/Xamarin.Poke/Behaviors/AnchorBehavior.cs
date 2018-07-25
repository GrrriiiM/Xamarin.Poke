﻿using System;
using System.Reflection;
using Xamarin.Forms;

namespace Xamarin.Poke.Behaviors
{
    public class AnchorBehavior : Behavior<View>
    {

        public View View
        {
            get;
            private set;
        }

        private PropertyInfo _property;


        public static readonly BindableProperty AnchorToProperty = BindableProperty.Create(
            nameof(AnchorTo),
            typeof(double),
            typeof(AnchorBehavior),
            0D, 
            propertyChanged: (b, o, n) =>
            {
            
                var ab = (AnchorBehavior)b;
                if (ab.View != null)
                {
                    var v = (double)n;

                    var d = ab.MaxValue - ab.MinValue;
                    
                    var i = 1D;
                    if (ab.MinValueAnchorTo > 0 && ab.MaxValueAnchorTo > 0)
                    {
                        i = ab.MaxValueAnchorTo - ab.MinValueAnchorTo;
                        i = (d) / i;
                        v = (v - ab.MinValueAnchorTo) * i;
                        v = v + ab.MinValue; 
                    }

                v = v * ab.ProportionalValueAnchorTo;
                    v = v + ab.InitialValue;

                    


                    if (v >= ab.MaxValue)
                    {
                        v = ab.MaxValue;
                    }
                    else if (v <= ab.MinValue)
                    {
                        v = ab.MinValue;
                    }

                    ab._property.SetValue(ab.View, v);
                }
            }
        );

        public double AnchorTo
        {
            get => (double)GetValue(AnchorToProperty);
            set => SetValue(AnchorToProperty, value);
        }

        public double ProportionalValueAnchorTo
        {
            get;
            set;
        } = 1;


        public double MinValueAnchorTo
        {
            get;
            set;
        } = -1;

        public double MaxValueAnchorTo
        {
            get;
            set;
        } = -1;


        public string PropertyNameToAnchor
        {
            get;
            set;
        }

        public double MaxValue
        {
            get;
            set;
        }

        public double MinValue
        {
            get;
            set;
        }

        public double InitialValue
        {
            get;
            set;
        }



        protected override void OnAttachedTo(View bindable)
        {
            

            this.View = bindable;

            var type = this.View.GetType();

            var propertyInfo = type.GetProperty(this.PropertyNameToAnchor);

            if (propertyInfo == null)
            {
                throw new Exception($"{nameof(this.PropertyNameToAnchor)} invalid");
            }
            this._property = propertyInfo;

            if (this.MaxValueAnchorTo >= 0 || this.MinValueAnchorTo >= 0)
            {
                if (this.MaxValueAnchorTo < 0)
                {
                    throw new Exception($"{this.MaxValueAnchorTo} invalid");
                }
                if (this.MinValueAnchorTo < 0)
                {
                    throw new Exception($"{this.MinValueAnchorTo} invalid");
                }
            }

            base.OnAttachedTo(bindable);
        }


    }
}