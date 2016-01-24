using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FixedWidthFormatter
{
    public class FixedWidthFormatter<T> where T : class
    {
        private readonly Dictionary<string, Position> propertyPositions = new Dictionary<string, Position>();
        private readonly PropertyInfo[] propertiesOfT;
        const string BlankString = "blankString";

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

        public Position InsertBlank()
        {
            var blankStringVariableName = GetNextBlankStringVariableName();
            var position = new Position();
            propertyPositions.Add(blankStringVariableName, position);
            return position;
        }

        public string Format(IEnumerable<T> listOfT)
        {
            if (PositionRangesOverlap())
                throw new InvalidOperationException("position range cannot overlap.");

            var stringBuilder = new StringBuilder();

            foreach (var type in listOfT)
            {
                foreach (var propertyPosition in propertyPositions)
                {
                    if (propertiesOfT.Any(x => x.Name.Contains(propertyPosition.Key)))
                    {
                        var propertyInfo = type.GetType().GetProperty(propertyPosition.Key);
                        var value = propertyInfo.GetValue(type);
                        AppendDataToAssignedPosition(stringBuilder, propertyPosition.Value, value?.ToString() ?? string.Empty);
                    }
                    else
                    {
                        AppendDataToAssignedPosition(stringBuilder, propertyPosition.Value, string.Empty);
                    }
                }
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        private bool PositionRangesOverlap()
        {
            var previousTo = 0;
            foreach (var position in propertyPositions.Values)
            {
                if (position.From < previousTo)
                    return true;
                previousTo = position.To;
            }
            return false;
        }

        private string GetNextBlankStringVariableName()
        {
            var result = propertyPositions.Where(x => x.Key.Contains(BlankString)).OrderByDescending(x => x.Key).FirstOrDefault().Key;
            if (result == null)
                return $"{BlankString}1";

            var highestBlankStringValueAsInt = Convert.ToInt32(result.Replace(BlankString, string.Empty));
            highestBlankStringValueAsInt++;
            return $"{BlankString}{highestBlankStringValueAsInt}";
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
