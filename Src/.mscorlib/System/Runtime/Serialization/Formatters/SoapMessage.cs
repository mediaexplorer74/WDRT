using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Holds the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x02000763 RID: 1891
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		/// <summary>Gets or sets the parameter names for the called method.</summary>
		/// <returns>The parameter names for the called method.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0012546A File Offset: 0x0012366A
		// (set) Token: 0x0600532D RID: 21293 RVA: 0x00125472 File Offset: 0x00123672
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		/// <summary>Gets or sets the parameter values for the called method.</summary>
		/// <returns>Parameter values for the called method.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x0012547B File Offset: 0x0012367B
		// (set) Token: 0x0600532F RID: 21295 RVA: 0x00125483 File Offset: 0x00123683
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		/// <summary>This property is reserved. Use the <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> and/or <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> properties instead.</summary>
		/// <returns>Parameter types for the called method.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x0012548C File Offset: 0x0012368C
		// (set) Token: 0x06005331 RID: 21297 RVA: 0x00125494 File Offset: 0x00123694
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x0012549D File Offset: 0x0012369D
		// (set) Token: 0x06005333 RID: 21299 RVA: 0x001254A5 File Offset: 0x001236A5
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name where the object that contains the called method is located.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06005334 RID: 21300 RVA: 0x001254AE File Offset: 0x001236AE
		// (set) Token: 0x06005335 RID: 21301 RVA: 0x001254B6 File Offset: 0x001236B6
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		/// <summary>Gets or sets the out-of-band data of the called method.</summary>
		/// <returns>The out-of-band data of the called method.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x001254BF File Offset: 0x001236BF
		// (set) Token: 0x06005337 RID: 21303 RVA: 0x001254C7 File Offset: 0x001236C7
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x040024DE RID: 9438
		internal string[] paramNames;

		// Token: 0x040024DF RID: 9439
		internal object[] paramValues;

		// Token: 0x040024E0 RID: 9440
		internal Type[] paramTypes;

		// Token: 0x040024E1 RID: 9441
		internal string methodName;

		// Token: 0x040024E2 RID: 9442
		internal string xmlNameSpace;

		// Token: 0x040024E3 RID: 9443
		internal Header[] headers;
	}
}
