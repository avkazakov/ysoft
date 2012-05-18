namespace Analyzer.Core.Analyzers
{
	/// <summary>
	/// Note the following common line feed chars: 
	/// \n - UNIX   \r\n - Win   \r - Mac 
	/// </summary>
	public sealed partial class LinesNumberAnalyzer : IAnalyzer
	{
		public void TreatChar(char c)
		{
			if(c == '\r')
			{
				lines++;
			}
			else if(c == '\n')
			{
				if(prev != '\r') // don't +lines like \r\n twice
					lines++;
			}
			prev = c;
		}

		public IAnalysisResult Finish()
		{
			if (prev != '\r' && prev != '\n')
				lines++;
			return new SimpleAnalysisResult(lines, "Number of lines");
		}

		private char prev = char.MinValue;
		private long lines;
	}
}