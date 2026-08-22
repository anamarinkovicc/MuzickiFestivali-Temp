using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class BinaRepository : Repository<Bina>, IBinaRepository
    {
        public BinaRepository(MuzickiFestivaliDbContext context) : base(context) { }
    }
}
