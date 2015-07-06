using System.Collections.Generic;
using NUnit.Framework;

namespace FixedWidthFormatter.Tests
{
    public static class FixedWidthFormatterTests
    {
        [TestFixture]
        public class WhenUsingFixedWidthFormatter
        {
            [SetUp]
            public void Given()
            {
                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetWidthFor(x => x.FirstName, new FixedWidth(1, 10));
                fixedWidthFormatter.SetWidthFor(x => x.LastName, new FixedWidth (11, 20));
                fixedWidthFormatter.SetWidthFor(x => x.Gender, new FixedWidth(21, 25));

                var dataExport = new List<DataExport>
                {
                    new DataExport { FirstName = "mike", LastName = "mccarthy", Gender = "m"},
                    new DataExport { FirstName = "jessica", LastName = "cleghorn", Gender = "f"},
                    new DataExport { FirstName = "joe", LastName = "schmoe", Gender = "m"}
                };
                
                var results = fixedWidthFormatter.Format(dataExport);
            }

            [Test]
            public void YourTestHere()
            {

            }

            public class DataExport
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Gender { get; set; }
            }
        }
    }
}
