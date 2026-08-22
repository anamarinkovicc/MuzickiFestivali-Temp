using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    internal class FestivalRepository : Repository<Festival>, IFestivalRepository
    {
        public FestivalRepository(MuzickiFestivaliDbContext context) : base(context)
        {
        }
    }
}
