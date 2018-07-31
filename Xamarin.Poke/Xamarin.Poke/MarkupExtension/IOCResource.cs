using System;
using Xamarin.Forms.Xaml;
using Xamarin.Poke.ViewModels;
using Ninject;
using System.Xml;
using Xamarin.Forms;

namespace Xamarin.Poke.MarkupExtension
{
    [ContentProperty("TypeInstance")]
    public class IOCResource : IMarkupExtension<IBaseModelView>
    {

        public Type TypeInstance
        {
            get;
            set;
        }

        public IBaseModelView ProvideValue(IServiceProvider serviceProvider)
        {
            var r = (IBaseModelView)App.Container.Get(this.TypeInstance);
            return r;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<IBaseModelView>).ProvideValue(serviceProvider);
        }
    }
}
