using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FixedWidthFormatter.Tests
{
    public static class FixedWidthFormatterTests
    {
        [TestFixture]
        public class WhenFormattingDataToFixedWidth
        {
            private string expected;
            private string actual;

            [SetUp]
            public void Given()
            {
                var dataToFormat = new List<DataExport>
                {
                    new DataExport { FirstName = "dave", LastName = "jones", Gender = "m"},
                    new DataExport { FirstName = "joe", LastName = "schmoe", Gender = "m"},
                    new DataExport { FirstName = "betty", LastName = "ann", Gender = "f"}
                };

                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetPositionFor(x => x.FirstName).From(1).To(10);
                fixedWidthFormatter.SetPositionFor(x => x.LastName).From(11).To(20);
                fixedWidthFormatter.SetPositionFor(x => x.Gender).From(21).To(25);

                expected = "dave      jones     m    \r\njoe       schmoe    m    \r\nbetty     ann       f    \r\n";
                actual = fixedWidthFormatter.Format(dataToFormat);
            }

            [Test]
            public void ReturnsTheCorrectResults()
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class WhenFormattingDataToFixedWidthAndDataContainsNullValue
        {
            private string expected;
            private string actual;

            [SetUp]
            public void Given()
            {
                var dataToFormat = new List<DataExport>
                {
                    new DataExport { FirstName = "dave", LastName = null, Gender = "m" }
                };

                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetPositionFor(x => x.FirstName).From(1).To(10);
                fixedWidthFormatter.SetPositionFor(x => x.LastName).From(11).To(20);
                fixedWidthFormatter.SetPositionFor(x => x.Gender).From(21).To(25);

                expected = "dave                m    \r\n";
                actual = fixedWidthFormatter.Format(dataToFormat);
            }

            [Test]
            public void ReturnsEmptyStringForNullValue()
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class WhenFormattingDataToFixedWidthAndThereIsABlankLine
        {
            private string expected;
            private string actual;

            [SetUp]
            public void Given()
            {
                var dataToFormat = new List<DataExport>
                {
                    new DataExport { FirstName = "dave", LastName = "jones", Gender = "m"},
                    new DataExport { FirstName = "joe", LastName = "schmoe", Gender = "m"},
                    new DataExport { FirstName = "betty", LastName = "ann", Gender = "f"}
                };

                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetPositionFor(x => x.FirstName).From(1).To(6);
                fixedWidthFormatter.SetPositionFor(x => x.LastName).From(7).To(14);
                fixedWidthFormatter.InsertBlank().From(15).To(16);
                fixedWidthFormatter.SetPositionFor(x => x.Gender).From(17).To(19);

                expected = "dave  jones     m  \r\njoe   schmoe    m  \r\nbetty ann       f  \r\n";

                actual = fixedWidthFormatter.Format(dataToFormat);
            }

            [Test]
            public void ReturnsTheCorrectResults()
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        //TODO
        [Ignore]
        [TestFixture]
        public class WhenFormattingDataToFixedWidthAndThereAreMultipleBlankLines
        {
        }

        [TestFixture]
        public class WhenPositionRangesOverlap
        {
            [Test]
            [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "position ranges cannot overlap.")]
            public void ThrowsInvalidOperationException()
            {
                var fixedWidthFormatter = new FixedWidthFormatter<DataExport>();
                fixedWidthFormatter.SetPositionFor(x => x.FirstName).From(1).To(4);
                fixedWidthFormatter.SetPositionFor(x => x.LastName).From(2).To(6);
                fixedWidthFormatter.Format(new List<DataExport>());
            }
        }

        public class DataExport
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
        }
    }
}
