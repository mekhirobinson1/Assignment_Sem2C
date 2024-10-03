using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public class Rank
    {
        public int Id { get; }
        public int Capacity { get; }
        public List<Taxi> Taxis { get; }
        private List<Taxi> taxis;

        public Rank()
        {
            taxis = new List<Taxi>();
        }

        // Method to check if a taxi is already present in the rank
        public bool Contains(Taxi taxi)
        {
            return taxis.Contains(taxi);
        }

        public Rank(int id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            Taxis = new List<Taxi>();
        }

        public bool AddTaxi(Taxi taxi)
        {
            if (Taxis.Count >= Capacity)
                return false;

            Taxis.Add(taxi);
            taxi.Rank = this;
            taxi.Location = Taxi.IN_RANK;
            return true;
        }

        public Taxi FrontTaxiTakesFare(string destination, double agreedPrice)
        {
            if (Taxis.Count == 0)
                return null;

            Taxi frontTaxi = Taxis[0];
            frontTaxi.AddFare(destination, agreedPrice);
            Taxis.RemoveAt(0);
            return frontTaxi;
        }
        public void AddTaxiToBack(Taxi taxi)
        {
            if (Taxis.Count >= Capacity)
                throw new InvalidOperationException("Rank is full.");

            Taxis.Add(taxi);
            taxi.Rank = this;
            taxi.Location = Taxi.IN_RANK;
        }

    }
}