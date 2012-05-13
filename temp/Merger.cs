using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace temp
{
	public class Merger
	{
		public void Merge(IInput[] inputs, Stream output)
		{
//			var dic = new SortedDictionary<string, int>();
//			dic.Keys.First();
			while(true)
			{
				string min = null;
				int minIndex = -1;
				for(int i = 0; i < inputs.Length; i++)
				{
					string current = inputs[i].Current;
					if (current == null)
						continue;
					if (min == null)
					{
						min = current;
						minIndex = i;
					}
					else if (String.CompareOrdinal(current, min) < 0)
					{
						min = current;
						minIndex = i;
					}
				}
				if (min == null)
					return;

				var count = Encoding.UTF8.GetBytes(min, 0, min.Length, buf, 0);
				output.Write(buf, 0, count);
				output.WriteByte(Chars.EOL);

				inputs[minIndex].MoveNext();
			}
		}
		
		private byte[] buf = new byte[1 * 1024 * 1024]; // in the real life there is no words longer then 200 KB (© wikipedia)
	}
}