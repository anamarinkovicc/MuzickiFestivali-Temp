using MuzickiFestivali.Domain.Interfaces;
using MuzickiFestivali.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Repositories
{
    public interface INastupaRepository : IRepository<Nastupa>
    {
        Task<Nastupa?> GetByKeyAsync(int idOsoba, int idFestival, int idNastup, int idTermin);

        Task<IEnumerable<Nastupa>> GetBySlotAsync(int idFestival, int idNastup, int idTermin);

        Task<IEnumerable<Nastupa>> GetByPerformerAsync(int idOsoba);
    }
}
