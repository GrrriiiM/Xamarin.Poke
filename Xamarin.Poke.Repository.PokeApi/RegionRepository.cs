using Poke.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poke.Repository.PokeApi
{
    public class RegionRepository
    {

        private static Dictionary<int, string> list = new Dictionary<int, string>
        {
            { 1, "national" },
            { 2, "kanto" },
            { 3, "original-johto"},
            {4, "hoenn"},
            {5, "original-sinnoh"},
            {6, "extended-sinnoh"},
            {7, "updated-johto"},
            {8, "original-unova"},
            {9,"updated-unova"},
            {10, "conquest-gallery"},
            {11, "kalos-central"},
            {12, "kalos-coastal"},
            {13, "kalos-mountain"},
            {14, "updated-hoenn"}
        };
        

        public async Task<IEnumerable<Region>> ListAll()
        {
            return list.Select(x => new Region { Id = x.Key, Name = x.Value }).ToList();
        }
    }
}
