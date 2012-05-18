namespace Analyzer.Core.Analyzers
{
	public sealed partial class CharsNumberAnalyzer : IAnalyzer
	{
		public void TreatChar(char c)
		{
			chars++;
		}

		public IAnalysisResult Finish()
		{
			return new SimpleAnalysisResult(chars, "Number of characters");
		}

		private long chars = 0;
	}
}