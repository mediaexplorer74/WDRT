using System;

namespace System.Management
{
	/// <summary>Provides a base class for query and enumeration-related options objects.</summary>
	// Token: 0x0200002C RID: 44
	public class EnumerationOptions : ManagementOptions
	{
		/// <summary>Gets or sets a value indicating whether the invoked operation should be performed in a synchronous or semisynchronous fashion. If this property is set to <see langword="true" />, the enumeration is invoked and the call returns immediately. The actual retrieval of the results will occur when the resulting collection is walked.</summary>
		/// <returns>
		///   <see langword="true" /> if the invoked operation should be performed in a synchronous or semisynchronous fashion; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000818E File Offset: 0x0000638E
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000819E File Offset: 0x0000639E
		public bool ReturnImmediately
		{
			get
			{
				return (base.Flags & 16) != 0;
			}
			set
			{
				base.Flags = ((!value) ? (base.Flags & -17) : (base.Flags | 16));
			}
		}

		/// <summary>Gets or sets the block size for block operations. When enumerating through a collection, WMI will return results in groups of the specified size.</summary>
		/// <returns>The block size in block operations.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000081BD File Offset: 0x000063BD
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000081C5 File Offset: 0x000063C5
		public int BlockSize
		{
			get
			{
				return this.blockSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.blockSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the collection is assumed to be rewindable. If <see langword="true" />, the objects in the collection will be kept available for multiple enumerations. If <see langword="false" />, the collection can only be enumerated one time.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is assumed to be rewindable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000081DD File Offset: 0x000063DD
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000081ED File Offset: 0x000063ED
		public bool Rewindable
		{
			get
			{
				return (base.Flags & 32) == 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags & -33) : (base.Flags | 32));
			}
		}

		/// <summary>Gets or sets a value indicating whether the objects returned from WMI should contain amended information. Typically, amended information is localizable information attached to the WMI object, such as object and property descriptions.</summary>
		/// <returns>
		///   <see langword="true" /> if the objects returned from WMI should contain amended information; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000821F File Offset: 0x0000641F
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

		/// <summary>Gets or sets a value indicating whether to the objects returned should have locatable information in them. This ensures that the system properties, such as __PATH, __RELPATH, and __SERVER, are non-NULL. This flag can only be used in queries, and is ignored in enumerations.</summary>
		/// <returns>
		///   <see langword="true" /> if the objects returned should have locatable information in them; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00008244 File Offset: 0x00006444
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00008257 File Offset: 0x00006457
		public bool EnsureLocatable
		{
			get
			{
				return (base.Flags & 256) != 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags | 256) : (base.Flags & -257));
			}
		}

		/// <summary>Gets or sets a value indicating whether the query should return a prototype of the result set instead of the actual results. This flag is used for prototyping.</summary>
		/// <returns>
		///   <see langword="true" /> if the query should return a prototype of the result set instead of the actual results; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000827C File Offset: 0x0000647C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000828B File Offset: 0x0000648B
		public bool PrototypeOnly
		{
			get
			{
				return (base.Flags & 2) != 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags | 2) : (base.Flags & -3));
			}
		}

		/// <summary>Gets or sets a value indicating whether direct access to the WMI provider is requested for the specified class, without any regard to its super class or derived classes.</summary>
		/// <returns>
		///   <see langword="true" /> if direct access to the WMI provider is requested for the specified class; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000082A9 File Offset: 0x000064A9
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000082BC File Offset: 0x000064BC
		public bool DirectRead
		{
			get
			{
				return (base.Flags & 512) != 0;
			}
			set
			{
				base.Flags = (value ? (base.Flags | 512) : (base.Flags & -513));
			}
		}

		/// <summary>Gets or sets a value indicating whether recursive enumeration is requested into all classes derived from the specified superclass. If <see langword="false" />, only immediate derived class members are returned.</summary>
		/// <returns>
		///   <see langword="true" /> if recursive enumeration is requested into all classes derived from the specified superclass; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000082E1 File Offset: 0x000064E1
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000082F0 File Offset: 0x000064F0
		public bool EnumerateDeep
		{
			get
			{
				return (base.Flags & 1) == 0;
			}
			set
			{
				base.Flags = ((!value) ? (base.Flags | 1) : (base.Flags & -2));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EnumerationOptions" /> class with default values (see the individual property descriptions for what the default values are). This is the default constructor.</summary>
		// Token: 0x0600015B RID: 347 RVA: 0x00008310 File Offset: 0x00006510
		public EnumerationOptions()
			: this(null, ManagementOptions.InfiniteTimeout, 1, true, true, false, false, false, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EnumerationOptions" /> class to be used for queries or enumerations, allowing the user to specify values for the different options.</summary>
		/// <param name="context">The options context object containing provider-specific information that can be passed through to the provider.</param>
		/// <param name="timeout">The time-out value for enumerating through the results.</param>
		/// <param name="blockSize">The number of items to retrieve at one time from WMI.</param>
		/// <param name="rewindable">
		///   <see langword="true" /> to show that the result set is rewindable (allows multiple traversal); otherwise, <see langword="false" />.</param>
		/// <param name="returnImmediatley">
		///   <see langword="true" /> to show that the operation should return immediately (semi-sync) or block until all results are available; otherwise, <see langword="false" />.</param>
		/// <param name="useAmendedQualifiers">
		///   <see langword="true" /> to show that the returned objects should contain amended (locale-aware) qualifiers; otherwise, <see langword="false" />.</param>
		/// <param name="ensureLocatable">
		///   <see langword="true" /> to ensure all returned objects have valid paths; otherwise, <see langword="false" />.</param>
		/// <param name="prototypeOnly">
		///   <see langword="true" /> to return a prototype of the result set instead of the actual results; otherwise, <see langword="false" />.</param>
		/// <param name="directRead">
		///   <see langword="true" /> to retrieve objects of only the specified class or from derived classes as well; otherwise, <see langword="false" />.</param>
		/// <param name="enumerateDeep">
		///   <see langword="true" /> to use recursive enumeration in subclasses; otherwise, <see langword="false" />.</param>
		// Token: 0x0600015C RID: 348 RVA: 0x00008334 File Offset: 0x00006534
		public EnumerationOptions(ManagementNamedValueCollection context, TimeSpan timeout, int blockSize, bool rewindable, bool returnImmediatley, bool useAmendedQualifiers, bool ensureLocatable, bool prototypeOnly, bool directRead, bool enumerateDeep)
			: base(context, timeout)
		{
			this.BlockSize = blockSize;
			this.Rewindable = rewindable;
			this.ReturnImmediately = returnImmediatley;
			this.UseAmendedQualifiers = useAmendedQualifiers;
			this.EnsureLocatable = ensureLocatable;
			this.PrototypeOnly = prototypeOnly;
			this.DirectRead = directRead;
			this.EnumerateDeep = enumerateDeep;
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x0600015D RID: 349 RVA: 0x00008388 File Offset: 0x00006588
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new EnumerationOptions(managementNamedValueCollection, base.Timeout, this.blockSize, this.Rewindable, this.ReturnImmediately, this.UseAmendedQualifiers, this.EnsureLocatable, this.PrototypeOnly, this.DirectRead, this.EnumerateDeep);
		}

		// Token: 0x0400013A RID: 314
		private int blockSize;
	}
}
