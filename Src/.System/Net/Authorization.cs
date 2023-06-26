using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020000C6 RID: 198
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		// Token: 0x06000696 RID: 1686 RVA: 0x000251F9 File Offset: 0x000233F9
		public Authorization(string token)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		/// <param name="finished">The completion status of the authorization attempt. <see langword="true" /> if the authorization attempt is complete; otherwise, <see langword="false" />.</param>
		// Token: 0x06000697 RID: 1687 RVA: 0x00025214 File Offset: 0x00023414
		public Authorization(string token, bool finished)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = finished;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		/// <param name="finished">The completion status of the authorization attempt. <see langword="true" /> if the authorization attempt is complete; otherwise, <see langword="false" />.</param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme.</param>
		// Token: 0x06000698 RID: 1688 RVA: 0x0002522F File Offset: 0x0002342F
		public Authorization(string token, bool finished, string connectionGroupId)
			: this(token, finished, connectionGroupId, false)
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002523B File Offset: 0x0002343B
		internal Authorization(string token, bool finished, string connectionGroupId, bool mutualAuth)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_ConnectionGroupId = ValidationHelper.MakeStringNull(connectionGroupId);
			this.m_Complete = finished;
			this.m_MutualAuth = mutualAuth;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0002526A File Offset: 0x0002346A
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00025272 File Offset: 0x00023472
		public string ConnectionGroupId
		{
			get
			{
				return this.m_ConnectionGroupId;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>
		///   <see langword="true" /> if the authentication process is complete; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0002527A File Offset: 0x0002347A
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00025282 File Offset: 0x00023482
		internal void SetComplete(bool complete)
		{
			this.m_Complete = complete;
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002528B File Offset: 0x0002348B
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00025294 File Offset: 0x00023494
		public string[] ProtectionRealm
		{
			get
			{
				return this.m_ProtectionRealm;
			}
			set
			{
				string[] array = ValidationHelper.MakeEmptyArrayNull(value);
				this.m_ProtectionRealm = array;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if both client and server were authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000252AF File Offset: 0x000234AF
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x000252C1 File Offset: 0x000234C1
		public bool MutuallyAuthenticated
		{
			get
			{
				return this.Complete && this.m_MutualAuth;
			}
			set
			{
				this.m_MutualAuth = value;
			}
		}

		// Token: 0x04000C81 RID: 3201
		private string m_Message;

		// Token: 0x04000C82 RID: 3202
		private bool m_Complete;

		// Token: 0x04000C83 RID: 3203
		private string[] m_ProtectionRealm;

		// Token: 0x04000C84 RID: 3204
		private string m_ConnectionGroupId;

		// Token: 0x04000C85 RID: 3205
		private bool m_MutualAuth;
	}
}
