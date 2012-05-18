using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Analyzer.Core
{
	internal static partial class KnownAbbreviations
	{
		[TestFixture]
		internal class KnownAbbreviations_Tests
		{
			[Test]
			public void Test_Contains()
			{
				foreach(CharRange abbr in abbreviations)
				{
					string str = new string(abbr.Buf, 0, abbr.Len);
					Test(str, true);
				}
			}

			[Test]
			public void Test_DoesnContain_1()
			{
				foreach(CharRange abbr in abbreviations)
				{
					string str = new string(abbr.Buf, 0, abbr.Len);
					Test(str + "1", false);
				}
			}

			[Test]
			public void Test_DoesnContain_2()
			{
				var rnd = new Random();
				for(int i = 0; i < 1000; i++)
				{
					Test("test" + rnd.Next(), false);
				}
			}

			private void Test(string str, bool expectedContains)
			{
				char[] chars = str.ToCharArray();
				var res = KnownAbbreviations.Contains(chars, chars.Length);
				Assert.AreEqual(expectedContains, res);

				char[] manychars = new char[chars.Length + 16];
				chars.CopyTo(manychars, 0);
				res = KnownAbbreviations.Contains(chars, chars.Length);
				Assert.AreEqual(expectedContains, res);
			}
		}
	}

	static partial class KnownAbbreviations
	{
		public static bool Contains(char[] wordBuff, int wordLen)
		{
			return abbreviations.Contains(new CharRange(wordBuff, wordLen));
		}

		static KnownAbbreviations()
		{
			AddAbbrev("mr");
			AddAbbrev("mrs");
			AddAbbrev("dr");
			AddAbbrev("prof");
			AddAbbrev("rev");
			AddAbbrev("gen");
			AddAbbrev("etc");
			AddAbbrev("ltd");
			AddAbbrev("inc");
		}

		private static void AddAbbrev(string abr)
		{
			var chars = abr.ToLower().ToCharArray();
			abbreviations.Add(new CharRange(chars, chars.Length));
		}

		private static readonly HashSet<CharRange> abbreviations = new HashSet<CharRange>(new CharRangeComparer());
	}

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

	internal struct CharRange
	{
		public CharRange(char[] buf, int len)
			: this()
		{
			Buf = buf;
			Len = len;
		}

		public char[] Buf { get; private set; }
		public int Len { get; private set; }
	}
}