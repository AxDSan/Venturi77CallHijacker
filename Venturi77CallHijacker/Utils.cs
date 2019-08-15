using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Venturi77CallHijacker {
    internal class Utils {
       
        public static object SearchMethodBy(CallHijacker.Function Function, MethodBase method, object[] Parameters) {
            bool flag = Function.SearchBy == "MethodName";
            if (flag) {
                bool flag2 = method.Name == Function.SearchFor.ToString();
                if (flag2) {
                    bool flag3 = Function.ContinueAdvanced != null;
                    if (flag3) {
                        return SearchMethodBy(Function.ContinueAdvanced, method, Parameters);
                    }
                    return Function.ReplaceResultWith;
                }
            }
            bool flag4 = Function.SearchBy == "Parameters";
            if (flag4) {
                bool flag5 = Parameters[Function.Parameter].ToString().Contains(Function.SearchFor.ToString());
                if (flag5) {
                    bool flag6 = Function.ContinueAdvanced != null;
                    if (flag6) {
                        return SearchMethodBy(Function.ContinueAdvanced, method, Parameters);
                    }
                    return Function.ReplaceResultWith;
                }
            }
            bool flag7 = Function.SearchBy == "MDToken";
            if (flag7) {
                bool flag8 = method.MetadataToken == Convert.ToInt32(Function.SearchFor);
                if (flag8) {
                    bool flag9 = Function.ContinueAdvanced != null;
                    if (flag9) {
                        return SearchMethodBy(Function.ContinueAdvanced, method, Parameters);
                    }
                    return true;
                }
            }
            return "Rip";
        }

       
        public static CallHijacker.Config Configuration = JsonConvert.DeserializeObject<CallHijacker.Config>(File.ReadAllText("Config.Json"));
    }
}
