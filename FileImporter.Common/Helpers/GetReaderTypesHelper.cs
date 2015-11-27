using System;
using System.Collections.Generic;
using System.Reflection;
using FileImporter.Common.Attributes;

namespace FileImporter.Common.Helpers
{
    public static class GetReaderTypesHelper
    {
        public static Dictionary<string, TypeStorage> GetFileReaderTypes(string assemblyPath)
        {
            var pluginTypes = new Dictionary<string, TypeStorage>(StringComparer.InvariantCultureIgnoreCase);
            var pluginAssembly = Assembly.LoadFile(assemblyPath);
            foreach (Type t in pluginAssembly.GetTypes())
            {
                if (t.GetInterface(typeof(IFileReader).Name) != null)
                {
                    foreach (object attribute in t.GetCustomAttributes(typeof(FileReaderTypeAttribute), true))
                    {
                        pluginTypes.Add((attribute as FileReaderTypeAttribute).StringValue, new TypeStorage{ ReaderType = t, AllowedToUse = true});
                    }
                }
            }

            return pluginTypes;
        }
    }
}
