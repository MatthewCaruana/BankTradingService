﻿using BankTradingService.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Application.DTOs
{
    public class NewTradeActivityDTO
    {
        public int UserID { get; set; }
        public string Symbol { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal OpenPrice { get; set; }
    }
}
