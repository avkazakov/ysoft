namespace Analyzer.Simple
{
	public interface IAnalyzer
	{
		void TreatChar(char c);
		IAnalysisResult Finish();
	}
}