using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000072 RID: 114
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x000195FD File Offset: 0x000177FD
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00019613 File Offset: 0x00017813
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019624 File Offset: 0x00017824
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001962C File Offset: 0x0001782C
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
