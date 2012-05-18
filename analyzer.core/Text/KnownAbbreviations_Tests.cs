using System;
using NUnit.Framework;

namespace Analyzer.Core.Text
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
}