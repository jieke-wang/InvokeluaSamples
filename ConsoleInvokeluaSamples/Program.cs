using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLua;

namespace ConsoleInvokeluaSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sample1();
            //Sample2();
            //Sample3();
            //Sample4();
            //Sample5();
            //Sample6();
            //Sample7();
            //Sample8();
            //Sample9();
            //Sample10();
            //Sample11();
        }

        static void Sample1()
        {
            using (Lua lua = new Lua())
            {
                lua.State.Encoding = Encoding.UTF8;
                var results = lua.DoString("return 10 + 3*(5 + 2)");
                var doubleValue = Convert.ToDouble(results[0]);
            }
        }

        static void Sample2()
        {
            using Lua lua = new Lua();
            double val = 12.0;
            lua["x"] = val;
            var results = lua.DoString("return 10 + x*(5 + 2)");
            var doubleValue = Convert.ToDouble(results[0]);
        }

        static void Sample3()
        {
            using Lua lua = new Lua();
            double val = 12.0;
            lua["x"] = val;
            _ = lua.DoString("y = 10 + x*(5 + 2)");
            var doubleValue = Convert.ToDouble(lua["y"]);
        }

        static void Sample4()
        {
            using Lua lua = new Lua();
            var a = lua.DoString(@"
function ScriptFunc (val1, val2)
    if val1 > val2 then
        return val1 + 1
    else
        return val2 - 1
    end
end
");

            var scriptFunc = lua["ScriptFunc"] as LuaFunction;
            var res = Convert.ToInt32(scriptFunc.Call(3, 5).First());
        }

        static void Sample5()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            //lua.LoadCLRPackage();

            lua.DoFile("./scripts/hello_word.lua");
            var scriptFunc = lua["sayhello"] as LuaFunction;
            var res = scriptFunc.Call("jieke")[0] as string;
        }

        static void Sample6()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            lua.DoFile("./scripts/test.lua");

            //Console.WriteLine($"width: {lua["width"]}, type: {lua["width"].GetType()}");
            //Console.WriteLine($"height: {lua["height"]}, type: {lua["height"].GetType()}");
            //Console.WriteLine($"message: {lua["message"]}, type: {lua["message"].GetType()}");
            //Console.WriteLine($"color: {lua["color"]}, type: {lua["color"].GetType()}");
            //Console.WriteLine($"tree: {lua["tree"]}, type: {lua["tree"].GetType()}");
            //Console.WriteLine($"func: {lua["func"]}, type: {lua["func"].GetType()}");

            //var color = lua["color"] as LuaTable;
            //VisitLuaTable(color, new List<string>());

            //var tree = lua["tree"] as LuaTable;
            //VisitLuaTable(tree, new List<string>());

            //var array = lua["array"] as LuaTable;
            //VisitLuaTable(array, new List<string>());

            //var func = lua["func"] as LuaFunction;
            //var res = func.Call(1, 2);
        }

        static void Sample7()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            lua.DoFile("./scripts/multiple_methods.lua");

            var res = (lua["add"] as LuaFunction).Call(1, 2);
            PrintResult(res);

            res = (lua["subtract"] as LuaFunction).Call(1, 2);
            PrintResult(res);

            res = (lua["multiplicati"] as LuaFunction).Call(1, 2);
            PrintResult(res);

            res = (lua["division"] as LuaFunction).Call(1, 2);
            PrintResult(res);

            res = (lua["division"] as LuaFunction).Call(1, 0);
            PrintResult(res);
        }

        static void Sample8()
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//注册Nuget包System.Text.Encoding.CodePages中的编码到.NET Core
            //Encoding encoding = Encoding.GetEncoding("GB2312");

            using Lua lua = new Lua();
            //lua.LoadCLRPackage();
            //lua.State.Encoding = Encoding.GetEncoding(936); // gb2312
            //lua.DoFile("./scripts/module_Sample.lua");
            lua.DoFile("./scripts/import_module_Sample.lua");
        }

        static void Sample9()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            lua.RegisterFunction("MyFunc", null, typeof(Program).GetMethod("RegisterFunction", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));
            lua.DoString($"MyFunc('字符串',1.2,100,true,os.time())");
        }

        static void Sample10()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            //lua.RegisterLuaClassType(typeof(CSharpClass), typeof(CSharpClass));
            lua.RegisterLuaClassType(typeof(Types.ITest), typeof(Types.LuaITestClassHandler));
            lua.DoString("luanet.load_assembly('ConsoleInvokeluaSamples', 'ConsoleInvokeluaSamples.Types')");
            lua.DoString("TestClass=luanet.import_type('ConsoleInvokeluaSamples.Types.TestClass')");
            lua.DoString("test=TestClass()");
            lua.DoString("itest={}");
            lua.DoString("function itest:test1(x,y) return x+y; end");
            lua.DoString("test=TestClass()");
            lua.DoString("a=test:callInterface1(itest)");
            int a = (int)lua.GetNumber("a");

            Console.WriteLine(a);
        }

        static void Sample11()
        {
            using Lua lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;
            var res = lua.DoFile("./scripts/invoke_csharp.lua");
            PrintResult(res);
        }

        static void RegisterFunction(params object[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine($"{arg}, {arg?.GetType()}");
            }
        }

        static void VisitLuaTable(LuaTable luaTable, List<string> keyPrefixs)
        {
            string keyPrefix = string.Empty;
            if (keyPrefixs.Count > 0)
            {
                keyPrefix = string.Join(".", keyPrefixs) + ".";
            }
            foreach (var key in luaTable.Keys)
            {
                var value = luaTable[key];
                if (value is LuaTable _luaTable)
                {
                    keyPrefixs.Add(key as string);
                    VisitLuaTable(_luaTable, keyPrefixs);
                }
                else Console.WriteLine($"{keyPrefix}{key}: {value}, {value?.GetType()}");

                if (keyPrefixs.Count > 0) keyPrefixs.RemoveAt(keyPrefixs.Count - 1);
            }
        }

        static void PrintResult(params object[] results)
        {
            foreach (var result in results)
            {
                Console.WriteLine($"{result}, {result?.GetType()}");
            }
        }
    }
}


namespace ConsoleInvokeluaSamples.CSharpNamespace
{
    public class CSharpClass
    {
        private int X { get; }
        private int Y { get; }

        public CSharpClass(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int CSharpInstanceMethod(int z)
        {
            return X + Y + z;
        }

        public static string CSharpStaticMethod(params string[] args)
        {
            System.Diagnostics.Debug.WriteLine("C#方法被调用");
            return string.Join(", ", args);
        }
    }
}