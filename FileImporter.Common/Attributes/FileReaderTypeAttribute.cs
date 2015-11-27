using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileImporter.Common.Attributes
{
    public class FileReaderTypeAttribute : Attribute
    {
        public FileReaderTypeAttribute()
        {
            StringValue = "Undefined Attribute";
        }
        public string StringValue { get; set; }
    }
}
