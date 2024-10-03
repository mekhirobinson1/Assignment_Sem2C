using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public class TaxiManager
    {
        private SortedDictionary<int, Taxi> taxis;

        public TaxiManager()
        {
            taxis = new SortedDictionary<int, Taxi>();
        }

        public Taxi CreateTaxi(int taxiNum)
        {
            if (taxis.ContainsKey(taxiNum))
                return taxis[taxiNum];

            Taxi taxi = new Taxi(taxiNum);
            taxis.Add(taxiNum, taxi);
            return taxi;
        }

        public Taxi FindTaxi(int taxiNum)
        {
            return taxis.ContainsKey(taxiNum) ? taxis[taxiNum] : null;
        }

        public SortedDictionary<int, Taxi> GetAllTaxis()
        {
            return taxis;
        }

        public void AddTaxi(Taxi taxi)
        {
            if (taxi == null)
            {
                throw new ArgumentNullException(nameof(taxi), "Taxi cannot be null.");
            }

            // Check if the taxi already exists
            if (taxis.ContainsKey(taxi.Number))
            {
                throw new ArgumentException($"Taxi with number {taxi.Number} already exists.", nameof(taxi));
            }

            // Add the taxi to the dictionary
            taxis.Add(taxi.Number, taxi);
        }
    }
}