using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Context
{
    public class TypingTutorDbContextInitializer: CreateDatabaseIfNotExists<TypingTutorDbContext>
    {
        protected override void Seed(TypingTutorDbContext context)
        {
           
        }
    }
}
