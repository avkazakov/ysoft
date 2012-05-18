namespace Analyzer.Simple.Analyzers.ComplexWordsAnalyzer
{
	public class WordsFrequency
	{
		public WordsFrequency(string word, int frequency)
		{
			Word = word;
			Frequency = frequency;
		}

		// input: "some_word 312"
		public static WordsFrequency Parse(string input)
		{
			int spaceIndex = input.IndexOf(' ');
			int freq = int.Parse(input.Substring(spaceIndex + 1));
			return new WordsFrequency(input.Substring(0, spaceIndex), freq);
		}

		public override string ToString()
		{
			return string.Format("Word: {0}, Frequency: {1}", Word, Frequency);
		}

		public string Word;
		public int Frequency;
	}
}