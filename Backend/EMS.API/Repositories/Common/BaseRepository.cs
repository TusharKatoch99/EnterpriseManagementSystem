using EMS.API.Interfaces.Common;
using Microsoft.Data.SqlClient;

public abstract class BaseRepository
{
    protected readonly IDatabaseConnection _databaseConnection;

    protected BaseRepository(IDatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    protected SqlConnection CreateConnection()
    {
        return _databaseConnection.CreateConnection();
    }
}