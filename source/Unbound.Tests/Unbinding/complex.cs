using System.Collections.Generic;
using Machine.Specifications;

namespace Unbound.Tests.Unbinding
{
	public class complex : unbinding<complex.Foo>
	{
		Establish context = () =>
		                    	{
		                    		Request = new Foo {Number = 42, Text = "Something"};
		                    		ModelName = "foo";
		                    	};

		It should_bind_type = () => Bound.ShouldBeOfType<Foo>();

		It should_bind_values = () =>
		                        	{
		                        		((Foo) Bound).Number.ShouldEqual(42);
		                        		((Foo) Bound).Text.ShouldEqual("Something");
		                        	};

		public class Foo
		{
			public int Number { get; set; }
			public string Text { get; set; }
		}
	}
}