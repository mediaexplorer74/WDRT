using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group.</summary>
	// Token: 0x02000697 RID: 1687
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06003EB5 RID: 16053 RVA: 0x00104E17 File Offset: 0x00103017
		internal Group(string text, int[] caps, int capcount, string name)
			: base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this._name = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>
		///   <see langword="true" /> if the match is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x00104E50 File Offset: 0x00103050
		[global::__DynamicallyInvokable]
		public bool Success
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._capcount != 0;
			}
		}

		/// <summary>Returns the name of the capturing group represented by the current instance.</summary>
		/// <returns>The name of the capturing group represented by the current instance.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x00104E5B File Offset: 0x0010305B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x00104E63 File Offset: 0x00103063
		[global::__DynamicallyInvokable]
		public CaptureCollection Captures
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		/// <summary>Returns a <see langword="Group" /> object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <returns>A regular expression <see langword="Group" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is <see langword="null" />.</exception>
		// Token: 0x06003EB9 RID: 16057 RVA: 0x00104E80 File Offset: 0x00103080
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner._capcount > 0)
			{
				Capture capture = captures[0];
			}
			return inner;
		}

		// Token: 0x04002DC0 RID: 11712
		internal static Group _emptygroup = new Group(string.Empty, new int[0], 0, string.Empty);

		// Token: 0x04002DC1 RID: 11713
		internal int[] _caps;

		// Token: 0x04002DC2 RID: 11714
		internal int _capcount;

		// Token: 0x04002DC3 RID: 11715
		internal CaptureCollection _capcoll;

		// Token: 0x04002DC4 RID: 11716
		[OptionalField]
		internal string _name;
	}
}
