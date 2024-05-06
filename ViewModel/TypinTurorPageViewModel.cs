using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProjectKeyboardApplication.ViewModel
{
    public class TypinTurorPageViewModel:ViewModelBase
    {
        public ICommand KeyDownCommand;
        public TypinTurorPageViewModel()
        {
            KeyDownCommand = new RelayCommand(OnKeyDownCommand);
           
        }
        public void OnKeyDownCommand(object param)
        {
            MessageBox.Show(((Key)param).ToString());
        }

    }
}
