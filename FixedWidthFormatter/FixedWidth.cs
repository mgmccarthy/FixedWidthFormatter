namespace FixedWidthFormatter
{
    public class FixedWidth
    {
        public FixedWidth(int from, int to)
        {
            From = from;
            To = to;
        }

        public int From { get; private set; }
        public int To { get; private set; }
    }
}
