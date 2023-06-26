using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the allowed usage of a private virtual file system. This class cannot be inherited.</summary>
	// Token: 0x020002EA RID: 746
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002666 RID: 9830 RVA: 0x0008D7C6 File Offset: 0x0008B9C6
		public IsolatedStorageFilePermission(PermissionState state)
			: base(state)
		{
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0008D7CF File Offset: 0x0008B9CF
		internal IsolatedStorageFilePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
			: base(UsageAllowed, ExpirationDays, PermanentData)
		{
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002668 RID: 9832 RVA: 0x0008D7DC File Offset: 0x0008B9DC
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
			IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
			if (base.IsUnrestricted() || isolatedStorageFilePermission.IsUnrestricted())
			{
				return new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = IsolatedStoragePermission.max(this.m_userQuota, isolatedStorageFilePermission.m_userQuota),
				m_machineQuota = IsolatedStoragePermission.max(this.m_machineQuota, isolatedStorageFilePermission.m_machineQuota),
				m_expirationDays = IsolatedStoragePermission.max(this.m_expirationDays, isolatedStorageFilePermission.m_expirationDays),
				m_permanentData = (this.m_permanentData || isolatedStorageFilePermission.m_permanentData),
				m_allowed = (IsolatedStorageContainment)IsolatedStoragePermission.max((long)this.m_allowed, (long)isolatedStorageFilePermission.m_allowed)
			};
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002669 RID: 9833 RVA: 0x0008D8BC File Offset: 0x0008BABC
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_userQuota == 0L && this.m_machineQuota == 0L && this.m_expirationDays == 0L && !this.m_permanentData && this.m_allowed == IsolatedStorageContainment.None;
			}
			bool flag;
			try
			{
				IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
				if (isolatedStorageFilePermission.IsUnrestricted())
				{
					flag = true;
				}
				else
				{
					flag = isolatedStorageFilePermission.m_userQuota >= this.m_userQuota && isolatedStorageFilePermission.m_machineQuota >= this.m_machineQuota && isolatedStorageFilePermission.m_expirationDays >= this.m_expirationDays && (isolatedStorageFilePermission.m_permanentData || !this.m_permanentData) && isolatedStorageFilePermission.m_allowed >= this.m_allowed;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission object. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x0600266A RID: 9834 RVA: 0x0008D994 File Offset: 0x0008BB94
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
			IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
			if (isolatedStorageFilePermission.IsUnrestricted())
			{
				return this.Copy();
			}
			if (base.IsUnrestricted())
			{
				return target.Copy();
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
			isolatedStorageFilePermission2.m_userQuota = IsolatedStoragePermission.min(this.m_userQuota, isolatedStorageFilePermission.m_userQuota);
			isolatedStorageFilePermission2.m_machineQuota = IsolatedStoragePermission.min(this.m_machineQuota, isolatedStorageFilePermission.m_machineQuota);
			isolatedStorageFilePermission2.m_expirationDays = IsolatedStoragePermission.min(this.m_expirationDays, isolatedStorageFilePermission.m_expirationDays);
			isolatedStorageFilePermission2.m_permanentData = this.m_permanentData && isolatedStorageFilePermission.m_permanentData;
			isolatedStorageFilePermission2.m_allowed = (IsolatedStorageContainment)IsolatedStoragePermission.min((long)this.m_allowed, (long)isolatedStorageFilePermission.m_allowed);
			if (isolatedStorageFilePermission2.m_userQuota == 0L && isolatedStorageFilePermission2.m_machineQuota == 0L && isolatedStorageFilePermission2.m_expirationDays == 0L && !isolatedStorageFilePermission2.m_permanentData && isolatedStorageFilePermission2.m_allowed == IsolatedStorageContainment.None)
			{
				return null;
			}
			return isolatedStorageFilePermission2;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x0600266B RID: 9835 RVA: 0x0008DAA0 File Offset: 0x0008BCA0
		public override IPermission Copy()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			if (!base.IsUnrestricted())
			{
				isolatedStorageFilePermission.m_userQuota = this.m_userQuota;
				isolatedStorageFilePermission.m_machineQuota = this.m_machineQuota;
				isolatedStorageFilePermission.m_expirationDays = this.m_expirationDays;
				isolatedStorageFilePermission.m_permanentData = this.m_permanentData;
				isolatedStorageFilePermission.m_allowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0008DAF9 File Offset: 0x0008BCF9
		int IBuiltInPermission.GetTokenIndex()
		{
			return IsolatedStorageFilePermission.GetTokenIndex();
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0008DB00 File Offset: 0x0008BD00
		internal static int GetTokenIndex()
		{
			return 3;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x0600266E RID: 9838 RVA: 0x0008DB03 File Offset: 0x0008BD03
		[ComVisible(false)]
		public override SecurityElement ToXml()
		{
			return base.ToXml("System.Security.Permissions.IsolatedStorageFilePermission");
		}
	}
}
