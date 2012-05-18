namespace Analyzer.Core.Analyzers
{
	internal sealed partial class SentencesNumberAnalyzer : IAnalyzer
	{
		public void TreatChar(char c)
		{
			if (char.IsLetterOrDigit(c))
			{
				currWord[currWordLen] = c;
				currWordLen++;
				//				if (currWordLen == 0) //new word found
			}
			else if (c == '.') 
			{
				if (!KnownAbbreviations.Contains(currWord, currWordLen)
					&& currWordLen != 1) //to handle cases like "K. James" or "House, M.D."
				{
					sentences++;
					missedDot = false;
				}
				else
					missedDot = true;
				currWordLen = 0;
			}
			else if (c == '!' || c == '?' || c == '…')
			{
				sentences++;
				currWordLen = 0;
				missedDot = false;
			}
			else
				currWordLen = 0;
		}

		public IAnalysisResult Finish()
		{
			if (currWordLen != 0 || missedDot)
				sentences++;
			return new SimpleAnalysisResult(sentences, "Number of sentences");
		}

		private bool missedDot = false; // to handle cases when dot in the end of the sentance is ignored as the part of abbreviation
		private int currWordLen = 0;
		private readonly char[] currWord = new char[Const.MAX_WORD_LEN];

		private long sentences = 0;
	}
}