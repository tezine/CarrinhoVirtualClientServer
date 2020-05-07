#region Imports
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedLib.Entities;
#endregion

namespace Server.Codes {
     public class SMySQLContext : DbContext {
        #region Fields
        public DbSet<EAdminUser> AdminUsers { get; set; }
        public DbSet<EUser> Users { get; set; }
        public DbSet<ECompanyAdminUser> CompanyAdminUsers { get; set; }
        public DbSet<EUserCompany> UserCompanies { get; set; }
        public DbSet<ECompany> Companies { get; set; }
        public DbSet<EProduct> Products { get; set; }
        public DbSet<ESearch> Searches { get; set; }
        
        #endregion

        #region OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseMySQL(SDefines.ConnectionString);
        }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                foreach (var property in entityType.GetProperties()) {
                    if (property.ClrType == typeof(bool)) {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }
        }
        #endregion
    }

    #region BoolToIntConverter - Para contornar bug do mysql entity framework que nao entende tinyint
    public class BoolToIntConverter : ValueConverter<bool, int> {
        public BoolToIntConverter(ConverterMappingHints mappingHints = null) : base(
            v => Convert.ToInt32(v),
            v => Convert.ToBoolean(v),
            mappingHints) {
        }

        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
    }
    #endregion
}