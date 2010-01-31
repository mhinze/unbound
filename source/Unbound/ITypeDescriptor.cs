using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Unbound
{
	public interface ITypeDescriptor
	{
		PropertyDescriptorCollection GetProperties(Type type);
	}

	public class CachedTypeDescriptor : ITypeDescriptor
	{
		readonly IDictionary<Type, PropertyDescriptorCollection> _propertyCollections =
			new Dictionary<Type, PropertyDescriptorCollection>();

		public PropertyDescriptorCollection GetProperties(Type type)
		{
			if (_propertyCollections.ContainsKey(type))
				return _propertyCollections[type];

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
			_propertyCollections.Add(type, properties);
			return properties;
		}
	}
}