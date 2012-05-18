using System.Collections.Generic;
using System.Linq;

namespace Analyzer.Simple.Analyzers.ComplexWordsAnalyzer
{
	public class TopSelector
	{
		public TopSelector()
		{
			top = new LinkedList<WordsFrequency>();
		}

		/// <summary>
		/// All lines in input should be already sorted
		/// </summary>
		/// <param name="input"></param>
		public List<WordsFrequency> CalcTop(IChunkReader input)
		{
			string line;
			while((line = input.MoveNextLine()) != null)
			{
				curr = WordsFrequency.Parse(line);
				if (prev != null && prev.Word != curr.Word)
				{
					Handle(prev);
					prev = curr;
				}
				else
				{
					if (prev == null)
						prev = curr;
					else
						prev.Frequency += curr.Frequency;
				}
			}
			Handle(prev);
			return top.ToList();
		}

		private void Handle(WordsFrequency freq)
		{
			if (top.Count == 0)
			{
				top.AddFirst(freq);
				return;
			}
			if (freq.Frequency <= top.Last.Value.Frequency)
			{
				if (top.Count < TOP_SIZE)
					top.AddLast(freq);
				return;
			}
			var node = top.Last;
			while (node != null && freq.Frequency > node.Value.Frequency)
				node = node.Previous;
			if (node == null)
				top.AddFirst(freq);
			else
				top.AddAfter(node, freq);
			if (top.Count > TOP_SIZE)
				top.RemoveLast();
		}

		private WordsFrequency prev;
		private WordsFrequency curr;

		private readonly LinkedList<WordsFrequency> top;
		public const int TOP_SIZE = 10;
	}
}