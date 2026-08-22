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
    }
}
