using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a stack object that can be used by a serializer to make information available to nested serializers.</summary>
	// Token: 0x02000603 RID: 1539
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ContextStack
	{
		/// <summary>Gets the current object on the stack.</summary>
		/// <returns>The current object on the stack, or <see langword="null" /> if no objects were pushed.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000F114D File Offset: 0x000EF34D
		public object Current
		{
			get
			{
				if (this.contextStack != null && this.contextStack.Count > 0)
				{
					return this.contextStack[this.contextStack.Count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the object on the stack at the specified level.</summary>
		/// <param name="level">The level of the object to retrieve on the stack. Level 0 is the top of the stack, level 1 is the next down, and so on. This level must be 0 or greater. If level is greater than the number of levels on the stack, it returns <see langword="null" />.</param>
		/// <returns>The object on the stack at the specified level, or <see langword="null" /> if no object exists at that level.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="level" /> is less than 0.</exception>
		// Token: 0x17000D82 RID: 3458
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				if (this.contextStack != null && level < this.contextStack.Count)
				{
					return this.contextStack[this.contextStack.Count - 1 - level];
				}
				return null;
			}
		}

		/// <summary>Gets the first object on the stack that inherits from or implements the specified type.</summary>
		/// <param name="type">A type to retrieve from the context stack.</param>
		/// <returns>The first object on the stack that inherits from or implements the specified type, or <see langword="null" /> if no object on the stack implements the type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x17000D83 RID: 3459
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this.contextStack != null)
				{
					int i = this.contextStack.Count;
					while (i > 0)
					{
						object obj = this.contextStack[--i];
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Appends an object to the end of the stack, rather than pushing it onto the top of the stack.</summary>
		/// <param name="context">A context object to append to the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is <see langword="null" />.</exception>
		// Token: 0x0600388B RID: 14475 RVA: 0x000F1228 File Offset: 0x000EF428
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Insert(0, context);
		}

		/// <summary>Removes the current object off of the stack, returning its value.</summary>
		/// <returns>The object removed from the stack; <see langword="null" /> if no objects are on the stack.</returns>
		// Token: 0x0600388C RID: 14476 RVA: 0x000F1258 File Offset: 0x000EF458
		public object Pop()
		{
			object obj = null;
			if (this.contextStack != null && this.contextStack.Count > 0)
			{
				int num = this.contextStack.Count - 1;
				obj = this.contextStack[num];
				this.contextStack.RemoveAt(num);
			}
			return obj;
		}

		/// <summary>Pushes, or places, the specified object onto the stack.</summary>
		/// <param name="context">The context object to push onto the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is <see langword="null" />.</exception>
		// Token: 0x0600388D RID: 14477 RVA: 0x000F12A5 File Offset: 0x000EF4A5
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Add(context);
		}

		// Token: 0x04002B59 RID: 11097
		private ArrayList contextStack;
	}
}
