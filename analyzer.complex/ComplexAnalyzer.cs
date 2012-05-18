using Analyzer.Core;
using Analyzer.Core.Analyzers;
using Analyzer.Core.Analyzers.ComplexWordsAnalyzer;
using Analyzer.Core.Utilities;

namespace Analyzer.Complex
{
	class ComplexAnalyzer : AnalyzerBase
	{
		static void Main(string[] args)
		{
			var p = new Parameters(args);

			if (!CheckParams(p))
				return;

			var analyzerSet = new AnalyzerSet(
											new LinesNumberAnalyzer(),
											new CharsNumberAnalyzer(),
											new SentencesNumberAnalyzer(),
											new WordsComplexAnalyzer());

			new FileProcessor().ProcessFile(GetFileName(p), analyzerSet);
		}
	}
}
