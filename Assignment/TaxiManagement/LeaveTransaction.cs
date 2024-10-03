using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaxiManagement
{
    public class LeaveTransaction : Transaction
    {
        public int RankId { get; }
        public int TaxiNum { get; }
        public string Destination { get; }
        public double AgreedPrice { get; }

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi taxi)
    : base("Leave", transactionDatetime)
        {
            RankId = rankId;
            TaxiNum = taxi.Number;
            Destination = "";
            AgreedPrice = 0;
        }

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi taxi, string destination, double agreedPrice)
            : base("Leave", transactionDatetime)
        {
            RankId = rankId;
            TaxiNum = taxi.Number;
            Destination = destination;
            AgreedPrice = agreedPrice;
        }




        public override string ToString()
        {
            string fareStr = string.Format("£{0:0.00}", AgreedPrice);
            return $"{TransactionDatetime.ToString("dd/MM/yyyy HH:mm")} Leave     - Taxi {TaxiNum} from rank {RankId} to {Destination} for {fareStr}";
        }


    }
}
