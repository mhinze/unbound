using Machine.Specifications;

namespace Unbound.Tests.Unbinding
{
	public class complex_arrays : unbinding<complex_arrays.Foo[]>
	{
		static readonly Foo[] _request = new[]
		                                 	{
		                                 		new Foo
		                                 			{
		                                 				Text = "Something",
		                                 				Bars = new[]
		                                 				       	{
		                                 				       		new Foo.Bar {Digit = 8},
		                                 				       		new Foo.Bar {Digit = 15},
		                                 				       	}
		                                 			},
		                                 		new Foo
		                                 			{
		                                 				Text = "Another",
		                                 				Bars = new[]
		                                 				       	{
		                                 				       		new Foo.Bar {Digit = 23},
		                                 				       		new Foo.Bar {Digit = 42},
		                                 				       	}
		                                 			},
		                                 	};

		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Request = _request;
		                    	};

		It should_bind_all_child_values = () =>
		                                  	{
		                                  		var foo = (Foo[]) Bound;
		                                  		foo[1].Bars.Length.ShouldEqual(2);
		                                  		foo[1].Bars[0].Digit.ShouldEqual(23);
		                                  		foo[1].Bars[1].Digit.ShouldEqual(42);
		                                  	};

		It should_bind_child_values = () =>
		                              	{
		                              		var foo = (Foo[]) Bound;
		                              		foo[0].Bars.Length.ShouldEqual(2);
		                              		foo[0].Bars[0].Digit.ShouldEqual(8);
		                              		foo[0].Bars[1].Digit.ShouldEqual(15);
		                              	};

		It should_bind_parent_values = () =>
		                               	{
		                               		var foo = (Foo[]) Bound;
		                               		foo.Length.ShouldEqual(2);
		                               		foo[0].Text.ShouldEqual("Something");
		                               		foo[1].Text.ShouldEqual("Another");
		                               	};

		It should_bind_type = () => Bound.ShouldBeOfType<Foo[]>();

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