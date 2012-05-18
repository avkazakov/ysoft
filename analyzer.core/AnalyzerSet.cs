using Analyzer.Core.Analyzers;

namespace Analyzer.Core
{
	public class AnalyzerSet
	{
		public AnalyzerSet(IAnalyzer[] analyzers)
		{
			this.analyzers = analyzers;
		}

		public void TreatChar(char c)
		{
			for(int i = 0; i < analyzers.Length; i++)
			{
				analyzers[i].TreatChar(c);
			}
		}
		public IAnalysisResult[] GetResult()
		{
			var res = new IAnalysisResult[analyzers.Length];
			for(int i = 0; i < analyzers.Length; i++)
			{
				res[i] = analyzers[i].Finish();
			}
			return res;
		}

		private readonly IAnalyzer[] analyzers;
	}
}