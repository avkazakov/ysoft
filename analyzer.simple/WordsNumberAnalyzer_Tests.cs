using NUnit.Framework;

namespace Analyzer.Simple
{
	internal partial class WordsNumberAnalyzer
	{
		[TestFixture]
		internal class WordsNumberAnalyzer_Tests
		{
			[Test]
			public void Test_WordsCalculation_NoWords()
			{
				Check("", 0);
				Check("\n", 0);
				Check(".1234$51\r", 0);
				Check("\r\n", 0);
				Check("\r\r", 0);
				Check("\n\r", 0);
				Check("\n\n", 0);
				Check("11111 2222 \r ...»« \n '&*)@&^8787*%^&*", 0);
			}

			[Test]
			public void Test_WordsCalculation()
			{
				Check("hello", 1);
				Check("hello world \n", 2);
				Check("one+two=three\r", 3);
				Check("ONE+TWO=THREE\r", 3);
				Check("To be, or not to be: that is the question:\r\n", 10);
				Check("let's have a look\r\r", 5);
			}

			private void Check(string text, int wordsExpected)
			{
				var analyzer = new WordsNumberAnalyzer();
				foreach(char c in text)
					analyzer.TreatChar(c);
				analyzer.Finish();
				Assert.AreEqual(wordsExpected, analyzer.words);
			}
		}
	}
}