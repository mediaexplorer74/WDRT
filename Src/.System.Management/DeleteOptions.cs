using System;

namespace System.Management
{
	/// <summary>Specifies options for deleting a management object.</summary>
	// Token: 0x02000030 RID: 48
	public class DeleteOptions : ManagementOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.DeleteOptions" /> class for the delete operation, using default values. This is the default constructor.</summary>
		// Token: 0x06000173 RID: 371 RVA: 0x0000863B File Offset: 0x0000683B
		public DeleteOptions()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.DeleteOptions" /> class for a delete operation, using the specified values.</summary>
		/// <param name="context">A provider-specific, named-value pairs object to be passed through to the provider.</param>
		/// <param name="timeout">The length of time to let the operation perform before it times out. The default value is <see cref="F:System.TimeSpan.MaxValue" />. Setting this parameter will invoke the operation semisynchronously.</param>
		// Token: 0x06000174 RID: 372 RVA: 0x00008643 File Offset: 0x00006843
		public DeleteOptions(ManagementNamedValueCollection context, TimeSpan timeout)
			: base(context, timeout)
		{
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>A cloned object.</returns>
		// Token: 0x06000175 RID: 373 RVA: 0x00008650 File Offset: 0x00006850
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new DeleteOptions(managementNamedValueCollection, base.Timeout);
		}
	}
}
