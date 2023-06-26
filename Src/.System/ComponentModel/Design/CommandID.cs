using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x020005CA RID: 1482
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to.</param>
		/// <param name="commandID">The numeric identifier of this menu command.</param>
		// Token: 0x0600374B RID: 14155 RVA: 0x000EFB14 File Offset: 0x000EDD14
		public CommandID(Guid menuGroup, int commandID)
		{
			this.menuGroup = menuGroup;
			this.commandID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000EFB2A File Offset: 0x000EDD2A
		public virtual int ID
		{
			get
			{
				return this.commandID;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equivalent to this one; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600374D RID: 14157 RVA: 0x000EFB34 File Offset: 0x000EDD34
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.menuGroup.Equals(this.menuGroup) && commandID.commandID == this.commandID;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600374E RID: 14158 RVA: 0x000EFB78 File Offset: 0x000EDD78
		public override int GetHashCode()
		{
			return (this.menuGroup.GetHashCode() << 2) | this.commandID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000EFBA2 File Offset: 0x000EDDA2
		public virtual Guid Guid
		{
			get
			{
				return this.menuGroup;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier.</returns>
		// Token: 0x06003750 RID: 14160 RVA: 0x000EFBAC File Offset: 0x000EDDAC
		public override string ToString()
		{
			return this.menuGroup.ToString() + " : " + this.commandID.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04002AD2 RID: 10962
		private readonly Guid menuGroup;

		// Token: 0x04002AD3 RID: 10963
		private readonly int commandID;
	}
}
