using NUnit.Framework;

namespace Analyzer.Core.Analyzers
{
	public sealed partial class CharsNumberAnalyzer
	{
		[TestFixture]
		internal class CharsNumberAnalyzer_Tests
		{
			[Test]
			public void Test_CharactersCalculation()
			{
				Check("", 0);
				Check("\n", 1);
				Check("\r\n", 2);
				Check("\r	\t\r,.", 6);
				Check("    ", 4);
				Check("Вотэбьютифллайф", 15);
				Check("«»…¹²³$‰↑∞←−—→", 14);
			}

			private void Check(string text, int wordsExpected)
			{
				var analyzer = new CharsNumberAnalyzer();
				foreach(char c in text)
					analyzer.TreatChar(c);
				analyzer.Finish();
				Assert.AreEqual(wordsExpected, analyzer.chars);
			}
		}
	}
}