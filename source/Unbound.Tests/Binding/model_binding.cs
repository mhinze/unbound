using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace Unbound.Tests.Binding
{
	public abstract class model_binding<T>
	{
		protected static object Bound;
		protected static IDictionary<string, string> Dictionary;
		protected static string ModelName;
		private static readonly Type ModelType = typeof(T);

		Because of = () =>
		             	{
		             		ModelBindingContext bindingContext = Dictionary.BuildContext(ModelType, ModelName);
		             		var binder = new DefaultModelBinder();
		             		Bound = binder.BindModel(new ControllerContext(), bindingContext);
		             	};
	}
}