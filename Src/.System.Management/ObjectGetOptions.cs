using System;

namespace System.Management
{
	/// <summary>Specifies options for getting a management object.</summary>
	// Token: 0x0200002E RID: 46
	public class ObjectGetOptions : ManagementOptions
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00008465 File Offset: 0x00006665
		internal static ObjectGetOptions _Clone(ObjectGetOptions options)
		{
			return ObjectGetOptions._Clone(options, null);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008470 File Offset: 0x00006670
		internal static ObjectGetOptions _Clone(ObjectGetOptions options, IdentifierChangedEventHandler handler)
		{
			ObjectGetOptions objectGetOptions;
			if (options != null)
			{
				objectGetOptions = new ObjectGetOptions(options.context, options.timeout, options.UseAmendedQualifiers);
			}
			else
			{
				objectGetOptions = new ObjectGetOptions();
			}
			if (handler != null)
			{
				objectGetOptions.IdentifierChanged += handler;
			}
			else if (options != null)
			{
				objectGetOptions.IdentifierChanged += options.HandleIdentifierChange;
			}
			return objectGetOptions;
		}

		/// <summary>Gets or sets a value indicating whether the objects returned from WMI should contain amended information. Typically, amended information is localizable information attached to the WMI object, such as object and property descriptions.</summary>
		/// <returns>Returns a <see cref="T:System.Boolean" /> value indicating whether the objects returned from WMI should contain amended information.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000084C2 File Offset: 0x000066C2
		public bool UseAmendedQualifiers
		{
			get
			{
				return (base.Flags & 131072) != 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags | 131072) : (base.Flags & -131073));
				base.FireIdentifierChanged();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectGetOptions" /> class for getting a WMI object, using default values. This is the default constructor.</summary>
		// Token: 0x06000167 RID: 359 RVA: 0x000084ED File Offset: 0x000066ED
		public ObjectGetOptions()
			: this(null, ManagementOptions.InfiniteTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectGetOptions" /> class for getting a WMI object, using the specified provider-specific context.</summary>
		/// <param name="context">A provider-specific, named-value pairs context object to be passed through to the provider.</param>
		// Token: 0x06000168 RID: 360 RVA: 0x000084FC File Offset: 0x000066FC
		public ObjectGetOptions(ManagementNamedValueCollection context)
			: this(context, ManagementOptions.InfiniteTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectGetOptions" /> class for getting a WMI object, using the given options values.</summary>
		/// <param name="context">A provider-specific, named-value pairs context object to be passed through to the provider.</param>
		/// <param name="timeout">The length of time to let the operation perform before it times out. The default is <see cref="F:System.TimeSpan.MaxValue" />.</param>
		/// <param name="useAmendedQualifiers">
		///   <see langword="true" /> if the returned objects should contain amended (locale-aware) qualifiers; otherwise, <see langword="false" />.</param>
		// Token: 0x06000169 RID: 361 RVA: 0x0000850B File Offset: 0x0000670B
		public ObjectGetOptions(ManagementNamedValueCollection context, TimeSpan timeout, bool useAmendedQualifiers)
			: base(context, timeout)
		{
			this.UseAmendedQualifiers = useAmendedQualifiers;
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x0600016A RID: 362 RVA: 0x0000851C File Offset: 0x0000671C
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new ObjectGetOptions(managementNamedValueCollection, base.Timeout, this.UseAmendedQualifiers);
		}
	}
}
