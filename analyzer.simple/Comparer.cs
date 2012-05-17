using System;
using System.Collections.Generic;

namespace Analyzer.Simple
{
	internal class Comparer : IComparer<char[]>, IComparer<byte[]>, IEqualityComparer<char[]>
	{
		public int Compare(char[] x, char[] y)
		{
			int min = Math.Min(x.Length, y.Length);
			for(int i = 0; i < min; i++)
			{
				if (x[i] != y[i]) return x[i] - y[i];
			}
			return x.Length - y.Length;
		}

		public int Compare(byte[] x, byte[] y)
		{
			int min = Math.Min(x.Length, y.Length);
			for(int i = 0; i < min; i++)
			{
				if (x[i] != y[i]) return x[i] - y[i];
			}
			return x.Length - y.Length;
		}

		public bool Equals(char[] x, char[] y)
		{
			return Compare(x, y) == 0;
		}

		public int GetHashCode(char[] obj)
		{
			return obj.ElementwiseHash();
		}
	}
}