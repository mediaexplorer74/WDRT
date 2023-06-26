using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
	/// <summary>An <see cref="T:System.Security.Principal.IPrincipal" /> implementation that supports multiple claims-based identities.</summary>
	// Token: 0x0200031C RID: 796
	[ComVisible(true)]
	[Serializable]
	public class ClaimsPrincipal : IPrincipal
	{
		// Token: 0x0600287C RID: 10364 RVA: 0x00095884 File Offset: 0x00093A84
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000958E8 File Offset: 0x00093AE8
		private static ClaimsPrincipal SelectClaimsPrincipal()
		{
			ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
			if (claimsPrincipal != null)
			{
				return claimsPrincipal;
			}
			return new ClaimsPrincipal(Thread.CurrentPrincipal);
		}

		/// <summary>Gets or sets the delegate used to select the claims identity returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Identity" /> property.</summary>
		/// <returns>The delegate. The default is <see langword="null" />.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x0009590F File Offset: 0x00093B0F
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x00095916 File Offset: 0x00093B16
		public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
		{
			get
			{
				return ClaimsPrincipal.s_identitySelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_identitySelector = value;
			}
		}

		/// <summary>Gets or sets the delegate used to select the claims principal returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" /> property.</summary>
		/// <returns>The delegate. The default is <see langword="null" />.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0009591E File Offset: 0x00093B1E
		// (set) Token: 0x06002881 RID: 10369 RVA: 0x00095925 File Offset: 0x00093B25
		public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
		{
			get
			{
				return ClaimsPrincipal.s_principalSelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_principalSelector = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class.</summary>
		// Token: 0x06002882 RID: 10370 RVA: 0x0009592D File Offset: 0x00093B2D
		public ClaimsPrincipal()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class using the specified claims identities.</summary>
		/// <param name="identities">The identities from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x06002883 RID: 10371 RVA: 0x0009594B File Offset: 0x00093B4B
		public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified identity.</summary>
		/// <param name="identity">The identity from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06002884 RID: 10372 RVA: 0x00095984 File Offset: 0x00093B84
		public ClaimsPrincipal(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_identities.Add(claimsIdentity);
				return;
			}
			this.m_identities.Add(new ClaimsIdentity(identity));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified principal.</summary>
		/// <param name="principal">The principal from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="principal" /> is null.</exception>
		// Token: 0x06002885 RID: 10373 RVA: 0x000959E4 File Offset: 0x00093BE4
		public ClaimsPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
			if (claimsPrincipal == null)
			{
				this.m_identities.Add(new ClaimsIdentity(principal.Identity));
				return;
			}
			if (claimsPrincipal.Identities != null)
			{
				this.m_identities.AddRange(claimsPrincipal.Identities);
			}
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Claims.ClaimsPrincipal" /> with the specified <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.IO.BinaryReader" /> pointing to a <see cref="T:System.Security.Claims.ClaimsPrincipal" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x06002886 RID: 10374 RVA: 0x00095A55 File Offset: 0x00093C55
		public ClaimsPrincipal(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06002887 RID: 10375 RVA: 0x00095A88 File Offset: 0x00093C88
		[SecurityCritical]
		protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context);
		}

		/// <summary>Contains any additional data provided by a derived type. Typically set when calling <see cref="M:System.Security.Claims.ClaimsIdentity.WriteTo(System.IO.BinaryWriter,System.Byte[])" />.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array representing the additional serialized data.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x00095ABC File Offset: 0x00093CBC
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		/// <summary>Returns a copy of this instance.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> object.</returns>
		// Token: 0x06002889 RID: 10377 RVA: 0x00095AC4 File Offset: 0x00093CC4
		public virtual ClaimsPrincipal Clone()
		{
			return new ClaimsPrincipal(this);
		}

		/// <summary>Creates a new claims identity.</summary>
		/// <param name="reader">The binary reader.</param>
		/// <returns>The created claims identity.</returns>
		// Token: 0x0600288A RID: 10378 RVA: 0x00095ACC File Offset: 0x00093CCC
		protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new ClaimsIdentity(reader);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00095AE2 File Offset: 0x00093CE2
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaimsIdentities = this.SerializeIdentities();
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00095AF9 File Offset: 0x00093CF9
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.DeserializeIdentities(this.m_serializedClaimsIdentities);
			this.m_serializedClaimsIdentities = null;
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsPrincipal" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600288D RID: 10381 RVA: 0x00095B17 File Offset: 0x00093D17
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("System.Security.ClaimsPrincipal.Identities", this.SerializeIdentities());
			info.AddValue("System.Security.ClaimsPrincipal.Version", this.m_version);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00095B4C File Offset: 0x00093D4C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "System.Security.ClaimsPrincipal.Identities"))
				{
					if (name == "System.Security.ClaimsPrincipal.Version")
					{
						this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
					}
				}
				else
				{
					this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
				}
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00095BC0 File Offset: 0x00093DC0
		[SecurityCritical]
		private void DeserializeIdentities(string identities)
		{
			this.m_identities = new List<ClaimsIdentity>();
			if (!string.IsNullOrEmpty(identities))
			{
				List<string> list = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(identities)))
				{
					list = (List<string>)binaryFormatter.Deserialize(memoryStream, null, false);
					for (int i = 0; i < list.Count; i += 2)
					{
						ClaimsIdentity claimsIdentity = null;
						using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(list[i + 1])))
						{
							claimsIdentity = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream2, null, false);
						}
						if (!string.IsNullOrEmpty(list[i]))
						{
							long num;
							if (!long.TryParse(list[i], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
							}
							claimsIdentity = new WindowsIdentity(claimsIdentity, new IntPtr(num));
						}
						this.m_identities.Add(claimsIdentity);
					}
				}
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00095CD0 File Offset: 0x00093ED0
		[SecurityCritical]
		private string SerializeIdentities()
		{
			List<string> list = new List<string>();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			foreach (ClaimsIdentity claimsIdentity in this.m_identities)
			{
				if (claimsIdentity.GetType() == typeof(WindowsIdentity))
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					list.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString(NumberFormatInfo.InvariantInfo));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						binaryFormatter.Serialize(memoryStream, windowsIdentity.CloneAsBase(), null, false);
						list.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						continue;
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					list.Add("");
					binaryFormatter.Serialize(memoryStream2, claimsIdentity, null, false);
					list.Add(Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
			string text;
			using (MemoryStream memoryStream3 = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream3, list, null, false);
				text = Convert.ToBase64String(memoryStream3.GetBuffer(), 0, (int)memoryStream3.Length);
			}
			return text;
		}

		/// <summary>Adds the specified claims identity to this claims principal.</summary>
		/// <param name="identity">The claims identity to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06002891 RID: 10385 RVA: 0x00095E58 File Offset: 0x00094058
		[SecurityCritical]
		public virtual void AddIdentity(ClaimsIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identities.Add(identity);
		}

		/// <summary>Adds the specified claims identities to this claims principal.</summary>
		/// <param name="identities">The claims identities to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x06002892 RID: 10386 RVA: 0x00095E74 File Offset: 0x00094074
		[SecurityCritical]
		public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Gets a collection that contains all of the claims from all of the claims identities associated with this claims principal.</summary>
		/// <returns>The claims associated with this principal.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x00095E90 File Offset: 0x00094090
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					foreach (Claim claim in claimsIdentity.Claims)
					{
						yield return claim;
					}
					IEnumerator<Claim> enumerator2 = null;
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		/// <summary>Gets the current claims principal.</summary>
		/// <returns>The current claims principal.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x00095EAD File Offset: 0x000940AD
		public static ClaimsPrincipal Current
		{
			get
			{
				if (ClaimsPrincipal.s_principalSelector != null)
				{
					return ClaimsPrincipal.s_principalSelector();
				}
				return ClaimsPrincipal.SelectClaimsPrincipal();
			}
		}

		/// <summary>Retrieves all of the claims that are matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The matching claims.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06002895 RID: 10389 RVA: 0x00095EC8 File Offset: 0x000940C8
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(match))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves all or the claims that have the specified claim type.</summary>
		/// <param name="type">The claim type against which to match claims.</param>
		/// <returns>The matching claims.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06002896 RID: 10390 RVA: 0x00095F6C File Offset: 0x0009416C
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(type))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves the first claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06002897 RID: 10391 RVA: 0x00096010 File Offset: 0x00094210
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			Claim claim = null;
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					claim = claimsIdentity.FindFirst(match);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <param name="type">The claim type to match.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06002898 RID: 10392 RVA: 0x0009607C File Offset: 0x0009427C
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Claim claim = null;
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null)
				{
					claim = this.m_identities[i].FindFirst(type);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>
		///   <see langword="true" /> if a matching claim exists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06002899 RID: 10393 RVA: 0x000960D8 File Offset: 0x000942D8
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(match))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim with the specified claim type and value.</summary>
		/// <param name="type">The type of the claim to match.</param>
		/// <param name="value">The value of the claim to match.</param>
		/// <returns>
		///   <see langword="true" /> if a matching claim exists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.  
		/// -or-  
		/// <paramref name="value" /> is null.</exception>
		// Token: 0x0600289A RID: 10394 RVA: 0x00096130 File Offset: 0x00094330
		public virtual bool HasClaim(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(type, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a collection that contains all of the claims identities associated with this claims principal.</summary>
		/// <returns>The collection of claims identities.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x00096195 File Offset: 0x00094395
		public virtual IEnumerable<ClaimsIdentity> Identities
		{
			get
			{
				return this.m_identities.AsReadOnly();
			}
		}

		/// <summary>Gets the primary claims identity associated with this claims principal.</summary>
		/// <returns>The primary claims identity associated with this claims principal.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000961A2 File Offset: 0x000943A2
		public virtual IIdentity Identity
		{
			get
			{
				if (ClaimsPrincipal.s_identitySelector != null)
				{
					return ClaimsPrincipal.s_identitySelector(this.m_identities);
				}
				return ClaimsPrincipal.SelectPrimaryIdentity(this.m_identities);
			}
		}

		/// <summary>Returns a value that indicates whether the entity (user) represented by this claims principal is in the specified role.</summary>
		/// <param name="role">The role for which to check.</param>
		/// <returns>
		///   <see langword="true" /> if claims principal is in the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600289D RID: 10397 RVA: 0x000961C8 File Offset: 0x000943C8
		public virtual bool IsInRole(string role)
		{
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(this.m_identities[i].RoleClaimType, role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00096224 File Offset: 0x00094424
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = 0;
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				num2++;
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.m_identities.Add(this.CreateClaimsIdentity(reader));
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num2++;
			}
			for (int j = num2; j < num; j++)
			{
				reader.ReadString();
			}
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x0600289F RID: 10399 RVA: 0x000962B9 File Offset: 0x000944B9
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <param name="userData">Additional data provided by the derived type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x060028A0 RID: 10400 RVA: 0x000962C4 File Offset: 0x000944C4
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
			if (this.m_identities.Count > 0)
			{
				serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				writer.Write(this.m_identities.Count);
				foreach (ClaimsIdentity claimsIdentity in this.m_identities)
				{
					claimsIdentity.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x04000FAD RID: 4013
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000FAE RID: 4014
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsPrincipal.";

		// Token: 0x04000FAF RID: 4015
		[NonSerialized]
		private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";

		// Token: 0x04000FB0 RID: 4016
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsPrincipal.Version";

		// Token: 0x04000FB1 RID: 4017
		[OptionalField(VersionAdded = 2)]
		private string m_version = "1.0";

		// Token: 0x04000FB2 RID: 4018
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaimsIdentities;

		// Token: 0x04000FB3 RID: 4019
		[NonSerialized]
		private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();

		// Token: 0x04000FB4 RID: 4020
		[NonSerialized]
		private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);

		// Token: 0x04000FB5 RID: 4021
		[NonSerialized]
		private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;

		// Token: 0x02000B4E RID: 2894
		private enum SerializationMask
		{
			// Token: 0x040033F7 RID: 13303
			None,
			// Token: 0x040033F8 RID: 13304
			HasIdentities,
			// Token: 0x040033F9 RID: 13305
			UserData
		}
	}
}
