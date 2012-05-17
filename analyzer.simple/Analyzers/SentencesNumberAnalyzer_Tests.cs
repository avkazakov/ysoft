using System.Diagnostics.Contracts;
using NUnit.Framework;

namespace Analyzer.Simple
{
	sealed internal partial class SentencesNumberAnalyzer
	{
		[TestFixture]
		internal class SentencesNumberAnalyzer_Tests
		{
			[Test]
			public void Test_SentencesCalculation_Trivial()
			{
				Check("Hello", 1);
				Check("Hello.", 1);
				Check("Hello!", 1);
				Check("Hello?", 1);

				Check("Hello World", 1);
				Check("Hello World.", 1);
				Check("Hello World!", 1);
				Check("Hello World?", 1);

				Check("Hello World. I'm here", 2);
				Check("Hello World? I'm here!", 2);
				Check("Hello World! I'm here?", 2);
			}

			[Test]
			public void Test_SentencesCalculation_Abbreviations_1()
			{
				Check("Hello Mr. Smith", 1);
				Check("Hello Mr. Smith.", 1);
				Check("Hello Mr. Smith.\r\n", 1);
				Check("Hello Mr. Smith!", 1);
				Check("Hello Mr. Smith!\r\n", 1);
				Check("\r\n Hello Mr. Smith!\r\n", 1);
				Check("Hello Mr. Smith?", 1);

				Check("Hello Mr. Smith. Do you like Dr. House?", 2);
				Check("Hello Mr. Smith. Do you like Dr. House?", 2);
				Check("Hello Mr. Smith! Do you like Dr. House?", 2);
				Check("Hello Mr. Smith? Do you like Dr. House?", 2);
			}

			[Test]
			public void Test_SentencesCalculation_Abbreviations_2()
			{
				Check("Philip K. Dick wrote the novel Lies", 1);
				Check("Mr. Philip K. Dick wrote the novel Lies for Dr. House\r\n", 1);
				Check("Mr. Philip K. Dick wrote the novel Lies for House M.D.", 1);
				Check("Mr. Philip K. Dick wrote the novel Lies for House M.D.\r\n", 1);
			}

			private void Check(string text, int wordsExpected)
			{
				var analyzer = new SentencesNumberAnalyzer();
				foreach(char c in text.ToLower()) // each char in lowcase, such a contract
					analyzer.TreatChar(c);
				analyzer.Finish();
				Assert.AreEqual(wordsExpected, analyzer.sentences);
			}
		}
	}
}