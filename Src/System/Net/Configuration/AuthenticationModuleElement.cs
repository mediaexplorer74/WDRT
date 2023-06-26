using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x02000324 RID: 804
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class.</summary>
		// Token: 0x06001CCE RID: 7374 RVA: 0x0008A346 File Offset: 0x00088546
		public AuthenticationModuleElement()
		{
			this.properties.Add(this.type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class with the specified type information.</summary>
		/// <param name="typeName">A string that identifies the type and the assembly that contains it.</param>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0008A386 File Offset: 0x00088586
		public AuthenticationModuleElement(string typeName)
			: this()
		{
			if (typeName != (string)this.type.DefaultValue)
			{
				this.Type = typeName;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0008A3AD File Offset: 0x000885AD
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or <see langword="null" /> if no value has been specified.</returns>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0008A3B5 File Offset: 0x000885B5
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0008A3C8 File Offset: 0x000885C8
		[ConfigurationProperty("type", IsRequired = true, IsKey = true)]
		public string Type
		{
			get
			{
				return (string)base[this.type];
			}
			set
			{
				base[this.type] = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0008A3D7 File Offset: 0x000885D7
		internal string Key
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x04001BAC RID: 7084
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BAD RID: 7085
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
