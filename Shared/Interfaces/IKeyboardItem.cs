using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace CourseProjectKeyboardApplication.Interfaces
{
    public interface IKeyboardItem
    {
        bool IsFocusKeyboardItem { get; set; }
        bool IsErrorPushedKeyboardItem { get; set; }
        string KeyTag { get; set; }
        string TextValue { get; set; }
    }
}
