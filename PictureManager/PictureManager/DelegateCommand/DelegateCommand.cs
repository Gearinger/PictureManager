using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureManager.DelegateCommandNameSpace
{
    class DelegateCommand : ICommand
    {
        /// <summary>
        /// Delegatecommand，这种WPF.SL都可以用，VIEW里面直接使用INTERACTION的trigger激发。比较靠谱，适合不同的UIElement控件
        /// </summary>
        Action executeAction;

        public DelegateCommand(Action executeAction)
        {
            this.executeAction = executeAction;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction();
        }

        #endregion
    }
}
