
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;
using Xunit;

namespace Wegister.DAL.XTest
{
    public abstract class EmployerServiceTest
    {
        protected DbContextOptions<WegisterDbContext> ContextOptions { get; }

        public EmployerServiceTest(DbContextOptions<WegisterDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var employerone = new Employer()
                {
                    Id = 1,
                    Name = "Mc Donalds"
                };

                var employertwo = new Employer()
                {
                    Id = 2,
                    Name = "Burger King"
                };

                context.AddRange(employerone, employertwo);
                context.SaveChanges();
            }
        }

        [Fact]
        public void DatabaseSeeded()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                var employers = context.Employers.ToList();

                Assert.Equal(2, employers.Count);
            }
        }

        [Fact]
        public void ReturnEmployers()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IEmployerService service = new EmployerService(context);

                var employers = service.ReadAll();

                Assert.Equal(2, employers.Count);
            }
        }

        [Fact]
        public void ReturnEmployerById()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IEmployerService service = new EmployerService(context);

                var employer = service.ReadById(1);

                Assert.Equal(1, employer.Id);
                Assert.Equal("Mc Donalds", employer.Name);
            }
        }

        [Fact]
        public void AddEmployer()
        {
            var employer = new Employer()
            {
                Id = 4,
                Name = "Deloitte"
            };

            using (var context = new WegisterDbContext(ContextOptions))
            {
                IEmployerService service = new EmployerService(context);

                service.Create(employer);
                service.Save();
            }

            using (var context = new WegisterDbContext(ContextOptions))
            {
                var employers = context.Employers.ToList();

                Assert.Equal(3, employers.Count);
                Assert.Equal("Deloitte", employers[2].Name);
            }
        }
    }
}
