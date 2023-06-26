using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000343 RID: 835
	internal sealed class SmtpSectionInternal
	{
		// Token: 0x06001DEE RID: 7662 RVA: 0x0008CF20 File Offset: 0x0008B120
		internal SmtpSectionInternal(SmtpSection section)
		{
			this.deliveryMethod = section.DeliveryMethod;
			this.deliveryFormat = section.DeliveryFormat;
			this.from = section.From;
			this.network = new SmtpNetworkElementInternal(section.Network);
			this.specifiedPickupDirectory = new SmtpSpecifiedPickupDirectoryElementInternal(section.SpecifiedPickupDirectory);
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0008CF79 File Offset: 0x0008B179
		internal SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0008CF81 File Offset: 0x0008B181
		internal SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0008CF89 File Offset: 0x0008B189
		internal SmtpNetworkElementInternal Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0008CF91 File Offset: 0x0008B191
		internal string From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0008CF99 File Offset: 0x0008B199
		internal SmtpSpecifiedPickupDirectoryElementInternal SpecifiedPickupDirectory
		{
			get
			{
				return this.specifiedPickupDirectory;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0008CFA1 File Offset: 0x0008B1A1
		internal static object ClassSyncObject
		{
			get
			{
				if (SmtpSectionInternal.classSyncObject == null)
				{
					Interlocked.CompareExchange(ref SmtpSectionInternal.classSyncObject, new object(), null);
				}
				return SmtpSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0008CFC0 File Offset: 0x0008B1C0
		internal static SmtpSectionInternal GetSection()
		{
			object obj = SmtpSectionInternal.ClassSyncObject;
			SmtpSectionInternal smtpSectionInternal;
			lock (obj)
			{
				SmtpSection smtpSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SmtpSectionPath) as SmtpSection;
				if (smtpSection == null)
				{
					smtpSectionInternal = null;
				}
				else
				{
					smtpSectionInternal = new SmtpSectionInternal(smtpSection);
				}
			}
			return smtpSectionInternal;
		}

		// Token: 0x04001C84 RID: 7300
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x04001C85 RID: 7301
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x04001C86 RID: 7302
		private string from;

		// Token: 0x04001C87 RID: 7303
		private SmtpNetworkElementInternal network;

		// Token: 0x04001C88 RID: 7304
		private SmtpSpecifiedPickupDirectoryElementInternal specifiedPickupDirectory;

		// Token: 0x04001C89 RID: 7305
		private static object classSyncObject;
	}
}
