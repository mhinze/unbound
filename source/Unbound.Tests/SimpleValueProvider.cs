using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Unbound.Tests
{
	public class DictionaryValueProvider : DictionaryValueProvider<string>
	{
		public DictionaryValueProvider(IDictionary<string, string> dictionary)
			: base(dictionary, CultureInfo.InvariantCulture)
		{
		}
	}
}