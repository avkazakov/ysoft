using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	internal class ComplexWordsAnalysisResult : IAnalysisResult
	{
		public ComplexWordsAnalysisResult(long wordsCount, List<WordsFrequency> top)
		{
			this.wordsCount = wordsCount;
			this.top = top;
		}

		public string ToHumanReadableText()
		{
			var sb = new StringBuilder();
			sb.AppendFormatLine("Number of words: {0}", wordsCount.ToString("N0", CultureInfo.InvariantCulture));
			sb.AppendLine("Top 10 most used words in the text: ");
			foreach(WordsFrequency wf in top)
				sb.AppendFormatLine("\t{0} \t [used {1} times]", wf.Word, wf.Frequency.ToString("N0", CultureInfo.InvariantCulture));
			return sb.ToString();
		}

		private readonly long wordsCount;
		private readonly List<WordsFrequency> top;
	}
}