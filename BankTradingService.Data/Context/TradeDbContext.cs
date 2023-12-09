using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Mappings;
using BankTradingService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Context
{
    public class TradeDbContext : DbContext, ITradeDbContext
    {
        protected readonly IConfiguration Configuration;

        public TradeDbContext() { }

        public TradeDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TradeMap());
        }

        public DbSet<UserDataModel> User{ get; set; }
        public DbSet<TradeDataModel> Trade { get; set; }

        public new void SaveChanges()
        {
            var a = this.ChangeTracker.Entries().ToArray();
            base.SaveChanges();
        }
    }
}
