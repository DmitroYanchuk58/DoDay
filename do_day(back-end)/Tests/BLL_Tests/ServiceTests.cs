using Data_Access_Layer.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Tests.BLL_Tests
{
    public abstract class ServiceTests
    {
        protected DoDayDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DoDayDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DoDayDBContext(options);
        }
    }
}
