using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class ZaposleniRepository : Repository<Zaposleni>, IZaposleniRepository
    {
        public ZaposleniRepository(MuzickiFestivaliDbContext context) : base(context) { }
    }
}
