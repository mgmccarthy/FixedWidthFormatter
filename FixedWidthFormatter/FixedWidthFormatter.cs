using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FixedWidthFormatter
{
    public class FixedWidthFormatter<T> where T : class
    {
        private readonly Dictionary<string, FixedWidth> propertyPositions = new Dictionary<string, FixedWidth>();

        public void SetWidthFor<TProperty>(Expression<Func<T, TProperty>> expression, FixedWidth fixedWidth)
        {
            var memberExpression = expression.Body as MemberExpression;
            propertyPositions.Add(memberExpression.Member.Name, fixedWidth);
        }

        public string Format(IEnumerable<T> collectionToFormat)
        {
            var stringBuilder = new StringBuilder();
            foreach (var data in collectionToFormat)
            {
                foreach (var propertyInfo in data.GetType().GetProperties())
                {
                    FixedWidth fixedWidth;
                    propertyPositions.TryGetValue(propertyInfo.Name, out fixedWidth);
                    AppendDataToAssignedPosition(stringBuilder, fixedWidth, propertyInfo.GetValue(data) == null ? string.Empty : propertyInfo.GetValue(data).ToString());
                }
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        private static void AppendDataToAssignedPosition(StringBuilder stringBuilder, FixedWidth fixedWidth, string data)
        {
            stringBuilder.Append(data);

            var availableSpaceForData = ((fixedWidth.To - fixedWidth.From) + 1);
            var whiteSpaceLeftOver = availableSpaceForData - data.Length;
            stringBuilder.Append(' ', whiteSpaceLeftOver);           
        }
    }
}
