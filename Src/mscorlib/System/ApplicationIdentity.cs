using System;
using System.Deployment.Internal.Isolation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Provides the ability to uniquely identify a manifest-activated application. This class cannot be inherited.</summary>
	// Token: 0x020000A4 RID: 164
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		private ApplicationIdentity()
		{
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		[SecurityCritical]
		private ApplicationIdentity(SerializationInfo info, StreamingContext context)
		{
			string text = (string)info.GetValue("FullName", typeof(string));
			if (text == null)
			{
				throw new ArgumentNullException("fullName");
			}
			this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationIdentity" /> class.</summary>
		/// <param name="applicationIdentityFullName">The full name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentityFullName" /> is <see langword="null" />.</exception>
		// Token: 0x06000987 RID: 2439 RVA: 0x0001F03A File Offset: 0x0001D23A
		[SecuritySafeCritical]
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, applicationIdentityFullName);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001F062 File Offset: 0x0001D262
		[SecurityCritical]
		internal ApplicationIdentity(IDefinitionAppId applicationIdentity)
		{
			this._appId = applicationIdentity;
		}

		/// <summary>Gets the full name of the application.</summary>
		/// <returns>The full name of the application, also known as the display name.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001F071 File Offset: 0x0001D271
		public string FullName
		{
			[SecuritySafeCritical]
			get
			{
				return IsolationInterop.AppIdAuthority.DefinitionToText(0U, this._appId);
			}
		}

		/// <summary>Gets the location of the deployment manifest as a URL.</summary>
		/// <returns>The URL of the deployment manifest.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001F084 File Offset: 0x0001D284
		public string CodeBase
		{
			[SecuritySafeCritical]
			get
			{
				return this._appId.get_Codebase();
			}
		}

		/// <summary>Returns the full name of the manifest-activated application.</summary>
		/// <returns>The full name of the manifest-activated application.</returns>
		// Token: 0x0600098B RID: 2443 RVA: 0x0001F091 File Offset: 0x0001D291
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001F099 File Offset: 0x0001D299
		internal IDefinitionAppId Identity
		{
			[SecurityCritical]
			get
			{
				return this._appId;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />) structure for the serialization.</param>
		// Token: 0x0600098D RID: 2445 RVA: 0x0001F0A1 File Offset: 0x0001D2A1
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("FullName", this.FullName, typeof(string));
		}

		// Token: 0x040003C7 RID: 967
		private IDefinitionAppId _appId;
	}
}
