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

namespace Xamarin.Poke.ViewModels
{
    public class MontersListAllResumeViewModel
    {

        
        public ObservableCollection<MonsterResume> MonstersResume { get; set; } = new ObservableCollection<MonsterResume>();
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
        }
    }
}
