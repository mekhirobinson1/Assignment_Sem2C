using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _05_Transactions_Tests
    {

        [TestMethod]
        public void _01_When_NewJoinTransaction_Expect_TransactionDateTimeIsNow()
        {
            DateTime now = DateTime.Now;
            Transaction t = new JoinTransaction(now, 1, 1);
            Assert.AreEqual(now, t.TransactionDatetime);
        }

        [TestMethod]
        public void _02_When_NewJoinTransaction_Expect_TransactionTypeIsJoin()
        {
            DateTime now = DateTime.Now;
            Transaction t = new JoinTransaction(now, 1, 1);
            Assert.AreEqual("Join", t.TransactionType);
        }

        [TestMethod]
        public void _03_When_NewJoinTransaction_Expect_ToStringReturnsCorrectString()
        {
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("dd/MM/yyyy HH:mm");
            JoinTransaction t = new JoinTransaction(now, 1, 2);
            Assert.AreEqual(
                dateStr + " Join      - Taxi 1 in rank 2",
                t.ToString());
        }

        [TestMethod]
        public void _04_When_NewLeaveTransaction_Expect_TransactionDateTimeIsNow()
        {
            DateTime now = DateTime.Now;
            Taxi t = new Taxi(1);
            Transaction lt = new LeaveTransaction(now, 1, t);
            Assert.AreEqual(now, lt.TransactionDatetime);
        }

        [TestMethod]
        public void _05_When_NewLeaveTransaction_Expect_TransactionTypeIsLeave()
        {
            DateTime now = DateTime.Now;
            Taxi t = new Taxi(1);
            Transaction lt = new LeaveTransaction(now, 1, t);
            Assert.AreEqual("Leave", lt.TransactionType);
        }

        [TestMethod]
        public void _06_When_NewLeaveTransaction_Expect_ToStringReturnsCorrectString()
        {
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("dd/MM/yyyy HH:mm");
            Taxi t = new Taxi(1);
            t.AddFare("Somewhere nice", 1.23);

            LeaveTransaction lt = new LeaveTransaction(now, 2, t);
            Assert.AreEqual(
                dateStr + " Leave     - Taxi 1 from rank 2 to Somewhere nice for £1.23",
                lt.ToString());
        }

        [TestMethod]
        public void _07_When_NewDropTransaction_Expect_TransactionDateTimeIsNow()
        {
            DateTime now = DateTime.Now;
            Transaction t = new DropTransaction(now, 1, true);
            Assert.AreEqual(now, t.TransactionDatetime);
        }

        [TestMethod]
        public void _08_When_NewDropTransaction_Expect_TransactionTypeIsDropFare()
        {
            DateTime now = DateTime.Now;
            Transaction t = new DropTransaction(now, 1, true);
            Assert.AreEqual("Drop fare", t.TransactionType);
        }

        [TestMethod]
        public void _09_When_NewDropTransactionAndPriceWasPaidIsTrue_Expect_ToStringReturnsCorrectString()
        {
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("dd/MM/yyyy HH:mm");
            DropTransaction t = new DropTransaction(now, 1, true);
            Assert.AreEqual(
                dateStr + " Drop fare - Taxi 1, price was paid",
                t.ToString());
        }

        [TestMethod]
        public void _10_When_NewDropTransactionAndPriceWasPaidIsFalse_Expect_ToStringReturnsCorrectString()
        {
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("dd/MM/yyyy HH:mm");
            DropTransaction t = new DropTransaction(now, 1, false);
            Assert.AreEqual(
                dateStr + " Drop fare - Taxi 1, price was not paid",
                t.ToString());
        }
    }
}
