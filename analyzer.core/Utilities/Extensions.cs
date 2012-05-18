using System.Collections.Generic;
using System.Text;

namespace Analyzer.Core.Utilities
{
	internal static class Extensions
	{
		public static int ElementwiseHash<T>(this IList<T> list)
		{
			return list.ElementwiseHash(list.Count);
		}

		public static int ElementwiseHash<T>(this IList<T> list, int len)
		{
			unchecked
			{
				int result = len;
				for(int i = 0; i < len; i++)
				{
					T item = list[i];
					result = (result * 397) ^ item.GetHashCode();
				}
				return result;
			}
		}

		public static void AppendFormatLine(this StringBuilder stringBuilder, string format, params object[] parameters)
		{
			stringBuilder.AppendLine(string.Format(format, parameters));
		}
	}
}