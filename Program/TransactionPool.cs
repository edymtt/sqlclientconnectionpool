using System.Data.SqlClient;
using System.Transactions;
using System.Threading.Tasks;
using System.Threading;

namespace TxTry
{
	public class TransactionPool
	{
		private Barrier _phases=new Barrier(2);
		
		public void TestConnectionPoolWithTransaction()
		{
			Parallel.Invoke( WriteTable2,WriteTable1);
		}
		
		public void WriteTable1()
		{
			System.Console.WriteLine("Table1");
			using(var tx=new TransactionScope())
			{
				WriteTable("txTable1", true, true);
				tx.Complete();
			}
		}
		
		public void WriteTable2()
		{
			System.Console.WriteLine("Table2");
			using(var tx=new TransactionScope(TransactionScopeOption.RequiresNew))
			{
				PrintDistributed();
				WriteTable("txTable2", false, true);
				System.Console.WriteLine("AfterSleep");
				PrintDistributed();
				try
				{
				WriteTable("txTable2", false, false);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("Ex {0} {1}", ex.GetType().FullName, ex.Message);
					_cleared=true;
				}
				PrintDistributed();
				tx.Complete();
			}
		}
		private void PrintDistributed()
		{
			System.Console.WriteLine(System.Transactions.Transaction.Current.TransactionInformation.DistributedIdentifier.ToString());
		}
		
		private void WriteTable(string tableName, bool clearPool, bool wait)
		{
			using(var sqlConn=new SqlConnection("server=LAPPY8\\SQLExpress;database=txTry;Integrated Security=true"))
				{
					sqlConn.Open();
					var command=new SqlCommand(string.Format("Insert INTO {0} (id) values (2)", tableName), sqlConn);
					command.ExecuteNonQuery();
					
					if(wait)
					{
						System.Console.WriteLine("Before");
					_phases.SignalAndWait();
						System.Console.WriteLine("After");
					}
					if(clearPool)
					{
						SqlConnection.ClearPool(sqlConn);
						System.Console.WriteLine("ClearedConnection");
					}
					if(wait)
					{
						System.Console.WriteLine("Before");
					_phases.SignalAndWait();
						System.Console.WriteLine("After");
					}
				}
		}
		
		private bool _cleared=false;
		public bool ClearedConnectionInSecondTransaction()
		{
			return _cleared;
		}
	}
}