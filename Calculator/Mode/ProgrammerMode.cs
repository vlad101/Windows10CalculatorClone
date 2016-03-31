using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Mode
{
    class ProgrammerMode : StandardMode
    {
        public bool ClearOperation(StandardMode stMode)
        {
            return stMode.ClearOperation();
        }
    }
}
