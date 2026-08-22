using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class IzvodjacRepository : Repository<Izvodjac>, IIzvodjacRepository
    {
        public IzvodjacRepository(MuzickiFestivaliDbContext context) : base(context) { }
    }
}
