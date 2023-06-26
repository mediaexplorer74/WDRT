using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that contains a qualifier. The qualifier, represented by an <see cref="T:System.Security.AccessControl.AceQualifier" /> object, specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms. The <see cref="T:System.Security.AccessControl.QualifiedAce" /> class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonAce" /> and <see cref="T:System.Security.AccessControl.ObjectAce" /> classes.</summary>
	// Token: 0x02000206 RID: 518
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x06001E65 RID: 7781 RVA: 0x0006A360 File Offset: 0x00068560
		private AceQualifier QualifierFromType(AceType type, out bool isCallback)
		{
			switch (type)
			{
			case AceType.AccessAllowed:
				isCallback = false;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDenied:
				isCallback = false;
				return AceQualifier.AccessDenied;
			case AceType.SystemAudit:
				isCallback = false;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarm:
				isCallback = false;
				return AceQualifier.SystemAlarm;
			case AceType.AccessAllowedObject:
				isCallback = false;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedObject:
				isCallback = false;
				return AceQualifier.AccessDenied;
			case AceType.SystemAuditObject:
				isCallback = false;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmObject:
				isCallback = false;
				return AceQualifier.SystemAlarm;
			case AceType.AccessAllowedCallback:
				isCallback = true;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedCallback:
				isCallback = true;
				return AceQualifier.AccessDenied;
			case AceType.AccessAllowedCallbackObject:
				isCallback = true;
				return AceQualifier.AccessAllowed;
			case AceType.AccessDeniedCallbackObject:
				isCallback = true;
				return AceQualifier.AccessDenied;
			case AceType.SystemAuditCallback:
				isCallback = true;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmCallback:
				isCallback = true;
				return AceQualifier.SystemAlarm;
			case AceType.SystemAuditCallbackObject:
				isCallback = true;
				return AceQualifier.SystemAudit;
			case AceType.SystemAlarmCallbackObject:
				isCallback = true;
				return AceQualifier.SystemAlarm;
			}
			throw new SystemException();
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0006A40E File Offset: 0x0006860E
		internal QualifiedAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque)
			: base(type, flags, accessMask, sid)
		{
			this._qualifier = this.QualifierFromType(type, out this._isCallback);
			this.SetOpaque(opaque);
		}

		/// <summary>Gets a value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</summary>
		/// <returns>A value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0006A436 File Offset: 0x00068636
		public AceQualifier AceQualifier
		{
			get
			{
				return this._qualifier;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data; otherwise, false.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0006A43E File Offset: 0x0006863E
		public bool IsCallback
		{
			get
			{
				return this._isCallback;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001E69 RID: 7785
		internal abstract int MaxOpaqueLengthInternal { get; }

		/// <summary>Gets the length of the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. This property is valid only for callback Access Control Entries (ACEs).</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x0006A446 File Offset: 0x00068646
		public int OpaqueLength
		{
			get
			{
				if (this._opaque != null)
				{
					return this._opaque.Length;
				}
				return 0;
			}
		}

		/// <summary>Returns the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <returns>An array of byte values that represents the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</returns>
		// Token: 0x06001E6B RID: 7787 RVA: 0x0006A45A File Offset: 0x0006865A
		public byte[] GetOpaque()
		{
			return this._opaque;
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</param>
		// Token: 0x06001E6C RID: 7788 RVA: 0x0006A464 File Offset: 0x00068664
		public void SetOpaque(byte[] opaque)
		{
			if (opaque != null)
			{
				if (opaque.Length > this.MaxOpaqueLengthInternal)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), 0, this.MaxOpaqueLengthInternal));
				}
				if (opaque.Length % 4 != 0)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), 4));
				}
			}
			this._opaque = opaque;
		}

		// Token: 0x04000AF6 RID: 2806
		private readonly bool _isCallback;

		// Token: 0x04000AF7 RID: 2807
		private readonly AceQualifier _qualifier;

		// Token: 0x04000AF8 RID: 2808
		private byte[] _opaque;
	}
}
