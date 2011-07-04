using System.Configuration;
using System.Data.SqlClient;

namespace $rootnamespace$.Helpers
{
    public class DatabaseSetup
    {
        public static void RestoreFromSnapshot()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString))
            {
                con.Open();

                var command = new SqlCommand(@"
                    use master 
                    alter database dbEPiIntegrationTests set single_user with rollback immediate;
                    RESTORE DATABASE dbEPiIntegrationTests FROM DATABASE_SNAPSHOT = 'IntegrationSnapshot';
                    ALTER DATABASE dbEPiIntegrationTests SET MULTI_USER WITH NO_WAIT
                ", con);
                    
                command.ExecuteNonQuery();

                con.Close();

                SqlConnection.ClearAllPools();
            }
        }
    }
}
