using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Models;
using System;

namespace MMM.Library.Infra.Data.DataSeeders
{
    public class DummyDataSeeder
    {
        public static ModelBuilder CategoryDataSeeder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category(1001, "Category 01"));
            modelBuilder.Entity<Category>().HasData(new Category(1002, "Category 02"));
            modelBuilder.Entity<Category>().HasData(new Category(1003, "Category 03"));
            
            modelBuilder.Entity<Author>().HasData(Author.AuthorFactory.NewAuthor(Guid.Parse("e0fcd7b8-4bff-441a-ac03-fb5eb3cfe6b7"), "Paul Rabbit", DateTime.Now.AddYears(-65), "Brazilian"));
            modelBuilder.Entity<Author>().HasData(Author.AuthorFactory.NewAuthor(Guid.Parse("bec99151-3568-46a7-922a-ce2a4ebc5b96"), "Eric Evans", DateTime.Now.AddYears(-50), "American"));
            modelBuilder.Entity<Author>().HasData(Author.AuthorFactory.NewAuthor(Guid.Parse("999beeec-1fdb-4d5e-aa17-4891dee36164"), "Jose Macoratti", DateTime.Now.AddYears(-50), "Brazilian"));
            
            modelBuilder.Entity<Publisher>().HasData(Publisher.PublisherFactory.NewPublisher(Guid.Parse("a8c72841-e6e1-4d6f-8c11-bb75ac812d67"), "Casa do Códig", "789-654", "Address 456"));
            modelBuilder.Entity<Publisher>().HasData(Publisher.PublisherFactory.NewPublisher(Guid.Parse("95a3bf3f-5cbe-4d43-8964-d12ada9f7abd"), "Novatec", "445-6789", "Address 445"));
            modelBuilder.Entity<Publisher>().HasData(Publisher.PublisherFactory.NewPublisher(Guid.Parse("851878f2-c5a1-4861-a0dc-c4217efaf829"), "Brasport", "639-741", "Address 141"));

            return modelBuilder;
        }
    }
}
