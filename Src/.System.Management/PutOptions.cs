using System;

namespace System.Management
{
	/// <summary>Specifies options for committing management object changes.</summary>
	// Token: 0x0200002F RID: 47
	public class PutOptions : ManagementOptions
	{
		/// <summary>Gets or sets a value indicating whether the objects returned from WMI should contain amended information. Typically, amended information is localizable information attached to the WMI object, such as object and property descriptions.</summary>
		/// <returns>
		///   <see langword="true" /> if the objects returned from WMI should contain amended information; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000821F File Offset: 0x0000641F
		public bool UseAmendedQualifiers
		{
			get
			{
				return (base.Flags & 131072) != 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags | 131072) : (base.Flags & -131073));
			}
		}

		/// <summary>Gets or sets the type of commit to be performed for the object.</summary>
		/// <returns>One of the enumeration values that indicates the type of commit to be performed for the object.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008551 File Offset: 0x00006751
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000856C File Offset: 0x0000676C
		public PutType Type
		{
			get
			{
				if ((base.Flags & 1) != 0)
				{
					return PutType.UpdateOnly;
				}
				if ((base.Flags & 2) == 0)
				{
					return PutType.UpdateOrCreate;
				}
				return PutType.CreateOnly;
			}
			set
			{
				switch (value)
				{
				case PutType.UpdateOnly:
					base.Flags |= 1;
					return;
				case PutType.CreateOnly:
					base.Flags |= 2;
					return;
				case PutType.UpdateOrCreate:
					base.Flags |= 0;
					return;
				default:
					throw new ArgumentException(null, "Type");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.PutOptions" /> class for put operations, using default values. This is the default constructor.</summary>
		// Token: 0x0600016F RID: 367 RVA: 0x000085C7 File Offset: 0x000067C7
		public PutOptions()
			: this(null, ManagementOptions.InfiniteTimeout, false, PutType.UpdateOrCreate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.PutOptions" /> class for committing a WMI object, using the specified provider-specific context.</summary>
		/// <param name="context">A provider-specific, named-value pairs context object to be passed through to the provider.</param>
		// Token: 0x06000170 RID: 368 RVA: 0x000085D7 File Offset: 0x000067D7
		public PutOptions(ManagementNamedValueCollection context)
			: this(context, ManagementOptions.InfiniteTimeout, false, PutType.UpdateOrCreate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.PutOptions" /> class for committing a WMI object, using the specified option values.</summary>
		/// <param name="context">A provider-specific, named-value pairs object to be passed through to the provider.</param>
		/// <param name="timeout">The length of time to let the operation perform before it times out. The default is <see cref="F:System.TimeSpan.MaxValue" />.</param>
		/// <param name="useAmendedQualifiers">
		///   <see langword="true" /> if the returned objects should contain amended (locale-aware) qualifiers; otherwise, <see langword="false" />.</param>
		/// <param name="putType">The type of commit to be performed (update or create).</param>
		// Token: 0x06000171 RID: 369 RVA: 0x000085E7 File Offset: 0x000067E7
		public PutOptions(ManagementNamedValueCollection context, TimeSpan timeout, bool useAmendedQualifiers, PutType putType)
			: base(context, timeout)
		{
			this.UseAmendedQualifiers = useAmendedQualifiers;
			this.Type = putType;
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x06000172 RID: 370 RVA: 0x00008600 File Offset: 0x00006800
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new PutOptions(managementNamedValueCollection, base.Timeout, this.UseAmendedQualifiers, this.Type);
		}
	}
}
