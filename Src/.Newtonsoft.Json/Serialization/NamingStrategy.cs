using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00023AA0 File Offset: 0x00021CA0
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00023AB1 File Offset: 0x00021CB1
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00023AB9 File Offset: 0x00021CB9
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00023AC2 File Offset: 0x00021CC2
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00023ACA File Offset: 0x00021CCA
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x06000825 RID: 2085 RVA: 0x00023AD3 File Offset: 0x00021CD3
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00023AE9 File Offset: 0x00021CE9
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00023AFC File Offset: 0x00021CFC
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000828 RID: 2088
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x06000829 RID: 2089 RVA: 0x00023B10 File Offset: 0x00021D10
		public override int GetHashCode()
		{
			return (((((base.GetType().GetHashCode() * 397) ^ this.ProcessDictionaryKeys.GetHashCode()) * 397) ^ this.ProcessExtensionDataNames.GetHashCode()) * 397) ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00023B67 File Offset: 0x00021D67
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00023B78 File Offset: 0x00021D78
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
