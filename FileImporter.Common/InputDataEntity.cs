using System;

namespace FileImporter.Common
{
    public class InputDataEntity
    {
        public DateTime Date { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public int Volume { get; set; }

        public override string ToString()
        {
            return String.Format("Date: [{0}] Open: [{1}] High: [{2}] Low: [{3}] Close: [{4}] Volume: [{5}]", Date, Open, High, Low, Close, Volume);
        }
    }
}
