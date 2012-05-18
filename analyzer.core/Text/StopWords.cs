using System;
using System.Collections.Generic;

namespace Analyzer.Core.Text
{
	public static class StopWords
	{
		static StopWords()
		{
			foreach(string stopWord in stopWordsArr)
				stopWords.Add(new CharRange(stopWord.ToCharArray(), stopWord.Length));
		}

		public static bool Contains(char[] wordBuff, int wordLen)
		{
			return stopWords.Contains(new CharRange(wordBuff, wordLen));
		}

		private static readonly String[] stopWordsArr = new[] {"a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with"};

		private static readonly HashSet<CharRange> stopWords = new HashSet<CharRange>(new CharRangeComparer());
	}
}