using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the out-of-band data for a call.</summary>
	// Token: 0x02000887 RID: 2183
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name and value.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		// Token: 0x06005CBC RID: 23740 RVA: 0x001466FF File Offset: 0x001448FF
		public Header(string _Name, object _Value)
			: this(_Name, _Value, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name, value, and additional configuration information.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand the out-of-band data.</param>
		// Token: 0x06005CBD RID: 23741 RVA: 0x0014670A File Offset: 0x0014490A
		public Header(string _Name, object _Value, bool _MustUnderstand)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_Value">The object that contains the value of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand out-of-band data.</param>
		/// <param name="_HeaderNamespace">The <see cref="T:System.Runtime.Remoting.Messaging.Header" /> XML namespace.</param>
		// Token: 0x06005CBE RID: 23742 RVA: 0x00146727 File Offset: 0x00144927
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		/// <summary>Contains the name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x040029DB RID: 10715
		public string Name;

		/// <summary>Contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x040029DC RID: 10716
		public object Value;

		/// <summary>Indicates whether the receiving end must understand the out-of-band data.</summary>
		// Token: 0x040029DD RID: 10717
		public bool MustUnderstand;

		/// <summary>Indicates the XML namespace that the current <see cref="T:System.Runtime.Remoting.Messaging.Header" /> belongs to.</summary>
		// Token: 0x040029DE RID: 10718
		public string HeaderNamespace;
	}
}
