using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	[Subject(typeof (DefaultModelBinder))]
	public class simple : model_binding<int>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Dictionary = new Dictionary<string, string> {{"foo", "42"}};
		                    	};

		It should_bind_value = () => Bound.ShouldEqual(42);
	}
}