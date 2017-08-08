using System.Data.Entity;
using Spectre.Database.Entities;

namespace Spectre.Database.Utils
{
    public class Context : DbContext
    {
        public DbSet<Dataset> Datasets { get; set; }
    }
}
