﻿using CourseProjectKeyboardApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Interfaces
{
    public interface IEducationResultLessonButton
    {
        EducationLesson EducationLesson { get; set; }
        string LessonNumber { get; set; }
    }
}
