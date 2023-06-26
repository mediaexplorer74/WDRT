using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>Contains APIs used for token binding.</summary>
	// Token: 0x02000448 RID: 1096
	public class TokenBinding
	{
		// Token: 0x0600288F RID: 10383 RVA: 0x000BA484 File Offset: 0x000B8684
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		/// <summary>Gets the raw token binding Id.</summary>
		/// <returns>The raw token binding Id. The first byte of the raw Id, which represents the binding type, is removed.</returns>
		// Token: 0x06002890 RID: 10384 RVA: 0x000BA49A File Offset: 0x000B869A
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		/// <summary>Gets the token binding type.</summary>
		/// <returns>The token binding type.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000BA4B6 File Offset: 0x000B86B6
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000BA4BE File Offset: 0x000B86BE
		public TokenBindingType BindingType { get; private set; }

		// Token: 0x0400225A RID: 8794
		private byte[] _rawTokenBindingId;
	}
}
