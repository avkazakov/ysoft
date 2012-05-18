using System.Text;
using Analyzer.Core.Analyzers;

namespace Analyzer.Core
{
	public class ComposedResult : IAnalysisResult
	{
		public ComposedResult(IAnalysisResult[] results)
		{
			this.results = results;
		}

		public string ToHumanReadableText()
		{
			var stringBuilder = new StringBuilder();
			foreach(var result in results)
				stringBuilder.AppendLine(result.ToHumanReadableText());
			return stringBuilder.ToString();
		}

		private readonly IAnalysisResult[] results;
	}
}