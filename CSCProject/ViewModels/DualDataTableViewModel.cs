using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.ViewModels
{
    class DualDataTableViewModel<T, U> : Conductor<object>.Collection.AllActive where T : Screen, new() where U : Screen, new()
    {
        public T FirstViewModel { get; } = new T();
        public U SecondViewModel { get; } = new U();

        public virtual string SecondViewName { get; set; }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            ActivateItem(FirstViewModel);
            ActivateItem(SecondViewModel);
        }
    }
}
