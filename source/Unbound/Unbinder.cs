using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Unbound
{
	public class Unbinder : IUnbinder
	{
		readonly ITypeDescriptor _typeDescriptor;
		readonly ITypeSimplicityEvaluator _typeSimplicityEvaluator;
		readonly IValueUnbinder _valueUnbinder;

		public Unbinder(ITypeDescriptor typeDescriptor,
		                ITypeSimplicityEvaluator typeSimplicityEvaluator,
		                IValueUnbinder valueUnbinder)
		{
			_typeDescriptor = typeDescriptor;
			_typeSimplicityEvaluator = typeSimplicityEvaluator;
			_valueUnbinder = valueUnbinder;
		}

		public Unbinder() : this(new CachedTypeDescriptor(),
		                         new CachedTypeSimplicityEvaluator(),
		                         new ValueUnbinder())
		{
		}

		public IDictionary<string, string> Unbind(object request, string prefix)
		{
			var result = new Dictionary<string, string>();
			var type = request.GetType();

			if (IsSimple(type) || HasCustomUnbinderAvailable(request))
			{
				BindSimpleValue(result, request, prefix);
				return result;
			}

			var enumerable = request as IEnumerable;
			if (enumerable != null)
			{
				BindEnumerable(result, enumerable, prefix);
				return result;
			}

			BindComplexType(result, request, type, prefix);
			return result;
		}

		void BindComplexType(IDictionary<string, string> dictionary, object request, Type valueType, string prefix)
		{
			var collection = _typeDescriptor.GetProperties(valueType);
			foreach (PropertyDescriptor descriptor in collection)
			{
				var value = descriptor.GetValue(request);
				var name = descriptor.Name;
				var modifiedPrefix = prefix + "." + name;

				if (IsSimple(value.GetType()))
				{
					BindSimpleValue(dictionary, value, modifiedPrefix);
				}
				else
				{
					var serializedComponent = Unbind(value, modifiedPrefix);
					Merge(dictionary, serializedComponent);
				}
			}
		}

		void BindEnumerable(IDictionary<string, string> result, IEnumerable toSerialize, string prefix)
		{
			var counter = 0;
			var prefixFormatString = prefix + "[{0}]";
			foreach (var element in toSerialize)
			{
				var modifiedPrefix = string.Format(prefixFormatString, counter++);
				Merge(result, Unbind(element, modifiedPrefix));
			}
		}

		static bool HasCustomUnbinderAvailable(object request)
		{
			return SpecificValueUnbinderFactory.CustomUnbinders().Where(x => x.AppropriatelyUnbinds(request)).Any();
		}

		void BindSimpleValue(IDictionary<string, string> result, object request, string prefix)
		{
			var value = _valueUnbinder.UnbindValue(request);
			result.Add(prefix, value);
		}

		static void Merge(IDictionary<string, string> target, IEnumerable<KeyValuePair<string, string>> additionalPairs)
		{
			foreach (var pair in additionalPairs)
			{
				target.Add(pair.Key, pair.Value);
			}
		}

		bool IsSimple(Type type)
		{
			return _typeSimplicityEvaluator.IsSimple(type);
		}
	}
}