using System;
using System.Collections.Generic;
using System.Linq;

namespace Unbound.Performance
{
	public class PerformanceFixture
	{
		public static void Main()
		{
//			Big[] graph = BigBuilder.BuildGraph();
//			Console.WriteLine("build graph");
//			new Unbinder().Unbind(graph, "baz");

			new Unbinder().Unbind(Enumerable.Range(int.MinValue, int.MaxValue), "foo");
		}
	}

	public static class BigBuilder
	{
		public static Big[] BuildGraph()
		{
			return buildGraph().Take(100).ToArray();
		}

		static IEnumerable<Big> buildGraph()
		{
			while (true)
			{
				yield return new Big
				             	{
				             		Children = children().Take(1000).ToArray(),
				             		GrandChildren = grandchildren().Take(1000).ToArray()
				             	};
			}
		}

		static IEnumerable<Big.GrandChild> grandchildren()
		{
			while (true)
			{
				yield return new Big.GrandChild {Text = new string('x', 500),};
			}
		}

		static IEnumerable<Big.Child> children()
		{
			while (true)
			{
				yield return new Big.Child {Number = 123};
			}
		}
	}

	public class Big
	{
		public Child[] Children { get; set; }
		public GrandChild[] GrandChildren { get; set; }

		public class Child
		{
			public int Number { get; set; }
		}

		public class GrandChild
		{
			public string Text { get; set; }
		}
	}
}