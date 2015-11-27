using System;
using System.Collections.Generic;
using FileImporter.Common;
using Rhino.Mocks;
using System.Data;
using NUnit.Framework;

namespace FileImporter.UnitTest
{
    class FileProcessorUnitTest : ContextTest
    {
        private const int EcpectedRowsCount = 1;
        private InputDataEntity expectedEntity;
        private DataRow resultDataRow;
        private FileProcessor fileProcessor;
        private DataTable resultEntityList;

        protected override void Arrange()
        {
            base.Arrange();
            var reader = MockRepository.GenerateStub<IFileReader>();
            expectedEntity = new InputDataEntity
            {
                    Date = DateTime.Now,
                    Open = 1.1f,
                    High = 2.2f,
                    Low = 3.3f,
                    Close = 4.4f,
                    Volume = 1000
            };
            reader.Stub(m => m.GetEntities(Arg<string>.Is.Anything)).Return(new List<InputDataEntity> { expectedEntity });
            this.fileProcessor = new FileProcessor(reader, "DummyFileName");
        }

        protected override void Act()
        {
            base.Act();
            this.resultEntityList = this.fileProcessor.GetDataTableFromFile();
            this.resultDataRow = this.resultEntityList.Rows[0];
        }

        [Test]
        public void ResultDataTableHasExpectedcountofRows()
        {
            Assert.AreEqual(EcpectedRowsCount, this.resultEntityList.Rows.Count, "Result Datatable has unexpected rows count");
        }

        [Test]
        public void OpenValueShouldBeAsExpected()
        {
            Assert.AreEqual(this.expectedEntity.Open, this.resultDataRow["open"], "Open field has unexpected value");
        }

        //etc
    }
}
