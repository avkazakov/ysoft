using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Analyzer.Simple.Analyzers.ComplexWordsAnalyzer
{
	[TestFixture]
	public class TopSelector_Tests
	{
		[Test]
		public void Test_Simple_Asc()
		{
			var ts = new TopSelector();
			int max = 100;
			var input = new SimpleTestInput(max);
			List<WordsFrequency> top = ts.CalcTop(input);
			int expected = max;
			foreach(WordsFrequency freq in top)
			{
				Console.Out.WriteLine(freq);
				Assert.AreEqual(expected, freq.Frequency);
				expected--;
			}
		}

		[Test]
		public void Test_Simple_Desc()
		{
			var ts = new TopSelector();
			int max = 100;
			var input = new SimpleTestInput(max);
			List<WordsFrequency> top = ts.CalcTop(input);
			int expected = max;
			foreach(WordsFrequency freq in top)
			{
				Console.Out.WriteLine(freq);
				Assert.AreEqual(expected, freq.Frequency);
				expected--;
			}
		}

		[Test]
		public void Test_Complex()
		{
			for(int i = 0; i < 100; i++)
			{
				var rnd = new Random();
				int count = rnd.Next(11, 1000);
				var input = new RndTestInput(count);
				var ts = new TopSelector();
				List<WordsFrequency> top = ts.CalcTop(input);
				Assert.AreEqual(TopSelector.TOP_SIZE, top.Count);
				foreach(WordsFrequency freq in top)
					Console.Out.WriteLine(freq);
				int[] frequences = input.Statistic.Select(pair => pair.Value).ToList().OrderByDescending(j => j).ToArray();
				int k = 0;
				foreach(WordsFrequency freq in top)
				{
					Console.Out.WriteLine(k);
					Console.Out.WriteLine(freq);
					Assert.AreEqual(frequences[k], freq.Frequency, input.Statistic.First(p => p.Value == frequences[k]).ToString());
					k++;
				}
			}
		}

		private class SimpleTestInput : IChunkReader
		{
			public SimpleTestInput(int count, bool desc = false)
			{
				this.count = count;
				this.desc = desc;
			}

			public string MoveNextLine()
			{
				if(curr == count)
				{
					CurrentLine = null;
					return null;
				}
				Next();
				return CurrentLine;
			}

			public string CurrentLine { get; set; }

			private void Next()
			{
				curr++;
				int freq = desc ? count - curr : curr;
				CurrentLine = "word" + curr + " " + freq;
			}

			private int curr;
			private readonly int count;
			private readonly bool desc;
		}

		private class RndTestInput : IChunkReader
		{
			public RndTestInput(int count)
			{
				this.count = count;
			}

			public string MoveNextLine()
			{
				if(curr == count)
				{
					CurrentLine = null;
					return null;
				}
				Next();
				return CurrentLine;
			}

			public string CurrentLine { get; set; }

			public readonly Dictionary<int, int> Statistic = new Dictionary<int, int>();

			private void Next()
			{
				if(rnd.Next(0, 100) < 80)
					curr++;
				int key = curr;
				int freq = rnd.Next(0, 100);
				CurrentLine = "word" + key + " " + freq;
				if(!Statistic.ContainsKey(key))
					Statistic.Add(key, freq);
				else
					Statistic[key] += freq;
			}

			private int curr;
			private readonly int count;
			private readonly Random rnd = new Random();
		}
	}
}