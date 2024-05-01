using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
