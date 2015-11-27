using System;
using System.Collections.Generic;
using System.Linq;
using FileImporter.Common;
using FileImporter.Common.Attributes;
using System.Xml.Linq;

namespace FileImporter.FileReaders
{
    [FileReaderType(StringValue = ".xml")]
    class XmlFileReader : IFileReader
    {
        public IEnumerable<InputDataEntity> GetEntities(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            IEnumerable<InputDataEntity> resultList = new List<InputDataEntity>();
            if (doc.Root != null)
                resultList = doc.Root.Elements().Select(el => new InputDataEntity
                {
                    Date = DateTime.Parse(el.Attribute("date").Value),
                    Open = float.Parse(el.Attribute("open").Value.Replace('.', ',')),
                    High = float.Parse(el.Attribute("high").Value.Replace('.', ',')),
                    Low = float.Parse(el.Attribute("low").Value.Replace('.', ',')),
                    Close = float.Parse(el.Attribute("close").Value.Replace('.', ',')),
                    Volume = int.Parse(el.Attribute("volume").Value)
                });

            return resultList;
        }
    }
}
