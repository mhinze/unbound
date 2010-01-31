using System;
using System.Collections.Generic;
using System.Linq;

namespace Unbound
{
	public class SpecificValueUnbinderFactory
	{
		public static Func<IEnumerable<ISpecificValueUnbinder>> CustomBinders
			= Enumerable.Empty<ISpecificValueUnbinder>;

		public static IEnumerable<ISpecificValueUnbinder> GetSpecificBinders()
		{
			foreach (var unbinder in CustomBinders())
			{
				yield return unbinder;
			}
			yield return new DefaultValueUnbinder();
		}

		public static IValueUnbinder GetValueUnbinder(object value)
		{
			if (!CustomBinders().Any()) return new DefaultValueUnbinder();
			return GetSpecificBinders().Where(x => x.IsSatisfiedBy(value)).First();
		}
	}

	public class CustomBinderAvailability
	{
		public bool IsCustomBinderAvailable(object value)
		{
			return SpecificValueUnbinderFactory.CustomBinders().Where(x => x.IsSatisfiedBy(value)).Any();
		}
	}
}