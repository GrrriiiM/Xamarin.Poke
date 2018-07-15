using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Poke.ViewModels
{
    public class PokemonViewModel
    {
        public ICommand InitCommand { get; set; }
        public ICommand LoadCommand { get; set; }
    }
}
