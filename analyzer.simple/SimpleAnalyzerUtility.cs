using Analyzer.Core;
using Analyzer.Core.Analyzers;
using Analyzer.Core.Utilities;

namespace Analyzer.Simple
{
	class SimpleAnalyzerUtility : AnalyzerUtilityBase
	{
		static void Main(string[] args)
		{
			var p = new Parameters(args);

			if (!CheckParams(p))
				return;

			var analyzerSet = new AnalyzerSet(
											new LinesNumberAnalyzer(), 
											new WordsNumberAnalyzer(), 
											new CharsNumberAnalyzer(),
											new SentencesNumberAnalyzer());

			new FileProcessor().ProcessFile(GetFileName(p), analyzerSet);
		}
	}
}
