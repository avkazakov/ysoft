using System.Linq;

namespace Analyzer.Core.Analyzers
{
	public class AnalyzerSet : IAnalyzer
	{
		public AnalyzerSet(params IAnalyzer[] analyzers)
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

		public IAnalysisResult Finish()
		{
			return new ComposedResult(analyzers.Select(a => a.Finish()).ToArray());
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