using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	[Subject(typeof (DefaultModelBinder))]
	public class complex : model_binding<complex.Foo>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Dictionary = new Dictionary<string, string> {{"foo.Number", "42"}, {"foo.Text", "Bar"}};
		                    	};

		It should_bind_to_type = () => Bound.ShouldBeOfType<Foo>();

		It should_bind_values = () =>
		                        	{
		                        		((Foo) Bound).Number.ShouldEqual(42);
		                        		((Foo) Bound).Text.ShouldEqual("Bar");
		                        	};

		public class Foo
		{
			public int Number { get; set; }
			public string Text { get; set; }
		}
	}
}