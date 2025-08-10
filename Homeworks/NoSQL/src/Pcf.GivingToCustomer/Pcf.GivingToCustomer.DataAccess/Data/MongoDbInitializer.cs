using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Domain;
using System.Linq;

namespace Pcf.GivingToCustomer.DataAccess.Data
{
    public class MongoDbInitializer : IDbInitializer
    {
        private readonly IMongoDBContext _context;

        public MongoDbInitializer(IMongoDBContext context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            // чистим БД
            _context.Database.DropCollection(nameof(Preference) + "s");
            _context.Database.DropCollection(nameof(Customer) + "s");

            // Добавляем Preferences
            var preferencesCollection = _context.GetCollection<Preference>();
            preferencesCollection.InsertMany(FakeDataFactory.Preferences);

            // Добавляем Customers с вложенными Preferences
            var customersCollection = _context.GetCollection<Customer>();
            var customers = FakeDataFactory.Customers.Select(c =>
            {
                // В Mongo мы обычно не храним навигационные свойства,
                // поэтому маппим Preferences -> список PreferenceIds
                c.PreferenceIds = c.Preferences?.Select(p => p.PreferenceId).ToList();
                c.Preferences = null; // навигационные свойства игнорируем
                return c;
            }).ToList();

            customersCollection.InsertMany(customers);
        }
    }
}