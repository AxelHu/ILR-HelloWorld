using System;
using System.IO;

namespace RunHelloWorldWithILR
{
    class Program
    {
        static ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        static void Main(string[] args)
        {
            LoadILR();
            RunHelloWorld();
        }

        static void LoadILR()
        {
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            string path = @".\HelloWorld.dll";
            var dllFs = new FileStream(path, FileMode.OpenOrCreate);
            var dllBytes = new byte[dllFs.Length];
            dllFs.Read(dllBytes, 0, (int)dllFs.Length);
            dllFs.Close();
            var dllMs = new MemoryStream(dllBytes);

            var pdbFs = new FileStream(@".\HelloWorld.pdb", FileMode.OpenOrCreate);
            var pdbBytes = new byte[pdbFs.Length];
            pdbFs.Read(pdbBytes, 0, (int)pdbFs.Length);
            pdbFs.Close();
            var pdbMs = new MemoryStream(pdbBytes);

            appdomain.LoadAssembly(dllMs, pdbMs, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }

        static void RunHelloWorld()
        {
            appdomain.Invoke("HelloWorld.Program", "HelloWorld", null, null);
        }
    }
}
