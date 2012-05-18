using System.Globalization;

namespace Analyzer.Core.Analyzers
{
	class SimpleAnalysisResult : IAnalysisResult
	{
		public SimpleAnalysisResult(long numer, string numMeaning)
		{
			this.numer = numer;
			this.numMeaning = numMeaning;
		}

		public string ToHumanReadableText()
		{
			return string.Format("{0} : {1}", numMeaning, numer.ToString("N0", CultureInfo.InvariantCulture));
		}

		private readonly long numer;
		private readonly string numMeaning;
	}
}