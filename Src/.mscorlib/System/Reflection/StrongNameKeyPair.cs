using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Runtime.Hosting;

namespace System.Reflection
{
	/// <summary>Encapsulates access to a public or private key pair used to sign strong name assemblies.</summary>
	// Token: 0x0200061E RID: 1566
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : IDeserializationCallback, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="FileStream" />.</summary>
		/// <param name="keyPairFile">A <see langword="FileStream" /> containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060048C1 RID: 18625 RVA: 0x00108DDC File Offset: 0x00106FDC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			int num = (int)keyPairFile.Length;
			this._keyPairArray = new byte[num];
			keyPairFile.Read(this._keyPairArray, 0, num);
			this._keyPairExported = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="byte" /> array.</summary>
		/// <param name="keyPairArray">An array of type <see langword="byte" /> containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060048C2 RID: 18626 RVA: 0x00108E27 File Offset: 0x00107027
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this._keyPairArray = new byte[keyPairArray.Length];
			Array.Copy(keyPairArray, this._keyPairArray, keyPairArray.Length);
			this._keyPairExported = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="String" />.</summary>
		/// <param name="keyPairContainer">A string containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairContainer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060048C3 RID: 18627 RVA: 0x00108E61 File Offset: 0x00107061
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this._keyPairExported = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060048C4 RID: 18628 RVA: 0x00108E88 File Offset: 0x00107088
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._keyPairExported = (bool)info.GetValue("_keyPairExported", typeof(bool));
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
			this._keyPairContainer = (string)info.GetValue("_keyPairContainer", typeof(string));
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
		}

		/// <summary>Gets the public part of the public key or public key token of the key pair.</summary>
		/// <returns>An array of type <see langword="byte" /> containing the public key or public key token of the key pair.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x00108F1C File Offset: 0x0010711C
		public byte[] PublicKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this._publicKey == null)
				{
					this._publicKey = this.ComputePublicKey();
				}
				byte[] array = new byte[this._publicKey.Length];
				Array.Copy(this._publicKey, array, this._publicKey.Length);
				return array;
			}
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x00108F60 File Offset: 0x00107160
		[SecurityCritical]
		private unsafe byte[] ComputePublicKey()
		{
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr zero = IntPtr.Zero;
				int num = 0;
				try
				{
					bool flag;
					if (this._keyPairExported)
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(null, this._keyPairArray, this._keyPairArray.Length, out zero, out num);
					}
					else
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(this._keyPairContainer, null, 0, out zero, out num);
					}
					if (!flag)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_StrongNameGetPublicKey"));
					}
					array = new byte[num];
					Buffer.Memcpy(array, 0, (byte*)zero.ToPointer(), 0, num);
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						StrongNameHelpers.StrongNameFreeBuffer(zero);
					}
				}
			}
			return array;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data required to reinstantiate the current <see cref="T:System.Reflection.StrongNameKeyPair" /> object.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060048C7 RID: 18631 RVA: 0x00109014 File Offset: 0x00107214
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray);
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_publicKey", this._publicKey);
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x060048C8 RID: 18632 RVA: 0x00109065 File Offset: 0x00107265
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x00109067 File Offset: 0x00107267
		private bool GetKeyPair(out object arrayOrContainer)
		{
			arrayOrContainer = (this._keyPairExported ? this._keyPairArray : this._keyPairContainer);
			return this._keyPairExported;
		}

		// Token: 0x04001E24 RID: 7716
		private bool _keyPairExported;

		// Token: 0x04001E25 RID: 7717
		private byte[] _keyPairArray;

		// Token: 0x04001E26 RID: 7718
		private string _keyPairContainer;

		// Token: 0x04001E27 RID: 7719
		private byte[] _publicKey;
	}
}
