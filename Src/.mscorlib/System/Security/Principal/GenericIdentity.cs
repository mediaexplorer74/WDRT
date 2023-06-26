using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic user.</summary>
	// Token: 0x02000320 RID: 800
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060028A4 RID: 10404 RVA: 0x000963E9 File Offset: 0x000945E9
		[SecuritySafeCritical]
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
			this.AddNameClaim();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name and authentication type.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running.</param>
		/// <param name="type">The type of authentication used to identify the user.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060028A5 RID: 10405 RVA: 0x00096417 File Offset: 0x00094617
		[SecuritySafeCritical]
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0009644F File Offset: 0x0009464F
		private GenericIdentity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class by using the specified <see cref="T:System.Security.Principal.GenericIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.GenericIdentity" />.</param>
		// Token: 0x060028A7 RID: 10407 RVA: 0x00096457 File Offset: 0x00094657
		protected GenericIdentity(GenericIdentity identity)
			: base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060028A8 RID: 10408 RVA: 0x00096478 File Offset: 0x00094678
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		/// <summary>Gets all claims for the user represented by this generic identity.</summary>
		/// <returns>A collection of claims for this <see cref="T:System.Security.Principal.GenericIdentity" /> object.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x00096480 File Offset: 0x00094680
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		/// <summary>Gets the user's name.</summary>
		/// <returns>The name of the user on whose behalf the code is being run.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x00096488 File Offset: 0x00094688
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x00096490 File Offset: 0x00094690
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the user was has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x00096498 File Offset: 0x00094698
		public override bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000964B0 File Offset: 0x000946B0
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			bool flag = false;
			using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Claim claim = enumerator.Current;
					flag = true;
				}
			}
			if (!flag)
			{
				this.AddNameClaim();
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00096508 File Offset: 0x00094708
		[SecuritySafeCritical]
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x04001012 RID: 4114
		private string m_name;

		// Token: 0x04001013 RID: 4115
		private string m_type;
	}
}
