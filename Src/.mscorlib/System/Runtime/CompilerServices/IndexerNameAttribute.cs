using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates the name by which an indexer is known in programming languages that do not support indexers directly.</summary>
	// Token: 0x020008B6 RID: 2230
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IndexerNameAttribute" /> class.</summary>
		/// <param name="indexerName">The name of the indexer, as shown to other languages.</param>
		// Token: 0x06005DC4 RID: 24004 RVA: 0x0014AF40 File Offset: 0x00149140
		[__DynamicallyInvokable]
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
