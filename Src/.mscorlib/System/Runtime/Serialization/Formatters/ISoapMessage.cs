using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Provides an interface for an object that contains the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x0200075E RID: 1886
	[ComVisible(true)]
	public interface ISoapMessage
	{
		/// <summary>Gets or sets the parameter names of the method call.</summary>
		/// <returns>The parameter names of the method call.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06005310 RID: 21264
		// (set) Token: 0x06005311 RID: 21265
		string[] ParamNames { get; set; }

		/// <summary>Gets or sets the parameter values of a method call.</summary>
		/// <returns>The parameter values of a method call.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005312 RID: 21266
		// (set) Token: 0x06005313 RID: 21267
		object[] ParamValues { get; set; }

		/// <summary>Gets or sets the parameter types of a method call.</summary>
		/// <returns>The parameter types of a method call.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005314 RID: 21268
		// (set) Token: 0x06005315 RID: 21269
		Type[] ParamTypes { get; set; }

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06005316 RID: 21270
		// (set) Token: 0x06005317 RID: 21271
		string MethodName { get; set; }

		/// <summary>Gets or sets the XML namespace of the SOAP RPC (Remote Procedure Call) <see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> element.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005318 RID: 21272
		// (set) Token: 0x06005319 RID: 21273
		string XmlNameSpace { get; set; }

		/// <summary>Gets or sets the out-of-band data of the method call.</summary>
		/// <returns>The out-of-band data of the method call.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600531A RID: 21274
		// (set) Token: 0x0600531B RID: 21275
		Header[] Headers { get; set; }
	}
}
