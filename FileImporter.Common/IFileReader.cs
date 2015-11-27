using System.Collections.Generic;

namespace FileImporter.Common
{
    public interface IFileReader
    {
        IEnumerable<InputDataEntity> GetEntities(string filePath);
    }
}
