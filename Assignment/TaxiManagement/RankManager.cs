using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagement
{
    public class RankManager
    {
        private Dictionary<int, Rank> ranks;

        public RankManager()
        {
            ranks = new Dictionary<int, Rank>();
            InitializeRanks();
        }

        private void InitializeRanks()
        {
            // Initialize ranks as per your requirements
            ranks.Add(1, new Rank(1, 5));  // Change the capacity to 5
            ranks.Add(2, new Rank(2, 2));
            ranks.Add(3, new Rank(3, 4));
            // Add more ranks if needed
        }


        public Rank FindRank(int rankId)
        {
            return ranks.ContainsKey(rankId) ? ranks[rankId] : null;
        }

        public bool AddTaxiToRank(Taxi taxi, int rankId)
        {
            if (!ranks.ContainsKey(rankId))
                return false;

            // Check if the taxi is already present in any other rank
            foreach (var kvp in ranks)
            {
                if (kvp.Value.Taxis.Contains(taxi))
                    return false;
            }

            Rank rank = ranks[rankId];
            if (taxi.Destination != "" || rank.Taxis.Contains(taxi)) // Check if taxi already in rank
                return false;

            bool added = rank.AddTaxi(taxi);
            if (added)
            {
                // Update the taxi's CurrentRank property
                taxi.CurrentRank = rank;
            }

            return added;
        }


        public Taxi FrontTaxiInRankTakesFare(int rankId, string destination, double agreedPrice)
        {
            if (!ranks.ContainsKey(rankId))
                return null;

            Rank rank = ranks[rankId];
            return rank.FrontTaxiTakesFare(destination, agreedPrice);
        }
    }
}