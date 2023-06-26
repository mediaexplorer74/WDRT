using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
	/// <summary>Represents a collection that can contain many different types of permissions.</summary>
	// Token: 0x020001DE RID: 478
	[ComVisible(true)]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, Name = "mscorlib", PublicKey = "0x00000000000000000400000000000000")]
	[Serializable]
	public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00062570 File Offset: 0x00060770
		[Conditional("_DEBUG")]
		private static void DEBUG_WRITE(string str)
		{
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00062572 File Offset: 0x00060772
		[Conditional("_DEBUG")]
		private static void DEBUG_COND_WRITE(bool exp, string str)
		{
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00062574 File Offset: 0x00060774
		[Conditional("_DEBUG")]
		private static void DEBUG_PRINTSTACK(Exception e)
		{
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00062576 File Offset: 0x00060776
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.Reset();
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00062580 File Offset: 0x00060780
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermissionSet != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermissionSet));
			}
			else if (this.m_normalPermSet != null)
			{
				this.m_permSet = this.m_normalPermSet.SpecialUnion(this.m_unrestrictedPermSet);
			}
			else if (this.m_unrestrictedPermSet != null)
			{
				this.m_permSet = this.m_unrestrictedPermSet.SpecialUnion(this.m_normalPermSet);
			}
			this.m_serializedPermissionSet = null;
			this.m_normalPermSet = null;
			this.m_unrestrictedPermSet = null;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00062600 File Offset: 0x00060800
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermissionSet = this.ToString();
				if (this.m_permSet != null)
				{
					this.m_permSet.SpecialSplit(ref this.m_unrestrictedPermSet, ref this.m_normalPermSet, this.m_ignoreTypeLoadFailures);
				}
				this.m_permSetSaved = this.m_permSet;
				this.m_permSet = null;
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00062660 File Offset: 0x00060860
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			if ((context.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermissionSet = null;
				this.m_permSet = this.m_permSetSaved;
				this.m_permSetSaved = null;
				this.m_unrestrictedPermSet = null;
				this.m_normalPermSet = null;
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00062699 File Offset: 0x00060899
		internal PermissionSet()
		{
			this.Reset();
			this.m_Unrestricted = true;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000626AE File Offset: 0x000608AE
		internal PermissionSet(bool fUnrestricted)
			: this()
		{
			this.SetUnrestricted(fUnrestricted);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission set's access to resources.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06001CCC RID: 7372 RVA: 0x000626BD File Offset: 0x000608BD
		public PermissionSet(PermissionState state)
			: this()
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with initial values taken from the <paramref name="permSet" /> parameter.</summary>
		/// <param name="permSet">The set from which to take the value of the new <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> to create an empty <see cref="T:System.Security.PermissionSet" />.</param>
		// Token: 0x06001CCD RID: 7373 RVA: 0x000626EC File Offset: 0x000608EC
		public PermissionSet(PermissionSet permSet)
			: this()
		{
			if (permSet == null)
			{
				this.Reset();
				return;
			}
			this.m_Unrestricted = permSet.m_Unrestricted;
			this.m_CheckedForNonCas = permSet.m_CheckedForNonCas;
			this.m_ContainsCas = permSet.m_ContainsCas;
			this.m_ContainsNonCas = permSet.m_ContainsNonCas;
			this.m_ignoreTypeLoadFailures = permSet.m_ignoreTypeLoadFailures;
			if (permSet.m_permSet != null)
			{
				this.m_permSet = new TokenBasedSet(permSet.m_permSet);
				for (int i = this.m_permSet.GetStartingIndex(); i <= this.m_permSet.GetMaxUsedIndex(); i++)
				{
					object item = this.m_permSet.GetItem(i);
					IPermission permission = item as IPermission;
					ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
					if (permission != null)
					{
						this.m_permSet.SetItem(i, permission.Copy());
					}
					else if (securityElementFactory != null)
					{
						this.m_permSet.SetItem(i, securityElementFactory.Copy());
					}
				}
			}
		}

		/// <summary>Copies the permission objects of the set to the indicated location in an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy.</param>
		/// <param name="index">The starting position in the array to begin copying (zero based).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter has more than one dimension.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is out of the range of the <paramref name="array" /> parameter.</exception>
		// Token: 0x06001CCE RID: 7374 RVA: 0x000627C8 File Offset: 0x000609C8
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				array.SetValue(obj, index++);
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00062809 File Offset: 0x00060A09
		private PermissionSet(object trash, object junk)
		{
			this.m_Unrestricted = false;
		}

		/// <summary>Gets the root object of the current collection.</summary>
		/// <returns>The root object of the current collection.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00062818 File Offset: 0x00060A18
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0006281B File Offset: 0x00060A1B
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0006281E File Offset: 0x00060A1E
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00062821 File Offset: 0x00060A21
		internal void Reset()
		{
			this.m_Unrestricted = false;
			this.m_allPermissionsDecoded = true;
			this.m_permSet = null;
			this.m_ignoreTypeLoadFailures = false;
			this.m_CheckedForNonCas = false;
			this.m_ContainsCas = false;
			this.m_ContainsNonCas = false;
			this.m_permSetSaved = null;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0006285B File Offset: 0x00060A5B
		internal void CheckSet()
		{
			if (this.m_permSet == null)
			{
				this.m_permSet = new TokenBasedSet();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CD5 RID: 7381 RVA: 0x00062870 File Offset: 0x00060A70
		public bool IsEmpty()
		{
			if (this.m_Unrestricted)
			{
				return false;
			}
			if (this.m_permSet == null || this.m_permSet.FastIsEmpty())
			{
				return true;
			}
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				IPermission permission = (IPermission)obj;
				if (!permission.IsSubsetOf(null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000628CA File Offset: 0x00060ACA
		internal bool FastIsEmpty()
		{
			return !this.m_Unrestricted && (this.m_permSet == null || this.m_permSet.FastIsEmpty());
		}

		/// <summary>Gets the number of permission objects contained in the permission set.</summary>
		/// <returns>The number of permission objects contained in the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x000628F0 File Offset: 0x00060AF0
		public virtual int Count
		{
			get
			{
				int num = 0;
				if (this.m_permSet != null)
				{
					num += this.m_permSet.GetCount();
				}
				return num;
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00062918 File Offset: 0x00060B18
		internal IPermission GetPermission(int index)
		{
			if (this.m_permSet == null)
			{
				return null;
			}
			object item = this.m_permSet.GetItem(index);
			if (item == null)
			{
				return null;
			}
			IPermission permission = item as IPermission;
			if (permission != null)
			{
				return permission;
			}
			permission = this.CreatePermission(item, index);
			if (permission == null)
			{
				return null;
			}
			return permission;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006295C File Offset: 0x00060B5C
		internal IPermission GetPermission(PermissionToken permToken)
		{
			if (permToken == null)
			{
				return null;
			}
			return this.GetPermission(permToken.m_index);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0006296F File Offset: 0x00060B6F
		internal IPermission GetPermission(IPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			return this.GetPermission(PermissionToken.GetToken(perm));
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <param name="permClass">The type of the desired permission object.</param>
		/// <returns>A copy of the permission object of the type specified by the <paramref name="permClass" /> parameter contained in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if none exists.</returns>
		// Token: 0x06001CDB RID: 7387 RVA: 0x00062982 File Offset: 0x00060B82
		public IPermission GetPermission(Type permClass)
		{
			return this.GetPermissionImpl(permClass);
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <param name="permClass">The type of the permission object.</param>
		/// <returns>A copy of the permission object, of the type specified by the <paramref name="permClass" /> parameter, contained in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if none exists.</returns>
		// Token: 0x06001CDC RID: 7388 RVA: 0x0006298B File Offset: 0x00060B8B
		protected virtual IPermission GetPermissionImpl(Type permClass)
		{
			if (permClass == null)
			{
				return null;
			}
			return this.GetPermission(PermissionToken.FindToken(permClass));
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <param name="perm">The permission to set.</param>
		/// <returns>The set permission.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CDD RID: 7389 RVA: 0x000629A4 File Offset: 0x00060BA4
		public IPermission SetPermission(IPermission perm)
		{
			return this.SetPermissionImpl(perm);
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <param name="perm">The permission to set.</param>
		/// <returns>The set permission.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CDE RID: 7390 RVA: 0x000629B0 File Offset: 0x00060BB0
		protected virtual IPermission SetPermissionImpl(IPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			PermissionToken token = PermissionToken.GetToken(perm);
			if ((token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
			{
				this.m_Unrestricted = false;
			}
			this.CheckSet();
			IPermission permission = this.GetPermission(token.m_index);
			this.m_CheckedForNonCas = false;
			this.m_permSet.SetItem(token.m_index, perm);
			return perm;
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="perm">The permission to add.</param>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CDF RID: 7391 RVA: 0x00062A09 File Offset: 0x00060C09
		public IPermission AddPermission(IPermission perm)
		{
			return this.AddPermissionImpl(perm);
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="perm">The permission to add.</param>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if <paramref name="perm" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CE0 RID: 7392 RVA: 0x00062A14 File Offset: 0x00060C14
		protected virtual IPermission AddPermissionImpl(IPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			this.m_CheckedForNonCas = false;
			PermissionToken token = PermissionToken.GetToken(perm);
			if (this.IsUnrestricted() && (token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
			{
				Type type = perm.GetType();
				return (IPermission)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, new object[] { PermissionState.Unrestricted }, null);
			}
			this.CheckSet();
			IPermission permission = this.GetPermission(token.m_index);
			if (permission != null)
			{
				IPermission permission2 = permission.Union(perm);
				this.m_permSet.SetItem(token.m_index, permission2);
				return permission2;
			}
			this.m_permSet.SetItem(token.m_index, perm);
			return perm;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00062AB8 File Offset: 0x00060CB8
		private IPermission RemovePermission(int index)
		{
			if (this.GetPermission(index) == null)
			{
				return null;
			}
			return (IPermission)this.m_permSet.RemoveItem(index);
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <param name="permClass">The type of permission to delete.</param>
		/// <returns>The permission removed from the set.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00062AE3 File Offset: 0x00060CE3
		public IPermission RemovePermission(Type permClass)
		{
			return this.RemovePermissionImpl(permClass);
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <param name="permClass">The type of the permission to remove.</param>
		/// <returns>The permission removed from the set.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x00062AEC File Offset: 0x00060CEC
		protected virtual IPermission RemovePermissionImpl(Type permClass)
		{
			if (permClass == null)
			{
				return null;
			}
			PermissionToken permissionToken = PermissionToken.FindToken(permClass);
			if (permissionToken == null)
			{
				return null;
			}
			return this.RemovePermission(permissionToken.m_index);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00062B1C File Offset: 0x00060D1C
		internal void SetUnrestricted(bool unrestricted)
		{
			this.m_Unrestricted = unrestricted;
			if (unrestricted)
			{
				this.m_permSet = null;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Security.PermissionSet" /> is <see langword="Unrestricted" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> is <see langword="Unrestricted" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CE5 RID: 7397 RVA: 0x00062B2F File Offset: 0x00060D2F
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00062B38 File Offset: 0x00060D38
		internal bool IsSubsetOfHelper(PermissionSet target, PermissionSet.IsSubsetOfType type, out IPermission firstPermThatFailed, bool ignoreNonCas)
		{
			firstPermThatFailed = null;
			if (target == null || target.FastIsEmpty())
			{
				if (this.IsEmpty())
				{
					return true;
				}
				firstPermThatFailed = this.GetFirstPerm();
				return false;
			}
			else
			{
				if (this.IsUnrestricted() && !target.IsUnrestricted())
				{
					return false;
				}
				if (this.m_permSet == null)
				{
					return true;
				}
				target.CheckSet();
				for (int i = this.m_permSet.GetStartingIndex(); i <= this.m_permSet.GetMaxUsedIndex(); i++)
				{
					IPermission permission = this.GetPermission(i);
					if (permission != null && !permission.IsSubsetOf(null))
					{
						IPermission permission2 = target.GetPermission(i);
						if (!target.m_Unrestricted)
						{
							CodeAccessPermission codeAccessPermission = permission as CodeAccessPermission;
							if (codeAccessPermission == null)
							{
								if (!ignoreNonCas && !permission.IsSubsetOf(permission2))
								{
									firstPermThatFailed = permission;
									return false;
								}
							}
							else
							{
								firstPermThatFailed = permission;
								switch (type)
								{
								case PermissionSet.IsSubsetOfType.Normal:
									if (!permission.IsSubsetOf(permission2))
									{
										return false;
									}
									break;
								case PermissionSet.IsSubsetOfType.CheckDemand:
									if (!codeAccessPermission.CheckDemand((CodeAccessPermission)permission2))
									{
										return false;
									}
									break;
								case PermissionSet.IsSubsetOfType.CheckPermitOnly:
									if (!codeAccessPermission.CheckPermitOnly((CodeAccessPermission)permission2))
									{
										return false;
									}
									break;
								case PermissionSet.IsSubsetOfType.CheckAssertion:
									if (!codeAccessPermission.CheckAssert((CodeAccessPermission)permission2))
									{
										return false;
									}
									break;
								}
								firstPermThatFailed = null;
							}
						}
					}
				}
				return true;
			}
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.PermissionSet" /> is a subset of the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="target">The permission set to test for the subset relationship. This must be either a <see cref="T:System.Security.PermissionSet" /> or a <see cref="T:System.Security.NamedPermissionSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.PermissionSet" /> is a subset of the <paramref name="target" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x00062C50 File Offset: 0x00060E50
		public bool IsSubsetOf(PermissionSet target)
		{
			IPermission permission;
			return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.Normal, out permission, false);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00062C68 File Offset: 0x00060E68
		internal bool CheckDemand(PermissionSet target, out IPermission firstPermThatFailed)
		{
			return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckDemand, out firstPermThatFailed, true);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00062C74 File Offset: 0x00060E74
		internal bool CheckPermitOnly(PermissionSet target, out IPermission firstPermThatFailed)
		{
			return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckPermitOnly, out firstPermThatFailed, true);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00062C80 File Offset: 0x00060E80
		internal bool CheckAssertion(PermissionSet target)
		{
			IPermission permission;
			return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckAssertion, out permission, true);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00062C98 File Offset: 0x00060E98
		internal bool CheckDeny(PermissionSet deniedSet, out IPermission firstPermThatFailed)
		{
			firstPermThatFailed = null;
			if (deniedSet == null || deniedSet.FastIsEmpty() || this.FastIsEmpty())
			{
				return true;
			}
			if (this.m_Unrestricted && deniedSet.m_Unrestricted)
			{
				return false;
			}
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
				if (codeAccessPermission != null && !codeAccessPermission.IsSubsetOf(null))
				{
					if (deniedSet.m_Unrestricted)
					{
						firstPermThatFailed = codeAccessPermission;
						return false;
					}
					CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)deniedSet.GetPermission(permissionSetEnumeratorInternal.GetCurrentIndex());
					if (!codeAccessPermission.CheckDeny(codeAccessPermission2))
					{
						firstPermThatFailed = codeAccessPermission;
						return false;
					}
				}
			}
			if (this.m_Unrestricted)
			{
				PermissionSetEnumeratorInternal permissionSetEnumeratorInternal2 = new PermissionSetEnumeratorInternal(deniedSet);
				while (permissionSetEnumeratorInternal2.MoveNext())
				{
					if (permissionSetEnumeratorInternal2.Current is IPermission)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00062D55 File Offset: 0x00060F55
		internal void CheckDecoded(CodeAccessPermission demandedPerm, PermissionToken tokenDemandedPerm)
		{
			if (this.m_allPermissionsDecoded || this.m_permSet == null)
			{
				return;
			}
			if (tokenDemandedPerm == null)
			{
				tokenDemandedPerm = PermissionToken.GetToken(demandedPerm);
			}
			this.CheckDecoded(tokenDemandedPerm.m_index);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00062D7F File Offset: 0x00060F7F
		internal void CheckDecoded(int index)
		{
			if (this.m_allPermissionsDecoded || this.m_permSet == null)
			{
				return;
			}
			this.GetPermission(index);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00062D9C File Offset: 0x00060F9C
		internal void CheckDecoded(PermissionSet demandedSet)
		{
			if (this.m_allPermissionsDecoded || this.m_permSet == null)
			{
				return;
			}
			PermissionSetEnumeratorInternal enumeratorInternal = demandedSet.GetEnumeratorInternal();
			while (enumeratorInternal.MoveNext())
			{
				this.CheckDecoded(enumeratorInternal.GetCurrentIndex());
			}
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00062DDC File Offset: 0x00060FDC
		internal static void SafeChildAdd(SecurityElement parent, ISecurityElementFactory child, bool copy)
		{
			if (child == parent)
			{
				return;
			}
			if (child.GetTag().Equals("IPermission") || child.GetTag().Equals("Permission"))
			{
				parent.AddChild(child);
				return;
			}
			if (parent.Tag.Equals(child.GetTag()))
			{
				SecurityElement securityElement = (SecurityElement)child;
				for (int i = 0; i < securityElement.InternalChildren.Count; i++)
				{
					ISecurityElementFactory securityElementFactory = (ISecurityElementFactory)securityElement.InternalChildren[i];
					parent.AddChildNoDuplicates(securityElementFactory);
				}
				return;
			}
			parent.AddChild((ISecurityElementFactory)(copy ? child.Copy() : child));
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00062E7C File Offset: 0x0006107C
		internal void InplaceIntersect(PermissionSet other)
		{
			Exception ex = null;
			this.m_CheckedForNonCas = false;
			if (this == other)
			{
				return;
			}
			if (other == null || other.FastIsEmpty())
			{
				this.Reset();
				return;
			}
			if (this.FastIsEmpty())
			{
				return;
			}
			int num = ((this.m_permSet == null) ? (-1) : this.m_permSet.GetMaxUsedIndex());
			int num2 = ((other.m_permSet == null) ? (-1) : other.m_permSet.GetMaxUsedIndex());
			if (this.IsUnrestricted() && num < num2)
			{
				num = num2;
				this.CheckSet();
			}
			if (other.IsUnrestricted())
			{
				other.CheckSet();
			}
			for (int i = 0; i <= num; i++)
			{
				object item = this.m_permSet.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object item2 = other.m_permSet.GetItem(i);
				IPermission permission2 = item2 as IPermission;
				ISecurityElementFactory securityElementFactory2 = item2 as ISecurityElementFactory;
				if (item != null || item2 != null)
				{
					if (securityElementFactory != null && securityElementFactory2 != null)
					{
						if (securityElementFactory.GetTag().Equals("PermissionIntersection") || securityElementFactory.GetTag().Equals("PermissionUnrestrictedIntersection"))
						{
							PermissionSet.SafeChildAdd((SecurityElement)securityElementFactory, securityElementFactory2, true);
						}
						else
						{
							bool flag = true;
							if (this.IsUnrestricted())
							{
								SecurityElement securityElement = new SecurityElement("PermissionUnrestrictedUnion");
								securityElement.AddAttribute("class", securityElementFactory.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement, securityElementFactory, false);
								securityElementFactory = securityElement;
							}
							if (other.IsUnrestricted())
							{
								SecurityElement securityElement2 = new SecurityElement("PermissionUnrestrictedUnion");
								securityElement2.AddAttribute("class", securityElementFactory2.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement2, securityElementFactory2, true);
								securityElementFactory2 = securityElement2;
								flag = false;
							}
							SecurityElement securityElement3 = new SecurityElement("PermissionIntersection");
							securityElement3.AddAttribute("class", securityElementFactory.Attribute("class"));
							PermissionSet.SafeChildAdd(securityElement3, securityElementFactory, false);
							PermissionSet.SafeChildAdd(securityElement3, securityElementFactory2, flag);
							this.m_permSet.SetItem(i, securityElement3);
						}
					}
					else if (item == null)
					{
						if (this.IsUnrestricted())
						{
							if (securityElementFactory2 != null)
							{
								SecurityElement securityElement4 = new SecurityElement("PermissionUnrestrictedIntersection");
								securityElement4.AddAttribute("class", securityElementFactory2.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement4, securityElementFactory2, true);
								this.m_permSet.SetItem(i, securityElement4);
							}
							else
							{
								PermissionToken permissionToken = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
								if ((permissionToken.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
								{
									this.m_permSet.SetItem(i, permission2.Copy());
								}
							}
						}
					}
					else if (item2 == null)
					{
						if (other.IsUnrestricted())
						{
							if (securityElementFactory != null)
							{
								SecurityElement securityElement5 = new SecurityElement("PermissionUnrestrictedIntersection");
								securityElement5.AddAttribute("class", securityElementFactory.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement5, securityElementFactory, false);
								this.m_permSet.SetItem(i, securityElement5);
							}
							else
							{
								PermissionToken permissionToken2 = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
								if ((permissionToken2.m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType)0)
								{
									this.m_permSet.SetItem(i, null);
								}
							}
						}
						else
						{
							this.m_permSet.SetItem(i, null);
						}
					}
					else
					{
						if (securityElementFactory != null)
						{
							permission = this.CreatePermission(securityElementFactory, i);
						}
						if (securityElementFactory2 != null)
						{
							permission2 = other.CreatePermission(securityElementFactory2, i);
						}
						try
						{
							IPermission permission3;
							if (permission == null)
							{
								permission3 = permission2;
							}
							else if (permission2 == null)
							{
								permission3 = permission;
							}
							else
							{
								permission3 = permission.Intersect(permission2);
							}
							this.m_permSet.SetItem(i, permission3);
						}
						catch (Exception ex2)
						{
							if (ex == null)
							{
								ex = ex2;
							}
						}
					}
				}
			}
			this.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Creates and returns a permission set that is the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="other">A permission set to intersect with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>A new permission set that represents the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified target. This object is <see langword="null" /> if the intersection is empty.</returns>
		// Token: 0x06001CF1 RID: 7409 RVA: 0x00063214 File Offset: 0x00061414
		public PermissionSet Intersect(PermissionSet other)
		{
			if (other == null || other.FastIsEmpty() || this.FastIsEmpty())
			{
				return null;
			}
			int num = ((this.m_permSet == null) ? (-1) : this.m_permSet.GetMaxUsedIndex());
			int num2 = ((other.m_permSet == null) ? (-1) : other.m_permSet.GetMaxUsedIndex());
			int num3 = ((num < num2) ? num : num2);
			if (this.IsUnrestricted() && num3 < num2)
			{
				num3 = num2;
				this.CheckSet();
			}
			if (other.IsUnrestricted() && num3 < num)
			{
				num3 = num;
				other.CheckSet();
			}
			PermissionSet permissionSet = new PermissionSet(false);
			if (num3 > -1)
			{
				permissionSet.m_permSet = new TokenBasedSet();
			}
			for (int i = 0; i <= num3; i++)
			{
				object item = this.m_permSet.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object item2 = other.m_permSet.GetItem(i);
				IPermission permission2 = item2 as IPermission;
				ISecurityElementFactory securityElementFactory2 = item2 as ISecurityElementFactory;
				if (item != null || item2 != null)
				{
					if (securityElementFactory != null && securityElementFactory2 != null)
					{
						bool flag = true;
						bool flag2 = true;
						SecurityElement securityElement = new SecurityElement("PermissionIntersection");
						securityElement.AddAttribute("class", securityElementFactory2.Attribute("class"));
						if (this.IsUnrestricted())
						{
							SecurityElement securityElement2 = new SecurityElement("PermissionUnrestrictedUnion");
							securityElement2.AddAttribute("class", securityElementFactory.Attribute("class"));
							PermissionSet.SafeChildAdd(securityElement2, securityElementFactory, true);
							flag2 = false;
							securityElementFactory = securityElement2;
						}
						if (other.IsUnrestricted())
						{
							SecurityElement securityElement3 = new SecurityElement("PermissionUnrestrictedUnion");
							securityElement3.AddAttribute("class", securityElementFactory2.Attribute("class"));
							PermissionSet.SafeChildAdd(securityElement3, securityElementFactory2, true);
							flag = false;
							securityElementFactory2 = securityElement3;
						}
						PermissionSet.SafeChildAdd(securityElement, securityElementFactory2, flag);
						PermissionSet.SafeChildAdd(securityElement, securityElementFactory, flag2);
						permissionSet.m_permSet.SetItem(i, securityElement);
					}
					else if (item == null)
					{
						if (this.m_Unrestricted)
						{
							if (securityElementFactory2 != null)
							{
								SecurityElement securityElement4 = new SecurityElement("PermissionUnrestrictedIntersection");
								securityElement4.AddAttribute("class", securityElementFactory2.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement4, securityElementFactory2, true);
								permissionSet.m_permSet.SetItem(i, securityElement4);
							}
							else if (permission2 != null)
							{
								PermissionToken permissionToken = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
								if ((permissionToken.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
								{
									permissionSet.m_permSet.SetItem(i, permission2.Copy());
								}
							}
						}
					}
					else if (item2 == null)
					{
						if (other.m_Unrestricted)
						{
							if (securityElementFactory != null)
							{
								SecurityElement securityElement5 = new SecurityElement("PermissionUnrestrictedIntersection");
								securityElement5.AddAttribute("class", securityElementFactory.Attribute("class"));
								PermissionSet.SafeChildAdd(securityElement5, securityElementFactory, true);
								permissionSet.m_permSet.SetItem(i, securityElement5);
							}
							else if (permission != null)
							{
								PermissionToken permissionToken2 = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
								if ((permissionToken2.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
								{
									permissionSet.m_permSet.SetItem(i, permission.Copy());
								}
							}
						}
					}
					else
					{
						if (securityElementFactory != null)
						{
							permission = this.CreatePermission(securityElementFactory, i);
						}
						if (securityElementFactory2 != null)
						{
							permission2 = other.CreatePermission(securityElementFactory2, i);
						}
						IPermission permission3;
						if (permission == null)
						{
							permission3 = permission2;
						}
						else if (permission2 == null)
						{
							permission3 = permission;
						}
						else
						{
							permission3 = permission.Intersect(permission2);
						}
						permissionSet.m_permSet.SetItem(i, permission3);
					}
				}
			}
			permissionSet.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
			if (permissionSet.FastIsEmpty())
			{
				return null;
			}
			return permissionSet;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0006358C File Offset: 0x0006178C
		internal void InplaceUnion(PermissionSet other)
		{
			if (this == other)
			{
				return;
			}
			if (other == null || other.FastIsEmpty())
			{
				return;
			}
			this.m_CheckedForNonCas = false;
			this.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
			if (this.m_Unrestricted)
			{
				this.m_permSet = null;
				return;
			}
			int num = -1;
			if (other.m_permSet != null)
			{
				num = other.m_permSet.GetMaxUsedIndex();
				this.CheckSet();
			}
			Exception ex = null;
			for (int i = 0; i <= num; i++)
			{
				object item = this.m_permSet.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object item2 = other.m_permSet.GetItem(i);
				IPermission permission2 = item2 as IPermission;
				ISecurityElementFactory securityElementFactory2 = item2 as ISecurityElementFactory;
				if (item != null || item2 != null)
				{
					if (securityElementFactory != null && securityElementFactory2 != null)
					{
						if (securityElementFactory.GetTag().Equals("PermissionUnion") || securityElementFactory.GetTag().Equals("PermissionUnrestrictedUnion"))
						{
							PermissionSet.SafeChildAdd((SecurityElement)securityElementFactory, securityElementFactory2, true);
						}
						else
						{
							SecurityElement securityElement;
							if (this.IsUnrestricted() || other.IsUnrestricted())
							{
								securityElement = new SecurityElement("PermissionUnrestrictedUnion");
							}
							else
							{
								securityElement = new SecurityElement("PermissionUnion");
							}
							securityElement.AddAttribute("class", securityElementFactory.Attribute("class"));
							PermissionSet.SafeChildAdd(securityElement, securityElementFactory, false);
							PermissionSet.SafeChildAdd(securityElement, securityElementFactory2, true);
							this.m_permSet.SetItem(i, securityElement);
						}
					}
					else if (item == null)
					{
						if (securityElementFactory2 != null)
						{
							this.m_permSet.SetItem(i, securityElementFactory2.Copy());
						}
						else if (permission2 != null)
						{
							PermissionToken permissionToken = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
							if ((permissionToken.m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType)0 || !this.m_Unrestricted)
							{
								this.m_permSet.SetItem(i, permission2.Copy());
							}
						}
					}
					else if (item2 != null)
					{
						if (securityElementFactory != null)
						{
							permission = this.CreatePermission(securityElementFactory, i);
						}
						if (securityElementFactory2 != null)
						{
							permission2 = other.CreatePermission(securityElementFactory2, i);
						}
						try
						{
							IPermission permission3;
							if (permission == null)
							{
								permission3 = permission2;
							}
							else if (permission2 == null)
							{
								permission3 = permission;
							}
							else
							{
								permission3 = permission.Union(permission2);
							}
							this.m_permSet.SetItem(i, permission3);
						}
						catch (Exception ex2)
						{
							if (ex == null)
							{
								ex = ex2;
							}
						}
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Creates a <see cref="T:System.Security.PermissionSet" /> that is the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="other">The permission set to form the union with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>A new permission set that represents the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x06001CF3 RID: 7411 RVA: 0x000637D4 File Offset: 0x000619D4
		public PermissionSet Union(PermissionSet other)
		{
			if (other == null || other.FastIsEmpty())
			{
				return this.Copy();
			}
			if (this.FastIsEmpty())
			{
				return other.Copy();
			}
			PermissionSet permissionSet = new PermissionSet();
			permissionSet.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
			if (permissionSet.m_Unrestricted)
			{
				return permissionSet;
			}
			this.CheckSet();
			other.CheckSet();
			int num = ((this.m_permSet.GetMaxUsedIndex() > other.m_permSet.GetMaxUsedIndex()) ? this.m_permSet.GetMaxUsedIndex() : other.m_permSet.GetMaxUsedIndex());
			permissionSet.m_permSet = new TokenBasedSet();
			for (int i = 0; i <= num; i++)
			{
				object item = this.m_permSet.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object item2 = other.m_permSet.GetItem(i);
				IPermission permission2 = item2 as IPermission;
				ISecurityElementFactory securityElementFactory2 = item2 as ISecurityElementFactory;
				if (item != null || item2 != null)
				{
					if (securityElementFactory != null && securityElementFactory2 != null)
					{
						SecurityElement securityElement;
						if (this.IsUnrestricted() || other.IsUnrestricted())
						{
							securityElement = new SecurityElement("PermissionUnrestrictedUnion");
						}
						else
						{
							securityElement = new SecurityElement("PermissionUnion");
						}
						securityElement.AddAttribute("class", securityElementFactory.Attribute("class"));
						PermissionSet.SafeChildAdd(securityElement, securityElementFactory, true);
						PermissionSet.SafeChildAdd(securityElement, securityElementFactory2, true);
						permissionSet.m_permSet.SetItem(i, securityElement);
					}
					else if (item == null)
					{
						if (securityElementFactory2 != null)
						{
							permissionSet.m_permSet.SetItem(i, securityElementFactory2.Copy());
						}
						else if (permission2 != null)
						{
							PermissionToken permissionToken = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
							if ((permissionToken.m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType)0 || !permissionSet.m_Unrestricted)
							{
								permissionSet.m_permSet.SetItem(i, permission2.Copy());
							}
						}
					}
					else if (item2 == null)
					{
						if (securityElementFactory != null)
						{
							permissionSet.m_permSet.SetItem(i, securityElementFactory.Copy());
						}
						else if (permission != null)
						{
							PermissionToken permissionToken2 = (PermissionToken)PermissionToken.s_tokenSet.GetItem(i);
							if ((permissionToken2.m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType)0 || !permissionSet.m_Unrestricted)
							{
								permissionSet.m_permSet.SetItem(i, permission.Copy());
							}
						}
					}
					else
					{
						if (securityElementFactory != null)
						{
							permission = this.CreatePermission(securityElementFactory, i);
						}
						if (securityElementFactory2 != null)
						{
							permission2 = other.CreatePermission(securityElementFactory2, i);
						}
						IPermission permission3;
						if (permission == null)
						{
							permission3 = permission2;
						}
						else if (permission2 == null)
						{
							permission3 = permission;
						}
						else
						{
							permission3 = permission.Union(permission2);
						}
						permissionSet.m_permSet.SetItem(i, permission3);
					}
				}
			}
			return permissionSet;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00063A54 File Offset: 0x00061C54
		internal void MergeDeniedSet(PermissionSet denied)
		{
			if (denied == null || denied.FastIsEmpty() || this.FastIsEmpty())
			{
				return;
			}
			this.m_CheckedForNonCas = false;
			if (this.m_permSet == null || denied.m_permSet == null)
			{
				return;
			}
			int num = ((denied.m_permSet.GetMaxUsedIndex() > this.m_permSet.GetMaxUsedIndex()) ? this.m_permSet.GetMaxUsedIndex() : denied.m_permSet.GetMaxUsedIndex());
			for (int i = 0; i <= num; i++)
			{
				IPermission permission = denied.m_permSet.GetItem(i) as IPermission;
				if (permission != null)
				{
					IPermission permission2 = this.m_permSet.GetItem(i) as IPermission;
					if (permission2 == null && !this.m_Unrestricted)
					{
						denied.m_permSet.SetItem(i, null);
					}
					else if (permission2 != null && permission != null && permission2.IsSubsetOf(permission))
					{
						this.m_permSet.SetItem(i, null);
						denied.m_permSet.SetItem(i, null);
					}
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00063B34 File Offset: 0x00061D34
		internal bool Contains(IPermission perm)
		{
			if (perm == null)
			{
				return true;
			}
			if (this.m_Unrestricted)
			{
				return true;
			}
			if (this.FastIsEmpty())
			{
				return false;
			}
			PermissionToken token = PermissionToken.GetToken(perm);
			if (this.m_permSet.GetItem(token.m_index) == null)
			{
				return perm.IsSubsetOf(null);
			}
			IPermission permission = this.GetPermission(token.m_index);
			if (permission != null)
			{
				return perm.IsSubsetOf(permission);
			}
			return perm.IsSubsetOf(null);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.PermissionSet" /> or <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current <see cref="T:System.Security.PermissionSet" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x00063BA0 File Offset: 0x00061DA0
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			PermissionSet permissionSet = obj as PermissionSet;
			if (permissionSet == null)
			{
				return false;
			}
			if (this.m_Unrestricted != permissionSet.m_Unrestricted)
			{
				return false;
			}
			this.CheckSet();
			permissionSet.CheckSet();
			this.DecodeAllPermissions();
			permissionSet.DecodeAllPermissions();
			int num = Math.Max(this.m_permSet.GetMaxUsedIndex(), permissionSet.m_permSet.GetMaxUsedIndex());
			for (int i = 0; i <= num; i++)
			{
				IPermission permission = (IPermission)this.m_permSet.GetItem(i);
				IPermission permission2 = (IPermission)permissionSet.m_permSet.GetItem(i);
				if (permission != null || permission2 != null)
				{
					if (permission == null)
					{
						if (!permission2.IsSubsetOf(null))
						{
							return false;
						}
					}
					else if (permission2 == null)
					{
						if (!permission.IsSubsetOf(null))
						{
							return false;
						}
					}
					else if (!permission.Equals(permission2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.PermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.PermissionSet" /> object.</returns>
		// Token: 0x06001CF7 RID: 7415 RVA: 0x00063C60 File Offset: 0x00061E60
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = (this.m_Unrestricted ? (-1) : 0);
			if (this.m_permSet != null)
			{
				this.DecodeAllPermissions();
				int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
				for (int i = this.m_permSet.GetStartingIndex(); i <= maxUsedIndex; i++)
				{
					IPermission permission = (IPermission)this.m_permSet.GetItem(i);
					if (permission != null)
					{
						num ^= permission.GetHashCode();
					}
				}
			}
			return num;
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permissions specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have the permission demanded.</exception>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x00063CCC File Offset: 0x00061ECC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Demand()
		{
			if (this.FastIsEmpty())
			{
				return;
			}
			this.ContainsNonCodeAccessPermissions();
			if (this.m_ContainsCas)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
				CodeAccessSecurityEngine.Check(this.GetCasOnlySet(), ref stackCrawlMark);
			}
			if (this.m_ContainsNonCas)
			{
				this.DemandNonCAS();
			}
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00063D10 File Offset: 0x00061F10
		[SecurityCritical]
		internal void DemandNonCAS()
		{
			this.ContainsNonCodeAccessPermissions();
			if (this.m_ContainsNonCas && this.m_permSet != null)
			{
				this.CheckSet();
				for (int i = this.m_permSet.GetStartingIndex(); i <= this.m_permSet.GetMaxUsedIndex(); i++)
				{
					IPermission permission = this.GetPermission(i);
					if (permission != null && !(permission is CodeAccessPermission))
					{
						permission.Demand();
					}
				}
			}
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.PermissionSet.Assert" /> can create security vulnerabilities.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.PermissionSet" /> instance asserted has not been granted to the asserting code.  
		///  -or-  
		///  There is already an active <see cref="M:System.Security.PermissionSet.Assert" /> for the current frame.</exception>
		// Token: 0x06001CFA RID: 7418 RVA: 0x00063D74 File Offset: 0x00061F74
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Assert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.Assert(this, ref stackCrawlMark);
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for a permission that has an intersection with a permission of a type contained in the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		/// <exception cref="T:System.Security.SecurityException">A previous call to <see cref="M:System.Security.PermissionSet.Deny" /> has already restricted the permissions for the current stack frame.</exception>
		// Token: 0x06001CFB RID: 7419 RVA: 0x00063D8C File Offset: 0x00061F8C
		[SecuritySafeCritical]
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Deny()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.Deny(this, ref stackCrawlMark);
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for any <see cref="T:System.Security.PermissionSet" /> that is not a subset of the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		// Token: 0x06001CFC RID: 7420 RVA: 0x00063DA4 File Offset: 0x00061FA4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void PermitOnly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.PermitOnly(this, ref stackCrawlMark);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00063DBC File Offset: 0x00061FBC
		internal IPermission GetFirstPerm()
		{
			IEnumerator enumerator = this.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return null;
			}
			return enumerator.Current as IPermission;
		}

		/// <summary>Creates a copy of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x06001CFE RID: 7422 RVA: 0x00063DE5 File Offset: 0x00061FE5
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00063DF0 File Offset: 0x00061FF0
		internal PermissionSet CopyWithNoIdentityPermissions()
		{
			PermissionSet permissionSet = new PermissionSet(this);
			permissionSet.RemovePermission(typeof(GacIdentityPermission));
			permissionSet.RemovePermission(typeof(PublisherIdentityPermission));
			permissionSet.RemovePermission(typeof(StrongNameIdentityPermission));
			permissionSet.RemovePermission(typeof(UrlIdentityPermission));
			permissionSet.RemovePermission(typeof(ZoneIdentityPermission));
			return permissionSet;
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x06001D00 RID: 7424 RVA: 0x00063E5A File Offset: 0x0006205A
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x06001D01 RID: 7425 RVA: 0x00063E62 File Offset: 0x00062062
		protected virtual IEnumerator GetEnumeratorImpl()
		{
			return new PermissionSetEnumerator(this);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00063E6A File Offset: 0x0006206A
		internal PermissionSetEnumeratorInternal GetEnumeratorInternal()
		{
			return new PermissionSetEnumeratorInternal(this);
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x06001D03 RID: 7427 RVA: 0x00063E72 File Offset: 0x00062072
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00063E80 File Offset: 0x00062080
		private void NormalizePermissionSet()
		{
			PermissionSet permissionSet = new PermissionSet(false);
			permissionSet.m_Unrestricted = this.m_Unrestricted;
			if (this.m_permSet != null)
			{
				for (int i = this.m_permSet.GetStartingIndex(); i <= this.m_permSet.GetMaxUsedIndex(); i++)
				{
					object item = this.m_permSet.GetItem(i);
					IPermission permission = item as IPermission;
					ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
					if (securityElementFactory != null)
					{
						permission = this.CreatePerm(securityElementFactory);
					}
					if (permission != null)
					{
						permissionSet.SetPermission(permission);
					}
				}
			}
			this.m_permSet = permissionSet.m_permSet;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00063F08 File Offset: 0x00062108
		private bool DecodeXml(byte[] data, HostProtectionResource fullTrustOnlyResources, HostProtectionResource inaccessibleResources)
		{
			if (data != null && data.Length != 0)
			{
				this.FromXml(new Parser(data, Tokenizer.ByteTokenEncoding.UnicodeTokens).GetTopElement());
			}
			this.FilterHostProtectionPermissions(fullTrustOnlyResources, inaccessibleResources);
			this.DecodeAllPermissions();
			return true;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00063F34 File Offset: 0x00062134
		private void DecodeAllPermissions()
		{
			if (this.m_permSet == null)
			{
				this.m_allPermissionsDecoded = true;
				return;
			}
			int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
			for (int i = 0; i <= maxUsedIndex; i++)
			{
				this.GetPermission(i);
			}
			this.m_allPermissionsDecoded = true;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00063F78 File Offset: 0x00062178
		internal void FilterHostProtectionPermissions(HostProtectionResource fullTrustOnly, HostProtectionResource inaccessible)
		{
			HostProtectionPermission.protectedResources = fullTrustOnly;
			HostProtectionPermission hostProtectionPermission = (HostProtectionPermission)this.GetPermission(HostProtectionPermission.GetTokenIndex());
			if (hostProtectionPermission == null)
			{
				return;
			}
			HostProtectionPermission hostProtectionPermission2 = (HostProtectionPermission)hostProtectionPermission.Intersect(new HostProtectionPermission(fullTrustOnly));
			if (hostProtectionPermission2 == null)
			{
				this.RemovePermission(typeof(HostProtectionPermission));
				return;
			}
			if (hostProtectionPermission2.Resources != hostProtectionPermission.Resources)
			{
				this.SetPermission(hostProtectionPermission2);
			}
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="et" /> parameter's version number is not supported.</exception>
		// Token: 0x06001D08 RID: 7432 RVA: 0x00063FDF File Offset: 0x000621DF
		public virtual void FromXml(SecurityElement et)
		{
			this.FromXml(et, false, false);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00063FEC File Offset: 0x000621EC
		internal static bool IsPermissionTag(string tag, bool allowInternalOnly)
		{
			return tag.Equals("Permission") || tag.Equals("IPermission") || (allowInternalOnly && (tag.Equals("PermissionUnion") || tag.Equals("PermissionIntersection") || tag.Equals("PermissionUnrestrictedIntersection") || tag.Equals("PermissionUnrestrictedUnion")));
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00064050 File Offset: 0x00062250
		internal virtual void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (!et.Tag.Equals("PermissionSet"))
			{
				throw new ArgumentException(string.Format(null, Environment.GetResourceString("Argument_InvalidXMLElement"), "PermissionSet", base.GetType().FullName));
			}
			this.Reset();
			this.m_ignoreTypeLoadFailures = ignoreTypeLoadFailures;
			this.m_allPermissionsDecoded = false;
			this.m_Unrestricted = XMLUtil.IsUnrestricted(et);
			if (et.InternalChildren != null)
			{
				int count = et.InternalChildren.Count;
				for (int i = 0; i < count; i++)
				{
					SecurityElement securityElement = (SecurityElement)et.Children[i];
					if (PermissionSet.IsPermissionTag(securityElement.Tag, allowInternalOnly))
					{
						string text = securityElement.Attribute("class");
						PermissionToken permissionToken;
						object obj;
						if (text != null)
						{
							permissionToken = PermissionToken.GetToken(text);
							if (permissionToken == null)
							{
								obj = this.CreatePerm(securityElement);
								if (obj != null)
								{
									permissionToken = PermissionToken.GetToken((IPermission)obj);
								}
							}
							else
							{
								obj = securityElement;
							}
						}
						else
						{
							IPermission permission = this.CreatePerm(securityElement);
							if (permission == null)
							{
								permissionToken = null;
								obj = null;
							}
							else
							{
								permissionToken = PermissionToken.GetToken(permission);
								obj = permission;
							}
						}
						if (permissionToken != null && obj != null)
						{
							if (this.m_permSet == null)
							{
								this.m_permSet = new TokenBasedSet();
							}
							if (this.m_permSet.GetItem(permissionToken.m_index) != null)
							{
								IPermission permission2;
								if (this.m_permSet.GetItem(permissionToken.m_index) is IPermission)
								{
									permission2 = (IPermission)this.m_permSet.GetItem(permissionToken.m_index);
								}
								else
								{
									permission2 = this.CreatePerm((SecurityElement)this.m_permSet.GetItem(permissionToken.m_index));
								}
								if (obj is IPermission)
								{
									obj = ((IPermission)obj).Union(permission2);
								}
								else
								{
									obj = this.CreatePerm((SecurityElement)obj).Union(permission2);
								}
							}
							if (this.m_Unrestricted && obj is IPermission)
							{
								obj = null;
							}
							this.m_permSet.SetItem(permissionToken.m_index, obj);
						}
					}
				}
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00064254 File Offset: 0x00062454
		internal virtual void FromXml(SecurityDocument doc, int position, bool allowInternalOnly)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			if (!doc.GetTagForElement(position).Equals("PermissionSet"))
			{
				throw new ArgumentException(string.Format(null, Environment.GetResourceString("Argument_InvalidXMLElement"), "PermissionSet", base.GetType().FullName));
			}
			this.Reset();
			this.m_allPermissionsDecoded = false;
			Exception ex = null;
			string attributeForElement = doc.GetAttributeForElement(position, "Unrestricted");
			if (attributeForElement != null)
			{
				this.m_Unrestricted = attributeForElement.Equals("True") || attributeForElement.Equals("true") || attributeForElement.Equals("TRUE");
			}
			else
			{
				this.m_Unrestricted = false;
			}
			ArrayList childrenPositionForElement = doc.GetChildrenPositionForElement(position);
			int count = childrenPositionForElement.Count;
			for (int i = 0; i < count; i++)
			{
				int num = (int)childrenPositionForElement[i];
				if (PermissionSet.IsPermissionTag(doc.GetTagForElement(num), allowInternalOnly))
				{
					try
					{
						string attributeForElement2 = doc.GetAttributeForElement(num, "class");
						PermissionToken permissionToken;
						object obj;
						if (attributeForElement2 != null)
						{
							permissionToken = PermissionToken.GetToken(attributeForElement2);
							if (permissionToken == null)
							{
								obj = this.CreatePerm(doc.GetElement(num, true));
								if (obj != null)
								{
									permissionToken = PermissionToken.GetToken((IPermission)obj);
								}
							}
							else
							{
								obj = ((ISecurityElementFactory)new SecurityDocumentElement(doc, num)).CreateSecurityElement();
							}
						}
						else
						{
							IPermission permission = this.CreatePerm(doc.GetElement(num, true));
							if (permission == null)
							{
								permissionToken = null;
								obj = null;
							}
							else
							{
								permissionToken = PermissionToken.GetToken(permission);
								obj = permission;
							}
						}
						if (permissionToken != null && obj != null)
						{
							if (this.m_permSet == null)
							{
								this.m_permSet = new TokenBasedSet();
							}
							IPermission permission2 = null;
							if (this.m_permSet.GetItem(permissionToken.m_index) != null)
							{
								if (this.m_permSet.GetItem(permissionToken.m_index) is IPermission)
								{
									permission2 = (IPermission)this.m_permSet.GetItem(permissionToken.m_index);
								}
								else
								{
									permission2 = this.CreatePerm(this.m_permSet.GetItem(permissionToken.m_index));
								}
							}
							if (permission2 != null)
							{
								if (obj is IPermission)
								{
									obj = permission2.Union((IPermission)obj);
								}
								else
								{
									obj = permission2.Union(this.CreatePerm(obj));
								}
							}
							if (this.m_Unrestricted && obj is IPermission)
							{
								obj = null;
							}
							this.m_permSet.SetItem(permissionToken.m_index, obj);
						}
					}
					catch (Exception ex2)
					{
						if (ex == null)
						{
							ex = ex2;
						}
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000644D0 File Offset: 0x000626D0
		private IPermission CreatePerm(object obj)
		{
			return PermissionSet.CreatePerm(obj, this.m_ignoreTypeLoadFailures);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000644E0 File Offset: 0x000626E0
		internal static IPermission CreatePerm(object obj, bool ignoreTypeLoadFailures)
		{
			SecurityElement securityElement = obj as SecurityElement;
			ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
			if (securityElement == null && securityElementFactory != null)
			{
				securityElement = securityElementFactory.CreateSecurityElement();
			}
			IPermission permission = null;
			string tag = securityElement.Tag;
			if (!(tag == "PermissionUnion"))
			{
				if (!(tag == "PermissionIntersection"))
				{
					if (!(tag == "PermissionUnrestrictedUnion"))
					{
						if (!(tag == "PermissionUnrestrictedIntersection"))
						{
							if (tag == "IPermission" || tag == "Permission")
							{
								permission = securityElement.ToPermission(ignoreTypeLoadFailures);
							}
						}
						else
						{
							foreach (object obj2 in securityElement.Children)
							{
								IPermission permission2 = PermissionSet.CreatePerm((SecurityElement)obj2, ignoreTypeLoadFailures);
								if (permission2 == null)
								{
									return null;
								}
								PermissionToken token = PermissionToken.GetToken(permission2);
								if ((token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
								{
									if (permission != null)
									{
										permission = permission2.Intersect(permission);
									}
									else
									{
										permission = permission2;
									}
								}
								else
								{
									permission = null;
								}
								if (permission == null)
								{
									return null;
								}
							}
						}
					}
					else
					{
						IEnumerator enumerator = securityElement.Children.GetEnumerator();
						bool flag = true;
						while (enumerator.MoveNext())
						{
							object obj3 = enumerator.Current;
							IPermission permission3 = PermissionSet.CreatePerm((SecurityElement)obj3, ignoreTypeLoadFailures);
							if (permission3 != null)
							{
								PermissionToken token2 = PermissionToken.GetToken(permission3);
								if ((token2.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType)0)
								{
									permission = XMLUtil.CreatePermission(PermissionSet.GetPermissionElement((SecurityElement)enumerator.Current), PermissionState.Unrestricted, ignoreTypeLoadFailures);
									break;
								}
								if (flag)
								{
									permission = permission3;
								}
								else
								{
									permission = permission3.Union(permission);
								}
								flag = false;
							}
						}
					}
				}
				else
				{
					foreach (object obj4 in securityElement.Children)
					{
						IPermission permission4 = PermissionSet.CreatePerm((SecurityElement)obj4, ignoreTypeLoadFailures);
						if (permission != null)
						{
							permission = permission.Intersect(permission4);
						}
						else
						{
							permission = permission4;
						}
						if (permission == null)
						{
							return null;
						}
					}
				}
			}
			else
			{
				foreach (object obj5 in securityElement.Children)
				{
					IPermission permission5 = PermissionSet.CreatePerm((SecurityElement)obj5, ignoreTypeLoadFailures);
					if (permission != null)
					{
						permission = permission.Union(permission5);
					}
					else
					{
						permission = permission5;
					}
				}
			}
			return permission;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000646E4 File Offset: 0x000628E4
		internal IPermission CreatePermission(object obj, int index)
		{
			IPermission permission = this.CreatePerm(obj);
			if (permission == null)
			{
				return null;
			}
			if (this.m_Unrestricted)
			{
				permission = null;
			}
			this.CheckSet();
			this.m_permSet.SetItem(index, permission);
			if (permission != null)
			{
				PermissionToken token = PermissionToken.GetToken(permission);
				if (token != null && token.m_index != index)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
				}
			}
			return permission;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00064744 File Offset: 0x00062944
		private static SecurityElement GetPermissionElement(SecurityElement el)
		{
			string tag = el.Tag;
			if (tag == "IPermission" || tag == "Permission")
			{
				return el;
			}
			IEnumerator enumerator = el.Children.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return PermissionSet.GetPermissionElement((SecurityElement)enumerator.Current);
			}
			return null;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0006479C File Offset: 0x0006299C
		internal static SecurityElement CreateEmptyPermissionSetXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", "System.Security.PermissionSet");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000647D8 File Offset: 0x000629D8
		internal SecurityElement ToXml(string permName)
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", permName);
			securityElement.AddAttribute("version", "1");
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				IPermission permission = (IPermission)obj;
				if (!this.m_Unrestricted)
				{
					securityElement.AddChild(permission.ToXml());
				}
			}
			return securityElement;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0006485C File Offset: 0x00062A5C
		internal SecurityElement InternalToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			if (this.m_permSet != null)
			{
				int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
				for (int i = this.m_permSet.GetStartingIndex(); i <= maxUsedIndex; i++)
				{
					object item = this.m_permSet.GetItem(i);
					if (item != null)
					{
						if (item is IPermission)
						{
							if (!this.m_Unrestricted)
							{
								securityElement.AddChild(((IPermission)item).ToXml());
							}
						}
						else
						{
							securityElement.AddChild((SecurityElement)item);
						}
					}
				}
			}
			return securityElement;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001D13 RID: 7443 RVA: 0x0006491C File Offset: 0x00062B1C
		public virtual SecurityElement ToXml()
		{
			return this.ToXml("System.Security.PermissionSet");
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0006492C File Offset: 0x00062B2C
		internal byte[] EncodeXml()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.Unicode);
			binaryWriter.Write(this.ToXml().ToString());
			binaryWriter.Flush();
			memoryStream.Position = 2L;
			int num = (int)memoryStream.Length - 2;
			byte[] array = new byte[num];
			memoryStream.Read(array, 0, array.Length);
			return array;
		}

		/// <summary>Converts an encoded <see cref="T:System.Security.PermissionSet" /> from one XML encoding format to another XML encoding format.</summary>
		/// <param name="inFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <param name="inData">An XML-encoded permission set.</param>
		/// <param name="outFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <returns>An encrypted permission set with the specified output format.</returns>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x06001D15 RID: 7445 RVA: 0x00064988 File Offset: 0x00062B88
		[Obsolete("This method is obsolete and shoud no longer be used.")]
		public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D16 RID: 7446 RVA: 0x00064990 File Offset: 0x00062B90
		public bool ContainsNonCodeAccessPermissions()
		{
			if (this.m_CheckedForNonCas)
			{
				return this.m_ContainsNonCas;
			}
			lock (this)
			{
				if (this.m_CheckedForNonCas)
				{
					return this.m_ContainsNonCas;
				}
				this.m_ContainsCas = false;
				this.m_ContainsNonCas = false;
				if (this.IsUnrestricted())
				{
					this.m_ContainsCas = true;
				}
				if (this.m_permSet != null)
				{
					PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
					while (permissionSetEnumeratorInternal.MoveNext() && (!this.m_ContainsCas || !this.m_ContainsNonCas))
					{
						IPermission permission = permissionSetEnumeratorInternal.Current as IPermission;
						if (permission != null)
						{
							if (permission is CodeAccessPermission)
							{
								this.m_ContainsCas = true;
							}
							else
							{
								this.m_ContainsNonCas = true;
							}
						}
					}
				}
				this.m_CheckedForNonCas = true;
			}
			return this.m_ContainsNonCas;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00064A6C File Offset: 0x00062C6C
		private PermissionSet GetCasOnlySet()
		{
			if (!this.m_ContainsNonCas)
			{
				return this;
			}
			if (this.IsUnrestricted())
			{
				return this;
			}
			PermissionSet permissionSet = new PermissionSet(false);
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(this);
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				IPermission permission = (IPermission)obj;
				if (permission is CodeAccessPermission)
				{
					permissionSet.AddPermission(permission);
				}
			}
			permissionSet.m_CheckedForNonCas = true;
			permissionSet.m_ContainsCas = !permissionSet.IsEmpty();
			permissionSet.m_ContainsNonCas = false;
			return permissionSet;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00064AE4 File Offset: 0x00062CE4
		[SecurityCritical]
		private static void SetupSecurity()
		{
			PolicyLevel policyLevel = PolicyLevel.CreateAppDomainLevel();
			CodeGroup codeGroup = new UnionCodeGroup(new AllMembershipCondition(), policyLevel.GetNamedPermissionSet("Execution"));
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			CodeGroup codeGroup2 = new UnionCodeGroup(new StrongNameMembershipCondition(strongNamePublicKeyBlob, null, null), policyLevel.GetNamedPermissionSet("FullTrust"));
			StrongNamePublicKeyBlob strongNamePublicKeyBlob2 = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			CodeGroup codeGroup3 = new UnionCodeGroup(new StrongNameMembershipCondition(strongNamePublicKeyBlob2, null, null), policyLevel.GetNamedPermissionSet("FullTrust"));
			CodeGroup codeGroup4 = new UnionCodeGroup(new GacMembershipCondition(), policyLevel.GetNamedPermissionSet("FullTrust"));
			codeGroup.AddChild(codeGroup2);
			codeGroup.AddChild(codeGroup3);
			codeGroup.AddChild(codeGroup4);
			policyLevel.RootCodeGroup = codeGroup;
			try
			{
				AppDomain.CurrentDomain.SetAppDomainPolicy(policyLevel);
			}
			catch (PolicyException)
			{
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00064BB0 File Offset: 0x00062DB0
		private static void MergePermission(IPermission perm, bool separateCasFromNonCas, ref PermissionSet casPset, ref PermissionSet nonCasPset)
		{
			if (perm == null)
			{
				return;
			}
			if (!separateCasFromNonCas || perm is CodeAccessPermission)
			{
				if (casPset == null)
				{
					casPset = new PermissionSet(false);
				}
				IPermission permission = casPset.GetPermission(perm);
				IPermission permission2 = casPset.AddPermission(perm);
				if (permission != null && !permission.IsSubsetOf(permission2))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
				}
			}
			else
			{
				if (nonCasPset == null)
				{
					nonCasPset = new PermissionSet(false);
				}
				IPermission permission3 = nonCasPset.GetPermission(perm);
				IPermission permission4 = nonCasPset.AddPermission(perm);
				if (permission3 != null && !permission3.IsSubsetOf(permission4))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
				}
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00064C40 File Offset: 0x00062E40
		private static byte[] CreateSerialized(object[] attrs, bool serialize, ref byte[] nonCasBlob, out PermissionSet casPset, HostProtectionResource fullTrustOnlyResources, bool allowEmptyPermissionSets)
		{
			casPset = null;
			PermissionSet permissionSet = null;
			for (int i = 0; i < attrs.Length; i++)
			{
				if (attrs[i] is PermissionSetAttribute)
				{
					PermissionSet permissionSet2 = ((PermissionSetAttribute)attrs[i]).CreatePermissionSet();
					if (permissionSet2 == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
					}
					PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(permissionSet2);
					while (permissionSetEnumeratorInternal.MoveNext())
					{
						object obj = permissionSetEnumeratorInternal.Current;
						IPermission permission = (IPermission)obj;
						PermissionSet.MergePermission(permission, serialize, ref casPset, ref permissionSet);
					}
					if (casPset == null)
					{
						casPset = new PermissionSet(false);
					}
					if (permissionSet2.IsUnrestricted())
					{
						casPset.SetUnrestricted(true);
					}
				}
				else
				{
					IPermission permission2 = ((SecurityAttribute)attrs[i]).CreatePermission();
					PermissionSet.MergePermission(permission2, serialize, ref casPset, ref permissionSet);
				}
			}
			if (casPset != null)
			{
				casPset.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
				casPset.ContainsNonCodeAccessPermissions();
				if (allowEmptyPermissionSets && casPset.IsEmpty())
				{
					casPset = null;
				}
			}
			if (permissionSet != null)
			{
				permissionSet.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
				permissionSet.ContainsNonCodeAccessPermissions();
				if (allowEmptyPermissionSets && permissionSet.IsEmpty())
				{
					permissionSet = null;
				}
			}
			byte[] array = null;
			nonCasBlob = null;
			if (serialize)
			{
				if (casPset != null)
				{
					array = casPset.EncodeXml();
				}
				if (permissionSet != null)
				{
					nonCasBlob = permissionSet.EncodeXml();
				}
			}
			return array;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06001D1B RID: 7451 RVA: 0x00064D5C File Offset: 0x00062F5C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.NormalizePermissionSet();
			this.m_CheckedForNonCas = false;
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer be in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x06001D1C RID: 7452 RVA: 0x00064D6C File Offset: 0x00062F6C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertAssert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertAssert(ref stackCrawlMark);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00064D84 File Offset: 0x00062F84
		internal static PermissionSet RemoveRefusedPermissionSet(PermissionSet assertSet, PermissionSet refusedSet, out bool bFailedToCompress)
		{
			PermissionSet permissionSet = null;
			bFailedToCompress = false;
			if (assertSet == null)
			{
				return null;
			}
			if (refusedSet != null)
			{
				if (refusedSet.IsUnrestricted())
				{
					return null;
				}
				PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(refusedSet);
				while (permissionSetEnumeratorInternal.MoveNext())
				{
					object obj = permissionSetEnumeratorInternal.Current;
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)obj;
					int currentIndex = permissionSetEnumeratorInternal.GetCurrentIndex();
					if (codeAccessPermission != null)
					{
						CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)assertSet.GetPermission(currentIndex);
						try
						{
							if (codeAccessPermission.Intersect(codeAccessPermission2) != null)
							{
								if (!codeAccessPermission.Equals(codeAccessPermission2))
								{
									bFailedToCompress = true;
									return assertSet;
								}
								if (permissionSet == null)
								{
									permissionSet = assertSet.Copy();
								}
								permissionSet.RemovePermission(currentIndex);
							}
						}
						catch (ArgumentException)
						{
							if (permissionSet == null)
							{
								permissionSet = assertSet.Copy();
							}
							permissionSet.RemovePermission(currentIndex);
						}
						continue;
					}
				}
			}
			if (permissionSet != null)
			{
				return permissionSet;
			}
			return assertSet;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00064E44 File Offset: 0x00063044
		internal static void RemoveAssertedPermissionSet(PermissionSet demandSet, PermissionSet assertSet, out PermissionSet alteredDemandSet)
		{
			alteredDemandSet = null;
			PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(demandSet);
			while (permissionSetEnumeratorInternal.MoveNext())
			{
				object obj = permissionSetEnumeratorInternal.Current;
				CodeAccessPermission codeAccessPermission = (CodeAccessPermission)obj;
				int currentIndex = permissionSetEnumeratorInternal.GetCurrentIndex();
				if (codeAccessPermission != null)
				{
					CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)assertSet.GetPermission(currentIndex);
					try
					{
						if (codeAccessPermission.CheckAssert(codeAccessPermission2))
						{
							if (alteredDemandSet == null)
							{
								alteredDemandSet = demandSet.Copy();
							}
							alteredDemandSet.RemovePermission(currentIndex);
						}
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00064EC0 File Offset: 0x000630C0
		internal static bool IsIntersectingAssertedPermissions(PermissionSet assertSet1, PermissionSet assertSet2)
		{
			bool flag = false;
			if (assertSet1 != null && assertSet2 != null)
			{
				PermissionSetEnumeratorInternal permissionSetEnumeratorInternal = new PermissionSetEnumeratorInternal(assertSet2);
				while (permissionSetEnumeratorInternal.MoveNext())
				{
					object obj = permissionSetEnumeratorInternal.Current;
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)obj;
					int currentIndex = permissionSetEnumeratorInternal.GetCurrentIndex();
					if (codeAccessPermission != null)
					{
						CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)assertSet1.GetPermission(currentIndex);
						try
						{
							if (codeAccessPermission2 != null && !codeAccessPermission2.Equals(codeAccessPermission))
							{
								flag = true;
							}
						}
						catch (ArgumentException)
						{
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x17000342 RID: 834
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00064F38 File Offset: 0x00063138
		internal bool IgnoreTypeLoadFailures
		{
			set
			{
				this.m_ignoreTypeLoadFailures = value;
			}
		}

		// Token: 0x04000A10 RID: 2576
		private bool m_Unrestricted;

		// Token: 0x04000A11 RID: 2577
		[OptionalField(VersionAdded = 2)]
		private bool m_allPermissionsDecoded;

		// Token: 0x04000A12 RID: 2578
		[OptionalField(VersionAdded = 2)]
		internal TokenBasedSet m_permSet;

		// Token: 0x04000A13 RID: 2579
		[OptionalField(VersionAdded = 2)]
		private bool m_ignoreTypeLoadFailures;

		// Token: 0x04000A14 RID: 2580
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermissionSet;

		// Token: 0x04000A15 RID: 2581
		[NonSerialized]
		private bool m_CheckedForNonCas;

		// Token: 0x04000A16 RID: 2582
		[NonSerialized]
		private bool m_ContainsCas;

		// Token: 0x04000A17 RID: 2583
		[NonSerialized]
		private bool m_ContainsNonCas;

		// Token: 0x04000A18 RID: 2584
		[NonSerialized]
		private TokenBasedSet m_permSetSaved;

		// Token: 0x04000A19 RID: 2585
		private bool readableonly;

		// Token: 0x04000A1A RID: 2586
		private TokenBasedSet m_unrestrictedPermSet;

		// Token: 0x04000A1B RID: 2587
		private TokenBasedSet m_normalPermSet;

		// Token: 0x04000A1C RID: 2588
		[OptionalField(VersionAdded = 2)]
		private bool m_canUnrestrictedOverride;

		// Token: 0x04000A1D RID: 2589
		internal static readonly PermissionSet s_fullTrust = new PermissionSet(true);

		// Token: 0x04000A1E RID: 2590
		private const string s_str_PermissionSet = "PermissionSet";

		// Token: 0x04000A1F RID: 2591
		private const string s_str_Permission = "Permission";

		// Token: 0x04000A20 RID: 2592
		private const string s_str_IPermission = "IPermission";

		// Token: 0x04000A21 RID: 2593
		private const string s_str_Unrestricted = "Unrestricted";

		// Token: 0x04000A22 RID: 2594
		private const string s_str_PermissionUnion = "PermissionUnion";

		// Token: 0x04000A23 RID: 2595
		private const string s_str_PermissionIntersection = "PermissionIntersection";

		// Token: 0x04000A24 RID: 2596
		private const string s_str_PermissionUnrestrictedUnion = "PermissionUnrestrictedUnion";

		// Token: 0x04000A25 RID: 2597
		private const string s_str_PermissionUnrestrictedIntersection = "PermissionUnrestrictedIntersection";

		// Token: 0x02000B2A RID: 2858
		internal enum IsSubsetOfType
		{
			// Token: 0x04003345 RID: 13125
			Normal,
			// Token: 0x04003346 RID: 13126
			CheckDemand,
			// Token: 0x04003347 RID: 13127
			CheckPermitOnly,
			// Token: 0x04003348 RID: 13128
			CheckAssertion
		}
	}
}
