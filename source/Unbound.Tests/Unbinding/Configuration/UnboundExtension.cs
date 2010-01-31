using System.Collections.Generic;

namespace Unbound.Tests.Unbinding.Configuration
{
	public static class UnboundExtension
	{
		static UnboundExtension()
		{
			SpecificValueUnbinderFactory.CustomBinders
				= () => new ISpecificValueUnbinder[]
				        	{
				        		new FooUnbinder(),
				        	};
		}

		public static IDictionary<string, string> ToHttpDictionary(this object request, string prefix)
		{
			IUnbinder unbinder = new Unbinder();
			return unbinder.Unbind(request, prefix);
		}
	}
}