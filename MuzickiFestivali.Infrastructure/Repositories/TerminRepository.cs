using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class TerminRepository : Repository<Termin>, ITerminRepository
    {
        public TerminRepository(MuzickiFestivaliDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Termin>> GetByNastupIdAsync(int idFestival, int idNastup) =>
            await DbSet.Where(t => t.idFestival == idFestival && t.idNastup == idNastup)
                       .ToListAsync();

        public async Task<IEnumerable<Termin>> GetGlavniTerminiByNastupAsync(int idFestival, int idNastup)
        {
            return await DbSet
                .Where(t => t.idFestival == idFestival &&
                            t.idNastup == idNastup &&
                            t.tip == TipTermina.GlavniNastup)
                .ToListAsync();
        }
    }
}
