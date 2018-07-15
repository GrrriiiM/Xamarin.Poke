using Poke.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Poke.Repository.PokeApi
{
    internal class RegionRepository
    {
        public async Task<IEnumerable<Region>> ListAll()
        {
            var regions = new List<Region>();
            //var 

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MonsterRepository)).Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.pokedex.json");
            string json = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            regions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Region>>(json);

            return regions;
        }
    }
}
