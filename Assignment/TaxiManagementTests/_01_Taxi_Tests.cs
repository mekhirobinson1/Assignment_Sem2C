using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _01_Taxi_Tests
    {
        /*
         * Uncomment from line 21
         */

        [TestMethod]
        public void _01_When_NewTaxi_Expect_NumberIsSet()
        {
            Taxi t = new Taxi(1);
            Assert.AreEqual(1, t.Number);
        }

        [TestMethod]
        public void _02_When_NewTaxi_Expect_CurrentFareIsZero()
        {
            Taxi t = new Taxi(1);
            Assert.AreEqual(0, t.CurrentFare);
        }

        [TestMethod]
        public void _03_When_NewTaxi_Expect_DestinationIsEmpty()
        {
            Taxi t = new Taxi(1);
            Assert.AreEqual(0, t.Destination.Length);
        }

        [TestMethod]
        public void _04_When_NewTaxi_Expect_LocationIsOnRoad()
        {
            Taxi t = new Taxi(1);
            Assert.AreEqual("on the road", t.Location);
        }

        [TestMethod]
        public void _05_When_NewTaxi_Expect_TotalMoneyPaidIsZero()
        {
            Taxi t = new Taxi(1);
            Assert.AreEqual(0, t.TotalMoneyPaid);
        }

        [TestMethod]
        public void _06_When_AddFare_Expect_AgreedPriceIsAddedToCurrentFare()
        {
            Taxi t = new Taxi(1);
            t.AddFare("", 1.23);
            Assert.AreEqual(1.23, t.CurrentFare);
        }

        [TestMethod]
        public void _07_When_AddFare_Expect_DestinationIsSet()
        {
            Taxi t = new Taxi(1);
            t.AddFare("New destination", 0);
            Assert.AreEqual("New destination", t.Destination);
        }

        [TestMethod]
        public void _08_When_AddFare_Expect_LocationIsChangedToOnRoad()
        {
            Taxi t = new Taxi(1);
            t.AddFare("", 0);
            Assert.AreEqual("on the road", t.Location);
        }

        [TestMethod]
        public void _09_When_DropFare_Expect_DestinationIsCleared()
        {
            Taxi t = new Taxi(1);
            t.AddFare("New destination", 0);
            t.DropFare(true);
            Assert.AreEqual("", t.Destination);
        }

        [TestMethod]
        public void _10_When_DropFare_Expect_CurrentFareSetToZero()
        {
            Taxi t = new Taxi(1);
            t.AddFare("", 1.23);
            t.DropFare(true);
            Assert.AreEqual(0, t.CurrentFare);
        }

        [TestMethod]
        public void _11_When_DropFare_Expect_LocationDoesNotChange()
        {
            Taxi t = new Taxi(1);
            t.AddFare("Here and there", 1.23);
            t.DropFare(true);
            Assert.AreEqual(Taxi.ON_ROAD, t.Location);
        }

        [TestMethod]
        public void _12_When_DropFareAndPriceWasPaidIsTrue_Expect_CurrentFareIsAddedToTotalMoneyTaken()
        {
            Taxi t = new Taxi(1);
            t.AddFare("", 1.23);
            t.DropFare(true);
            Assert.AreEqual(1.23, t.TotalMoneyPaid);
        }

        [TestMethod]
        public void _13_When_DropFareAndPriceWasPaidIsFalse_Expect_TotalMoneyTakenDoesNotChange()
        {
            Taxi t = new Taxi(1);
            t.AddFare("", 1.23);
            t.DropFare(false);
            Assert.AreEqual(0, t.TotalMoneyPaid);
        }

        [TestMethod]
        public void _14_When_NewTaxi_Expect_RankIsNull()
        {
            Taxi t = new Taxi(1);
            Assert.IsNull(t.Rank);
        }

        [TestMethod]
        public void _15_When_RankIsSetToNull_Expect_ThrowsException()
        {
            Taxi t = new Taxi(1);
            Assert.ThrowsException<Exception>(() => t.Rank = null);
        }

        [TestMethod]
        public void _16_When_RankIsSetToNull_Expect_CorrectExceptionMessage()
        {
            Taxi t = new Taxi(1);
            try
            {
                t.Rank = null;
            }
            catch (Exception e)
            {
                Assert.AreEqual("Rank cannot be null", e.Message);
            }
        }

        [TestMethod]
        public void _17_When_RankIsSetAndDestinationIsNotEmpty_Expect_ThrowsException()
        {
            Taxi t = new Taxi(1);
            t.AddFare("Somewhere", 0);
            Rank r = new Rank(1, 1);
            Assert.ThrowsException<Exception>(() => t.Rank = r);
        }

        [TestMethod]
        public void _18_When_RankIsSetAndDestinationIsNotEmpty_Expect_CorrectExceptionMessage()
        {
            Taxi t = new Taxi(1);
            t.AddFare("Somewhere", 0);
            Rank r = new Rank(1, 1);
            try
            {
                t.Rank = r;
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot join rank if fare has not been dropped", e.Message);
            }
        }

        [TestMethod]
        public void _19_When_RankIsSetAndDestinationIsEmpty_Expect_RankIsSet()
        {
            Taxi t = new Taxi(1);
            Rank r = new Rank(1, 1);
            t.Rank = r;
            Assert.AreEqual(r, t.Rank);
        }

        [TestMethod]
        public void _20_When_RankIsSetAndDestinationIsEmpty_Expect_LocationIsChangedToInRank()
        {
            Taxi t = new Taxi(1);
            Rank r = new Rank(1, 1);
            t.Rank = r;
            Assert.AreEqual(Taxi.IN_RANK, t.Location);
        }

        [TestMethod]
        public void _21_When_AddFare_Expect_RankIsSetToNull()
        {
            Taxi t = new Taxi(1);
            Rank r = new Rank(1, 1);
            t.Rank = r;
            t.AddFare("", 0);
            Assert.IsNull(t.Rank);
        }
    }
}
