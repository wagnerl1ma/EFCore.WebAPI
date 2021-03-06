﻿using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public class HeroiContext : DbContext
    {

        public HeroiContext(DbContextOptions<HeroiContext> options) : base(options)
        {
                
        }

        //public HeroiContext()
        //{

        //}

        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<HeroiBatalha> HeroiBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadeSecreta { get; set; }


        // configurando string de conexao
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-BK18BBM;Database=HerosAppDB;User Id=sa;Password=123456;");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(hb => { hb.HasKey(x => new { x.BatalhaId, x.HeroiId }); });  //defininco chave composta para HeroiBatalha
        }
    }
}
