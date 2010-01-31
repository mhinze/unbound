using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	[Subject(typeof (DefaultModelBinder))]
	public class arrays : model_binding<int[]>
	{
		Establish context = () =>
		                    	{
		                    		ModelName = "foo";
		                    		Dictionary = new Dictionary<string, string>
		                    		             	{
		                    		             		{"foo[0]", "4"},
		                    		             		{"foo[1]", "8"},
		                    		             		{"foo[2]", "15"},
		                    		             		{"foo[3]", "16"},
		                    		             		{"foo[4]", "23"},
		                    		             		{"foo[5]", "42"},
		                    		             	};
		                    	};

		It should_bind_elements = () =>
		                          	{
		                          		var array = (int[]) Bound;
		                          		array.Length.ShouldEqual(6);
		                          		array[0].ShouldEqual(4);
		                          		array[1].ShouldEqual(8);
		                          		array[2].ShouldEqual(15);
		                          		array[3].ShouldEqual(16);
		                          		array[4].ShouldEqual(23);
		                          		array[5].ShouldEqual(42);
		                          	};

		It should_bind_to_type = () => Bound.ShouldBeOfType<int[]>();
	}
}