using Poke.Model;
using Poke.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Poke.Helpers;

namespace Xamarin.Poke.ViewModels
{
    public class MontersListAllResumeViewModel : ObservableObject, IBaseModelView
    {

        
        public ObservableCollection<MonsterResume> MonstersResume { get; set; } = new ObservableCollection<MonsterResume>();
        public ObservableCollection<Region> Regions { get; set; } = new ObservableCollection<Region>();

        private MonsterResume monster;
        public MonsterResume Monster
        {
            get => this.monster;
            set => SetProperty(ref this.monster, value);
        }

        public ICommand LoadCommand { get; set; }

        private readonly IRepositoryService _repositoryService;
        public MontersListAllResumeViewModel(IRepositoryService repositoryService)
        {
             this._repositoryService = repositoryService;
            this.LoadCommand = new Command(async () => await load());
        }

        private async Task load()
        {
            this.MonstersResume.Clear();
            foreach(var i in (await this._repositoryService.MonsterListAllResumePerRegion(2)))
            {
                this.MonstersResume.Add(i);
            }

            this.Regions.Clear();
            foreach (var i in (await this._repositoryService.RegionListAll()))
            {
                this.Regions.Add(i);
            }
        }
    }
}
