using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class KorisnikRepository : Repository<Korisnik>, IKorisnikRepository
    {
        public KorisnikRepository(MuzickiFestivaliDbContext context) : base(context) { }
    }
}
