using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the SMTP section in the <see langword="System.Net" /> configuration file.</summary>
	// Token: 0x02000342 RID: 834
	public sealed class SmtpSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SmtpSection" /> class.</summary>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x0008CD6C File Offset: 0x0008AF6C
		public SmtpSection()
		{
			this.properties.Add(this.deliveryMethod);
			this.properties.Add(this.deliveryFormat);
			this.properties.Add(this.from);
			this.properties.Add(this.network);
			this.properties.Add(this.specifiedPickupDirectory);
		}

		/// <summary>Gets or sets the Simple Mail Transport Protocol (SMTP) delivery method. The default delivery method is <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" />.</summary>
		/// <returns>A string that represents the SMTP delivery method.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0008CE81 File Offset: 0x0008B081
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0008CE94 File Offset: 0x0008B094
		[ConfigurationProperty("deliveryMethod", DefaultValue = SmtpDeliveryMethod.Network)]
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (SmtpDeliveryMethod)base[this.deliveryMethod];
			}
			set
			{
				base[this.deliveryMethod] = value;
			}
		}

		/// <summary>Gets or sets the delivery format to use for sending outgoing email using the Simple Mail Transport Protocol (SMTP).</summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.  
		///  The delivery format to use for sending outgoing email using SMTP.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0008CEA8 File Offset: 0x0008B0A8
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0008CEBB File Offset: 0x0008B0BB
		[ConfigurationProperty("deliveryFormat", DefaultValue = SmtpDeliveryFormat.SevenBit)]
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return (SmtpDeliveryFormat)base[this.deliveryFormat];
			}
			set
			{
				base[this.deliveryFormat] = value;
			}
		}

		/// <summary>Gets or sets the default value that indicates who the email message is from.</summary>
		/// <returns>A string that represents the default value indicating who a mail message is from.</returns>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0008CECF File Offset: 0x0008B0CF
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0008CEE2 File Offset: 0x0008B0E2
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base[this.from];
			}
			set
			{
				base[this.from] = value;
			}
		}

		/// <summary>Gets the configuration element that controls the network settings used by the Simple Mail Transport Protocol (SMTP). file.<see cref="T:System.Net.Configuration.SmtpNetworkElement" />.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> object.  
		///  The configuration element that controls the network settings used by SMTP.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0008CEF1 File Offset: 0x0008B0F1
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base[this.network];
			}
		}

		/// <summary>Gets the pickup directory that will be used by the SMPT client.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> object that specifies the pickup directory folder.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0008CF04 File Offset: 0x0008B104
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base[this.specifiedPickupDirectory];
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0008CF17 File Offset: 0x0008B117
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C7E RID: 7294
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C7F RID: 7295
		private readonly ConfigurationProperty from = new ConfigurationProperty("from", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C80 RID: 7296
		private readonly ConfigurationProperty network = new ConfigurationProperty("network", typeof(SmtpNetworkElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C81 RID: 7297
		private readonly ConfigurationProperty specifiedPickupDirectory = new ConfigurationProperty("specifiedPickupDirectory", typeof(SmtpSpecifiedPickupDirectoryElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C82 RID: 7298
		private readonly ConfigurationProperty deliveryMethod = new ConfigurationProperty("deliveryMethod", typeof(SmtpDeliveryMethod), SmtpDeliveryMethod.Network, new SmtpSection.SmtpDeliveryMethodTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C83 RID: 7299
		private readonly ConfigurationProperty deliveryFormat = new ConfigurationProperty("deliveryFormat", typeof(SmtpDeliveryFormat), SmtpDeliveryFormat.SevenBit, new SmtpSection.SmtpDeliveryFormatTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x020007C4 RID: 1988
		private class SmtpDeliveryMethodTypeConverter : TypeConverter
		{
			// Token: 0x06004367 RID: 17255 RVA: 0x0011C1F5 File Offset: 0x0011A3F5
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004368 RID: 17256 RVA: 0x0011C214 File Offset: 0x0011A414
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					if (text == "network")
					{
						return SmtpDeliveryMethod.Network;
					}
					if (text == "specifiedpickupdirectory")
					{
						return SmtpDeliveryMethod.SpecifiedPickupDirectory;
					}
					if (text == "pickupdirectoryfromiis")
					{
						return SmtpDeliveryMethod.PickupDirectoryFromIis;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x020007C5 RID: 1989
		private class SmtpDeliveryFormatTypeConverter : TypeConverter
		{
			// Token: 0x0600436A RID: 17258 RVA: 0x0011C286 File Offset: 0x0011A486
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600436B RID: 17259 RVA: 0x0011C2A4 File Offset: 0x0011A4A4
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					if (text == "sevenbit")
					{
						return SmtpDeliveryFormat.SevenBit;
					}
					if (text == "international")
					{
						return SmtpDeliveryFormat.International;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
