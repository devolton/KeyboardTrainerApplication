using CourseProjectKeyboardApplication.Database.Entities;
using CourseProjectKeyboardApplication.Database.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Shared.Providers
{
    public static class DatabaseModelProvider
    {
        private static EducationUserProgressModel _educationProgressModel;
        static DatabaseModelProvider()
        {

            _educationProgressModel = new EducationUserProgressModel();

        }
        public static EducationUserProgressModel EducationUserProgressModel => _educationProgressModel;
       
    }
}
