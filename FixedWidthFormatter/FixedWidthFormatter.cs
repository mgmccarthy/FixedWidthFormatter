using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FixedWidthFormatter
{
    public class FixedWidthFormatter<T> where T : class
    {
        private readonly Dictionary<string, Position> propertyPositions = new Dictionary<string, Position>();
        private readonly PropertyInfo[] propertiesOfT;

        public FixedWidthFormatter()
        {
            propertiesOfT = typeof(T).GetProperties();
        }

        public Position SetPositionFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var position = new Position();

            var memberExpression = expression.Body as MemberExpression;
            propertyPositions.Add(memberExpression.Member.Name, position);

            return position;
        }

        public string Format(IEnumerable<T> dataToFormat)
        {
            var stringBuilder = new StringBuilder();
            foreach (var data in dataToFormat)
            {
                foreach (var propertyInfo in propertiesOfT)
                {
                    Position position;
                    propertyPositions.TryGetValue(propertyInfo.Name, out position);
                    var value = propertyInfo.GetValue(data);
                    AppendDataToAssignedPosition(stringBuilder, position, value?.ToString() ?? string.Empty);
                }
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        private static void AppendDataToAssignedPosition(StringBuilder stringBuilder, Position position, string data)
        {
            stringBuilder.Append(data);
            var availableSpaceForData = ((position.To - position.From) + 1);
            var whiteSpaceLeftOver = availableSpaceForData - data.Length;
            stringBuilder.Append(' ', whiteSpaceLeftOver);   
        }
    }
}
