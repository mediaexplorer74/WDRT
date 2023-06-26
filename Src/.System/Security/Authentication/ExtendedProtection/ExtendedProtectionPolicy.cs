using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class represents the extended protection policy used by the server to validate incoming client connections.</summary>
	// Token: 0x02000442 RID: 1090
	[TypeConverter(typeof(ExtendedProtectionPolicyTypeConverter))]
	[Serializable]
	public class ExtendedProtectionPolicy : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced, the kind of protection enforced by the policy, and a custom Service Provider Name (SPN) list that is used to match against a client's SPN.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="protectionScenario">A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</param>
		/// <param name="customServiceNames">A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains the custom SPN list that is used to match against a client's SPN.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customServiceNames" /> is <see langword="null" /> or an empty list.</exception>
		// Token: 0x06002878 RID: 10360 RVA: 0x000B9CCC File Offset: 0x000B7ECC
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ServiceNameCollection customServiceNames)
		{
			if (policyEnforcement == PolicyEnforcement.Never)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_UseDifferentConstructorForNever"), "policyEnforcement");
			}
			if (customServiceNames != null && customServiceNames.Count == 0)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection"), "customServiceNames");
			}
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = protectionScenario;
			this.customServiceNames = customServiceNames;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced, the kind of protection enforced by the policy, and a custom Service Provider Name (SPN) list that is used to match against a client's SPN.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="protectionScenario">A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</param>
		/// <param name="customServiceNames">A <see cref="T:System.Collections.ICollection" /> that contains the custom SPN list that is used to match against a client's SPN.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customServiceNames" /> is <see langword="null" /> or an empty list.</exception>
		// Token: 0x06002879 RID: 10361 RVA: 0x000B9D2C File Offset: 0x000B7F2C
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ICollection customServiceNames)
			: this(policyEnforcement, protectionScenario, (customServiceNames == null) ? null : new ServiceNameCollection(customServiceNames))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced and the channel binding token (CBT) to be used.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="customChannelBinding">A <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that contains a custom channel binding to use for validation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customChannelBinding" /> is <see langword="null" />.</exception>
		// Token: 0x0600287A RID: 10362 RVA: 0x000B9D44 File Offset: 0x000B7F44
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ChannelBinding customChannelBinding)
		{
			if (policyEnforcement == PolicyEnforcement.Never)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_UseDifferentConstructorForNever"), "policyEnforcement");
			}
			if (customChannelBinding == null)
			{
				throw new ArgumentNullException("customChannelBinding");
			}
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = ProtectionScenario.TransportSelected;
			this.customChannelBinding = customChannelBinding;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		// Token: 0x0600287B RID: 10363 RVA: 0x000B9D92 File Offset: 0x000B7F92
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement)
		{
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = ProtectionScenario.TransportSelected;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class from a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the required data to populate the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		// Token: 0x0600287C RID: 10364 RVA: 0x000B9DA8 File Offset: 0x000B7FA8
		protected ExtendedProtectionPolicy(SerializationInfo info, StreamingContext context)
		{
			this.policyEnforcement = (PolicyEnforcement)info.GetInt32("policyEnforcement");
			this.protectionScenario = (ProtectionScenario)info.GetInt32("protectionScenario");
			this.customServiceNames = (ServiceNameCollection)info.GetValue("customServiceNames", typeof(ServiceNameCollection));
			byte[] array = (byte[])info.GetValue("customChannelBinding", typeof(byte[]));
			if (array != null)
			{
				this.customChannelBinding = SafeLocalFreeChannelBinding.LocalAlloc(array.Length);
				Marshal.Copy(array, 0, this.customChannelBinding.DangerousGetHandle(), array.Length);
			}
		}

		/// <summary>Gets the custom Service Provider Name (SPN) list used to match against a client's SPN.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains the custom SPN list that is used to match against a client's SPN.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x000B9E3E File Offset: 0x000B803E
		public ServiceNameCollection CustomServiceNames
		{
			get
			{
				return this.customServiceNames;
			}
		}

		/// <summary>Gets when the extended protection policy should be enforced.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x000B9E46 File Offset: 0x000B8046
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return this.policyEnforcement;
			}
		}

		/// <summary>Gets the kind of protection enforced by the extended protection policy.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000B9E4E File Offset: 0x000B804E
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return this.protectionScenario;
			}
		}

		/// <summary>Gets a custom channel binding token (CBT) to use for validation.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that contains a custom channel binding to use for validation.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x000B9E56 File Offset: 0x000B8056
		public ChannelBinding CustomChannelBinding
		{
			get
			{
				return this.customChannelBinding;
			}
		}

		/// <summary>Gets a string representation for the extended protection policy instance.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the representation of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</returns>
		// Token: 0x06002881 RID: 10369 RVA: 0x000B9E60 File Offset: 0x000B8060
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProtectionScenario=");
			stringBuilder.Append(this.protectionScenario.ToString());
			stringBuilder.Append("; PolicyEnforcement=");
			stringBuilder.Append(this.policyEnforcement.ToString());
			stringBuilder.Append("; CustomChannelBinding=");
			if (this.customChannelBinding == null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				stringBuilder.Append(this.customChannelBinding.ToString());
			}
			stringBuilder.Append("; ServiceNames=");
			if (this.customServiceNames == null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				bool flag = true;
				foreach (object obj in this.customServiceNames)
				{
					string text = (string)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Indicates whether the operating system supports integrated windows authentication with extended protection.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system supports integrated windows authentication with extended protection, otherwise <see langword="false" />.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x000B9F7C File Offset: 0x000B817C
		public static bool OSSupportsExtendedProtection
		{
			get
			{
				return AuthenticationManager.OSSupportsExtendedProtection;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized data for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</param>
		// Token: 0x06002883 RID: 10371 RVA: 0x000B9F84 File Offset: 0x000B8184
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("policyEnforcement", (int)this.policyEnforcement);
			info.AddValue("protectionScenario", (int)this.protectionScenario);
			info.AddValue("customServiceNames", this.customServiceNames, typeof(ServiceNameCollection));
			if (this.customChannelBinding == null)
			{
				info.AddValue("customChannelBinding", null, typeof(byte[]));
				return;
			}
			byte[] array = new byte[this.customChannelBinding.Size];
			Marshal.Copy(this.customChannelBinding.DangerousGetHandle(), array, 0, this.customChannelBinding.Size);
			info.AddValue("customChannelBinding", array, typeof(byte[]));
		}

		// Token: 0x04002248 RID: 8776
		private const string policyEnforcementName = "policyEnforcement";

		// Token: 0x04002249 RID: 8777
		private const string protectionScenarioName = "protectionScenario";

		// Token: 0x0400224A RID: 8778
		private const string customServiceNamesName = "customServiceNames";

		// Token: 0x0400224B RID: 8779
		private const string customChannelBindingName = "customChannelBinding";

		// Token: 0x0400224C RID: 8780
		private ServiceNameCollection customServiceNames;

		// Token: 0x0400224D RID: 8781
		private PolicyEnforcement policyEnforcement;

		// Token: 0x0400224E RID: 8782
		private ProtectionScenario protectionScenario;

		// Token: 0x0400224F RID: 8783
		private ChannelBinding customChannelBinding;
	}
}
