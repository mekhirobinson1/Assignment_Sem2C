using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _04_RankManager_Tests
    {

        [TestMethod]
        public void _01_When_NewTaxiManager_Expect_ConstructorCreatesRank1()
        {
            RankManager rm = new RankManager();
            Assert.IsNotNull(rm.FindRank(1));
        }

        [TestMethod]
        public void _02_When_NewTaxiManager_Expect_Rank1CanAcceptFiveTaxis()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(1);
            r.AddTaxi(new Taxi(1));
            r.AddTaxi(new Taxi(2));
            r.AddTaxi(new Taxi(3));
            r.AddTaxi(new Taxi(4));
            Assert.IsTrue(r.AddTaxi(new Taxi(5)));
        }

        [TestMethod]
        public void _03_When_NewTaxiManager_Rank1CannotAcceptSixthTaxis()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(1);
            r.AddTaxi(new Taxi(1));
            r.AddTaxi(new Taxi(2));
            r.AddTaxi(new Taxi(3));
            r.AddTaxi(new Taxi(4));
            r.AddTaxi(new Taxi(5));
            Assert.IsFalse(r.AddTaxi(new Taxi(6)));
        }

        [TestMethod]
        public void _04_When_NewTaxiManager_Expect_ConstructorCreatesRank2()
        {
            RankManager rm = new RankManager();
            Assert.IsNotNull(rm.FindRank(2));
        }

        [TestMethod]
        public void _05_When_NewTaxiManager_Expect_Rank2CanAcceptTwoTaxis()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(2);
            r.AddTaxi(new Taxi(1));
            Assert.IsTrue(r.AddTaxi(new Taxi(2)));
        }

        [TestMethod]
        public void _06_When_NewTaxiManager_Expect_Rank2CannotAcceptThirdTaxi()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(2);
            r.AddTaxi(new Taxi(1));
            r.AddTaxi(new Taxi(2));
            Assert.IsFalse(r.AddTaxi(new Taxi(3)));
        }

        [TestMethod]
        public void _07_When_NewTaxiManager_Expect_ConstructorCreatesRank3()
        {
            RankManager rm = new RankManager();
            Assert.IsNotNull(rm.FindRank(3));
        }

        [TestMethod]
        public void _08_When_NewTaxiManager_Expect_Rank3CanAcceptFourTaxis()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(3);
            r.AddTaxi(new Taxi(1));
            r.AddTaxi(new Taxi(2));
            r.AddTaxi(new Taxi(3));
            Assert.IsTrue(r.AddTaxi(new Taxi(4)));
        }

        [TestMethod]
        public void _09_When_NewTaxiManager_Expect_Rank3CannotAcceptFifthTaxi()
        {
            RankManager rm = new RankManager();
            Rank r = rm.FindRank(3);
            r.AddTaxi(new Taxi(1));
            r.AddTaxi(new Taxi(2));
            r.AddTaxi(new Taxi(3));
            r.AddTaxi(new Taxi(4));
            Assert.IsFalse(r.AddTaxi(new Taxi(5)));
        }

        [TestMethod]
        public void _10_When_FindRankAndRankNotFound_Expect_NullIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.IsNull(rm.FindRank(4));
        }

        [TestMethod]
        public void _11_When_FindRankAndRankFound_Expect_CorrectRankIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.AreEqual(1, rm.FindRank(1).Id);
        }

        [TestMethod]
        public void _12_When_AddTaxiToRankAndTaxiAlreadyInThatRank_Expect_FalseIsReturned()
        {
            RankManager rm = new RankManager();
            Taxi t = new Taxi(1);
            rm.AddTaxiToRank(t, 1);
            Assert.IsFalse(rm.AddTaxiToRank(t, 1));
        }

        [TestMethod]
        public void _13_When_AddTaxiToRankAndTaxiAlreadyInAnotherRank_Expect_FalseIsReturned()
        {
            RankManager rm = new RankManager();
            Taxi t = new Taxi(1);
            rm.AddTaxiToRank(t, 1);
            Assert.IsFalse(rm.AddTaxiToRank(t, 2));
        }

        [TestMethod]
        public void _14_When_AddTaxiToRankAndRankDoesNotExist_Expect_FalseIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.IsFalse(rm.AddTaxiToRank(new Taxi(3), 4));
        }

        [TestMethod]
        public void _15_When_AddTaxiToRankAndTaxiHasDestination_Expect_FalseIsReturned()
        {
            RankManager rm = new RankManager();
            Taxi t = new Taxi(1);
            t.AddFare("Somewhere", 1.23);
            Assert.IsFalse(rm.AddTaxiToRank(t, 1));
        }

        [TestMethod]
        public void _16_When_AddTaxiToRankAndSpaceIsNotAvailable_Expect_FalseIsReturned()
        {
            RankManager rm = new RankManager();
            rm.AddTaxiToRank(new Taxi(1), 2);
            rm.AddTaxiToRank(new Taxi(2), 2);
            Assert.IsFalse(rm.AddTaxiToRank(new Taxi(3), 2));
        }

        [TestMethod]
        public void _17_When_AddTaxiToRankAndSpaceIsAvailable_Expect_TrueIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.IsTrue(rm.AddTaxiToRank(new Taxi(2), 1));
        }

        [TestMethod]
        public void _18_When_FrontTaxiInRankTakesFare_Expect_CorrectTaxiIsReturned()
        {
            RankManager rm = new RankManager();
            Taxi t = new Taxi(2);
            rm.AddTaxiToRank(t, 1);
            rm.AddTaxiToRank(new Taxi(5), 1);
            Assert.AreEqual(t, rm.FrontTaxiInRankTakesFare(1, "Anywhere", 1.23));
        }

        [TestMethod]
        public void _19_When_FrontTaxiInRankTakesFareAndRankIsEmpty_Expect_NullIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.IsNull(rm.FrontTaxiInRankTakesFare(1, "Anywhere", 1.23));
        }

        [TestMethod]
        public void _20_When_FrontTaxiInRankTakesFareAndRankDoesNotExist_Expect_NullIsReturned()
        {
            RankManager rm = new RankManager();
            Assert.IsNull(rm.FrontTaxiInRankTakesFare(4, "Anywhere", 1.23));
        }
    }
}
