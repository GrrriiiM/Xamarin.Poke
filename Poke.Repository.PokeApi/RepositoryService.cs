using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Model;

namespace Poke.Repository.PokeApi
{
    public class RepositoryService : IRepositoryService
    {

        private MonsterRepository _monsterRepository;
        private MonsterRepository getMonsterRepository => _monsterRepository = _monsterRepository ?? new MonsterRepository();


        private RegionRepository _regionRepository;
        private RegionRepository getRegionRepository => _regionRepository = _regionRepository ?? new RegionRepository();


        public async Task<IEnumerable<MonsterResume>> MonsterListAllResumePerRegion(int regionId) => await getMonsterRepository.ListAllResumePerRegion(regionId);

        public async Task<IEnumerable<Region>> RegionListAll() => await getRegionRepository.ListAll();

    }
}
