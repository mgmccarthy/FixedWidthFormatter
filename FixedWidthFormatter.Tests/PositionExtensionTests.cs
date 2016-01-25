using System;
using NUnit.Framework;

namespace FixedWidthFormatter.Tests
{
    public static class PositionExtensionTests
    {
        [TestFixture]
        public class WhenFromValueIsSetToZero
        {
            [Test]
            [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "from value of Position cannot be 0.")]
            public void ThrowsInvalidOperationException()
            {
                var position = new Position();
                position.From(0).To(2);
            }
        }

        [TestFixture]
        public class WhenToValueIsSetToZero
        {
            [Test]
            [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "to value of Position cannot be 0.")]
            public void ThrowsInvalidOperationException()
            {
                var position = new Position();
                position.From(1).To(0);
            }
        }

        [TestFixture]
        public class WhenToIsEqualToFrom
        {
            [Test]
            [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "To cannot be less than or equal to From.")]
            public void ThrowsInvalidOperationException()
            {
                var position = new Position();
                position.From(1).To(1);
            }
        }

        [TestFixture]
        public class WhenToIsLessThanToFrom
        {
            [Test]
            [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "To cannot be less than or equal to From.")]
            public void ThrowsInvalidOperationException()
            {
                var position = new Position();
                position.From(2).To(1);
            }
        }
    }
}
