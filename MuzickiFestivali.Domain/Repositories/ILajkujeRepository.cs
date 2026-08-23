using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Repositories
{
    public interface ILajkujeRepository : IRepository<Lajkuje>
    {
        Task<Lajkuje?> GetByKeyAsync(int idOsoba, int idFestival, int idNastup);

        Task<IEnumerable<Lajkuje>> GetLikesByKorisnikAsync(int idOsoba);
    }
}
