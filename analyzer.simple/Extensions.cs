using System.Collections.Generic;
using System.Linq;

namespace Analyzer.Simple
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
	}
}