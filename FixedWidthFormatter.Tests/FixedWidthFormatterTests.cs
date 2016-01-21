using System.Collections.Generic;
using NUnit.Framework;

namespace FixedWidthFormatter.Tests
{
    public static class FixedWidthFormatterTests
    {
        [TestFixture]
        public class WhenUsingFixedWidthFormatter
        {
            private string expected;
            private string actual;

            [SetUp]
            public void Given()
            {
                var dataExport = new List<DataExport>
                {
                    new DataExport { FirstName = "dave", LastName = "jones", Gender = "m"},
                    new DataExport { FirstName = "joe", LastName = "schmoe", Gender = "m"},
                    new DataExport { FirstName = "betty", LastName = "ann", Gender = "f"},
                };

                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetPositionFor(x => x.FirstName).From(1).To(10);
                fixedWidthFormatter.SetPositionFor(x => x.LastName).From(11).To(20);
                fixedWidthFormatter.SetPositionFor(x => x.Gender).From(21).To(25);

                expected = "dave      jones     m    \r\njoe       schmoe    m    \r\nbetty     ann       f    \r\n";

                actual = fixedWidthFormatter.Format(dataExport);
            }

            [Test]
            public void ReturnsTheCorrectResults()
            {
                Assert.That(actual, Is.EqualTo(expected));
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
