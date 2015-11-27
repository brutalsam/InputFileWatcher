using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileImporter.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<InputDataEntity> GetTextLines(string filePath);
    }
}
