using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Allows control of code access security permissions.</summary>
	// Token: 0x02000489 RID: 1161
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class.</summary>
		// Token: 0x06002AFF RID: 11007 RVA: 0x000C38AA File Offset: 0x000C1AAA
		protected ResourcePermissionBase()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class with the specified level of access to resources at creation.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002B00 RID: 11008 RVA: 0x000C38BD File Offset: 0x000C1ABD
		protected ResourcePermissionBase(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.isUnrestricted = false;
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidPermissionState"), "state");
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000C38FB File Offset: 0x000C1AFB
		private static Hashtable CreateHashtable()
		{
			return new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000C3908 File Offset: 0x000C1B08
		private string ComputerName
		{
			get
			{
				if (ResourcePermissionBase.computerName == null)
				{
					Type typeFromHandle = typeof(ResourcePermissionBase);
					lock (typeFromHandle)
					{
						if (ResourcePermissionBase.computerName == null)
						{
							StringBuilder stringBuilder = new StringBuilder(256);
							int capacity = stringBuilder.Capacity;
							ResourcePermissionBase.UnsafeNativeMethods.GetComputerName(stringBuilder, ref capacity);
							ResourcePermissionBase.computerName = stringBuilder.ToString();
						}
					}
				}
				return ResourcePermissionBase.computerName;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000C3988 File Offset: 0x000C1B88
		private bool IsEmpty
		{
			get
			{
				return !this.isUnrestricted && this.rootTable.Count == 0;
			}
		}

		/// <summary>Gets or sets an enumeration value that describes the types of access that you are giving the resource.</summary>
		/// <returns>An enumeration value that is derived from <see cref="T:System.Type" /> and describes the types of access that you are giving the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property value is not an enumeration value.</exception>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000C39A2 File Offset: 0x000C1BA2
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000C39AA File Offset: 0x000C1BAA
		protected Type PermissionAccessType
		{
			get
			{
				return this.permissionAccessType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException(SR.GetString("PermissionBadParameterEnum"), "value");
				}
				this.permissionAccessType = value;
			}
		}

		/// <summary>Gets or sets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are trying to protect.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the array is 0.</exception>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000C39E4 File Offset: 0x000C1BE4
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x000C39EC File Offset: 0x000C1BEC
		protected string[] TagNames
		{
			get
			{
				return this.tagNames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("PermissionInvalidLength", new object[] { "0" }), "value");
				}
				this.tagNames = value;
			}
		}

		/// <summary>Adds a permission entry to the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.  
		///  -or-  
		///  The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is already included in the permission.</exception>
		// Token: 0x06002B08 RID: 11016 RVA: 0x000C3A2C File Offset: 0x000C1C2C
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath.Length != this.TagNames.Length)
			{
				throw new InvalidOperationException(SR.GetString("PermissionNumberOfElements"));
			}
			Hashtable hashtable = this.rootTable;
			string[] permissionAccessPath = entry.PermissionAccessPath;
			for (int i = 0; i < permissionAccessPath.Length - 1; i++)
			{
				if (hashtable.ContainsKey(permissionAccessPath[i]))
				{
					hashtable = (Hashtable)hashtable[permissionAccessPath[i]];
				}
				else
				{
					Hashtable hashtable2 = ResourcePermissionBase.CreateHashtable();
					hashtable[permissionAccessPath[i]] = hashtable2;
					hashtable = hashtable2;
				}
			}
			if (hashtable.ContainsKey(permissionAccessPath[permissionAccessPath.Length - 1]))
			{
				throw new InvalidOperationException(SR.GetString("PermissionItemExists"));
			}
			hashtable[permissionAccessPath[permissionAccessPath.Length - 1]] = entry.PermissionAccess;
		}

		/// <summary>Clears the permission of the added permission entries.</summary>
		// Token: 0x06002B09 RID: 11017 RVA: 0x000C3AEC File Offset: 0x000C1CEC
		protected void Clear()
		{
			this.rootTable.Clear();
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06002B0A RID: 11018 RVA: 0x000C3AFC File Offset: 0x000C1CFC
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = this.CreateInstance();
			resourcePermissionBase.tagNames = this.tagNames;
			resourcePermissionBase.permissionAccessType = this.permissionAccessType;
			resourcePermissionBase.isUnrestricted = this.isUnrestricted;
			resourcePermissionBase.rootTable = this.CopyChildren(this.rootTable, 0);
			return resourcePermissionBase;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000C3B48 File Offset: 0x000C1D48
		private Hashtable CopyChildren(object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			while (enumerator.MoveNext())
			{
				if (tagIndex < this.TagNames.Length - 1)
				{
					hashtable[enumerator.Key] = this.CopyChildren(enumerator.Value, tagIndex + 1);
				}
				else
				{
					hashtable[enumerator.Key] = enumerator.Value;
				}
			}
			return hashtable;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000C3BAE File Offset: 0x000C1DAE
		private ResourcePermissionBase CreateInstance()
		{
			new PermissionSet(PermissionState.Unrestricted).Assert();
			return (ResourcePermissionBase)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
		}

		/// <summary>Returns an array of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects added to this permission.</summary>
		/// <returns>An array of <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects that were added to this permission.</returns>
		// Token: 0x06002B0D RID: 11021 RVA: 0x000C3BD3 File Offset: 0x000C1DD3
		protected ResourcePermissionBaseEntry[] GetPermissionEntries()
		{
			return this.GetChildrenAccess(this.rootTable, 0);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000C3BE4 File Offset: 0x000C1DE4
		private ResourcePermissionBaseEntry[] GetChildrenAccess(object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			ArrayList arrayList = new ArrayList();
			while (enumerator.MoveNext())
			{
				if (tagIndex < this.TagNames.Length - 1)
				{
					ResourcePermissionBaseEntry[] childrenAccess = this.GetChildrenAccess(enumerator.Value, tagIndex + 1);
					for (int i = 0; i < childrenAccess.Length; i++)
					{
						childrenAccess[i].PermissionAccessPath[tagIndex] = (string)enumerator.Key;
					}
					arrayList.AddRange(childrenAccess);
				}
				else
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = new ResourcePermissionBaseEntry((int)enumerator.Value, new string[this.TagNames.Length]);
					resourcePermissionBaseEntry.PermissionAccessPath[tagIndex] = (string)enumerator.Key;
					arrayList.Add(resourcePermissionBaseEntry);
				}
			}
			return (ResourcePermissionBaseEntry[])arrayList.ToArray(typeof(ResourcePermissionBaseEntry));
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> parameter is not a valid permission element.  
		///  -or-  
		///  The version number of the <paramref name="securityElement" /> parameter is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B0F RID: 11023 RVA: 0x000C3CB0 File Offset: 0x000C1EB0
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("Permission") && !securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("Argument_NotAPermissionElement"));
			}
			string text = securityElement.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidXMLBadVersion"));
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.isUnrestricted = true;
				return;
			}
			this.isUnrestricted = false;
			this.rootTable = (Hashtable)this.ReadChildren(securityElement, 0);
		}

		/// <summary>Creates and returns a permission object that is the intersection of the current permission object and a target permission object.</summary>
		/// <param name="target">A permission object of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The target permission object is not of the same type as the current permission object.</exception>
		// Token: 0x06002B10 RID: 11024 RVA: 0x000C3D6C File Offset: 0x000C1F6C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw new ArgumentException(SR.GetString("PermissionTypeMismatch"), "target");
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.Copy();
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return this.Copy();
			}
			ResourcePermissionBase resourcePermissionBase2 = null;
			Hashtable hashtable = (Hashtable)this.IntersectContents(this.rootTable, resourcePermissionBase.rootTable);
			if (hashtable != null)
			{
				resourcePermissionBase2 = this.CreateInstance();
				resourcePermissionBase2.rootTable = hashtable;
			}
			return resourcePermissionBase2;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000C3DF8 File Offset: 0x000C1FF8
		private object IntersectContents(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return num & num2;
			}
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			object obj = ((Hashtable)currentContent)["."];
			object obj2 = ((Hashtable)currentContent)[this.ComputerName];
			if (obj != null || obj2 != null)
			{
				object obj3 = ((Hashtable)targetContent)["."];
				object obj4 = ((Hashtable)targetContent)[this.ComputerName];
				if (obj3 != null || obj4 != null)
				{
					object obj5 = obj;
					if (obj != null && obj2 != null)
					{
						obj5 = this.UnionOfContents(obj, obj2);
					}
					else if (obj2 != null)
					{
						obj5 = obj2;
					}
					object obj6 = obj3;
					if (obj3 != null && obj4 != null)
					{
						obj6 = this.UnionOfContents(obj3, obj4);
					}
					else if (obj4 != null)
					{
						obj6 = obj4;
					}
					object obj7 = this.IntersectContents(obj5, obj6);
					if (this.HasContent(obj7))
					{
						if (obj2 != null || obj4 != null)
						{
							hashtable[this.ComputerName] = obj7;
						}
						else
						{
							hashtable["."] = obj7;
						}
					}
				}
			}
			IDictionaryEnumerator dictionaryEnumerator;
			Hashtable hashtable2;
			if (((Hashtable)currentContent).Count < ((Hashtable)targetContent).Count)
			{
				dictionaryEnumerator = ((Hashtable)currentContent).GetEnumerator();
				hashtable2 = (Hashtable)targetContent;
			}
			else
			{
				dictionaryEnumerator = ((Hashtable)targetContent).GetEnumerator();
				hashtable2 = (Hashtable)currentContent;
			}
			while (dictionaryEnumerator.MoveNext())
			{
				string text = (string)dictionaryEnumerator.Key;
				if (hashtable2.ContainsKey(text) && text != "." && text != this.ComputerName)
				{
					object value = dictionaryEnumerator.Value;
					object obj8 = hashtable2[text];
					object obj9 = this.IntersectContents(value, obj8);
					if (this.HasContent(obj9))
					{
						hashtable[text] = obj9;
					}
				}
			}
			if (hashtable.Count <= 0)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000C3FCC File Offset: 0x000C21CC
		private bool HasContent(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (value is int)
			{
				int num = (int)value;
				return num != 0;
			}
			Hashtable hashtable = (Hashtable)value;
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.HasContent(enumerator.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000C401C File Offset: 0x000C221C
		private bool IsContentSubset(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return (num & num2) == num;
			}
			Hashtable hashtable = (Hashtable)currentContent;
			Hashtable hashtable2 = (Hashtable)targetContent;
			object obj = hashtable2["*"];
			if (obj != null)
			{
				foreach (object obj2 in hashtable)
				{
					if (!this.IsContentSubset(((DictionaryEntry)obj2).Value, obj))
					{
						return false;
					}
				}
				return true;
			}
			foreach (object obj3 in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				string text = (string)dictionaryEntry.Key;
				if (this.HasContent(dictionaryEntry.Value) && text != "." && text != this.ComputerName)
				{
					if (!hashtable2.ContainsKey(text))
					{
						return false;
					}
					if (!this.IsContentSubset(dictionaryEntry.Value, hashtable2[text]))
					{
						return false;
					}
				}
			}
			object obj4 = this.MergeContents(hashtable["."], hashtable[this.ComputerName]);
			if (obj4 != null)
			{
				object obj5 = this.MergeContents(hashtable2["."], hashtable2[this.ComputerName]);
				if (obj5 != null)
				{
					return this.IsContentSubset(obj4, obj5);
				}
				if (!this.IsEmpty)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000C41DC File Offset: 0x000C23DC
		private object MergeContents(object content1, object content2)
		{
			if (content1 == null)
			{
				if (content2 == null)
				{
					return null;
				}
				return content2;
			}
			else
			{
				if (content2 == null)
				{
					return content1;
				}
				return this.UnionOfContents(content1, content2);
			}
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <param name="target">A permission object that is to be tested for the subset relationship.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission object is a subset of the specified permission object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B15 RID: 11029 RVA: 0x000C41F8 File Offset: 0x000C23F8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty;
			}
			if (target.GetType() != base.GetType())
			{
				return false;
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			return resourcePermissionBase.IsUnrestricted() || (!this.IsUnrestricted() && this.IsContentSubset(this.rootTable, resourcePermissionBase.rootTable));
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B16 RID: 11030 RVA: 0x000C4251 File Offset: 0x000C2451
		public bool IsUnrestricted()
		{
			return this.isUnrestricted;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000C425C File Offset: 0x000C245C
		private object ReadChildren(SecurityElement securityElement, int tagIndex)
		{
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			if (securityElement.Children != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = (SecurityElement)securityElement.Children[i];
					if (securityElement2.Tag == this.TagNames[tagIndex])
					{
						string text = securityElement2.Attribute("name");
						if (tagIndex < this.TagNames.Length - 1)
						{
							hashtable[text] = this.ReadChildren(securityElement2, tagIndex + 1);
						}
						else
						{
							string text2 = securityElement2.Attribute("access");
							int num = 0;
							if (text2 != null)
							{
								num = (int)Enum.Parse(this.PermissionAccessType, text2);
							}
							hashtable[text] = num;
						}
					}
				}
			}
			return hashtable;
		}

		/// <summary>Removes a permission entry from the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.  
		///  -or-  
		///  The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is not in the permission.</exception>
		// Token: 0x06002B18 RID: 11032 RVA: 0x000C4320 File Offset: 0x000C2520
		protected void RemovePermissionAccess(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath.Length != this.TagNames.Length)
			{
				throw new InvalidOperationException(SR.GetString("PermissionNumberOfElements"));
			}
			Hashtable hashtable = this.rootTable;
			string[] permissionAccessPath = entry.PermissionAccessPath;
			for (int i = 0; i < permissionAccessPath.Length; i++)
			{
				if (hashtable == null || !hashtable.ContainsKey(permissionAccessPath[i]))
				{
					throw new InvalidOperationException(SR.GetString("PermissionItemDoesntExist"));
				}
				Hashtable hashtable2 = hashtable;
				if (i < permissionAccessPath.Length - 1)
				{
					hashtable = (Hashtable)hashtable[permissionAccessPath[i]];
					if (hashtable.Count == 1)
					{
						hashtable2.Remove(permissionAccessPath[i]);
					}
				}
				else
				{
					hashtable = null;
					hashtable2.Remove(permissionAccessPath[i]);
				}
			}
		}

		/// <summary>Creates and returns an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B19 RID: 11033 RVA: 0x000C43D0 File Offset: 0x000C25D0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.isUnrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			this.WriteChildren(securityElement, this.rootTable, 0);
			return securityElement;
		}

		/// <summary>Creates a permission object that combines the current permission object and the target permission object.</summary>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> permission object is not of the same type as the current permission object.</exception>
		// Token: 0x06002B1A RID: 11034 RVA: 0x000C445C File Offset: 0x000C265C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw new ArgumentException(SR.GetString("PermissionTypeMismatch"), "target");
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			ResourcePermissionBase resourcePermissionBase2 = null;
			if (this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted())
			{
				resourcePermissionBase2 = this.CreateInstance();
				resourcePermissionBase2.isUnrestricted = true;
			}
			else
			{
				Hashtable hashtable = (Hashtable)this.UnionOfContents(this.rootTable, resourcePermissionBase.rootTable);
				if (hashtable != null)
				{
					resourcePermissionBase2 = this.CreateInstance();
					resourcePermissionBase2.rootTable = hashtable;
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000C44F0 File Offset: 0x000C26F0
		private object UnionOfContents(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return num | num2;
			}
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			IDictionaryEnumerator enumerator2 = ((Hashtable)targetContent).GetEnumerator();
			while (enumerator.MoveNext())
			{
				hashtable[(string)enumerator.Key] = enumerator.Value;
			}
			while (enumerator2.MoveNext())
			{
				if (!hashtable.ContainsKey(enumerator2.Key))
				{
					hashtable[enumerator2.Key] = enumerator2.Value;
				}
				else
				{
					object obj = hashtable[enumerator2.Key];
					object value = enumerator2.Value;
					hashtable[enumerator2.Key] = this.UnionOfContents(obj, value);
				}
			}
			if (hashtable.Count <= 0)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000C45C8 File Offset: 0x000C27C8
		private void WriteChildren(SecurityElement currentElement, object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			while (enumerator.MoveNext())
			{
				SecurityElement securityElement = new SecurityElement(this.TagNames[tagIndex]);
				currentElement.AddChild(securityElement);
				securityElement.AddAttribute("name", (string)enumerator.Key);
				if (tagIndex < this.TagNames.Length - 1)
				{
					this.WriteChildren(securityElement, enumerator.Value, tagIndex + 1);
				}
				else
				{
					int num = (int)enumerator.Value;
					if (this.PermissionAccessType != null && num != 0)
					{
						string text = Enum.Format(this.PermissionAccessType, num, "g");
						securityElement.AddAttribute("access", text);
					}
				}
			}
		}

		// Token: 0x04002656 RID: 9814
		private static volatile string computerName;

		// Token: 0x04002657 RID: 9815
		private string[] tagNames;

		// Token: 0x04002658 RID: 9816
		private Type permissionAccessType;

		// Token: 0x04002659 RID: 9817
		private bool isUnrestricted;

		// Token: 0x0400265A RID: 9818
		private Hashtable rootTable = ResourcePermissionBase.CreateHashtable();

		/// <summary>Specifies the character to be used to represent the any wildcard character.</summary>
		// Token: 0x0400265B RID: 9819
		public const string Any = "*";

		/// <summary>Specifies the character to be used to represent a local reference.</summary>
		// Token: 0x0400265C RID: 9820
		public const string Local = ".";

		// Token: 0x02000879 RID: 2169
		[SuppressUnmanagedCodeSecurity]
		private static class UnsafeNativeMethods
		{
			// Token: 0x0600454F RID: 17743
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
			internal static extern bool GetComputerName(StringBuilder lpBuffer, ref int nSize);
		}
	}
}
