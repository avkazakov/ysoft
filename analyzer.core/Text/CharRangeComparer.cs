using System;
using System.Collections.Generic;
using Analyzer.Core.Utilities;

namespace Analyzer.Core.Text
{
	internal class CharRangeComparer : IEqualityComparer<CharRange>
	{
		public bool Equals(CharRange x, CharRange y)
		{
			return Compare(x.Buf, x.Len, y.Buf, y.Len) == 0;
		}

		public int GetHashCode(CharRange range)
		{
			return range.Buf.ElementwiseHash(range.Len);
		}

		private int Compare(char[] x, int xlen, char[] y, int ylen)
		{
			int min = Math.Min(xlen, ylen);
			for(int i = 0; i < min; i++)
			{
				if(x[i] != y[i]) return x[i] - y[i];
			}
			return xlen - ylen;
		}
	}
}