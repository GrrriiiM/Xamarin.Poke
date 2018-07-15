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


        public async Task<IEnumerable<MonsterResume>> MonsterListAllResumePerRegion(int regionId) => await getMonsterRepository.ListAllResumePerRegion(regionId);

    }
}
