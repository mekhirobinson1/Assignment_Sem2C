using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public class TransactionManager
    {
        private List<Transaction> transactions;

        public TransactionManager()
        {
            transactions = new List<Transaction>();
        }

        public IReadOnlyList<Transaction> Transactions => transactions;

        public void RecordDrop(int taxiNum, bool pricePaid)
        {
            DateTime now = DateTime.Now;
            DropTransaction dropTransaction = new DropTransaction(now, taxiNum, pricePaid);
            transactions.Add(dropTransaction);
        }


        public void RecordJoin(int taxiNum, int rankId)
        {
            DateTime now = DateTime.Now;
            JoinTransaction joinTransaction = new JoinTransaction(now, taxiNum, rankId);
            transactions.Add(joinTransaction);
        }

        public void RecordLeave(int rankId, Taxi taxi)
        {
            DateTime now = DateTime.Now;
            LeaveTransaction leaveTransaction = new LeaveTransaction(now, rankId, taxi, taxi.Destination, taxi.CurrentFare);

            // Update the taxi's current location
            taxi.CurrentLocation = $"on the road to {taxi.Destination}";

            // Reset the taxi's CurrentRank property
            taxi.CurrentRank = null;

            transactions.Add(leaveTransaction);
        }

    }
}