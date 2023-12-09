using BankTradingService.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Mappings
{
    public class TradeMap : IEntityTypeConfiguration<TradeDataModel>
    {
        public void Configure(EntityTypeBuilder<TradeDataModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("trade_id");
            builder.Property(x => x.UserID).HasColumnName("user_ref");
            builder.Property(x => x.TransactionType).HasColumnName("transaction_type");
            builder.Property(x => x.Symbol).HasColumnName("symbol");
            builder.Property(x => x.Amount).HasColumnName("amount");
            builder.Property(x => x.OpenPrice).HasColumnName("open_price");
            builder.Property(x => x.OpenTimestamp).HasColumnName("open_timestamp");
            builder.Property(x => x.ClosePrice).HasColumnName("close_price");
            builder.Property(x => x.CloseTimestamp).HasColumnName("close_timestamp");

            builder.HasOne(x => x.User).WithMany(x => x.Trades).HasForeignKey(x => x.UserID);
            builder.ToTable("trade");
        }
    }
}
