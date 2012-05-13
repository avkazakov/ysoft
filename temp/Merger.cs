using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace temp
{
	public class Pair
	{
		public Pair(int inputIndex, string key)
		{
			InputIndex = inputIndex;
			Key = key;
		}

		public string Key;
		public int InputIndex;
	}

	public class Merger
	{
		private void AddToList(LinkedList<Pair> list, Pair pair)
		{
			if (list.Count == 0)
			{
				list.AddFirst(pair);
				return;
			}
			LinkedListNode<Pair> node = list.Last;
			while(node != null)
			{
				 if (String.CompareOrdinal(pair.Key, node.Value.Key) >= 0)
				{
					list.AddAfter(node, pair);
					return;
				}
				node = node.Previous;
			}
			list.AddFirst(pair);
		}

		public void Merge(IInput[] inputs, Stream output)
		{
			var list = new LinkedList<Pair>();

			for(int i = 0; i < inputs.Length; i++)
			{
				string current = inputs[i].Current;
				if (current == null)
					continue;
				AddToList(list, new Pair(i, current));
			}

			while(list.Count > 0)
			{
				Pair pair = list.First.Value;
				list.RemoveFirst();

				string min = pair.Key;

				var count = Encoding.UTF8.GetBytes(min, 0, min.Length, buf, 0);
				output.Write(buf, 0, count);
				output.WriteByte(Chars.EOL);

				string current = inputs[pair.InputIndex].MoveNext();

				if (current == null)
					continue;
				pair.Key = current;
				AddToList(list, pair);
			}
		}
		
		private Comparer comparer = new Comparer();
		private byte[] buf = new byte[1 * 1024 * 1024]; // in the real life there is no words longer then 200 KB (© wikipedia)
	}
}