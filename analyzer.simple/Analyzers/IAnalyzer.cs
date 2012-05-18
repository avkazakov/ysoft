namespace Analyzer.Simple.Analyzers
{
	public interface IAnalyzer
	{
		//TODO: contract for c as 'lowcase'
		void TreatChar(char c);
		IAnalysisResult Finish();
	}
}