using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x02000616 RID: 1558
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x060038EB RID: 14571 RVA: 0x000F1A81 File Offset: 0x000EFC81
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x060038EC RID: 14572 RVA: 0x000F1A94 File Offset: 0x000EFC94
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this.createType = createType;
			this.initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x060038ED RID: 14573 RVA: 0x000F1ABA File Offset: 0x000EFCBA
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x060038EE RID: 14574 RVA: 0x000F1AE5 File Offset: 0x000EFCE5
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x060038EF RID: 14575 RVA: 0x000F1B10 File Offset: 0x000EFD10
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this.createType = createType;
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x060038F0 RID: 14576 RVA: 0x000F1B31 File Offset: 0x000EFD31
		public CodeArrayCreateExpression(string createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x060038F1 RID: 14577 RVA: 0x000F1B57 File Offset: 0x000EFD57
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x060038F2 RID: 14578 RVA: 0x000F1B7D File Offset: 0x000EFD7D
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this.createType = createType;
			this.sizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x060038F3 RID: 14579 RVA: 0x000F1B9E File Offset: 0x000EFD9E
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x060038F4 RID: 14580 RVA: 0x000F1BC4 File Offset: 0x000EFDC4
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000F1BEA File Offset: 0x000EFDEA
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x000F1C0A File Offset: 0x000EFE0A
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference("");
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000F1C13 File Offset: 0x000EFE13
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000F1C1B File Offset: 0x000EFE1B
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x000F1C23 File Offset: 0x000EFE23
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000F1C2C File Offset: 0x000EFE2C
		// (set) Token: 0x060038FB RID: 14587 RVA: 0x000F1C34 File Offset: 0x000EFE34
		public CodeExpression SizeExpression
		{
			get
			{
				return this.sizeExpression;
			}
			set
			{
				this.sizeExpression = value;
			}
		}

		// Token: 0x04002B6C RID: 11116
		private CodeTypeReference createType;

		// Token: 0x04002B6D RID: 11117
		private CodeExpressionCollection initializers = new CodeExpressionCollection();

		// Token: 0x04002B6E RID: 11118
		private CodeExpression sizeExpression;

		// Token: 0x04002B6F RID: 11119
		private int size;
	}
}
