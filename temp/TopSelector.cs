using System.Collections.Generic;

namespace temp
{
	public partial class TopSelector
	{
		public TopSelector()
		{
			top = new LinkedList<Freq>();
		}

		/// <summary>
		/// All lines in input are already sorted by words
		/// </summary>
		/// <param name="input"></param>
		public void Do(IInput input)
		{
			string line;
			while((line = input.MoveNext()) != null)
			{
				curr = Freq.Parse(line);
				if (prev != null && prev.Word != curr.Word)
				{
					Add(prev);
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
			Add(prev);
		}

		private void Add(Freq freq)
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

		public LinkedList<Freq> Top { get { return top; } }

		private Freq prev;
		private Freq curr;

		private readonly LinkedList<Freq> top;
		public const int TOP_SIZE = 10;
	}
}