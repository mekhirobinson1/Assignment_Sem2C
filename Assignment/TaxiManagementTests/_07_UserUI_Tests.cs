using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _07_UserUI_Tests
    {
        [TestMethod]
        public void _01_When_TaxiJoinsRankAndTaxiExistsAndRankExists_Expect_TaxiIsAddedToRank()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);
            Taxi t = txm.CreateTaxi(14);

            ui.TaxiJoinsRank(14, 1);
            Assert.AreEqual(14, rm.FindRank(1).FrontTaxiTakesFare("Nowhere", 1.23).Number);
        }

        [TestMethod]
        public void _02_When_TaxiJoinsRankAndTaxiNumberNotAlreadyInUse_Expect_TaxiIsCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            Assert.IsNotNull(txm.FindTaxi(12));
        }

        [TestMethod]
        public void _03_When_TaxiJoinsRank_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 17 has joined rank 3.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiJoinsRank(17, 3));
        }

        [TestMethod]
        public void _04_When_TaxiJoinsRankAndTaxiDoesNotJoinRank_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);
            Taxi t = txm.CreateTaxi(19);
            rm.AddTaxiToRank(t, 3);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 19 has not joined rank 1.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiJoinsRank(19, 1));
        }

        [TestMethod]
        public void _05_When_TaxiJoinsRank_Expect_JoinTransactionIsCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);

            Assert.AreEqual("JoinTransaction", trm.Transactions[0].GetType().Name);
        }

        [TestMethod]
        public void _06_When_TaxiJoinsRankAndTaxiDoesNotJoinRank_Expect_JoinTransactionIsNotCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(16, 1);
            ui.TaxiJoinsRank(16, 2);

            Assert.AreEqual(1, trm.Transactions.Count);
        }

        [TestMethod]
        public void _07_When_TaxiLeavesRank_Expect_LeaveTransactionIsCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            Assert.AreEqual("LeaveTransaction", trm.Transactions[1].GetType().Name);
        }

        [TestMethod]
        public void _08_When_TaxiLeavesRankAndTaxiDoesNotLeaveRank_Expect_LeaveTransactionIsNotCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiLeavesRank(1, "Somewhere else", 3.45);

            Assert.AreEqual(2, trm.Transactions.Count);
        }

        [TestMethod]
        public void _09_When_TaxiLeavesRank_Expected_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 11 has left rank 1 to take a fare to Somewhere for £1.23.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiLeavesRank(1, "Somewhere", 1.23));
        }

        [TestMethod]
        public void _10_When_TaxiLeavesRankAndTaxiDoesNotLeaveRank_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi has not left rank 1.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiLeavesRank(1, "Somewhere", 1.23));
        }

        [TestMethod]
        public void _11_When_TaxiDropsFareAndTaxiDoesNotDropFare_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 11 has not dropped its fare.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiDropsFare(11, false));
        }

        [TestMethod]
        public void _12_When_TaxiDropsFareAndPricePaidIsTrue_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 11 has dropped its fare and the price was paid.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiDropsFare(11, true));
        }

        [TestMethod]
        public void _13_When_TaxiDropsFareAndPricePaidIsFalse_Expect_CorrectMessageIsReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi 11 has dropped its fare and the price was not paid.");

            CollectionAssert.AreEqual(
                expectedLines,
                ui.TaxiDropsFare(11, false));
        }

        [TestMethod]
        public void _14_When_TaxiDropsFare_Expect_DropTransactionIsCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(11, true);

            Assert.AreEqual("DropTransaction", trm.Transactions[2].GetType().Name);
        }

        [TestMethod]
        public void _15_When_TaxiDropsFareAndTaxiDoesNotDropFare_Expect_DropTransactionIsNotCreated()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(11, 1);
            ui.TaxiDropsFare(11, true);

            Assert.AreEqual(1, trm.Transactions.Count);
        }

        [TestMethod]
        public void _16_When_ViewTaxiLocationsAndThereAreNoTaxis_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");
            expectedLines.Add("No taxis");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTaxiLocations())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTaxiLocations());
        }

        [TestMethod]
        public void _17_When_ViewTaxiLocationsAndThereIsOneTaxiInRank_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");
            expectedLines.Add("Taxi 12 is in rank 1");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTaxiLocations())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTaxiLocations());
        }

        [TestMethod]
        public void _18_When_ViewTaxiLocationsAndThereIsOneTaxiOnRoadToDestination_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");
            expectedLines.Add("Taxi 12 is on the road to Somewhere");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTaxiLocations())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTaxiLocations());
        }

        [TestMethod]
        public void _19_When_ViewTaxiLocationsAndThereIsOneTaxiOnRoadAfterDroppingFare_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, true);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");
            expectedLines.Add("Taxi 12 is on the road");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTaxiLocations())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTaxiLocations());
        }

        [TestMethod]
        public void _20_When_ViewTaxiLocationsAndThereAreThreeTaxis_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiJoinsRank(13, 1);
            ui.TaxiJoinsRank(14, 2);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");
            expectedLines.Add("Taxi 12 is on the road to Somewhere");
            expectedLines.Add("Taxi 13 is in rank 1");
            expectedLines.Add("Taxi 14 is in rank 2");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTaxiLocations())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTaxiLocations());
        }

        [TestMethod]
        public void _21_When_ViewFinancialReportAndThereAreNoTaxis_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            expectedLines.Add("No taxis, so no money taken");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewFinancialReport())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewFinancialReport());
        }

        [TestMethod]
        public void _22_When_ViewFinancialReportAndThereIsOnlyOneTaxiAndItHasTakenNoMoney_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            expectedLines.Add("Taxi 12      0.00");
            expectedLines.Add("           ======");
            expectedLines.Add("Total:       0.00");
            expectedLines.Add("           ======");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewFinancialReport())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewFinancialReport());
        }

        [TestMethod]
        public void _23_When_ViewFinancialReportAndThereIsOnlyOneTaxiAndItHasTakenMoneyFromOneFare_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, true);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            expectedLines.Add("Taxi 12      1.23");
            expectedLines.Add("           ======");
            expectedLines.Add("Total:       1.23");
            expectedLines.Add("           ======");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewFinancialReport())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewFinancialReport());
        }

        [TestMethod]
        public void _24_When_ViewFinancialReportAndThereIsOneTaxiAndItHasTakenMoneyFromTwoFares_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, true);
            ui.TaxiJoinsRank(12, 2);
            ui.TaxiLeavesRank(2, "Somewhere else", 2.34);
            ui.TaxiDropsFare(12, true);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            expectedLines.Add("Taxi 12      3.57");
            expectedLines.Add("           ======");
            expectedLines.Add("Total:       3.57");
            expectedLines.Add("           ======");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewFinancialReport())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewFinancialReport());
        }

        [TestMethod]
        public void _25_When_ViewFinancialReportAndThereAreTwoTaxisWhichHaveEachTakenMoneyFromTwoFares_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, true);
            ui.TaxiJoinsRank(12, 2);
            ui.TaxiLeavesRank(2, "Somewhere else", 2.34);
            ui.TaxiDropsFare(12, true);

            ui.TaxiJoinsRank(13, 1);
            ui.TaxiLeavesRank(1, "Here", 1.23);
            ui.TaxiDropsFare(13, true);
            ui.TaxiJoinsRank(13, 2);
            ui.TaxiLeavesRank(2, "There", 2.34);
            ui.TaxiDropsFare(13, true);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            expectedLines.Add("Taxi 12      3.57");
            expectedLines.Add("Taxi 13      3.57");
            expectedLines.Add("           ======");
            expectedLines.Add("Total:       7.14");
            expectedLines.Add("           ======");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewFinancialReport())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewFinancialReport());
        }

        [TestMethod]
        public void _26_When_ViewTransactionLogAndThereAreNoTransactions_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");
            expectedLines.Add("No transactions");

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTransactionLog())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTransactionLog());
        }

        [TestMethod]
        public void _27_When_ViewTransactionLogAndThereIsOnlyOneTransactionAndItIsAJoinTransaction_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");
            expectedLines.Add(
                string.Format(
                    "{0} Join      - Taxi 12 in rank 1",
                    trm.Transactions[0].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTransactionLog())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTransactionLog());
        }

        [TestMethod]
        public void _28_When_ViewTransactionLogAndThereIsOneJoinAndOneLeaveTransaction_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");
            expectedLines.Add(
                string.Format(
                    "{0} Join      - Taxi 12 in rank 1",
                    trm.Transactions[0].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));
            expectedLines.Add(
                string.Format(
                    "{0} Leave     - Taxi 12 from rank 1 to Somewhere for £1.23",
                    trm.Transactions[1].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTransactionLog())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTransactionLog());
        }

        [TestMethod]
        public void _29_When_ViewTransactionLogAndThereIsOneJoinOneLeaveAndOneDropTransactionWithPricePaid_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, true);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");
            expectedLines.Add(
                string.Format(
                    "{0} Join      - Taxi 12 in rank 1",
                    trm.Transactions[0].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));
            expectedLines.Add(
                string.Format(
                    "{0} Leave     - Taxi 12 from rank 1 to Somewhere for £1.23",
                    trm.Transactions[1].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));
            expectedLines.Add(
                string.Format(
                    "{0} Drop fare - Taxi 12, price was paid",
                    trm.Transactions[2].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTransactionLog())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTransactionLog());
        }

        [TestMethod]
        public void _30_When_ViewTransactionLogAndThereIsOneJoinOneLeaveAndOneDropTransactionWithPriceNotPaid_Expect_CorrectStringsAreReturned()
        {
            RankManager rm = new RankManager();
            TaxiManager txm = new TaxiManager();
            TransactionManager trm = new TransactionManager();
            UserUI ui = new UserUI(rm, txm, trm);

            ui.TaxiJoinsRank(12, 1);
            ui.TaxiLeavesRank(1, "Somewhere", 1.23);
            ui.TaxiDropsFare(12, false);

            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");
            expectedLines.Add(
                string.Format(
                    "{0} Join      - Taxi 12 in rank 1",
                    trm.Transactions[0].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));
            expectedLines.Add(
                string.Format(
                    "{0} Leave     - Taxi 12 from rank 1 to Somewhere for £1.23",
                    trm.Transactions[1].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));
            expectedLines.Add(
                string.Format(
                    "{0} Drop fare - Taxi 12, price was not paid",
                    trm.Transactions[2].TransactionDatetime.ToString("dd/MM/yyyy HH:mm")));

            foreach (string s in expectedLines)
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            foreach (string s in ui.ViewTransactionLog())
            {
                Console.WriteLine("'" + s + "' (" + s.Length + ")\n");
            }

            CollectionAssert.AreEqual(
                expectedLines,
                ui.ViewTransactionLog());
        }
    }
}
