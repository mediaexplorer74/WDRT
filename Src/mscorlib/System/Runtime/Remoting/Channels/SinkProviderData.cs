using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores sink provider data for sink providers.</summary>
	// Token: 0x0200084C RID: 2124
	[ComVisible(true)]
	public class SinkProviderData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> class.</summary>
		/// <param name="name">The name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</param>
		// Token: 0x06005A4C RID: 23116 RVA: 0x0013EEF7 File Offset: 0x0013D0F7
		public SinkProviderData(string name)
		{
			this._name = name;
		}

		/// <summary>Gets the name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> with the name of the XML node that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x0013EF21 File Offset: 0x0013D121
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a dictionary through which properties on the sink provider can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink provider can be accessed.</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x0013EF29 File Offset: 0x0013D129
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		/// <summary>Gets a list of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x0013EF31 File Offset: 0x0013D131
		public IList Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x04002906 RID: 10502
		private string _name;

		// Token: 0x04002907 RID: 10503
		private Hashtable _properties = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04002908 RID: 10504
		private ArrayList _children = new ArrayList();
	}
}
