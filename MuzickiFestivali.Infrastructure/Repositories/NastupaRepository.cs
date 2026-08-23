using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class NastupaRepository : Repository<Nastupa>, INastupaRepository
    {
        public NastupaRepository(MuzickiFestivaliDbContext context) : base(context) { }
        public async Task<Nastupa?> GetByKeyAsync(int idOsoba, int idFestival, int idNastup, int idTermin) =>
           await DbSet.FirstOrDefaultAsync(n =>
               n.idOsoba == idOsoba &&
               n.idFestival == idFestival &&
               n.idNastup == idNastup &&
               n.idTermin == idTermin);

        public async Task<IEnumerable<Nastupa>> GetBySlotAsync(int idFestival, int idNastup, int idTermin) =>
            await DbSet.Include(n => n.izvodjac) 
                       .Where(n => n.idFestival == idFestival && n.idNastup == idNastup && n.idTermin == idTermin)
                       .ToListAsync();

        public async Task<IEnumerable<Nastupa>> GetByPerformerAsync(int idOsoba) =>
            await DbSet.Include(n => n.termin)
                    .ThenInclude(t => t.bina) 
               .Include(n => n.termin)
                    .ThenInclude(t => t.nastup)
                        .ThenInclude(nas => nas.festival) 
               .Where(n => n.idOsoba == idOsoba)
               .ToListAsync();
    }
}
