using System.Data;

namespace FileImporter.Common
{
    public class FileProcessor
    {
        IFileReader fileReader;
        string fileName;
        public FileProcessor(IFileReader reader, string fileName)
        {
            this.fileReader = reader;
            this.fileName = fileName;
        }

        public DataTable GetDataTableFromFile()
        {
            var entityList = this.fileReader.GetEntities(this.fileName);
            DataTable dt = new DataTable();
            dt = entityList.ToDataTable();
            return dt;
        }
    }
}
