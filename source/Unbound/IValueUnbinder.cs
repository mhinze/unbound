namespace Unbound
{
	public interface IValueUnbinder
	{
		string UnbindValue(object value);
	}

	public interface ISpecificValueUnbinder : IValueUnbinder
	{
		bool AppropriatelyUnbinds(object value);
	}

	public class ValueUnbinder : IValueUnbinder
	{
		public string UnbindValue(object value)
		{
			var unbinder = SpecificValueUnbinderFactory.GetValueUnbinder(value);
			return unbinder.UnbindValue(value);
		}
	}

	public class DefaultValueUnbinder : ISpecificValueUnbinder
	{
		public string UnbindValue(object value)
		{
			return value.ToString();
		}

		public bool AppropriatelyUnbinds(object value)
		{
			return true;
		}
	}
}