using System;
using System.Collections.Generic;
using System.Linq;

namespace Unbound
{
	public static class SpecificValueUnbinderFactory
	{
		public static Func<IEnumerable<ISpecificValueUnbinder>> CustomUnbinders
			= Enumerable.Empty<ISpecificValueUnbinder>;

		static IEnumerable<ISpecificValueUnbinder> GetSpecificBinders()
		{
			foreach (var unbinder in CustomUnbinders())
				yield return unbinder;

			yield return new DefaultValueUnbinder();
		}

		public static IValueUnbinder GetValueUnbinder(object value)
		{
			if (!CustomUnbinders().Any()) return new DefaultValueUnbinder();
			return GetSpecificBinders().Where(x => x.AppropriatelyUnbinds(value)).First();
		}
	}
}