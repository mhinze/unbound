using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	[Subject(typeof(DefaultModelBinder))]
	public class complex_arrays : model_binding<complex_arrays.Foo[]>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Dictionary = new Dictionary<string, string>
		                    		             	{
		                    		             		{"foo[0].Text", "Something"},
		                    		             		{"foo[0].Bars[0].Digit", "8"},
		                    		             		{"foo[0].Bars[1].Digit", "15"},
		                    		             		{"foo[1].Text", "Another"},
		                    		             		{"foo[1].Bars[0].Digit", "23"},
		                    		             		{"foo[1].Bars[1].Digit", "42"},
		                    		             	};
		                    	};

		It should_bind_elements = () =>
		                          	{
		                          		var foo = (Foo[])Bound;
		                          		foo.Length.ShouldEqual(2);
		                          		foo[0].Bars.Length.ShouldEqual(2);
		                          		foo[1].Bars.Length.ShouldEqual(2);
		                          		foo[0].Text.ShouldEqual("Something");
		                          		foo[0].Bars[0].Digit.ShouldEqual(8);
		                          		foo[0].Bars[1].Digit.ShouldEqual(15);
		                          		foo[1].Text.ShouldEqual("Another");
		                          		foo[1].Bars[0].Digit.ShouldEqual(23);
		                          		foo[1].Bars[1].Digit.ShouldEqual(42);
		                          	};

		It should_bind_to_type = () => Bound.ShouldBeOfType<Foo[]>();

		public class Foo
		{
			public string Text { get; set; }
			public Bar[] Bars { get; set; }
			public class Bar
			{
				public int Digit { get; set; }
			}
		}
	}
}