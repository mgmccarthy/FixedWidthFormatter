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
            position.From = from;
            return position;
        }

        public static void To(this Position position, int to)
        {
            position.To = to;
        }
    }
}
