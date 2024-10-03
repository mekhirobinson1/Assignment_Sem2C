using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public class JoinTransaction : Transaction
    {
        public int TaxiNum { get; }
        public int RankId { get; }

        public JoinTransaction(DateTime transactionDatetime, int taxiNum, int rankId)
            : base("Join", transactionDatetime)
        {
            TaxiNum = taxiNum;
            RankId = rankId;
        }

        public override string ToString()
        {
            return $"{TransactionDatetime.ToString("dd/MM/yyyy HH:mm")} Join      - Taxi {TaxiNum} in rank {RankId}";
        }

    }
}
