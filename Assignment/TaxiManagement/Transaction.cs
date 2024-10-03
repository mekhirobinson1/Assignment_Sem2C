using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public abstract class Transaction
    {
        public DateTime TransactionDatetime { get; }
        public string TransactionType { get; }

        public Transaction(string type, DateTime dt)
        {
            TransactionType = type;
            TransactionDatetime = dt;
        }

        public abstract override string ToString();
    }
}