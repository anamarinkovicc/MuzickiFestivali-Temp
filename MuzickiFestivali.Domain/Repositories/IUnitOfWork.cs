using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOsobaRepository Osobe { get; }
        IZaposleniRepository Zaposleni { get; }
        IIzvodjacRepository Izvodjaci { get; }
        IKorisnikRepository Korisnici { get; }
        IFestivalRepository Festivali { get; }
        INastupRepository Nastupi { get; }
        ITerminRepository Termini { get; }
        IBinaRepository Bine { get; }
        ILajkujeRepository Lajkovi { get; }
        INastupaRepository Nastupanja { get; }

        Task<int> CompleteAsync();
    }
}
