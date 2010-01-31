using Machine.Specifications;

namespace Unbound.Tests.Unbinding
{
	public class simple_arrays : unbinding<int[]>
	{
		static readonly int[] _request = new[] {4, 8, 15, 16, 23, 42};

		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Request = _request;
		                    	};

		It should_bind_type = () => Bound.ShouldBeOfType<int[]>();
		It should_bind_values = () => Bound.ShouldEqual(_request);
	}
}