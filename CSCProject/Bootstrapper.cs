namespace CSCProject {
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;

    public class Bootstrapper : BootstrapperBase {
        public Bootstrapper() {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<ViewModels.ShellViewModel>();
        }
    }
}