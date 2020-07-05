using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.CQRS;
using MMM.Library.Domain.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Context
{
    public class LibraryDbContext : DbContext
    {
        private readonly IUser _user;
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, IUser user)
            : base(options)
        {
            _user = user;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingItem> BookingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)"); // breaking changes EF Core 3.1 // v2.2 => property.Relational().ColumnType = "varchar(100)";

            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

            //// disable cascade delete for all relations
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("StockCode").StartsAt(1000).IncrementsBy(2);
            modelBuilder.HasSequence<int>("CategoryCode").StartsAt(100).IncrementsBy(2);
            modelBuilder.HasSequence<int>("BookingCode").StartsAt(10000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("BookingItemCode").StartsAt(10).IncrementsBy(10);

            // Relationships ::::
            // 1 : N: Booking : BookingItem
            modelBuilder.Entity<Booking>().HasMany(b => b.BookinkItems).WithOne(bi => bi.Booking).HasForeignKey(b => b.BookingId);

            // 1 : N: Stock : BookItem


            // 1 -> N: Category : Books
            modelBuilder.Entity<Category>().HasMany(c => c.Books).WithOne(b => b.Category).HasForeignKey(b => b.CategoryId);

            // 1 -> N: Publisher : Books
            modelBuilder.Entity<Publisher>().HasMany(p => p.Books).WithOne(b => b.Publisher).HasForeignKey(b => b.PublisherId);

            // N <-> N: Book : Author
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author).WithMany(a => a.BookAuthors).HasForeignKey(ba => ba.AuthorId);

            //DummyDataSeeder.CategoryDataSeeder(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            // Audit Entities
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity is IAudit))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                    entry.Property("CreateUser").CurrentValue = await _user.GetUserName();
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateDate").IsModified = false;
                    entry.Property("CreateUser").IsModified = false;
                    entry.Property("LastUpdateDate").CurrentValue = DateTime.Now;
                    entry.Property("LastUpdateUser").CurrentValue = await _user.GetUserName();
                }
            }

            var sucess = await base.SaveChangesAsync() > 0;
            return sucess;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        // DesignTimeDbContextFactory https://go.microsoft.com/fwlink/?linkid=851728
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
        {
            public LibraryDbContext CreateDbContext(string[] args)
            {
                //    IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("appsettings.Production.json").Build();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MMM.Library.Services.AspNetWebApi/appsettings.Development.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<LibraryDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);

                return new LibraryDbContext(builder.Options);
            }
        }
    }
}
