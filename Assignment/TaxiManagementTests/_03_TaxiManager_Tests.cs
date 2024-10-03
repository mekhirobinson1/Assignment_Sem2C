using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _03_TaxiManager_Tests
    {

        [TestMethod]
        public void _01_When_NewTaxiManager_Expect_EmptyTaxiCollection()
        {
            TaxiManager tm = new TaxiManager();
            Assert.AreEqual(0, tm.GetAllTaxis().Count);
        }

        [TestMethod]
        public void _02_When_FindTaxiAndTaxiNotFound_Expect_NullIsReturned()
        {
            TaxiManager tm = new TaxiManager();
            Assert.IsNull(tm.FindTaxi(1));
        }

        [TestMethod]
        public void _03_When_FindTaxiAndTaxiIsFound_Expect_CorrectTaxiIsReturned()
        {
            TaxiManager tm = new TaxiManager();
            SortedDictionary<int, Taxi> taxis = tm.GetAllTaxis();
            Taxi t = new Taxi(2);
            taxis.Add(1, new Taxi(1));
            taxis.Add(2, t);
            taxis.Add(3, new Taxi(3));
            Assert.AreEqual(t, tm.FindTaxi(2));
        }

        [TestMethod]
        public void _04_When_CreateTaxi_Expect_NewTaxiIsReturned()
        {
            TaxiManager tm = new TaxiManager();
            Taxi t = new Taxi(2);
            Assert.AreNotSame(t, tm.CreateTaxi(2));
        }

        [TestMethod]
        public void _05_When_CreateTaxi_Expect_NewTaxiHasCorrectNumber()
        {
            TaxiManager tm = new TaxiManager();
            Assert.AreEqual(2, tm.CreateTaxi(2).Number);
        }

        [TestMethod]
        public void _06_When_CreateTaxiAndTaxiNumberNotAlreadyInUse_Expect_NewTaxiIsAddedToCollection()
        {
            TaxiManager tm = new TaxiManager();
            tm.CreateTaxi(2);
            Assert.AreEqual(2, tm.FindTaxi(2).Number);
        }

        [TestMethod]
        public void _07_When_CreateTaxiAndTaxiNumberAlreadyInUse_Expect_ExistingTaxiIsReturned()
        {
            TaxiManager tm = new TaxiManager();
            SortedDictionary<int, Taxi> taxis = tm.GetAllTaxis();
            Taxi t = new Taxi(1);
            taxis.Add(1, t);
            Assert.AreEqual(t, tm.CreateTaxi(1));
        }
    }
}
