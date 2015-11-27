using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using FileImporter.Common;
using FileImporter.Common.Helpers;
using System.Configuration;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var pluginAssemblyPath = ConfigurationManager.AppSettings["PluginsPath"];
            var readerTypes = GetReaderTypesHelper.GetFileReaderTypes(pluginAssemblyPath);
            Console.WriteLine(String.Join(Environment.NewLine, readerTypes.Values));
            
            //var fileToRead = @"C:\pub\dotNet\source files\input.csv";
            //var fileExtension = Path.GetExtension(fileToRead);
            //Type readerType = readerTypes[fileExtension];
            //var reader = Activator.CreateInstance(readerType) as IFileReader;
            //var result = reader.GetTextLines(fileToRead).Take(5).ToList();
            //Console.WriteLine(String.Join(Environment.NewLine, result));

            var landingDirectory = ConfigurationManager.AppSettings["LandingDirectory"];
            //var scanner = new DirectoryScanner(landingDirectory);

            //scanner.StartScanning();
            //var watcher = new CustomFileWatcher(5000, landingDirectory);
            //watcher.Start();
            //Console.ReadKey();
            //foreach (var item in watcher.FileList)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.ReadKey();
        }
    }
}
