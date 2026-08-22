using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Repositories
{
    public interface INastupRepository : IRepository<Nastup>
    {
        Task<IEnumerable<Nastup>> GetByFestivalIdAsync(int idFestival);
    }
}
