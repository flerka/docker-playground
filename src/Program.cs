using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static TestDocker.TestDockerDbContext;
using Microsoft.EntityFrameworkCore;
namespace TestDocker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new TestDockerDbContext())
            {
                db.Database.Migrate();

                db.AppLaunchHistoryEntries.Add(new AppLaunchHistoryEntry { AppLaunchTime = DateTime.Now });
                await db.SaveChangesAsync();
                Console.WriteLine("Record was added to the database");

                Console.WriteLine("All records in database:");
                foreach (var item in db.AppLaunchHistoryEntries)
                {
                    Console.WriteLine($" - {item.AppLaunchTime.ToString()}");
                }
            }
        }
    }
}
