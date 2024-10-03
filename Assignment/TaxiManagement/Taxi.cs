using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaxiManagement
{
    public class Taxi
    {
        public const string ON_ROAD = "on the road";
        public const string IN_RANK = "in rank";

        public int Number { get; }
        public double CurrentFare { get; private set; }
        public Rank CurrentRank { get; set; } // Add property for tracking current rank
        public string CurrentLocation { get; set; }
        public string Destination { get; private set; }
        public string Location { get; set; }
        public double TotalMoneyPaid { get; private set; }
        private Rank _rank;

        public Taxi(int number)
        {
            Number = number;
            CurrentFare = 0;
            CurrentRank = null;
            Destination = "";
            TotalMoneyPaid = 0;
            Location = ON_ROAD; // Location initialized to ON_ROAD by default
            Rank = null;
        }



        public Rank Rank
        {
            get { return _rank; }
            set
            {
                // Check if the new rank is not null
                if (value != null)
                {
                    // Check if the destination is not empty
                    if (!string.IsNullOrEmpty(Destination))
                    {
                        throw new Exception("Destination must be empty before setting the rank.");
                    }
                    else
                    {
                        // If destination is empty, update location to IN_RANK
                        Location = IN_RANK;
                    }
                }
                else
                {
                    // If new rank is null, update location to ON_ROAD
                    Location = ON_ROAD;
                }

                // Set the rank to the new value
                _rank = value;
            }
        }




        public void AddFare(string destination, double agreedPrice)
        {
            if (!string.IsNullOrEmpty(Destination))
            {
                throw new Exception("Cannot add fare when destination is not empty.");
            }

            Destination = destination;
            CurrentFare += agreedPrice;
            Location = ON_ROAD;
            Rank = null; // Set Rank to null after adding fare
        }

        public void DropFare(bool priceWasPaid)
        {
            Destination = "";
            if (priceWasPaid)
            {
                TotalMoneyPaid += CurrentFare;
            }
            CurrentFare = 0;
        }
    }
}
