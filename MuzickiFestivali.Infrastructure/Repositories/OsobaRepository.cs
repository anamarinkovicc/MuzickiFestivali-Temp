using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class OsobaRepository : Repository<Osoba>, IOsobaRepository
    {
        public OsobaRepository(MuzickiFestivaliDbContext context) : base(context)
        {
        }

        public async Task<Osoba?> GetByEmailAndPasswordAsync(string email, string lozinka) =>
            await DbSet.FirstOrDefaultAsync(o => o.email == email && o.lozinka == lozinka);
    }
}
