using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Repositories
{
    public interface IOsobaRepository : IRepository<Osoba>
    {
        Task<Osoba?> GetByEmailAndPasswordAsync(string email, string lozinka);
    }
}
