using Machine.Specifications;

namespace Unbound.Tests.Unbinding
{
	public class complexer : unbinding<complexer.Foo>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Request = new Foo
		                    		          	{
		                    		          		Number = 42,
		                    		          		Text = "Bar",
		                    		          		Barf = new Foo.Bar
		                    		          		       	{
		                    		          		       		Digit = 12
		                    		          		       	}
		                    		          	};
		                    	};

		It should_bind_complexer_values = () => ((Foo) Bound).Barf.Digit.ShouldEqual(12);

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
			public Bar Barf { get; set; }

			public class Bar
			{
				public int Digit { get; set; }
			}
		}
	}
}