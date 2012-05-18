using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Analyzer.Core
{
	[TestFixture]
	internal class Extensions_Tests
	{
		[Test]
		public void Test_List_ElementwiseHash_1()
		{
			var list1 = new List<int>(){1,2,3,4,5};
			int hash1 = list1.ElementwiseHash();
			int hash2 = list1.ElementwiseHash();
			list1.Add(5);
			int hash3 = list1.ElementwiseHash();

			Console.Out.WriteLine(hash1);
			Console.Out.WriteLine(hash3);

			Assert.AreEqual(hash1, hash2);
			Assert.AreNotEqual(hash1, hash3);
		}

		[Test]
		public void Test_List_ElementwiseHash_2()
		{
			var list1 = new List<int>(){1,2,3,4,5};
			var list2 = new List<int>(){1,2,3,4,5};
			int hash1 = list1.ElementwiseHash();
			int hash2 = list2.ElementwiseHash();

			Console.Out.WriteLine(hash1);
			Console.Out.WriteLine(hash2);

			Assert.AreEqual(hash1, hash2);
		}

		[Test]
		public void Test_List_ElementwiseHash_3()
		{
			var list1 = new List<int>(){1,2,3,4,5};
			var list2 = new List<int>(){1,2,3,4};
			int hash1 = list1.ElementwiseHash();
			int hash2 = list2.ElementwiseHash();

			Console.Out.WriteLine(hash1);
			Console.Out.WriteLine(hash2);

			Assert.AreNotEqual(hash1, hash2);
		}

		[Test]
		public void Test_List_ElementwiseHash_4()
		{
			var list1 = new List<int>(){};
			int hash1 = list1.ElementwiseHash();

			Console.Out.WriteLine(hash1);
		}
	}
}