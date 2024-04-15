using Microsoft.EntityFrameworkCore;

namespace EfCoreOpenJson.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookCover> BookCovers { get; set; }

        [DbFunction("OPENJSON", IsBuiltIn = true)]
        public static IQueryable<JsonResult> OpenJson(object json) => throw new NotSupportedException();

        [DbFunction("OPENJSON", IsBuiltIn = true)]
        public static IQueryable<JsonResult> OpenJson(object json, string path) => throw new NotSupportedException();

        public MyDbContext(DbContextOptions<MyDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDbFunction(typeof(MyDbContext).GetMethod(nameof(OpenJson), new[] { typeof(object) })!, b => b.HasParameter("json").HasStoreType("nvarchar(max)"));
            builder.HasDbFunction(typeof(MyDbContext).GetMethod(nameof(OpenJson), new[] { typeof(object), typeof(string) })!, b => b.HasParameter("json").HasStoreType("nvarchar(max)"));

            builder.Entity<Book>(b => b.Property(x => x.Data).IsJson());

            // Prevent migration error "Cannot scaffold C# literals of type 'System.Reflection.NullabilityInfoContext'"
            // See: https://github.com/dotnet/efcore/issues/30142
            // We also need to exclude it from migrations to prevent EF core from trying to add a table for it
            builder.Entity<JsonResult>().ToTable(nameof(JsonResult)).Metadata.SetIsTableExcludedFromMigrations(true);
        }

        [Keyless]
        public class JsonResult
        {
            public required string Key { get; set; }

            public required string Value { get; set; }

            public int Type { get; set; }
        }
    }
}
