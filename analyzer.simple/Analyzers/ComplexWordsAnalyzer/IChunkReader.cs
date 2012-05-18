namespace Analyzer.Simple.Analyzers.ComplexWordsAnalyzer
{
	public interface IChunkReader
	{
		string MoveNextLine();
		string CurrentLine { get; }
	}

}