namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	public interface IChunkReader
	{
		string MoveNextLine();
		string CurrentLine { get; }
	}

}