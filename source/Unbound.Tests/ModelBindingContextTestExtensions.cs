using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Unbound.Tests
{
	public static class ModelBindingContextTestExtensions
	{
		public static ModelBindingContext BuildContext(this IDictionary<string, string> dictionary,
		                                               Type modelType,
		                                               string modelName)
		{
			return new ModelBindingContext
			       	{
			       		ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType),
			       		ModelName = modelName,
			       		ValueProvider = new DictionaryValueProvider(dictionary),
			       	};
		}
	}
}