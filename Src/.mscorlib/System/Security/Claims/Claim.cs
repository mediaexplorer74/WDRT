using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	/// <summary>Represents a claim.</summary>
	// Token: 0x0200031A RID: 794
	[Serializable]
	public class Claim
	{
		/// <summary>Initializes an instance of <see cref="T:System.Security.Claims.Claim" /> with the specified <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.IO.BinaryReader" /> pointing to a <see cref="T:System.Security.Claims.Claim" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x0600282D RID: 10285 RVA: 0x00093F4B File Offset: 0x0009214B
		public Claim(BinaryReader reader)
			: this(reader, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified reader and subject.</summary>
		/// <param name="reader">The binary reader.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		// Token: 0x0600282E RID: 10286 RVA: 0x00093F55 File Offset: 0x00092155
		public Claim(BinaryReader reader, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader, subject);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, and value.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600282F RID: 10287 RVA: 0x00093F7E File Offset: 0x0009217E
		public Claim(string type, string value)
			: this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, and value type.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002830 RID: 10288 RVA: 0x00093F98 File Offset: 0x00092198
		public Claim(string type, string value, string valueType)
			: this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, and issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002831 RID: 10289 RVA: 0x00093FAE File Offset: 0x000921AE
		public Claim(string type, string value, string valueType, string issuer)
			: this(type, value, valueType, issuer, issuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer,  and original issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or <see langword="null" />, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002832 RID: 10290 RVA: 0x00093FBE File Offset: 0x000921BE
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer)
			: this(type, value, valueType, issuer, originalIssuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer, original issuer and subject.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or <see langword="null" />, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002833 RID: 10291 RVA: 0x00093FD0 File Offset: 0x000921D0
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
			: this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00093FF0 File Offset: 0x000921F0
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class.</summary>
		/// <param name="other">The security claim.</param>
		// Token: 0x06002835 RID: 10293 RVA: 0x000940AC File Offset: 0x000922AC
		protected Claim(Claim other)
			: this(other, (other == null) ? null : other.m_subject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified security claim and subject.</summary>
		/// <param name="other">The security claim.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		// Token: 0x06002836 RID: 10294 RVA: 0x000940C4 File Offset: 0x000922C4
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string text in other.m_properties.Keys)
				{
					this.m_properties.Add(text, other.m_properties[text]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
		}

		/// <summary>Contains any additional data provided by a derived type.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array representing the additional serialized data.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x000941C0 File Offset: 0x000923C0
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		/// <summary>Gets the issuer of the claim.</summary>
		/// <returns>A name that refers to the issuer of the claim.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000941C8 File Offset: 0x000923C8
		public string Issuer
		{
			get
			{
				return this.m_issuer;
			}
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000941D0 File Offset: 0x000923D0
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		/// <summary>Gets the original issuer of the claim.</summary>
		/// <returns>A name that refers to the original issuer of the claim.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000941DD File Offset: 0x000923DD
		public string OriginalIssuer
		{
			get
			{
				return this.m_originalIssuer;
			}
		}

		/// <summary>Gets a dictionary that contains additional properties associated with this claim.</summary>
		/// <returns>A dictionary that contains additional properties associated with the claim. The properties are represented as name-value pairs.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000941E8 File Offset: 0x000923E8
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		/// <summary>Gets the subject of the claim.</summary>
		/// <returns>The subject of the claim.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x00094244 File Offset: 0x00092444
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x0009424C File Offset: 0x0009244C
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		/// <summary>Gets the claim type of the claim.</summary>
		/// <returns>The claim type.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x00094255 File Offset: 0x00092455
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets the value of the claim.</summary>
		/// <returns>The claim value.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x0009425D File Offset: 0x0009245D
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Gets the value type of the claim.</summary>
		/// <returns>The claim value type.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x00094265 File Offset: 0x00092465
		public string ValueType
		{
			get
			{
				return this.m_valueType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The new claim does not have a subject.</summary>
		/// <returns>The new claim object.</returns>
		// Token: 0x06002841 RID: 10305 RVA: 0x0009426D File Offset: 0x0009246D
		public virtual Claim Clone()
		{
			return this.Clone(null);
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The subject of the new claim is set to the specified ClaimsIdentity.</summary>
		/// <param name="identity">The intended subject of the new claim.</param>
		/// <returns>The new claim object.</returns>
		// Token: 0x06002842 RID: 10306 RVA: 0x00094276 File Offset: 0x00092476
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00094280 File Offset: 0x00092480
		private void Initialize(BinaryReader reader, ClaimsIdentity subject)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_subject = subject;
			Claim.SerializationMask serializationMask = (Claim.SerializationMask)reader.ReadInt32();
			int num = 1;
			int num2 = reader.ReadInt32();
			this.m_value = reader.ReadString();
			if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
			{
				this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
			{
				this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			else
			{
				this.m_type = reader.ReadString();
				num++;
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				this.m_valueType = reader.ReadString();
				num++;
			}
			else
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				this.m_issuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				this.m_originalIssuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_originalIssuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.Properties.Add(reader.ReadString(), reader.ReadString());
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num++;
			}
			for (int j = num; j < num2; j++)
			{
				reader.ReadString();
			}
		}

		/// <summary>Writes this <see cref="T:System.Security.Claims.Claim" /> to the writer.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x06002844 RID: 10308 RVA: 0x000943EA File Offset: 0x000925EA
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		/// <summary>Writes this <see cref="T:System.Security.Claims.Claim" /> to the writer.</summary>
		/// <param name="writer">The writer to write this claim.</param>
		/// <param name="userData">The user data to claim.</param>
		// Token: 0x06002845 RID: 10309 RVA: 0x000943F4 File Offset: 0x000925F4
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 1;
			Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
			if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
			{
				serializationMask |= Claim.SerializationMask.NameClaimType;
			}
			else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
			{
				serializationMask |= Claim.SerializationMask.RoleClaimType;
			}
			else
			{
				num++;
			}
			if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.StringType;
			}
			if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.Issuer;
			}
			if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
			{
				serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
			}
			else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.OriginalIssuer;
			}
			if (this.Properties.Count > 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.HasProperties;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			writer.Write(this.m_value);
			if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_type);
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				writer.Write(this.m_valueType);
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				writer.Write(this.m_issuer);
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				writer.Write(this.m_originalIssuer);
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				writer.Write(this.Properties.Count);
				foreach (string text in this.Properties.Keys)
				{
					writer.Write(text);
					writer.Write(this.Properties[text]);
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</returns>
		// Token: 0x06002846 RID: 10310 RVA: 0x000945DC File Offset: 0x000927DC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x04000F8B RID: 3979
		private string m_issuer;

		// Token: 0x04000F8C RID: 3980
		private string m_originalIssuer;

		// Token: 0x04000F8D RID: 3981
		private string m_type;

		// Token: 0x04000F8E RID: 3982
		private string m_value;

		// Token: 0x04000F8F RID: 3983
		private string m_valueType;

		// Token: 0x04000F90 RID: 3984
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000F91 RID: 3985
		private Dictionary<string, string> m_properties;

		// Token: 0x04000F92 RID: 3986
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x04000F93 RID: 3987
		[NonSerialized]
		private ClaimsIdentity m_subject;

		// Token: 0x02000B4B RID: 2891
		private enum SerializationMask
		{
			// Token: 0x040033DD RID: 13277
			None,
			// Token: 0x040033DE RID: 13278
			NameClaimType,
			// Token: 0x040033DF RID: 13279
			RoleClaimType,
			// Token: 0x040033E0 RID: 13280
			StringType = 4,
			// Token: 0x040033E1 RID: 13281
			Issuer = 8,
			// Token: 0x040033E2 RID: 13282
			OriginalIssuerEqualsIssuer = 16,
			// Token: 0x040033E3 RID: 13283
			OriginalIssuer = 32,
			// Token: 0x040033E4 RID: 13284
			HasProperties = 64,
			// Token: 0x040033E5 RID: 13285
			UserData = 128
		}
	}
}
