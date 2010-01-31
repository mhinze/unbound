using Machine.Specifications;

namespace Unbound.Tests.Unbinding
{
	public class simple : unbinding<int>
	{
		Establish context = () =>
		                    	{
		                    		Request = 42;
		                    		ModelName = "foo";
		                    	};

		It should_bind_type = () => Bound.ShouldBeOfType<int>();
		It should_bind_value = () => Bound.ShouldEqual(42);
	}

	public class FooUnbinder : ISpecificValueUnbinder
	{
		public string UnbindValue(object value)
		{
			return ((specification.Foo) value).Id.ToString();
		}

		public bool IsSatisfiedBy(object value)
		{
			return value is specification.Foo;
		}
	}
}