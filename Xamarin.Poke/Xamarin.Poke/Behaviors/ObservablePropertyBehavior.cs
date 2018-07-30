using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Poke.Behaviors
{
    public class ObservablePropertyBehavior : Behavior<View>
    {
        public View View
        {
            get;
            private set;
        }

        private PropertyInfo _property;

        public string PropertyName
        {
            get;
            set;
        }

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
            nameof(MinValue),
            typeof(double),
            typeof(ObservablePropertyBehavior),
            -1D);

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
            nameof(MaxValue),
            typeof(double),
            typeof(ObservablePropertyBehavior),
            -1D);

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {


            this.View = bindable;

            var type = this.View.GetType();

            var propertyInfo = type.GetProperty(this.PropertyName);

            if (propertyInfo == null)
            {
                throw new Exception($"{nameof(this.PropertyName)} invalid");
            }
            this._property = propertyInfo;

            if (this.MaxValue >= 0 || this.MinValue >= 0)
            {
                if (this.MaxValue < 0)
                {
                    throw new Exception($"{this.MaxValue} invalid");
                }
                if (this.MinValue < 0)
                {
                    throw new Exception($"{this.MinValue} invalid");
                }
            }

            this.View.PropertyChanged += View_PropertyChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            this.View.PropertyChanged -= View_PropertyChanged;
        }

        private PropertyChangedEventHandler propertyChanged = async (s, e) =>
        {
            
        };

        private bool alredyDoMax = false;
        private bool alredyDoMin = false;
        private async void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.PropertyName)
            {
                var v = (double)this._property.GetValue(sender);

                if (v <= this.MinValue)
                {
                    if (!alredyDoMin)
                    {
                        if (!this.View.Behaviors.Any(x => this.hasConflict(x, v)))
                        {
                            this.ObservablePropertyChanged?.Invoke(sender, new ObservableBehaviorEventArgs(0));
                        }
                        alredyDoMin = true;
                    }
                    return;
                }
                if (v >= this.MaxValue)
                {
                    if (!alredyDoMax)
                    {
                        if (!this.View.Behaviors.Any(x => this.hasConflict(x, v)))
                        {
                            this.ObservablePropertyChanged?.Invoke(sender, new ObservableBehaviorEventArgs(1));
                        }
                        alredyDoMax = true;
                    }
                    return;
                }
                var i = this.MaxValue - this.MinValue;
                i = (v - this.MinValue) / i;
                alredyDoMax = false;
                alredyDoMin = false;
                if (!this.View.Behaviors.Any(x => this.hasConflict(x, v)))
                {
                    this.ObservablePropertyChanged?.Invoke(sender, new ObservableBehaviorEventArgs(i));
                }
            }
        }

        private bool hasConflict(Behavior b, double value)
        {
            var bv = b as ObservablePropertyBehavior;

            if (bv == null)
            {
                return false;
            }

            if (bv != this)
            {
                if (bv.PropertyName == this.PropertyName)
                {
                    if (value > this.MaxValue && bv.MinValue >= this.MaxValue)
                    {
                        return true;
                    }
                    if (value < this.MinValue && bv.MaxValue<= this.MinValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public event EventHandler<ObservableBehaviorEventArgs> ObservablePropertyChanged;
        
    }

    public class ObservableBehaviorEventArgs : EventArgs
    {
        public double Proportion { get; set; }
        public ObservableBehaviorEventArgs(double proportion)
        {
            this.Proportion = proportion;
        }
    }

}
