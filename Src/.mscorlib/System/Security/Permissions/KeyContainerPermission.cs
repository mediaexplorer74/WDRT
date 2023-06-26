using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access key containers. This class cannot be inherited.</summary>
	// Token: 0x02000316 RID: 790
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with either restricted or unrestricted permission.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x060027F8 RID: 10232 RVA: 0x00092620 File Offset: 0x00090820
		public KeyContainerPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
			}
			else
			{
				if (state != PermissionState.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
				}
				this.m_flags = KeyContainerPermissionFlags.NoFlags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified access.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</exception>
		// Token: 0x060027F9 RID: 10233 RVA: 0x00092671 File Offset: 0x00090871
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified global access and specific key container access rights.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <param name="accessList">An array of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects identifying specific key container access rights.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessList" /> is <see langword="null" />.</exception>
		// Token: 0x060027FA RID: 10234 RVA: 0x00092698 File Offset: 0x00090898
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			if (accessList == null)
			{
				throw new ArgumentNullException("accessList");
			}
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			for (int i = 0; i < accessList.Length; i++)
			{
				this.m_accessEntries.Add(accessList[i]);
			}
		}

		/// <summary>Gets the key container permission flags that apply to all key containers associated with the permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x000926F4 File Offset: 0x000908F4
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects associated with the current permission.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" /> containing the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects for this <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000926FC File Offset: 0x000908FC
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this.m_accessEntries;
			}
		}

		/// <summary>Determines whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060027FD RID: 10237 RVA: 0x00092704 File Offset: 0x00090904
		public bool IsUnrestricted()
		{
			if (this.m_flags != KeyContainerPermissionFlags.AllFlags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				if ((keyContainerPermissionAccessEntry.Flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.AllFlags)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00092754 File Offset: 0x00090954
		private bool IsEmpty()
		{
			if (this.Flags == KeyContainerPermissionFlags.NoFlags)
			{
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
				{
					if (keyContainerPermissionAccessEntry.Flags != KeyContainerPermissionFlags.NoFlags)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x060027FF RID: 10239 RVA: 0x00092794 File Offset: 0x00090994
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if ((this.m_flags & keyContainerPermission.m_flags) != this.m_flags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry, keyContainerPermission);
				if ((keyContainerPermissionAccessEntry.Flags & applicableFlags) != keyContainerPermissionAccessEntry.Flags)
				{
					return false;
				}
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags2 = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry2, this);
				if ((applicableFlags2 & keyContainerPermissionAccessEntry2.Flags) != applicableFlags2)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06002800 RID: 10240 RVA: 0x0009286C File Offset: 0x00090A6C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsEmpty() || keyContainerPermission.IsEmpty())
			{
				return null;
			}
			KeyContainerPermissionFlags keyContainerPermissionFlags = keyContainerPermission.m_flags & this.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(keyContainerPermissionFlags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(keyContainerPermissionAccessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(keyContainerPermissionAccessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06002801 RID: 10241 RVA: 0x00092938 File Offset: 0x00090B38
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsUnrestricted() || keyContainerPermission.IsUnrestricted())
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			KeyContainerPermissionFlags keyContainerPermissionFlags = this.m_flags | keyContainerPermission.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(keyContainerPermissionFlags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(keyContainerPermissionAccessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(keyContainerPermissionAccessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002802 RID: 10242 RVA: 0x00092A0C File Offset: 0x00090C0C
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(this.m_flags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
			}
			return keyContainerPermission;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including state information.</returns>
		// Token: 0x06002803 RID: 10243 RVA: 0x00092A5C File Offset: 0x00090C5C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.KeyContainerPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
				if (this.AccessEntries.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("AccessList");
					foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
					{
						SecurityElement securityElement3 = new SecurityElement("AccessEntry");
						securityElement3.AddAttribute("KeyStore", keyContainerPermissionAccessEntry.KeyStore);
						securityElement3.AddAttribute("ProviderName", keyContainerPermissionAccessEntry.ProviderName);
						securityElement3.AddAttribute("ProviderType", keyContainerPermissionAccessEntry.ProviderType.ToString(null, null));
						securityElement3.AddAttribute("KeyContainerName", keyContainerPermissionAccessEntry.KeyContainerName);
						securityElement3.AddAttribute("KeySpec", keyContainerPermissionAccessEntry.KeySpec.ToString(null, null));
						securityElement3.AddAttribute("Flags", keyContainerPermissionAccessEntry.Flags.ToString());
						securityElement2.AddChild(securityElement3);
					}
					securityElement.AddChild(securityElement2);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number of <paramref name="securityElement" /> is not supported.</exception>
		// Token: 0x06002804 RID: 10244 RVA: 0x00092B9C File Offset: 0x00090D9C
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
			if (XMLUtil.IsUnrestricted(securityElement))
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
				this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
				return;
			}
			this.m_flags = KeyContainerPermissionFlags.NoFlags;
			string text = securityElement.Attribute("Flags");
			if (text != null)
			{
				KeyContainerPermissionFlags keyContainerPermissionFlags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text);
				KeyContainerPermission.VerifyFlags(keyContainerPermissionFlags);
				this.m_flags = keyContainerPermissionFlags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessList"))
					{
						this.AddAccessEntries(securityElement2);
					}
				}
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x00092C72 File Offset: 0x00090E72
		int IBuiltInPermission.GetTokenIndex()
		{
			return KeyContainerPermission.GetTokenIndex();
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x00092C7C File Offset: 0x00090E7C
		private void AddAccessEntries(SecurityElement securityElement)
		{
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessEntry"))
					{
						int count = securityElement2.m_lAttributes.Count;
						string text = null;
						string text2 = null;
						int num = -1;
						string text3 = null;
						int num2 = -1;
						KeyContainerPermissionFlags keyContainerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
						for (int i = 0; i < count; i += 2)
						{
							string text4 = (string)securityElement2.m_lAttributes[i];
							string text5 = (string)securityElement2.m_lAttributes[i + 1];
							if (string.Equals(text4, "KeyStore"))
							{
								text = text5;
							}
							if (string.Equals(text4, "ProviderName"))
							{
								text2 = text5;
							}
							else if (string.Equals(text4, "ProviderType"))
							{
								num = Convert.ToInt32(text5, null);
							}
							else if (string.Equals(text4, "KeyContainerName"))
							{
								text3 = text5;
							}
							else if (string.Equals(text4, "KeySpec"))
							{
								num2 = Convert.ToInt32(text5, null);
							}
							else if (string.Equals(text4, "Flags"))
							{
								keyContainerPermissionFlags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text5);
							}
						}
						KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(text, text2, num, text3, num2, keyContainerPermissionFlags);
						this.AccessEntries.Add(keyContainerPermissionAccessEntry);
					}
				}
			}
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00092DF8 File Offset: 0x00090FF8
		private void AddAccessEntryAndUnion(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags |= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x00092E30 File Offset: 0x00091030
		private void AddAccessEntryAndIntersect(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags &= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x00092E65 File Offset: 0x00091065
		internal static void VerifyFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & ~(KeyContainerPermissionFlags.Create | KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Delete | KeyContainerPermissionFlags.Import | KeyContainerPermissionFlags.Export | KeyContainerPermissionFlags.Sign | KeyContainerPermissionFlags.Decrypt | KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl)) != KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flags }));
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x00092E90 File Offset: 0x00091090
		private static KeyContainerPermissionFlags GetApplicableFlags(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionFlags keyContainerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
			bool flag = true;
			int num = target.AccessEntries.IndexOf(accessEntry);
			if (num != -1)
			{
				return target.AccessEntries[num].Flags;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in target.AccessEntries)
			{
				if (accessEntry.IsSubsetOf(keyContainerPermissionAccessEntry))
				{
					if (!flag)
					{
						keyContainerPermissionFlags &= keyContainerPermissionAccessEntry.Flags;
					}
					else
					{
						keyContainerPermissionFlags = keyContainerPermissionAccessEntry.Flags;
						flag = false;
					}
				}
			}
			if (flag)
			{
				keyContainerPermissionFlags = target.Flags;
			}
			return keyContainerPermissionFlags;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00092F12 File Offset: 0x00091112
		private static int GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x04000F7B RID: 3963
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04000F7C RID: 3964
		private KeyContainerPermissionAccessEntryCollection m_accessEntries;
	}
}
