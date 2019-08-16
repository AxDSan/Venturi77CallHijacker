using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;
using Newtonsoft.Json;
using Venturi77CallHijacker;
namespace Venturi77Hijacker {
    class Program {
        static void Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] Inject Dll\n[2] Build Config\n[3] Build Debug Config");
            var key = Console.ReadKey();
            Console.Clear();
            if(key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) {
                InjectDll();
                return;
            }
            if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2) {
                CreateConfig();
                return;
            }
            if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3) {
                CreateDebugConfig();
                return;
            }
        }
        public static void InjectDll() {
            Obfuscator obf = Obfuscator.Unknown;
            Console.WriteLine("Path: ");
            string Path = Console.ReadLine().Replace("\"", "");
            ModuleDefMD Module = ModuleDefMD.Load(Path);
            Console.Clear();
            Console.WriteLine("[1] Detect Obfuscator\n[2] Select Obfuscator");
            var key = Console.ReadKey();
            Console.Clear();
            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) {
                obf = DetectObfuscator(Module,Path);
                Console.WriteLine("Detected: " + obf);
               
            }
            if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2) {
                Console.WriteLine("[1] KoiVM\n[2] EazVM\n[3] AgileVM");
                obf = (Obfuscator)(Convert.ToInt32(Console.ReadLine())-1);
                Console.Clear();
            }
            if(obf == Obfuscator.KoiVM) {
                if (InjectKoiVM(Module, Path)) {
                    Console.WriteLine("Successfully Injected KoiVM");
                } else {
                    Console.WriteLine("Failed Injecting KoiVM");
                }
                Console.ReadLine();
                return;
            }
            if(obf == Obfuscator.EazVM) {
                if (InjectEazVM(Module, Path)) {
                    Console.WriteLine("Successfully Injected EazVM");
                } else {
                    Console.WriteLine("Failed Injecting EazVM");
                }
                Console.ReadLine();
                return;
            }
            if(obf == Obfuscator.AgileVM) {
                /*  if (InjectAgileVM(Module, Path)) {
                      Console.WriteLine("Successfully Injected AgileVM");
                  } else {
                      Console.WriteLine("Failed Injecting AgileVM");
                  }*/
                Console.WriteLine("Some Issues With Agile Will Fix In A Bit!");
                Console.ReadLine();
                return;
            }
            if(obf == Obfuscator.Unknown) {
                Console.WriteLine("Unknown Obfuscator!");
                Console.ReadLine();
                return;
            }

        }
        public static int WriteShit() {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[1] New Function");
            Console.WriteLine("[2] Save");
            Console.ForegroundColor = ConsoleColor.Cyan;

            int parse2 = Convert.ToInt32(WriteInput());
            Console.Clear();
            return parse2;
        }
        public static void CreateDebugConfig() {

            CallHijacker.Config Config = new CallHijacker.Config();
            Config.Debug = true;
            File.AppendAllText("Config.Json", JsonConvert.SerializeObject(Config, Formatting.Indented, new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore
            }));

        }
            public static void CreateConfig() {
            CallHijacker.Config Config = new CallHijacker.Config();
            Config.Debug = false;
            Config.Functions = new List<CallHijacker.Function>();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            for (; ; )
       {
                if (WriteShit() == 1) {
                    Config.Functions.Add(CreateFunction());
                    continue;
                }



                File.AppendAllText("Config.Json", JsonConvert.SerializeObject(Config, Formatting.Indented, new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                }));

            }

        }
        public static CallHijacker.Function CreateFunction() {
            CallHijacker.Function func = new CallHijacker.Function();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Search By: " + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] MethodName" + Environment.NewLine + "[2] Parameters" + Environment.NewLine + "[3] MDToken" + Environment.NewLine);
            int parse3 = Convert.ToInt32(WriteInput());
            Console.Clear();
            if (parse3 == 1) {
                func.SearchBy = "MethodName";
            } else if (parse3 == 2) {
                func.SearchBy = "Parameters";
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Parameter: ");
                func.Parameter = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
            } else if (parse3 == 3) {
                func.SearchBy = "MDToken";
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("SearchFor: ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            func.SearchFor = Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Replace Result With: ");
            string readline = Console.ReadLine();
            Console.Clear();
            func.ReplaceResultWith = readline;
            if (readline == "*Nothing*") {
                func.ReplaceResultWith = null;
            }
            if (readline == "*True*") {
                func.ReplaceResultWith = true;
            }
            if (readline == "*False*") {
                func.ReplaceResultWith = false;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("[1] Add Function Inside This Function");
            Console.WriteLine("[2] Continue");
            int inpupt = Convert.ToInt32(WriteInput());
            Console.Clear();
            if (inpupt == 2) {
                return func;
            }
            if (inpupt == 1) {
                func.ContinueAdvanced = CreateFunction();
            }
            return func;

        }
        public static string WriteInput() {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Input: ");
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            return input;
        }
        public static bool InjectAgileVM(ModuleDefMD Module, string Pathh) {
            bool injected = false;
            TypeDef[] array = Module.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++) {
                foreach (MethodDef method in array[i].Methods.ToArray<MethodDef>()) {
                    if(method.HasBody && method.Body.HasInstructions) {
                        for (int d = 0; d < method.Body.Instructions.Count(); d++) {

                            if(method.Body.Instructions[d].OpCode == OpCodes.Callvirt) {
                                if (method.Body.Instructions[d].ToString().Contains("System.Reflection.MethodBase::Invoke(System.Object,System.Object[])") && method.Body.Instructions[d + 1].IsStloc() && method.Body.Instructions[d - 1].IsLdarg() && method.Body.Instructions[d - 3].IsLdarg()) {
                                    method.Body.Instructions[d].Operand = method.Module.Import(ModuleDefMD.Load("Venturi77CallHijacker.dll").Types.Single(t => t.IsPublic && t.Name == "Handler").Methods.Single(m => m.Name == "HandleInvoke"));
                                    injected = true;
                                }
                        }
                           
                        }
                    }
                }
            }
            if(!injected) {
                return injected;
            }
            ModuleWriterOptions nativeModuleWriterOptions = new ModuleWriterOptions(Module);
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            nativeModuleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            nativeModuleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly);
            
          //  var otherstrteams = Module.Metadata.AllStreams.Where(a => a.GetType() == typeof(DotNetStream));
          //  nativeModuleWriterOptions.MetadataOptions.PreserveHeapOrder(Module, addCustomHeaps: true);
            Module.Write(Pathh, nativeModuleWriterOptions);



            // File.Delete(Pathh);
          
            File.Copy("Venturi77CallHijacker.dll", Path.GetDirectoryName(Pathh) + "\\Venturi77CallHijacker.dll");
            File.Copy("Newtonsoft.Json.dll", Path.GetDirectoryName(Pathh)+ "\\Newtonsoft.Json.dll");
            return injected;
        }
        public static bool InjectEazVM(ModuleDefMD Module, string Pathh) {
            bool injected = false;
            TypeDef[] array = Module.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++) {
                foreach (MethodDef method in array[i].Methods.ToArray<MethodDef>()) {
                    bool flag = method.HasBody && method.Body.HasInstructions && !method.FullName.Contains("My.") && !method.FullName.Contains(".My") && !method.IsConstructor && !method.DeclaringType.IsGlobalModuleType;
                    if (flag) {
                        for (int j = 0; j < method.Body.Instructions.Count - 1; j++) {
                            if (method.Body.Instructions[j].OpCode == OpCodes.Ldarg_0) {
                                if (method.Body.Instructions[j + 1].OpCode == OpCodes.Ldarg_1) {
                                    if (method.Body.Instructions[j + 2].OpCode == OpCodes.Ldarg_2) {
                                        if (method.Body.Instructions[j + 3].OpCode == OpCodes.Callvirt) {
                                            if (method.Body.Instructions[j + 4].OpCode == OpCodes.Ret) {
                                                if (method.Body.Instructions[j + 5].OpCode == OpCodes.Ldloc_0) {
                                                    method.Body.Instructions[j + 3].OpCode = OpCodes.Call;
                                                    method.Body.Instructions[j + 3].Operand = method.Module.Import(ModuleDefMD.Load("Venturi77CallHijacker.dll").Types.Single(t => t.IsPublic && t.Name == "Handler").Methods.Single(m => m.Name == "HandleInvoke"));
                                                    injected = true;
                                                }

                                            }
                                        }

                                    }

                                }
                            }
                        }
                    }
                }

            }
            if (!injected) {
                return injected;
            }
            ModuleWriterOptions nativeModuleWriterOptions = new ModuleWriterOptions(Module);
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            nativeModuleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            nativeModuleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly);
            var otherstrteams = Module.Metadata.AllStreams.Where(a => a.GetType() == typeof(DotNetStream));
            nativeModuleWriterOptions.MetadataOptions.PreserveHeapOrder(Module, addCustomHeaps: true);
            Module.Write(Path.Combine(Path.GetDirectoryName(Pathh), Path.GetFileNameWithoutExtension(Pathh) + "_Injected" + ".exe"), nativeModuleWriterOptions);
            File.Copy("Venturi77CallHijacker.dll", Path.GetDirectoryName(Pathh) + "\\Venturi77CallHijacker.dll");
            File.Copy("Newtonsoft.Json.dll", Path.GetDirectoryName(Pathh )+ "\\Newtonsoft.Json.dll");
            return injected;
        }
        public static bool InjectKoiVM(ModuleDefMD Module,string Pathh) {
            bool injected = false;
            TypeDef[] array = Module.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++) {
                foreach (MethodDef method in array[i].Methods.ToArray<MethodDef>()) {
                    bool flag = method.HasBody && method.Body.HasInstructions && !method.FullName.Contains("My.") && !method.FullName.Contains(".My") && !method.IsConstructor && !method.DeclaringType.IsGlobalModuleType;
                    if (flag) {
                        for (int j = 0; j < method.Body.Instructions.Count - 1; j++) {
                            if (method.Body.Instructions[j].OpCode == OpCodes.Ldarg_2) {
                                if (method.Body.Instructions[j + 1].OpCode == OpCodes.Ldloc_2) {
                                    if (method.Body.Instructions[j + 2].OpCode == OpCodes.Ldloc_3) {
                                        if (method.Body.Instructions[j + 3].OpCode == OpCodes.Callvirt) {
                                            if (method.Body.Instructions[j + 4].OpCode == OpCodes.Stloc_S) {
                                                method.Body.Instructions[j + 3].OpCode = OpCodes.Call;
                                                method.Body.Instructions[j + 3].Operand = method.Module.Import(ModuleDefMD.Load("Venturi77CallHijacker.dll").Types.Single(t => t.IsPublic && t.Name == "Handler").Methods.Single(m => m.Name == "HandleInvoke"));
                                                injected = true;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (!injected) {
                return injected;
            }
            ModuleWriterOptions nativeModuleWriterOptions = new ModuleWriterOptions(Module);
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            nativeModuleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            nativeModuleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            nativeModuleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly);
            var otherstrteams = Module.Metadata.AllStreams.Where(a => a.GetType() == typeof(DotNetStream));
            nativeModuleWriterOptions.MetadataOptions.PreserveHeapOrder(Module, addCustomHeaps: true);
            Module.Write(Path.Combine(Path.GetDirectoryName(Pathh),Path.GetFileNameWithoutExtension(Pathh) + "_Injected" + ".exe"), nativeModuleWriterOptions);
            File.Copy("Venturi77CallHijacker.dll", Path.GetDirectoryName(Pathh) + "\\Venturi77CallHijacker.dll");
            File.Copy("Newtonsoft.Json.dll", Path.GetDirectoryName(Pathh)+ "\\Newtonsoft.Json.dll");
            return injected;
        }
        public static Obfuscator DetectObfuscator(ModuleDefMD Module,string Path) {
            if(Module.GetAssemblyRefs().Where(q=>q.ToString().Contains("AgileDotNet.VMRuntime")).ToArray().Count() > 0){
                return Obfuscator.AgileVM;
            }
            if(Module.Types.SingleOrDefault(t => t.HasFields && t.Fields.Count == 119) != null) {
                return Obfuscator.KoiVM;
            }
            foreach(var type in Module.Types) {
                foreach(var method in type.Methods) {
                    if(method.HasBody && method.Body.HasInstructions&& method.Body.Instructions.Count() >= 6) {
                       if(method.Body.Instructions[0].OpCode == OpCodes.Ldarg_0) {
                            if(method.Body.Instructions[1].OpCode == OpCodes.Ldarg_1) {
                                if (method.Body.Instructions[2].OpCode == OpCodes.Ldarg_2) {
                                    if (method.Body.Instructions[3].OpCode == OpCodes.Ldarg_3) {
                                        if (method.Body.Instructions[5].OpCode == OpCodes.Pop) {
                                            if (method.Body.Instructions[6].OpCode == OpCodes.Ret) {
                                                if (method.Body.Instructions[4].OpCode == OpCodes.Call) {
                                                    
                                                    if(method.Body.Instructions[4].ToString().Contains("(System.IO.Stream,System.String,System.Object[])")) {
                                                         return Obfuscator.EazVM;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Obfuscator.Unknown;
        }
        
    }
}
