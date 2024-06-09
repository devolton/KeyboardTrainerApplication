using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.ViewModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class MultiCommand : ICommand
    {
        private readonly List<ICommand> commands = new List<ICommand>();

        public event EventHandler CanExecuteChanged
        {
            add
            {
                foreach (var command in commands)
                {
                    command.CanExecuteChanged += value;
                }
            }
            remove
            {
                foreach (var command in commands)
                {
                    command.CanExecuteChanged -= value;
                }
            }
        }

        public void Add(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            commands.Add(command);
        }

        public bool CanExecute(object parameter)
        {
            foreach (var command in commands)
            {
                if (!command.CanExecute(parameter))
                {
                    return false;
                }
            }

            return true;
        }

        public void Execute(object parameter)
        {
            foreach (var command in commands)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
    }

}
