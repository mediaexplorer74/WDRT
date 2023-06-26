using System;

namespace System.Reflection
{
	/// <summary>Provides migration from an older, simpler strong name key to a larger key with a stronger hashing algorithm.</summary>
	// Token: 0x020005C3 RID: 1475
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Reflection.AssemblySignatureKeyAttribute" /> class by using the specified public key and countersignature.</summary>
		/// <param name="publicKey">The public or identity key.</param>
		/// <param name="countersignature">The countersignature, which is the signature key portion of the strong-name key.</param>
		// Token: 0x06004490 RID: 17552 RVA: 0x000FDCF5 File Offset: 0x000FBEF5
		[__DynamicallyInvokable]
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this._publicKey = publicKey;
			this._countersignature = countersignature;
		}

		/// <summary>Gets the public key for the strong name used to sign the assembly.</summary>
		/// <returns>The public key for this assembly.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x000FDD0B File Offset: 0x000FBF0B
		[__DynamicallyInvokable]
		public string PublicKey
		{
			[__DynamicallyInvokable]
			get
			{
				return this._publicKey;
			}
		}

		/// <summary>Gets the countersignature for the strong name for this assembly.</summary>
		/// <returns>The countersignature for this signature key.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000FDD13 File Offset: 0x000FBF13
		[__DynamicallyInvokable]
		public string Countersignature
		{
			[__DynamicallyInvokable]
			get
			{
				return this._countersignature;
			}
		}

		// Token: 0x04001C17 RID: 7191
		private string _publicKey;

		// Token: 0x04001C18 RID: 7192
		private string _countersignature;
	}
}
