using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.DatabaseContext
{
    public class DoDayDBContext : DbContext
    {
        public DoDayDBContext(DbContextOptions<DoDayDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
