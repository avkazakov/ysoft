namespace Analyzer.Core.Analyzers
{
	sealed partial class WordsNumberAnalyzer : IAnalyzer
	{
		public void TreatChar(char c)
		{
			if (char.IsLetter(c))
			{
				if (!currentIsWord) //new word started
				{
					words++;
				}
				currentIsWord = true;
			}
			else
				currentIsWord = false;
		}

		public IAnalysisResult Finish()
		{
			return new SimpleAnalysisResult(words, "Number of words");
		}

		private bool currentIsWord;
		private long words = 0;
	}
}