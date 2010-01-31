using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Unbound
{
	public class Unbinder : IUnbinder
	{
		readonly ITypeDescriptor _typeDescriptor;
		readonly ITypeSimplicityEvaluator _typeSimplicityEvaluator;
		readonly IValueUnbinder _valueUnbinder;

		public Unbinder()
		{
			_typeSimplicityEvaluator = new CachedTypeSimplicityEvaluator();
			_typeDescriptor = new CachedTypeDescriptor();
			_valueUnbinder = new ValueUnbinder();
		}

		public IDictionary<string, string> Unbind(object request, string prefix)
		{
			var result = new Dictionary<string, string>();
			var type = request.GetType();

			if (IsSimple(type) || IsReadyForUnbinding(request))
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
			foreach (var element in toSerialize)
			{
				var modifiedPrefix = string.Format("{0}[{1}]", prefix, counter++);
				Merge(result, Unbind(element, modifiedPrefix));
			}
		}

		static bool IsReadyForUnbinding(object request)
		{
			var ready = new CustomBinderAvailability().IsCustomBinderAvailable(request);
			return ready;
		}

		void BindSimpleValue(IDictionary<string, string> result, object request, string prefix)
		{
			var value = _valueUnbinder.UnbindValue(request);
			result.Add(prefix, value);
		}

		static void Merge(IDictionary<string, string> left, IEnumerable<KeyValuePair<string, string>> right)
		{
			foreach (var pair in right)
			{
				left.Add(pair.Key, pair.Value);
			}
		}

		bool IsSimple(Type type)
		{
			return _typeSimplicityEvaluator.IsSimple(type);
		}
	}
}