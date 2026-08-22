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
    }
}
