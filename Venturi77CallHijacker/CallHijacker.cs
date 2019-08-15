using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venturi77CallHijacker {
    internal class CallHijacker {
        internal class Config {
            public bool Debug { get; set; }
            public uint GetMethodByMDToken { get; set; }
            public List<CallHijacker.Function> Functions { get; set; }

        }
        internal class Function {
            public string SearchBy { get; set; }
            public object SearchFor { get; set; }
            public object ReplaceResultWith { get; set; }
            public int Parameter { get; set; }
            public CallHijacker.Function ContinueAdvanced { get; set; }
        }
    }
}
