using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	[Subject(typeof (DefaultModelBinder))]
	public class complexer : model_binding<complexer.Foo>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Dictionary = new Dictionary<string, string>
		                    		             	{
		                    		             		{"foo.Number", "42"},
		                    		             		{"foo.Text", "Bar"},
		                    		             		{"foo.Barf.Digit", "12"}
		                    		             	};
		                    	};

		It should_bind_to_type = () => Bound.ShouldBeOfType<Foo>();

		It should_bind_values = () =>
		                        	{
		                        		((Foo) Bound).Number.ShouldEqual(42);
		                        		((Foo) Bound).Text.ShouldEqual("Bar");
		                        		((Foo) Bound).Barf.Digit.ShouldEqual(12);
		                        	};

		public class Foo
		{
			public int Number { get; set; }
			public string Text { get; set; }
			public Bar Barf { get; set; }

			public class Bar
			{
				public int Digit { get; set; }
			}
		}
	}
}