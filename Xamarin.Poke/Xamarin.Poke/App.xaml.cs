using Ninject;
using Poke.Repository;
using Poke.Repository.PokeApi;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Poke.ViewModels;
using Xamarin.Poke.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Xamarin.Poke
{
    public partial class App : Application
    {
        public new static App Current
        {
            get
            {
                return (App)Application.Current;
            }
        }


        public static IKernel Container { get; set; }


        public App ()
		{
			InitializeComponent();

            //this.MontersListAllResumeViewModel = new MontersListAllResumeViewModel(new RepositoryService());

            this.Resources["screenDensity"] = DeviceDisplay.ScreenMetrics.Density;
            this.Resources["screenDensityHeight"] = (DeviceDisplay.ScreenMetrics.Height / DeviceDisplay.ScreenMetrics.Density) - new OnPlatform<double> { Android = 20 };
            this.Resources["screenDensityWidth"] = DeviceDisplay.ScreenMetrics.Width / DeviceDisplay.ScreenMetrics.Density;
            this.Resources["screenDensityWidthMinusButtonSize"] = (double)this.Resources["screenDensityWidth"] - (double)this.Resources["buttonSize"];

            App.Container = new Ninject.StandardKernel(new IOC());

            MainPage = new PokedexPage();
        }

        //public MontersListAllResumeViewModel MontersListAllResumeViewModel { get; private set; }
        public PokemonViewModel PokemonViewModel { get; private set; } = new PokemonViewModel();

        public T GetResource<T>(string name)
        {
            return (T)this.Resources[name];
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
