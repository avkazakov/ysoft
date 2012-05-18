namespace Analyzer.Simple.Analyzers
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
			return string.Format("{0} : {1}", numMeaning, numer);
		}

		private readonly long numer;
		private readonly string numMeaning;
	}
}