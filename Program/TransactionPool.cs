using System.Data.SqlClient;
using System.Transactions;
using System.Threading.Tasks;

namespace TxTry
{
	public class TransactionPool
	{
		public void TestConnectionPoolWithTransaction()
		{
			try
			{
			Parallel.Invoke(WriteTable1, WriteTable2);
			}
			catch(System.Exception ex)
			{
			}
		}
		
		public void WriteTable1()
		{
			System.Console.WriteLine("Table1");
			using(var tx=new TransactionScope())
			{
				WriteTable("txTable1", true);
				tx.Complete();
			}
		}
		
		public void WriteTable2()
		{
			System.Console.WriteLine("Table2");
			using(var tx=new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions(){IsolationLevel=IsolationLevel.Snapshot}))
			{
				WriteTable("txTable2", false);
				System.Threading.Thread.Sleep(5000);
				System.Console.WriteLine("AfterSleep");
				try
				{
				WriteTable("txTable2", false);
				}
				catch(System.Exception ex)
				{
					_cleared=true;
				}
				tx.Complete();
			}
		}
		
		private void WriteTable(string tableName, bool clearPool)
		{
			using(var sqlConn=new SqlConnection("server=LAPPY8\\SQLExpress;database=txTry;Integrated Security=true"))
				{
					sqlConn.Open();
					var command=new SqlCommand(string.Format("Insert INTO {0} (id) values (2)", tableName), sqlConn);
					command.ExecuteNonQuery();
					
					if(clearPool)
					{
						SqlConnection.ClearPool(sqlConn);
						System.Console.WriteLine("ClearedConnection");
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