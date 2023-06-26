using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents an SMTP pickup directory configuration element.</summary>
	// Token: 0x02000346 RID: 838
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> class.</summary>
		// Token: 0x06001E10 RID: 7696 RVA: 0x0008D439 File Offset: 0x0008B639
		public SmtpSpecifiedPickupDirectoryElement()
		{
			this.properties.Add(this.pickupDirectoryLocation);
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0008D479 File Offset: 0x0008B679
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the SMTP server.</summary>
		/// <returns>A string that specifies the pickup directory for email messages.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0008D481 File Offset: 0x0008B681
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0008D494 File Offset: 0x0008B694
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[this.pickupDirectoryLocation];
			}
			set
			{
				base[this.pickupDirectoryLocation] = value;
			}
		}

		// Token: 0x04001C99 RID: 7321
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C9A RID: 7322
		private readonly ConfigurationProperty pickupDirectoryLocation = new ConfigurationProperty("pickupDirectoryLocation", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
