using BankTradingService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserDataModel>
    {
        public void Configure(EntityTypeBuilder<UserDataModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("user_id");
            builder.Property(x => x.FullName).HasColumnName("full_name");
            builder.Property(x => x.FullAddress).HasColumnName("full_address");
            builder.Property(x => x.Email).HasColumnName("email");

            builder.HasMany(x => x.Trades).WithOne(x => x.User).HasForeignKey(x => x.UserID);
            builder.ToTable("user");
        }
    }
}
