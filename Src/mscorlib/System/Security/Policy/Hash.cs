using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Provides evidence about the hash value for an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000373 RID: 883
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : EvidenceBase, ISerializable
	{
		// Token: 0x06002BE8 RID: 11240 RVA: 0x000A481C File Offset: 0x000A2A1C
		[SecurityCritical]
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			Dictionary<Type, byte[]> dictionary = info.GetValueNoThrow("Hashes", typeof(Dictionary<Type, byte[]>)) as Dictionary<Type, byte[]>;
			if (dictionary != null)
			{
				this.m_hashes = dictionary;
				return;
			}
			this.m_hashes = new Dictionary<Type, byte[]>();
			byte[] array = info.GetValueNoThrow("Md5", typeof(byte[])) as byte[];
			if (array != null)
			{
				this.m_hashes[typeof(MD5)] = array;
			}
			byte[] array2 = info.GetValueNoThrow("Sha1", typeof(byte[])) as byte[];
			if (array2 != null)
			{
				this.m_hashes[typeof(SHA1)] = array2;
			}
			byte[] array3 = info.GetValueNoThrow("RawData", typeof(byte[])) as byte[];
			if (array3 != null)
			{
				this.GenerateDefaultHashes(array3);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Hash" /> class.</summary>
		/// <param name="assembly">The assembly for which to compute the hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is not a run-time <see cref="T:System.Reflection.Assembly" /> object.</exception>
		// Token: 0x06002BE9 RID: 11241 RVA: 0x000A48F0 File Offset: 0x000A2AF0
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly.IsDynamic)
			{
				throw new ArgumentException(Environment.GetResourceString("Security_CannotGenerateHash"), "assembly");
			}
			this.m_hashes = new Dictionary<Type, byte[]>();
			this.m_assembly = assembly as RuntimeAssembly;
			if (this.m_assembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000A496E File Offset: 0x000A2B6E
		private Hash(Hash hash)
		{
			this.m_assembly = hash.m_assembly;
			this.m_rawData = hash.m_rawData;
			this.m_hashes = new Dictionary<Type, byte[]>(hash.m_hashes);
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000A49A0 File Offset: 0x000A2BA0
		private Hash(Type hashType, byte[] hashValue)
		{
			this.m_hashes = new Dictionary<Type, byte[]>();
			byte[] array = new byte[hashValue.Length];
			Array.Copy(hashValue, array, array.Length);
			this.m_hashes[hashType] = hashValue;
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains a <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</summary>
		/// <param name="sha1">A byte array that contains a <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</param>
		/// <returns>An object that contains the hash value provided by the <paramref name="sha1" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sha1" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002BEC RID: 11244 RVA: 0x000A49DE File Offset: 0x000A2BDE
		public static Hash CreateSHA1(byte[] sha1)
		{
			if (sha1 == null)
			{
				throw new ArgumentNullException("sha1");
			}
			return new Hash(typeof(SHA1), sha1);
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains a <see cref="T:System.Security.Cryptography.SHA256" /> hash value.</summary>
		/// <param name="sha256">A byte array that contains a <see cref="T:System.Security.Cryptography.SHA256" /> hash value.</param>
		/// <returns>A hash object that contains the hash value provided by the <paramref name="sha256" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sha256" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002BED RID: 11245 RVA: 0x000A49FE File Offset: 0x000A2BFE
		public static Hash CreateSHA256(byte[] sha256)
		{
			if (sha256 == null)
			{
				throw new ArgumentNullException("sha256");
			}
			return new Hash(typeof(SHA256), sha256);
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</summary>
		/// <param name="md5">A byte array that contains an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</param>
		/// <returns>An object that contains the hash value provided by the <paramref name="md5" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="md5" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002BEE RID: 11246 RVA: 0x000A4A1E File Offset: 0x000A2C1E
		public static Hash CreateMD5(byte[] md5)
		{
			if (md5 == null)
			{
				throw new ArgumentNullException("md5");
			}
			return new Hash(typeof(MD5), md5);
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002BEF RID: 11247 RVA: 0x000A4A3E File Offset: 0x000A2C3E
		public override EvidenceBase Clone()
		{
			return new Hash(this);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000A4A46 File Offset: 0x000A2C46
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.GenerateDefaultHashes();
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06002BF1 RID: 11249 RVA: 0x000A4A50 File Offset: 0x000A2C50
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GenerateDefaultHashes();
			byte[] array;
			if (this.m_hashes.TryGetValue(typeof(MD5), out array))
			{
				info.AddValue("Md5", array);
			}
			byte[] array2;
			if (this.m_hashes.TryGetValue(typeof(SHA1), out array2))
			{
				info.AddValue("Sha1", array2);
			}
			info.AddValue("RawData", null);
			info.AddValue("PEFile", IntPtr.Zero);
			info.AddValue("Hashes", this.m_hashes);
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		public byte[] SHA1
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(SHA1), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(SHA1), typeof(SHA1)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.SHA256" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.SHA256" /> hash value for the assembly.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000A4B3C File Offset: 0x000A2D3C
		public byte[] SHA256
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(SHA256), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(SHA256), typeof(SHA256)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000A4B98 File Offset: 0x000A2D98
		public byte[] MD5
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(MD5), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(MD5), typeof(MD5)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		/// <summary>Computes the hash value for the assembly using the specified hash algorithm.</summary>
		/// <param name="hashAlg">The hash algorithm to use to compute the hash value for the assembly.</param>
		/// <returns>A byte array that represents the hash value for the assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hashAlg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The hash value for the assembly cannot be generated.</exception>
		// Token: 0x06002BF5 RID: 11253 RVA: 0x000A4BF4 File Offset: 0x000A2DF4
		public byte[] GenerateHash(HashAlgorithm hashAlg)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			byte[] array = this.GenerateHash(hashAlg.GetType());
			byte[] array2 = new byte[array.Length];
			Array.Copy(array, array2, array2.Length);
			return array2;
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000A4C30 File Offset: 0x000A2E30
		private byte[] GenerateHash(Type hashType)
		{
			Type hashIndexType = Hash.GetHashIndexType(hashType);
			byte[] array = null;
			if (!this.m_hashes.TryGetValue(hashIndexType, out array))
			{
				if (this.m_assembly == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Security_CannotGenerateHash"));
				}
				array = Hash.GenerateHash(hashType, this.GetRawData());
				this.m_hashes[hashIndexType] = array;
			}
			return array;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000A4C90 File Offset: 0x000A2E90
		private static byte[] GenerateHash(Type hashType, byte[] assemblyBytes)
		{
			byte[] array;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.FullName))
			{
				array = hashAlgorithm.ComputeHash(assemblyBytes);
			}
			return array;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000A4CD0 File Offset: 0x000A2ED0
		private void GenerateDefaultHashes()
		{
			if (this.m_assembly != null)
			{
				this.GenerateDefaultHashes(this.GetRawData());
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000A4CEC File Offset: 0x000A2EEC
		private void GenerateDefaultHashes(byte[] assemblyBytes)
		{
			Type[] array = new Type[]
			{
				Hash.GetHashIndexType(typeof(SHA1)),
				Hash.GetHashIndexType(typeof(SHA256)),
				Hash.GetHashIndexType(typeof(MD5))
			};
			foreach (Type type in array)
			{
				Type defaultHashImplementation = Hash.GetDefaultHashImplementation(type);
				if (defaultHashImplementation != null && !this.m_hashes.ContainsKey(type))
				{
					this.m_hashes[type] = Hash.GenerateHash(defaultHashImplementation, assemblyBytes);
				}
			}
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000A4D80 File Offset: 0x000A2F80
		private static Type GetDefaultHashImplementationOrFallback(Type hashAlgorithm, Type fallbackImplementation)
		{
			Type defaultHashImplementation = Hash.GetDefaultHashImplementation(hashAlgorithm);
			if (!(defaultHashImplementation != null))
			{
				return fallbackImplementation;
			}
			return defaultHashImplementation;
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000A4DA0 File Offset: 0x000A2FA0
		private static Type GetDefaultHashImplementation(Type hashAlgorithm)
		{
			if (hashAlgorithm.IsAssignableFrom(typeof(MD5)))
			{
				if (!CryptoConfig.AllowOnlyFipsAlgorithms)
				{
					return typeof(MD5CryptoServiceProvider);
				}
				return null;
			}
			else
			{
				if (hashAlgorithm.IsAssignableFrom(typeof(SHA256)))
				{
					return Type.GetType("System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				}
				return hashAlgorithm;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000A4DF4 File Offset: 0x000A2FF4
		private static Type GetHashIndexType(Type hashType)
		{
			Type type = hashType;
			while (type != null && type.BaseType != typeof(HashAlgorithm))
			{
				type = type.BaseType;
			}
			if (type == null)
			{
				type = typeof(HashAlgorithm);
			}
			return type;
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000A4E44 File Offset: 0x000A3044
		private byte[] GetRawData()
		{
			byte[] array = null;
			if (this.m_assembly != null)
			{
				if (this.m_rawData != null)
				{
					array = this.m_rawData.Target as byte[];
				}
				if (array == null)
				{
					array = this.m_assembly.GetRawBytes();
					this.m_rawData = new WeakReference(array);
				}
			}
			return array;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000A4E98 File Offset: 0x000A3098
		private SecurityElement ToXml()
		{
			this.GenerateDefaultHashes();
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Hash");
			securityElement.AddAttribute("version", "2");
			foreach (KeyValuePair<Type, byte[]> keyValuePair in this.m_hashes)
			{
				SecurityElement securityElement2 = new SecurityElement("hash");
				securityElement2.AddAttribute("algorithm", keyValuePair.Key.Name);
				securityElement2.AddAttribute("value", Hex.EncodeHexString(keyValuePair.Value));
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Hash" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Hash" />.</returns>
		// Token: 0x06002BFF RID: 11263 RVA: 0x000A4F48 File Offset: 0x000A3148
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x040011B8 RID: 4536
		private RuntimeAssembly m_assembly;

		// Token: 0x040011B9 RID: 4537
		private Dictionary<Type, byte[]> m_hashes;

		// Token: 0x040011BA RID: 4538
		private WeakReference m_rawData;
	}
}
