using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _02_Rank_Tests
    {

        [TestMethod]
        public void _01_When_NewRank_Expect_IdIsSet()
        {
            Rank r = new Rank(1, 1);
            Assert.AreEqual(1, r.Id);
        }

        [TestMethod]
        public void _02_When_AddTaxi_Expect_RankIsSetInTaxi()
        {
            Rank r = new Rank(1, 1);
            Taxi t = new Taxi(1);
            r.AddTaxi(t);
            Assert.AreEqual(r, t.Rank);
        }

        [TestMethod]
        public void _03_When_AddTaxiAndRankHasNoSpareSpace_Expect_FalseIsReturned()
        {
            Rank r = new Rank(1, 1);
            r.AddTaxi(new Taxi(1));
            Assert.IsFalse(r.AddTaxi(new Taxi(2)));
        }

        [TestMethod]
        public void _04_When_AddTaxiAndRankHasOneSpareSpace_Expect_TrueIsReturned()
        {
            Rank r = new Rank(1, 1);
            Assert.IsTrue(r.AddTaxi(new Taxi(1)));
        }

        [TestMethod]
        public void _05_When_TwoTaxisInRank_Expect_FirstTaxiInIsFirstOut()
        {
            Rank r = new Rank(1, 2);
            Taxi t = new Taxi(1);
            r.AddTaxi(t);
            r.AddTaxi(new Taxi(2));
            Assert.AreEqual(t, r.FrontTaxiTakesFare("Somewhere", 1.23));
        }

        [TestMethod]
        public void _06_When_FrontTaxiTakesFareAndRankIsNotEmpty_Expect_CorrectTaxiIsReturned()
        {
            Taxi t = new Taxi(1);
            Rank r = new Rank(1, 1);
            r.AddTaxi(t);
            Assert.AreEqual(t, r.FrontTaxiTakesFare("Somewhere", 1.23));
        }

        [TestMethod]
        public void _07_When_FrontTaxiTakesFare_Expect_TaxiIsRemovedFromRank()
        {
            Rank r = new Rank(1, 2);
            Taxi t = new Taxi(1);
            r.AddTaxi(t);
            r.AddTaxi(new Taxi(2));
            Taxi t2 = r.FrontTaxiTakesFare("Somewhere", 1.23);
            t2.DropFare(true);
            Assert.IsTrue(r.AddTaxi(t));
        }

        [TestMethod]
        public void _08_When_FrontTaxiTakesFareAndRankIsEmpty_Expect_NullIsReturned()
        {
            Rank r = new Rank(1, 1);
            Assert.IsNull(r.FrontTaxiTakesFare("Nowhere", 1.23));
        }
    }
}
