using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSCProject.Dialogs
{
    public class Dialog : UserControl
    {
        public ICommand CloseCommand { get; }
        public bool UpdateDialog { get; set; }

        public Dialog()
        {
            CloseCommand = new Misc.RelayCommand(o => DialogHost.CloseDialogCommand.Execute(true, null), o => o != null && !Misc.Utils.IsAnyNullOrEmpty(o) && VerifyObject(o));
        }

        protected virtual bool VerifyObject(object o)
        {
            return true;
        }
    }
}
