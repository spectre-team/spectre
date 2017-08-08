using System.Data.Entity.Migrations;

namespace Spectre.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Utils.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
