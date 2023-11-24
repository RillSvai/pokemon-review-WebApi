using Microsoft.EntityFrameworkCore;
using PokemonReview.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReview_API.Tests.Repositories
{
    internal static class Helper
    {
        internal static ApplicationDbContext GetDbContext() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            ApplicationDbContext db = new ApplicationDbContext(options);
            db.Database.EnsureCreated();
            return db;
        }

    }
}
