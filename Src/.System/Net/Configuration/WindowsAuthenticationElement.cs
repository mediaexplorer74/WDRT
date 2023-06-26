using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the Windows authentication element in a configuration file. This class cannot be inherited.</summary>
	// Token: 0x0200034F RID: 847
	public sealed class WindowsAuthenticationElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WindowsAuthenticationElement" /> class.</summary>
		// Token: 0x06001E4B RID: 7755 RVA: 0x0008DD1C File Offset: 0x0008BF1C
		public WindowsAuthenticationElement()
		{
			this.defaultCredentialsHandleCacheSize = new ConfigurationProperty("defaultCredentialsHandleCacheSize", typeof(int), 0, null, new WindowsAuthenticationElement.CacheSizeValidator(), ConfigurationPropertyOptions.None);
			this.properties = new ConfigurationPropertyCollection();
			this.properties.Add(this.defaultCredentialsHandleCacheSize);
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0008DD72 File Offset: 0x0008BF72
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Defines the default size of the Windows credential handle cache.</summary>
		/// <returns>The default size of the Windows credential handle cache.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0008DD7A File Offset: 0x0008BF7A
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0008DD8D File Offset: 0x0008BF8D
		[ConfigurationProperty("defaultCredentialsHandleCacheSize", DefaultValue = 0)]
		public int DefaultCredentialsHandleCacheSize
		{
			get
			{
				return (int)base[this.defaultCredentialsHandleCacheSize];
			}
			set
			{
				base[this.defaultCredentialsHandleCacheSize] = value;
			}
		}

		// Token: 0x04001CAD RID: 7341
		private ConfigurationPropertyCollection properties;

		// Token: 0x04001CAE RID: 7342
		private readonly ConfigurationProperty defaultCredentialsHandleCacheSize;

		// Token: 0x020007CA RID: 1994
		private class CacheSizeValidator : ConfigurationValidatorBase
		{
			// Token: 0x0600437B RID: 17275 RVA: 0x0011C4AA File Offset: 0x0011A6AA
			public override bool CanValidate(Type type)
			{
				return type == typeof(int);
			}

			// Token: 0x0600437C RID: 17276 RVA: 0x0011C4BC File Offset: 0x0011A6BC
			public override void Validate(object value)
			{
				int num = (int)value;
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { 0, int.MaxValue }));
				}
			}
		}
	}
}
