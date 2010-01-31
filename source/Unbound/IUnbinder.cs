using System.Collections.Generic;

namespace Unbound
{
	public interface IUnbinder
	{
		IDictionary<string, string> Unbind(object request, string prefix);
	}
}