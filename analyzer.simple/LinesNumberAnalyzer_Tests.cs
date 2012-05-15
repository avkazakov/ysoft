using NUnit.Framework;

namespace Analyzer.Simple
{
	partial class LinesNumberAnalyzer 
	{
		[TestFixture]
		internal class LinesNumberAnalyzer_Tests
		{
			[Test]
			public void Test_LinesCalculation()
			{
				Check("", 1);
				Check("\n", 1);
				Check("\r", 1);
				Check("\r\n", 1);
				Check("\r\r", 2);
				Check("\n\r", 2);
				Check("\n\n", 2);
				Check("first line \r second line \n third line", 3);
				Check("first line \r second line \n third line \r\n", 3);
				Check("first line \r second line \n third line \r\n 4 line \r 5 line \n 6 line", 6);
				Check("first line \r second line \n third line \r\n 4 line \r 5 line \n 6 line \n", 6);
				Check("first line \r second line \n third line \r\n 4 line \r 5 line \n 6 line \r", 6);
				Check("first line \r second line \n third line \r\n 4 line \r 5 line \n 6 line \r\n", 6);
			}

			private void Check(string text, int linesExpected)
			{
				var analyzer = new LinesNumberAnalyzer();
				foreach(char c in text)
					analyzer.TreatChar(c);
				analyzer.Finish();
				Assert.AreEqual(linesExpected, analyzer.lines);
			}
		}
	}
}