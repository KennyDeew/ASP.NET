using MongoDB.Driver;

namespace Pcf.GivingToCustomer.DataAccess
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>(string name = null);
    }
}
