using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class NastupRepository : Repository<Nastup>, INastupRepository
    {
        public NastupRepository(MuzickiFestivaliDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Nastup>> GetByFestivalIdAsync(int idFestival) =>
            await DbSet.Include(n => n.lajkovi).Where(n => n.idFestival == idFestival)
                       .ToListAsync();
    }
}
