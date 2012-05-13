using System;
using System.Collections.Generic;

namespace temp
{
	internal class Comparer : IComparer<char[]>, IComparer<byte[]>
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
	}
}