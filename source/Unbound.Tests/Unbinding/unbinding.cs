using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;
using Unbound.Tests.Unbinding.Configuration;

namespace Unbound.Tests.Unbinding
{
	public abstract class unbinding<T>
	{
		protected static DefaultModelBinder Binder = new DefaultModelBinder();
		protected static object Bound;
		protected static string ModelName;
		protected static Type ModelType = typeof (T);
		protected static T Request;
		protected static IDictionary<string, string> Unbound;

		Because of = () =>
		             	{
		             		Unbound = Request.ToHttpDictionary(ModelName);
		             		ModelBindingContext bindingContext = Unbound.BuildContext(ModelType, ModelName);
		             		Bound = Binder.BindModel(new ControllerContext(), bindingContext);
		             	};
	}
}