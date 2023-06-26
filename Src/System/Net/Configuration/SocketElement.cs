using System;
using System.Configuration;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000348 RID: 840
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class.</summary>
		// Token: 0x06001E16 RID: 7702 RVA: 0x0008D4C0 File Offset: 0x0008B6C0
		public SocketElement()
		{
			this.properties.Add(this.alwaysUseCompletionPortsForAccept);
			this.properties.Add(this.alwaysUseCompletionPortsForConnect);
			this.properties.Add(this.ipProtectionLevel);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0008D574 File Offset: 0x0008B774
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.UnrestrictedSocketPermission.Demand();
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_element_permission", new object[] { "socket" }), ex);
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>
		///   <see langword="true" /> to use completion ports; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0008D5CC File Offset: 0x0008B7CC
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0008D5DF File Offset: 0x0008B7DF
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = false)]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[this.alwaysUseCompletionPortsForAccept];
			}
			set
			{
				base[this.alwaysUseCompletionPortsForAccept] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>
		///   <see langword="true" /> to use completion ports; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0008D5F3 File Offset: 0x0008B7F3
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x0008D606 File Offset: 0x0008B806
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = false)]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[this.alwaysUseCompletionPortsForConnect];
			}
			set
			{
				base[this.alwaysUseCompletionPortsForConnect] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the default <see cref="T:System.Net.Sockets.IPProtectionLevel" /> to use for a socket.</summary>
		/// <returns>The value of the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> for the current instance.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x0008D61A File Offset: 0x0008B81A
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x0008D62D File Offset: 0x0008B82D
		[ConfigurationProperty("ipProtectionLevel", DefaultValue = IPProtectionLevel.Unspecified)]
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				return (IPProtectionLevel)base[this.ipProtectionLevel];
			}
			set
			{
				base[this.ipProtectionLevel] = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0008D641 File Offset: 0x0008B841
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C9C RID: 7324
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C9D RID: 7325
		private readonly ConfigurationProperty alwaysUseCompletionPortsForConnect = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9E RID: 7326
		private readonly ConfigurationProperty alwaysUseCompletionPortsForAccept = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C9F RID: 7327
		private readonly ConfigurationProperty ipProtectionLevel = new ConfigurationProperty("ipProtectionLevel", typeof(IPProtectionLevel), IPProtectionLevel.Unspecified, ConfigurationPropertyOptions.None);
	}
}
