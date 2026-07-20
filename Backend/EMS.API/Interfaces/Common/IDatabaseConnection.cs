using Microsoft.Data.SqlClient;

namespace EMS.API.Interfaces.Common
{
    public interface IDatabaseConnection
    {
        SqlConnection CreateConnection();
    }
}