using System;
using System.Collections.Generic;

namespace TaxiManagement
{
    public class UserUI
    {
        private RankManager rankMgr;
        private TaxiManager taxiMgr;
        private TransactionManager transactionMgr;

        public UserUI(RankManager rkMgr, TaxiManager txMgr, TransactionManager trMgr)
        {
            // Ensure all managers are properly initialized
            if (rkMgr == null || txMgr == null || trMgr == null)
            {
                throw new ArgumentNullException("Managers cannot be null.");
            }

            rankMgr = rkMgr;
            taxiMgr = txMgr;
            transactionMgr = trMgr;
        }

        public List<string> TaxiJoinsRank(int taxiNum, int rankId)
        {
            // Ensure that managers are properly initialized
            if (rankMgr == null || taxiMgr == null || transactionMgr == null)
            {
                throw new InvalidOperationException("Managers are not properly initialized.");
            }

            // Ensure that RankManager is initialized
            if (rankMgr == null)
            {
                throw new InvalidOperationException("RankManager is not properly initialized.");
            }

            Taxi taxi = taxiMgr.FindTaxi(taxiNum);
            if (taxi == null)
            {
                // Create a new taxi if it doesn't exist
                taxi = new Taxi(taxiNum);
                taxiMgr.AddTaxi(taxi);
            }

            // Add the taxi to the specified rank
            bool added = rankMgr.AddTaxiToRank(taxiMgr.FindTaxi(taxiNum), rankId);

            List<string> result = new List<string>();
            if (added)
            {
                // Record the join transaction
                transactionMgr.RecordJoin(taxiNum, rankId);
                result.Add($"Taxi {taxiNum} has joined rank {rankId}.");
            }
            else
            {
                result.Add($"Taxi {taxiNum} has not joined rank {rankId}.");
            }
            return result;
        }

        public List<string> TaxiLeavesRank(int rankId, string destination, double agreedPrice)
        {
            // Ensure that RankManager is initialized
            if (rankMgr == null)
            {
                throw new InvalidOperationException("RankManager is not properly initialized.");
            }

            // Get the front taxi in the rank and record leave transaction
            Taxi taxi = rankMgr.FrontTaxiInRankTakesFare(rankId, destination, agreedPrice);

            List<string> result = new List<string>();
            if (taxi != null)
            {
                // Record the leave transaction
                transactionMgr.RecordLeave(rankId, taxi);

                // Add the taxi to the back of the same rank
                rankMgr.AddTaxiToRank(taxi, rankId);

                result.Add($"Taxi {taxi.Number} left Rank {rankId} to {destination} with agreed price of {agreedPrice}");

                // Update the taxi's current location and reset the rank if it's not the final destination
                if (!string.IsNullOrEmpty(destination))
                {
                    taxi.CurrentLocation = $"on the road to {destination}";
                    taxi.CurrentRank = null;
                }
            }
            else
            {
                result.Add($"Taxi has not left rank {rankId}.");
            }
            return result;
        }








        public List<string> TaxiDropsFare(int taxiNum, bool pricePaid)
        {
            // Ensure that TaxiManager and TransactionManager are initialized
            if (taxiMgr == null || transactionMgr == null)
            {
                throw new InvalidOperationException("TaxiManager and TransactionManager are not properly initialized.");
            }

            Taxi taxi = taxiMgr.FindTaxi(taxiNum);
            if (taxi == null)
            {
                throw new ArgumentException($"Taxi with number {taxiNum} does not exist.", nameof(taxiNum));
            }

            // Record the drop fare transaction only if the taxi actually drops its fare
            if (taxi.CurrentFare > 0)
            {
                transactionMgr.RecordDrop(taxiNum, pricePaid);
            }

            if (taxi.CurrentFare > 0 && pricePaid)
            {
                return new List<string> { $"Taxi {taxiNum} has dropped its fare and the price was paid." };
            }
            else if (taxi.CurrentFare > 0 && !pricePaid)
            {
                return new List<string> { $"Taxi {taxiNum} has dropped its fare and the price was not paid." };
            }
            else
            {
                return new List<string> { $"Taxi {taxiNum} has not dropped its fare." };
            }
        }



        public List<string> ViewFinancialReport()
        {
            List<string> result = new List<string>();

            // Get all transactions
            IReadOnlyList<Transaction> allTransactions = transactionMgr.Transactions;

            // Initialize total money earned and paid
            double totalEarned = 0;
            double totalPaid = 0;

            // Calculate total money earned and paid
            foreach (var transaction in allTransactions)
            {
                if (transaction is DropTransaction dropTransaction)
                {
                    Taxi taxi = taxiMgr.FindTaxi(dropTransaction.TaxiNum);
                    if (taxi != null)
                    {
                        // If price was paid, consider the fare as earned
                        if (dropTransaction.PricePaid)
                        {
                            totalEarned += taxi.CurrentFare;
                        }
                        // Otherwise, consider it unpaid
                        else
                        {
                            totalPaid += taxi.CurrentFare;
                        }
                    }
                }
                // Join and leave transactions don't involve money
            }

            // Generate financial report
            result.Add("Financial report");
            result.Add("================");
            if (allTransactions.Count == 0)
            {
                result.Add("No transactions, so no money earned or paid");
            }
            else
            {
                result.Add($"Total money earned: ${totalEarned}");
                result.Add($"Total money paid: ${totalPaid}");
            }

            return result;
        }



        public List<string> ViewTaxiLocations()
        {
            List<string> result = new List<string>();
            result.Add("Taxi locations");
            result.Add("==============");
            // Get all taxis
            SortedDictionary<int, Taxi> allTaxis = taxiMgr.GetAllTaxis();
            if (allTaxis.Count == 0)
            {
                result.Add("No taxis");
            }
            else
            {
                // Add taxi locations
                foreach (var taxiEntry in allTaxis)
                {
                    Taxi taxi = taxiEntry.Value;
                    if (!string.IsNullOrEmpty(taxi.CurrentLocation))
                    {
                        result.Add($"Taxi {taxi.Number} is on the road");
                    }
                    else if (taxi.CurrentRank != null)
                    {
                        result.Add($"Taxi {taxi.Number} is in rank {taxi.CurrentRank.Id}");
                    }
                    else
                    {
                        result.Add($"Taxi {taxi.Number} is at an unspecified location");
                    }
                }
            }

    return result;
        }





        public List<string> ViewTransactionLog()
        {
            List<string> result = new List<string>();

            result.Add("Transaction report");
            result.Add("==================");

            if (transactionMgr.Transactions.Count == 0)
            {
                result.Add("No transactions");
            }
            else
            {
                foreach (var transaction in transactionMgr.Transactions)
                {
                    result.Add(transaction.ToString());
                }
            }

            return result;
        }

    }
}
