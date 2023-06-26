using System;
using System.Globalization;

namespace System.Security.Permissions
{
	/// <summary>Controls access to stores containing X.509 certificates. This class cannot be inherited.</summary>
	// Token: 0x02000483 RID: 1155
	[Serializable]
	public sealed class StorePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermission" /> class with either fully restricted or unrestricted permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x06002ACC RID: 10956 RVA: 0x000C2F88 File Offset: 0x000C1188
		public StorePermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = StorePermissionFlags.AllFlags;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_flags = StorePermissionFlags.NoFlags;
				return;
			}
			throw new ArgumentException(SR.GetString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermission" /> class with the specified access.</summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid combination of <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values.</exception>
		// Token: 0x06002ACD RID: 10957 RVA: 0x000C2FBA File Offset: 0x000C11BA
		public StorePermission(StorePermissionFlags flag)
		{
			StorePermission.VerifyFlags(flag);
			this.m_flags = flag;
		}

		/// <summary>Gets or sets the type of <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> access allowed by the current permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.StorePermissionFlags" /> for the valid values.</exception>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000C2FDE File Offset: 0x000C11DE
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x000C2FCF File Offset: 0x000C11CF
		public StorePermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				StorePermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AD0 RID: 10960 RVA: 0x000C2FE6 File Offset: 0x000C11E6
		public bool IsUnrestricted()
		{
			return this.m_flags == StorePermissionFlags.AllFlags;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002AD1 RID: 10961 RVA: 0x000C2FF8 File Offset: 0x000C11F8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission permission;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags storePermissionFlags = this.m_flags | storePermission.m_flags;
				if (storePermissionFlags == StorePermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new StorePermission(storePermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return permission;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002AD2 RID: 10962 RVA: 0x000C3078 File Offset: 0x000C1278
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == StorePermissionFlags.NoFlags;
			}
			bool flag;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags flags = this.m_flags;
				StorePermissionFlags flags2 = storePermission.m_flags;
				flag = (flags & flags2) == flags;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> s not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002AD3 RID: 10963 RVA: 0x000C30F4 File Offset: 0x000C12F4
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission permission;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags storePermissionFlags = storePermission.m_flags & this.m_flags;
				if (storePermissionFlags == StorePermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new StorePermission(storePermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return permission;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002AD4 RID: 10964 RVA: 0x000C316C File Offset: 0x000C136C
		public override IPermission Copy()
		{
			return new StorePermission(this.m_flags);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002AD5 RID: 10965 RVA: 0x000C317C File Offset: 0x000C137C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number in <paramref name="securityElement" /> is not valid.</exception>
		// Token: 0x06002AD6 RID: 10966 RVA: 0x000C321C File Offset: 0x000C141C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidClassAttribute"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = StorePermissionFlags.AllFlags;
				return;
			}
			this.m_flags = StorePermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				StorePermissionFlags storePermissionFlags = (StorePermissionFlags)Enum.Parse(typeof(StorePermissionFlags), text3);
				StorePermission.VerifyFlags(storePermissionFlags);
				this.m_flags = storePermissionFlags;
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000C32D2 File Offset: 0x000C14D2
		internal static void VerifyFlags(StorePermissionFlags flags)
		{
			if ((flags & ~(StorePermissionFlags.CreateStore | StorePermissionFlags.DeleteStore | StorePermissionFlags.EnumerateStores | StorePermissionFlags.OpenStore | StorePermissionFlags.AddToStore | StorePermissionFlags.RemoveFromStore | StorePermissionFlags.EnumerateCertificates)) != StorePermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { (int)flags }));
			}
		}

		// Token: 0x04002645 RID: 9797
		private StorePermissionFlags m_flags;
	}
}
