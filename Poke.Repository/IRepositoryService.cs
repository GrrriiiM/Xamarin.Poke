using Poke.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poke.Repository
{
    public interface IRepositoryService
    {
        Task<IEnumerable<MonsterResume>> MonsterListAllResumePerRegion(int regionId);

    }
}
