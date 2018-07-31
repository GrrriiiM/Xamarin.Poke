using System;
using Poke.Repository;
using Poke.Repository.PokeApi;
using Xamarin.Poke.ViewModels;

namespace Xamarin.Poke
{
    public class IOC : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            this.Bind<PokedexViewModel>().To<PokedexViewModel>();
            this.Bind<MontersListAllResumeViewModel>().To<MontersListAllResumeViewModel>();



            this.Bind<IRepositoryService>().To<RepositoryService>();
        }
    }
}
