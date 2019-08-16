using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Venturi77CallHijacker {
    public class Handler {
        // Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
        public static object HandleInvoke(MethodBase method, object obj2, object[] Parameters) {
            try {
                if(Utils.Configuration.Debug) {
                    object objj = null;
                    try {
                        objj = method.Invoke(obj2, Parameters);
                    } catch {
                        objj = null;
                    }
                    string debug = "Calling Method: " + method.ToString() + "\nMethodName: " + method.Name + "\nParameters: \n";
                   for(int i = 0; i<Parameters.Length; i++) {
                        debug += "Param[" + i + "]" + " Value: " + Parameters[i].ToString()+ "\n";
                    }
                    debug += "MDToken: " + method.MetadataToken+"\n";
                    System.IO.File.AppendAllText("Debug.txt", debug + Environment.NewLine +"------------------------" + Environment.NewLine);
                    return objj;
                }
                using (List<CallHijacker.Function>.Enumerator enumerator = Utils.Configuration.Functions.GetEnumerator()) {
                    if (enumerator.MoveNext()) {

                        CallHijacker.Function funct = enumerator.Current;
                        object shit = Utils.SearchMethodBy(funct, method, Parameters);
                        bool flag = shit is string;
                        if (!flag) {
                            return shit;
                        }
                        bool flag2 = shit.ToString() == "Rip";
                        if (flag2) {
                            return method.Invoke(obj2, Parameters);
                        }
                        return shit;
                    }
                }
            } catch {
                return null;
            }
            return null;
        }
    }
}
