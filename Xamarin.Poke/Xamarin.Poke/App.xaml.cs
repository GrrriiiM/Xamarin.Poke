using Poke.Repository;
using Poke.Repository.PokeApi;
using System;
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

        private IRepositoryService repositoryService;

        public App ()
		{
			InitializeComponent();

			

            this.repositoryService = new RepositoryService();

            this.MontersListAllResumeViewModel = new MontersListAllResumeViewModel(repositoryService);

            MainPage = new PokedexPage();
        }

        public MontersListAllResumeViewModel MontersListAllResumeViewModel { get; private set; }
        public PokemonViewModel PokemonViewModel { get; private set; } = new PokemonViewModel();


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
