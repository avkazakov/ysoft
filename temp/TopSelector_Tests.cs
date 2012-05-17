using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace temp
{
	[TestFixture]
	public class TopSelector_Tests
	{
		[Test]
		public void Test_Simple()
		{
			var	ts = new TopSelector();
			int max = 100;
			var input = new SimpleTestInput(max);
			ts.Do(input);

			var top = ts.Top;

			int expected = max;
			foreach(Freq freq in top)
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
				int count = rnd.Next(10, 1000);
				var input = new RndTestInput(count);
				var	ts = new TopSelector();
				ts.Do(input);

				var top = ts.Top;

				Assert.AreEqual(TopSelector.TOP_SIZE, top.Count);

				foreach(Freq freq in top)
					Console.Out.WriteLine(freq);

				var frequences = input.Statistic.Select(pair => pair.Value).ToList().OrderByDescending(j => j).ToArray();

				int k = 0;
				foreach(Freq freq in top)
				{
					Console.Out.WriteLine(k);
					Console.Out.WriteLine(freq);
					Assert.AreEqual(frequences[k], freq.Frequency);
					k++;
				}
			}
		}

		class SimpleTestInput : IInput
		{
			public SimpleTestInput(int count)
			{
				this.count = count;
				curr++;
				Current = "word" + curr + " " + curr;
			}

			public string MoveNext()
			{
				if (curr == count)
				{
					Current = null;
					return null;
				}
				curr++;
				Current = "word" + curr + " " + curr;
				return Current;
			}

			public string Current { get; set; }

			private int curr = 0;
			private readonly int count;
		}

		class RndTestInput : IInput
		{
			public RndTestInput(int count)
			{
				this.count = count;
				Next();
			}

			public string MoveNext()
			{
				if (curr == count)
				{
					Current = null;
					return null;
				}
				Next();
				return Current;
			}

			private void Next()
			{
				if (rnd.Next(0, 100) < 80)
					curr++;
				var key = curr;
				var freq = rnd.Next(0, 100);
				Current = "word" + key + " " + freq;
				if (!Statistic.ContainsKey(key))
					Statistic.Add(key, freq);
				else
					Statistic[key] += freq;
			}

			public string Current { get; set; }

			public Dictionary<int, int> Statistic = new Dictionary<int, int>();

			private int curr = 0;
			private readonly int count;
			private Random rnd = new Random();
		}
	}
}