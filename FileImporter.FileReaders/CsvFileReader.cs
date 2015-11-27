using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FileImporter.Common;
using FileImporter.Common.Attributes;

namespace FileImporter.FileReaders
{
    [FileReaderType(StringValue = ".csv")]
    class CsvFileReader : IFileReader
    {
        public IEnumerable<InputDataEntity> GetEntities(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var rowValues = line.Split(',');
                var entity = new InputDataEntity
                {
                    Date = DateTime.Parse(rowValues[0]),
                    Open = float.Parse(rowValues[1].Replace('.', ',')),
                    High = float.Parse(rowValues[2].Replace('.', ',')),
                    Low = float.Parse(rowValues[3].Replace('.', ',')),
                    Close = float.Parse(rowValues[4].Replace('.', ',')),
                    Volume = int.Parse(rowValues[5])
                };

                yield return entity;
            }

        }
    }
}
