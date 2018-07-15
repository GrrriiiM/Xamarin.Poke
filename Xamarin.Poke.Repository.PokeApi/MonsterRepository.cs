using Poke.Model;
using PokeAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poke.Repository.PokeApi
{
    public class MonsterRepository
    {
        private const string IMAGE_DEFAULT = "http://pokeapi.co/media/sprites/pokemon/";
        private const string IMAGE_BACK = "http://pokeapi.co/media/sprites/pokemon/back/";
        private const string IMAGE_SHINY = "http://pokeapi.co/media/sprites/pokemon/shiny/";
        private const string IMAGE_BACK_SHINY = "http://pokeapi.co/media/sprites/pokemon/back/shiny/";
        private const string IMAGE_EXTENSION = ".png";


        public async Task<IEnumerable<MonsterResume>> ListAllPerRegion(int regionId)
        {
            var pokedex = await DataFetcher.GetApiObject<Pokedex>(regionId);
            return pokedex.Entries.Select(x => new MonsterResume
            {
                Id = x.EntryNumber,
                Name = x.Species.Name,
                ImagePath = $"{IMAGE_DEFAULT}{x.EntryNumber}{IMAGE_EXTENSION}"
            });
        }
    }
}
