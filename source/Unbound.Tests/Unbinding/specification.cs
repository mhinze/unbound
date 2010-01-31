using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;
using Unbound.Tests.Unbinding.Configuration;

namespace Unbound.Tests.Unbinding
{
	public class FooUnbinder : ISpecificValueUnbinder
	{
		public string UnbindValue(object value)
		{
			return ((specification.Foo) value).Id.ToString();
		}

		public bool AppropriatelyUnbinds(object value)
		{
			return value is specification.Foo;
		}
	}

	public class specification
	{
		protected static DefaultModelBinder Binder = new DefaultModelBinder();
		protected static object Bound;
		protected static string ModelName;
		protected static Type ModelType;
		protected static object Request;
		protected static IDictionary<string, string> Unbound;

		Establish context = () =>
		                    	{
		                    		ModelType = typeof (int);
		                    		Request = new Foo {Id = 3, Text = "foo"};
		                    		ModelName = "foo";
		                    	};

		Because of = () =>
		             	{
		             		Unbound = Request.ToHttpDictionary(ModelName);
		             		var bindingContext = Unbound.BuildContext(ModelType, ModelName);
		             		Bound = Binder.BindModel(new ControllerContext(), bindingContext);
		             	};

		It should_bind_value = () => Bound.ShouldEqual(3);

		public class Foo
		{
			public int Id { get; set; }
			public string Text { get; set; }
		}
	}
}