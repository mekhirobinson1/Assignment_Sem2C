using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiManagement;

namespace TaxiManagementTests
{
    [TestClass]
    public class _06_TransactionManager_Tests
    {

        [TestMethod]
        public void _01_When_NewTransactionManager_Expect_EmptyTransactionCollection()
        {
            TransactionManager tm = new TransactionManager();
            Assert.AreEqual(0, tm.Transactions.Count);
        }

        [TestMethod]
        public void _02_When_RecordJoin_Expect_JoinTransactionIsAddedToCollection()
        {
            TransactionManager tm = new TransactionManager();
            tm.RecordJoin(1, 1);
            Assert.AreEqual("JoinTransaction", tm.Transactions[0].GetType().Name);
        }

        [TestMethod]
        public void _03_When_RecordLeave_Expect_LeaveTransactionIsAddedToCollection()
        {
            TransactionManager tm = new TransactionManager();
            tm.RecordLeave(1, new Taxi(1));
            Assert.AreEqual("LeaveTransaction", tm.Transactions[0].GetType().Name);
        }

        [TestMethod]
        public void _04_When_RecordDrop_Expect_DropTransactionIsAddedToCollection()
        {
            TransactionManager tm = new TransactionManager();
            tm.RecordDrop(1, true);
            Assert.AreEqual("DropTransaction", tm.Transactions[0].GetType().Name);
        }
    }
}
