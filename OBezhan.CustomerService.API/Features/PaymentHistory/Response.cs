using System;
using System.Collections.Generic;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Response
    {
        public Response(long id, string name, string email, string mobile, IEnumerable<ResponseTransaction> transactions)
        {
            Id = id;
            Name = name;
            Email = email;
            Mobile = mobile;
            Transactions = transactions;
        }

        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Mobile { get; }
        public IEnumerable<ResponseTransaction> Transactions { get; }

    }

    public class ResponseTransaction
    {
        public ResponseTransaction(long id, DateTime dateTimeUtc, decimal amount, string currency, string status)
        {
            Id = id;
            DateTimeUtc = dateTimeUtc;
            Amount = amount;
            Currency = currency;
            Status = status;
        }

        public long Id { get; }
        public DateTime DateTimeUtc { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Status { get; }
    }
}
