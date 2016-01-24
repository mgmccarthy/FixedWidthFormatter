using System;

namespace FixedWidthFormatter
{
    public class Position
    {
        public int From { get; set; }
        public int To { get; set; }
    }

    public static class PositionExtensions
    {
        public static Position From(this Position position, int from)
        {
            if (from == 0)
                throw new InvalidOperationException("from value of Position cannot be 0.");

            position.From = from;
            return position;
        }

        public static void To(this Position position, int to)
        {
            if (to == 0)
                throw new InvalidOperationException("to value of Position cannot be 0.");

            if (to == position.From || to < position.From)
                throw new InvalidOperationException("To cannot be less than or equal to From.");

            position.To = to;
        }
    }
}
