using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.Misc
{
    class Column
    {
        public string Name { get; set; }
        public Binding PropertyBinding;
        public bool AllowSearch;
    }
}
