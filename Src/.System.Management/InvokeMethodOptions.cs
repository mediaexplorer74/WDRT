using System;

namespace System.Management
{
	/// <summary>Specifies options for invoking a management method.</summary>
	// Token: 0x02000031 RID: 49
	public class InvokeMethodOptions : ManagementOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.InvokeMethodOptions" /> class for the <see cref="M:System.Management.ManagementObject.InvokeMethod(System.String,System.Object[])" /> operation, using default values. This is the default constructor.</summary>
		// Token: 0x06000176 RID: 374 RVA: 0x0000863B File Offset: 0x0000683B
		public InvokeMethodOptions()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.InvokeMethodOptions" /> class for an invoke operation using the specified values.</summary>
		/// <param name="context">A provider-specific, named-value pairs object to be passed through to the provider.</param>
		/// <param name="timeout">The length of time to let the operation perform before it times out. The default value is <see cref="F:System.TimeSpan.MaxValue" />. Setting this parameter will invoke the operation semisynchronously.</param>
		// Token: 0x06000177 RID: 375 RVA: 0x00008643 File Offset: 0x00006843
		public InvokeMethodOptions(ManagementNamedValueCollection context, TimeSpan timeout)
			: base(context, timeout)
		{
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x06000178 RID: 376 RVA: 0x00008680 File Offset: 0x00006880
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new InvokeMethodOptions(managementNamedValueCollection, base.Timeout);
		}
	}
}
