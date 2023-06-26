using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its hash value. This class cannot be inherited.</summary>
	// Token: 0x02000374 RID: 884
	[ComVisible(true)]
	[Serializable]
	public sealed class HashMembershipCondition : ISerializable, IDeserializationCallback, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IReportMatchMembershipCondition
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000A4F58 File Offset: 0x000A3158
		private object InternalSyncObject
		{
			get
			{
				if (this.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref this.s_InternalSyncObject, obj, null);
				}
				return this.s_InternalSyncObject;
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000A4F87 File Offset: 0x000A3187
		internal HashMembershipCondition()
		{
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000A4F90 File Offset: 0x000A3190
		private HashMembershipCondition(SerializationInfo info, StreamingContext context)
		{
			this.m_value = (byte[])info.GetValue("HashValue", typeof(byte[]));
			string text = (string)info.GetValue("HashAlgorithm", typeof(string));
			if (text != null)
			{
				this.m_hashAlg = HashAlgorithm.Create(text);
				return;
			}
			this.m_hashAlg = new SHA1Managed();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.HashMembershipCondition" /> class with the hash algorithm and hash value that determine membership.</summary>
		/// <param name="hashAlg">The hash algorithm to use to compute the hash value for the assembly.</param>
		/// <param name="value">The hash value for which to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hashAlg" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hashAlg" /> parameter is not a valid hash algorithm.</exception>
		// Token: 0x06002C03 RID: 11267 RVA: 0x000A4FFC File Offset: 0x000A31FC
		public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			this.m_value = new byte[value.Length];
			Array.Copy(value, this.m_value, value.Length);
			this.m_hashAlg = hashAlg;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination <see cref="T:System.Runtime.Serialization.StreamingContext" /> for this serialization.</param>
		// Token: 0x06002C04 RID: 11268 RVA: 0x000A504F File Offset: 0x000A324F
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("HashValue", this.HashValue);
			info.AddValue("HashAlgorithm", this.HashAlgorithm.ToString());
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06002C05 RID: 11269 RVA: 0x000A5078 File Offset: 0x000A3278
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Gets or sets the hash algorithm to use for the membership condition.</summary>
		/// <returns>The hash algorithm to use for the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> to <see langword="null" />.</exception>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000A5091 File Offset: 0x000A3291
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x000A507A File Offset: 0x000A327A
		public HashAlgorithm HashAlgorithm
		{
			get
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				return this.m_hashAlg;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashAlgorithm");
				}
				this.m_hashAlg = value;
			}
		}

		/// <summary>Gets or sets the hash value for which the membership condition tests.</summary>
		/// <returns>The hash value for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> to <see langword="null" />.</exception>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000A50DC File Offset: 0x000A32DC
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000A50AF File Offset: 0x000A32AF
		public byte[] HashValue
		{
			get
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_value == null)
				{
					return null;
				}
				byte[] array = new byte[this.m_value.Length];
				Array.Copy(this.m_value, array, this.m_value.Length);
				return array;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_value = new byte[value.Length];
				Array.Copy(value, this.m_value, value.Length);
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The evidence set against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C0A RID: 11274 RVA: 0x000A512C File Offset: 0x000A332C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000A5144 File Offset: 0x000A3344
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Hash hostEvidence = evidence.GetHostEvidence<Hash>();
			if (hostEvidence != null)
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				byte[] array = null;
				object internalSyncObject = this.InternalSyncObject;
				lock (internalSyncObject)
				{
					array = hostEvidence.GenerateHash(this.m_hashAlg);
				}
				if (array != null && HashMembershipCondition.CompareArrays(array, this.m_value))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06002C0C RID: 11276 RVA: 0x000A51E4 File Offset: 0x000A33E4
		public IMembershipCondition Copy()
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			return new HashMembershipCondition(this.m_hashAlg, this.m_value);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002C0D RID: 11277 RVA: 0x000A5223 File Offset: 0x000A3423
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06002C0E RID: 11278 RVA: 0x000A522C File Offset: 0x000A342C
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The policy level context for resolving named permission set references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002C0F RID: 11279 RVA: 0x000A5238 File Offset: 0x000A3438
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.HashMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_value != null)
			{
				securityElement.AddAttribute("HashValue", Hex.EncodeHexString(this.HashValue));
			}
			if (this.m_hashAlg != null)
			{
				securityElement.AddAttribute("HashAlgorithm", this.HashAlgorithm.GetType().FullName);
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level context, used to resolve named permission set references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002C10 RID: 11280 RVA: 0x000A52E0 File Offset: 0x000A34E0
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_element = e;
				this.m_value = null;
				this.m_hashAlg = null;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and the <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> from the specified object are equivalent to the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> contained in the current <see cref="T:System.Security.Policy.HashMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.HashMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> from the specified object is equivalent to the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> contained in the current <see cref="T:System.Security.Policy.HashMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C11 RID: 11281 RVA: 0x000A5360 File Offset: 0x000A3560
		public override bool Equals(object o)
		{
			HashMembershipCondition hashMembershipCondition = o as HashMembershipCondition;
			if (hashMembershipCondition != null)
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				if (hashMembershipCondition.m_hashAlg == null && hashMembershipCondition.m_element != null)
				{
					hashMembershipCondition.ParseHashAlgorithm();
				}
				if (this.m_hashAlg != null && hashMembershipCondition.m_hashAlg != null && this.m_hashAlg.GetType() == hashMembershipCondition.m_hashAlg.GetType())
				{
					if (this.m_value == null && this.m_element != null)
					{
						this.ParseHashValue();
					}
					if (hashMembershipCondition.m_value == null && hashMembershipCondition.m_element != null)
					{
						hashMembershipCondition.ParseHashValue();
					}
					if (this.m_value.Length != hashMembershipCondition.m_value.Length)
					{
						return false;
					}
					for (int i = 0; i < this.m_value.Length; i++)
					{
						if (this.m_value[i] != hashMembershipCondition.m_value[i])
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06002C12 RID: 11282 RVA: 0x000A5444 File Offset: 0x000A3644
		public override int GetHashCode()
		{
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			int num = ((this.m_hashAlg != null) ? this.m_hashAlg.GetType().GetHashCode() : 0);
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			return num ^ HashMembershipCondition.GetByteArrayHashCode(this.m_value);
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		// Token: 0x06002C13 RID: 11283 RVA: 0x000A54A8 File Offset: 0x000A36A8
		public override string ToString()
		{
			if (this.m_hashAlg == null)
			{
				this.ParseHashAlgorithm();
			}
			return Environment.GetResourceString("Hash_ToString", new object[]
			{
				this.m_hashAlg.GetType().AssemblyQualifiedName,
				Hex.EncodeHexString(this.HashValue)
			});
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000A54F4 File Offset: 0x000A36F4
		private void ParseHashValue()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashValue");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
						{
							"HashValue",
							base.GetType().FullName
						}));
					}
					this.m_value = Hex.DecodeHexString(text);
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000A55A0 File Offset: 0x000A37A0
		private void ParseHashAlgorithm()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashAlgorithm");
					if (text != null)
					{
						this.m_hashAlg = HashAlgorithm.Create(text);
					}
					else
					{
						this.m_hashAlg = new SHA1Managed();
					}
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000A5628 File Offset: 0x000A3828
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000A565C File Offset: 0x000A385C
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8) ^ (int)baData[i] ^ (num >> 24);
			}
			return num;
		}

		// Token: 0x040011BB RID: 4539
		private byte[] m_value;

		// Token: 0x040011BC RID: 4540
		private HashAlgorithm m_hashAlg;

		// Token: 0x040011BD RID: 4541
		private SecurityElement m_element;

		// Token: 0x040011BE RID: 4542
		private object s_InternalSyncObject;

		// Token: 0x040011BF RID: 4543
		private const string s_tagHashValue = "HashValue";

		// Token: 0x040011C0 RID: 4544
		private const string s_tagHashAlgorithm = "HashAlgorithm";
	}
}
