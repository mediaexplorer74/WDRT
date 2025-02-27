﻿using System;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	/// <summary>Allows declarative printing permission checks.</summary>
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06000803 RID: 2051 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		public PrintingPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the type of printing allowed.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not one of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</exception>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00020AD9 File Offset: 0x0001ECD9
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00020AE1 File Offset: 0x0001ECE1
		public PrintingPermissionLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				if (value < PrintingPermissionLevel.NoPrinting || value > PrintingPermissionLevel.AllPrinting)
				{
					throw new ArgumentException(SR.GetString("PrintingPermissionAttributeInvalidPermissionLevel"), "value");
				}
				this.level = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels, which are set through the <see cref="P:System.Drawing.Printing.PrintingPermissionAttribute.Level" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x06000806 RID: 2054 RVA: 0x00020B07 File Offset: 0x0001ED07
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PrintingPermission(PermissionState.Unrestricted);
			}
			return new PrintingPermission(this.level);
		}

		// Token: 0x040006EC RID: 1772
		private PrintingPermissionLevel level;
	}
}
