using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Repositories
{
    public interface ITerminRepository : IRepository<Termin>
    {
        Task<IEnumerable<Termin>> GetByNastupIdAsync(int idFestival, int idNastup);
    }
}
