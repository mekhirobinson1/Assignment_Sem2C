using System;

namespace TaxiManagement
{
    public class DropTransaction : Transaction
    {
        public int TaxiNum { get; }
        public bool PricePaid { get; } // Added property for indicating if the price was paid

        public DropTransaction(DateTime transactionDatetime, int taxiNum, bool pricePaid)
            : base("Drop fare", transactionDatetime)
        {
            TaxiNum = taxiNum;
            PricePaid = pricePaid;
        }

        public override string ToString()
        {
            string pricePaidStr = PricePaid ? "price was paid" : "price was not paid"; // Determine if the price was paid or not
            return $"{TransactionDatetime.ToString("dd/MM/yyyy HH:mm")} Drop fare - Taxi {TaxiNum}, {pricePaidStr}"; // Include indication of whether the price was paid
        }
    }
}
