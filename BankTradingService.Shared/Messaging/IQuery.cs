using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Shared.Messaging
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
