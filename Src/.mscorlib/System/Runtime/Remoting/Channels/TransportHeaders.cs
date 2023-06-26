using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores a collection of headers used in the channel sinks.</summary>
	// Token: 0x0200084B RID: 2123
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> class.</summary>
		// Token: 0x06005A48 RID: 23112 RVA: 0x0013EDD3 File Offset: 0x0013CFD3
		public TransportHeaders()
		{
			this._headerList = new ArrayList(6);
		}

		/// <summary>Gets or sets a transport header that is associated with the given key.</summary>
		/// <param name="key">The <see cref="T:System.String" /> that the requested header is associated with.</param>
		/// <returns>A transport header that is associated with the given key, or <see langword="null" /> if the key was not found.</returns>
		// Token: 0x17000F00 RID: 3840
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				string text = (string)key;
				foreach (object obj in this._headerList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (string.Compare((string)dictionaryEntry.Key, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (key == null)
				{
					return;
				}
				string text = (string)key;
				for (int i = this._headerList.Count - 1; i >= 0; i--)
				{
					string text2 = (string)((DictionaryEntry)this._headerList[i]).Key;
					if (string.Compare(text2, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this._headerList.RemoveAt(i);
						break;
					}
				}
				if (value != null)
				{
					this._headerList.Add(new DictionaryEntry(key, value));
				}
			}
		}

		/// <summary>Returns an enumerator of the stored transport headers.</summary>
		/// <returns>An enumerator of the stored transport headers.</returns>
		// Token: 0x06005A4B RID: 23115 RVA: 0x0013EEEA File Offset: 0x0013D0EA
		[SecurityCritical]
		public IEnumerator GetEnumerator()
		{
			return this._headerList.GetEnumerator();
		}

		// Token: 0x04002905 RID: 10501
		private ArrayList _headerList;
	}
}
