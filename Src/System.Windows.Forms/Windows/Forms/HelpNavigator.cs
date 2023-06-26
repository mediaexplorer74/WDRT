using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants indicating which elements of the Help file to display.</summary>
	// Token: 0x02000273 RID: 627
	public enum HelpNavigator
	{
		/// <summary>The Help file opens to a specified topic, if the topic exists.</summary>
		// Token: 0x04001087 RID: 4231
		Topic = -2147483647,
		/// <summary>The Help file opens to the table of contents.</summary>
		// Token: 0x04001088 RID: 4232
		TableOfContents,
		/// <summary>The Help file opens to the index.</summary>
		// Token: 0x04001089 RID: 4233
		Index,
		/// <summary>The Help file opens to the search page.</summary>
		// Token: 0x0400108A RID: 4234
		Find,
		/// <summary>The Help file opens to the index entry for the first letter of a specified topic.</summary>
		// Token: 0x0400108B RID: 4235
		AssociateIndex,
		/// <summary>The Help file opens to the topic with the specified index entry, if one exists; otherwise, the index entry closest to the specified keyword is displayed.</summary>
		// Token: 0x0400108C RID: 4236
		KeywordIndex,
		/// <summary>The Help file opens to a topic indicated by a numeric topic identifier.</summary>
		// Token: 0x0400108D RID: 4237
		TopicId
	}
}
