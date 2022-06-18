﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndividualTask.Models;

namespace ConsumerService.DAL
{
    public class NewsContext: DbContext
    {
        public DbSet<News> News { get; set; }
       
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
