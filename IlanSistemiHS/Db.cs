using System.Data.SqlClient;

namespace IlanSistemiHS
{
	public static class Db
	{
		public static SqlConnection Conn()
		{
			return new SqlConnection("Server=.\\SQLEXPRESS; Database=IlanSistemiHS; Integrated Security=True; TrustServerCertificate=Yes");
		}
	}
}
