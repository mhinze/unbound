using System.Collections.Generic;

namespace Unbound
{
	public interface IValueUnbinder
	{
		string UnbindValue(object value);
	}

	public interface ISpecificValueUnbinder : IValueUnbinder
	{
		bool IsSatisfiedBy(object value);
	}

	public class ValueUnbinder : IValueUnbinder
	{
		public string UnbindValue(object value)
		{
			IValueUnbinder unbinder = SpecificValueUnbinderFactory.GetValueUnbinder(value);
			return unbinder.UnbindValue(value);
		}
	}

	public class DefaultValueUnbinder : ISpecificValueUnbinder
	{
		public string UnbindValue(object value)
		{
			return value.ToString();
		}

		public bool IsSatisfiedBy(object value)
		{
			return true;
		}
	}
}