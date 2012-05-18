using System;
using System.Collections.Generic;

namespace Analyzer.Core.Analyzers.ComplexWordsAnalyzer
{
	public class ChunksMerger
	{
		public void Merge(IChunkReader[] chunkReaders, IChunkWriter output)
		{
			var list = new LinkedList<Pair>();
			for(int i = 0; i < chunkReaders.Length; i++)
			{
				string current = chunkReaders[i].MoveNextLine();
				if(current == null)
					continue;
				AddToList(list, new Pair(i, current));
			}
			while(list.Count > 0)
			{
				Pair pair = list.First.Value;
				list.RemoveFirst();
				string line = pair.Key;
				output.WriteLine(line);
				string current = chunkReaders[pair.InputIndex].MoveNextLine();
				if(current == null)
					continue;
				pair.Key = current;
				AddToList(list, pair);
			}
		}

		private void AddToList(LinkedList<Pair> list, Pair pair)
		{
			if(list.Count == 0)
			{
				list.AddFirst(pair);
				return;
			}
			LinkedListNode<Pair> node = list.Last;
			while(node != null)
			{
				if(String.CompareOrdinal(pair.Key, node.Value.Key) >= 0)
				{
					list.AddAfter(node, pair);
					return;
				}
				node = node.Previous;
			}
			list.AddFirst(pair);
		}

		private class Pair
		{
			public Pair(int inputIndex, string key)
			{
				InputIndex = inputIndex;
				Key = key;
			}

			public string Key;
			public readonly int InputIndex;
		}
	}
}