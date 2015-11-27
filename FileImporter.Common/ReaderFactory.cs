using System;
using System.Collections.Generic;
using System.Linq;
namespace FileImporter.Common
{
    public class ReaderFactory
    {
        private Dictionary<string, TypeStorage> readerTypes;
        public ReaderFactory(Dictionary<string, TypeStorage> readerTypes)
        {
            this.readerTypes = readerTypes;
        }
        public IFileReader GetFileReader(string fileExtension)
        {
            IFileReader fileReader = null;
            if (readerTypes.ContainsKey(fileExtension))
            {
                var readerTypeStorage = readerTypes[fileExtension];
                if (readerTypeStorage.AllowedToUse)
                {
                    fileReader = Activator.CreateInstance(readerTypeStorage.ReaderType) as IFileReader;
                }
            }

            return fileReader;
        }
    }
}
