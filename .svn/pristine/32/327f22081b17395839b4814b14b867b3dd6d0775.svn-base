using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace United_IMS.Web.Repository
{
	public static class DbHelper
	{
		public  static SqlConnection GetDBConnection()
		{
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["united_ims"].ToString();
			var connection = new SqlConnection(connectionString.ToString());
			connection.Open();
			return connection;
		}
		
	}
}