using NUnit.Framework;

namespace TxTry
{
	[TestFixture]
	public class TransactionPoolTest
	{
		[Test]
		public void TestConnectionPoolWithTransactionTest()
		{
			var txPool=new TransactionPool();
			txPool.TestConnectionPoolWithTransaction();
			Assert.IsTrue(txPool.ClearedConnectionInSecondTransaction());
		}
	}
}