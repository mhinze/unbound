using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Unbound
{
	public interface ITypeSimplicityEvaluator
	{
		bool IsSimple(Type type);
	}

	public class CachedTypeSimplicityEvaluator : ITypeSimplicityEvaluator
	{
		readonly ICollection<Type> _complexTypes = new HashSet<Type>();
		readonly ICollection<Type> _simpleTypes = new HashSet<Type>();

		public bool IsSimple(Type type)
		{
			if (_simpleTypes.Contains(type))
				return true;

			if (_complexTypes.Contains(type))
				return false;

			bool isSimple = TypeDescriptor.GetConverter(type).CanConvertFrom(typeof (string));

			if (isSimple)
			{
				_simpleTypes.Add(type);
				return true;
			}

			_complexTypes.Add(type);
			return false;
		}
	}
}