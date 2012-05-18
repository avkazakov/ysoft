using System.Collections.Generic;

namespace Analyzer.Core
{
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
}