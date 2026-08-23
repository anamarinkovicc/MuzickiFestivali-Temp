using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class LajkujeRepository : Repository<Lajkuje>, ILajkujeRepository
    {
        public LajkujeRepository(MuzickiFestivaliDbContext context) : base(context) { }
        public async Task<Lajkuje?> GetByKeyAsync(int idOsoba, int idFestival, int idNastup) =>
            await DbSet.FirstOrDefaultAsync(l => l.idOsoba == idOsoba && l.idFestival == idFestival && l.idNastup == idNastup);

        public async Task<IEnumerable<Lajkuje>> GetLikesByKorisnikAsync(int idOsoba) =>
            await DbSet.Include(l => l.nastup) 
                       .Where(l => l.idOsoba == idOsoba)
                       .ToListAsync();
    }
}
