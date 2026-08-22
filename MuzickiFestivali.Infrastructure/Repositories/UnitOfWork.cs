using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;
using MuzickiFestivali.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MuzickiFestivaliDbContext _context;

        public UnitOfWork(MuzickiFestivaliDbContext context)
        {
            _context = context;
            Osobe = new OsobaRepository(_context);
            Zaposleni = new ZaposleniRepository(_context);
            Izvodjaci = new IzvodjacRepository(_context);
            Korisnici = new KorisnikRepository(_context);
            Festivali = new FestivalRepository(_context);
            Nastupi = new NastupRepository(_context);
            Termini = new TerminRepository(_context);
            Bine = new BinaRepository(_context);
            Lajkovi = new LajkujeRepository(_context);
            Nastupanja = new NastupaRepository(_context);
        }
        public IOsobaRepository Osobe { get; private set; }
        public IZaposleniRepository Zaposleni { get; private set; }
        public IIzvodjacRepository Izvodjaci { get; private set; }
        public IKorisnikRepository Korisnici { get; private set; }
        public IFestivalRepository Festivali { get; private set; }
        public INastupRepository Nastupi { get; private set; }
        public ITerminRepository Termini { get; private set; }
        public IBinaRepository Bine { get; private set; }
        public ILajkujeRepository Lajkovi { get; private set; }
        public INastupaRepository Nastupanja { get; private set; }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
