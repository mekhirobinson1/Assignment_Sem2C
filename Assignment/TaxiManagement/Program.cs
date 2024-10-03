using System;
using System.Collections.Generic;

namespace TaxiManagement
{
    class Program
    {
        static RankManager rankManager = new RankManager();
        static TaxiManager taxiManager = new TaxiManager();
        static TransactionManager transactionManager = new TransactionManager();
        static UserUI userInterface = new UserUI(rankManager, taxiManager, transactionManager);

        static void Main(string[] args)
        {
            InitializeTaxisAndLocations();

            while (true)
            {
                Console.WriteLine("Taxi Management System");
                Console.WriteLine("=====================");
                Console.WriteLine("1. View Taxi Locations");
                Console.WriteLine("2. View Financial Report");
                Console.WriteLine("3. Take a Taxi Journey");
                Console.WriteLine("4. View Transaction Log");
                Console.WriteLine("5. Exit");
                Console.WriteLine();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayResults(userInterface.ViewTaxiLocations());
                        break;
                    case "2":
                        DisplayResults(userInterface.ViewFinancialReport());
                        break;
                    case "3":
                        TakeTaxiJourney();
                        break;
                    case "4":
                        DisplayResults(userInterface.ViewTransactionLog());
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void DisplayResults(List<string> results)
        {
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }

        static void InitializeTaxisAndLocations()
        {
            // Add some taxis to ranks
            userInterface.TaxiJoinsRank(1, 1);
            userInterface.TaxiJoinsRank(2, 1);
            userInterface.TaxiJoinsRank(3, 2);
            userInterface.TaxiJoinsRank(4, 3);

            // Add some locations
            rankManager.FindRank(1).AddTaxi(taxiManager.CreateTaxi(5));
            rankManager.FindRank(2).AddTaxi(taxiManager.CreateTaxi(6));
            rankManager.FindRank(3).AddTaxi(taxiManager.CreateTaxi(7));
        }

        static void TakeTaxiJourney()
        {
            Console.WriteLine("Take a Taxi Journey");
            Console.WriteLine("===================");

            Console.WriteLine("Choose a rank:");
            Console.WriteLine("1. Rank 1");
            Console.WriteLine("2. Rank 2");
            Console.WriteLine("3. Rank 3");

            Console.Write("Enter your choice: ");
            string rankChoice = Console.ReadLine();

            int rankId;
            if (!int.TryParse(rankChoice, out rankId) || rankId < 1 || rankId > 3)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return;
            }

            Console.Write("Enter your destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter the agreed price: ");
            string priceInput = Console.ReadLine();
            double agreedPrice;
            if (!double.TryParse(priceInput, out agreedPrice) || agreedPrice <= 0)
            {
                Console.WriteLine("Invalid price. Please enter a positive number.");
                return;
            }

            List<string> result = userInterface.TaxiLeavesRank(rankId, destination, agreedPrice);
            DisplayResults(result);
        }
    }
}
