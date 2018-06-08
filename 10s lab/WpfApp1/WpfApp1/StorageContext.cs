using System.Data.Entity;

namespace WpfApp1
{
    class StorageContext : DbContext
    {
        public StorageContext()
            : base("DbConnection")
        { }
        public DbSet<StorageItem> StorageItem { get; set; }
    }
}
