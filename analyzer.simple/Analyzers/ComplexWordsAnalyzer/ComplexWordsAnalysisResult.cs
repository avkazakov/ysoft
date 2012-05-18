using System.Collections.Generic;
using System.Text;
using Analyzer.Simple.Analyzers.ComplexWordsAnalyzer;

namespace Analyzer.Simple
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
			sb.AppendFormatLine("Number of words: {0}", wordsCount);
			sb.AppendLine("Top 10 most used words in the text: ");
			foreach(WordsFrequency wf in top)
				sb.AppendFormatLine("{0} \t [used {1} times]", wf.Word, wf.Frequency);
			return sb.ToString();
		}

		private readonly long wordsCount;
		private readonly List<WordsFrequency> top;
	}
}