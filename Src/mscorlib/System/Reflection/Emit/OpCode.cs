using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
	/// <summary>Describes an intermediate language (IL) instruction.</summary>
	// Token: 0x02000654 RID: 1620
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct OpCode
	{
		// Token: 0x06004CBD RID: 19645 RVA: 0x001182E4 File Offset: 0x001164E4
		internal OpCode(OpCodeValues value, int flags)
		{
			this.m_stringname = null;
			this.m_pop = (StackBehaviour)((flags >> 12) & 31);
			this.m_push = (StackBehaviour)((flags >> 17) & 31);
			this.m_operand = (OperandType)(flags & 31);
			this.m_type = (OpCodeType)((flags >> 9) & 7);
			this.m_size = (flags >> 22) & 3;
			this.m_s1 = (byte)(value >> 8);
			this.m_s2 = (byte)value;
			this.m_ctrl = (FlowControl)((flags >> 5) & 15);
			this.m_endsUncondJmpBlk = (flags & 16777216) != 0;
			this.m_stackChange = flags >> 28;
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0011836C File Offset: 0x0011656C
		internal bool EndsUncondJmpBlk()
		{
			return this.m_endsUncondJmpBlk;
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x00118374 File Offset: 0x00116574
		internal int StackChange()
		{
			return this.m_stackChange;
		}

		/// <summary>The operand type of an intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The operand type of an IL instruction.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x0011837C File Offset: 0x0011657C
		[__DynamicallyInvokable]
		public OperandType OperandType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_operand;
			}
		}

		/// <summary>The flow control characteristics of the intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The type of flow control.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x00118384 File Offset: 0x00116584
		[__DynamicallyInvokable]
		public FlowControl FlowControl
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ctrl;
			}
		}

		/// <summary>The type of intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The type of intermediate language (IL) instruction.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x0011838C File Offset: 0x0011658C
		[__DynamicallyInvokable]
		public OpCodeType OpCodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		/// <summary>How the intermediate language (IL) instruction pops the stack.</summary>
		/// <returns>Read-only. The way the IL instruction pops the stack.</returns>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x00118394 File Offset: 0x00116594
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPop
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_pop;
			}
		}

		/// <summary>How the intermediate language (IL) instruction pushes operand onto the stack.</summary>
		/// <returns>Read-only. The way the IL instruction pushes operand onto the stack.</returns>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004CC4 RID: 19652 RVA: 0x0011839C File Offset: 0x0011659C
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPush
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_push;
			}
		}

		/// <summary>The size of the intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The size of the IL instruction.</returns>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x001183A4 File Offset: 0x001165A4
		[__DynamicallyInvokable]
		public int Size
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_size;
			}
		}

		/// <summary>Gets the numeric value of the intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The numeric value of the IL instruction.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x001183AC File Offset: 0x001165AC
		[__DynamicallyInvokable]
		public short Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_size == 2)
				{
					return (short)(((int)this.m_s1 << 8) | (int)this.m_s2);
				}
				return (short)this.m_s2;
			}
		}

		/// <summary>The name of the intermediate language (IL) instruction.</summary>
		/// <returns>Read-only. The name of the IL instruction.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x001183D0 File Offset: 0x001165D0
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Size == 0)
				{
					return null;
				}
				string[] array = OpCode.g_nameCache;
				if (array == null)
				{
					array = new string[287];
					OpCode.g_nameCache = array;
				}
				OpCodeValues opCodeValues = (OpCodeValues)((ushort)this.Value);
				int num = (int)opCodeValues;
				if (num > 255)
				{
					if (num < 65024 || num > 65054)
					{
						return null;
					}
					num = 256 + (num - 65024);
				}
				string text = Volatile.Read<string>(ref array[num]);
				if (text != null)
				{
					return text;
				}
				text = Enum.GetName(typeof(OpCodeValues), opCodeValues).ToLowerInvariant().Replace("_", ".");
				Volatile.Write<string>(ref array[num], text);
				return text;
			}
		}

		/// <summary>Tests whether the given object is equal to this <see langword="Opcode" />.</summary>
		/// <param name="obj">The object to compare to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="Opcode" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CC8 RID: 19656 RVA: 0x00118483 File Offset: 0x00116683
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is OpCode && this.Equals((OpCode)obj);
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CC9 RID: 19657 RVA: 0x0011849B File Offset: 0x0011669B
		[__DynamicallyInvokable]
		public bool Equals(OpCode obj)
		{
			return obj.Value == this.Value;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CCA RID: 19658 RVA: 0x001184AC File Offset: 0x001166AC
		[__DynamicallyInvokable]
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004CCB RID: 19659 RVA: 0x001184B6 File Offset: 0x001166B6
		[__DynamicallyInvokable]
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		/// <summary>Returns the generated hash code for this <see langword="Opcode" />.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06004CCC RID: 19660 RVA: 0x001184C2 File Offset: 0x001166C2
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		/// <summary>Returns this <see langword="Opcode" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A string containing the name of this <see langword="Opcode" />.</returns>
		// Token: 0x06004CCD RID: 19661 RVA: 0x001184CA File Offset: 0x001166CA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002138 RID: 8504
		internal const int OperandTypeMask = 31;

		// Token: 0x04002139 RID: 8505
		internal const int FlowControlShift = 5;

		// Token: 0x0400213A RID: 8506
		internal const int FlowControlMask = 15;

		// Token: 0x0400213B RID: 8507
		internal const int OpCodeTypeShift = 9;

		// Token: 0x0400213C RID: 8508
		internal const int OpCodeTypeMask = 7;

		// Token: 0x0400213D RID: 8509
		internal const int StackBehaviourPopShift = 12;

		// Token: 0x0400213E RID: 8510
		internal const int StackBehaviourPushShift = 17;

		// Token: 0x0400213F RID: 8511
		internal const int StackBehaviourMask = 31;

		// Token: 0x04002140 RID: 8512
		internal const int SizeShift = 22;

		// Token: 0x04002141 RID: 8513
		internal const int SizeMask = 3;

		// Token: 0x04002142 RID: 8514
		internal const int EndsUncondJmpBlkFlag = 16777216;

		// Token: 0x04002143 RID: 8515
		internal const int StackChangeShift = 28;

		// Token: 0x04002144 RID: 8516
		private string m_stringname;

		// Token: 0x04002145 RID: 8517
		private StackBehaviour m_pop;

		// Token: 0x04002146 RID: 8518
		private StackBehaviour m_push;

		// Token: 0x04002147 RID: 8519
		private OperandType m_operand;

		// Token: 0x04002148 RID: 8520
		private OpCodeType m_type;

		// Token: 0x04002149 RID: 8521
		private int m_size;

		// Token: 0x0400214A RID: 8522
		private byte m_s1;

		// Token: 0x0400214B RID: 8523
		private byte m_s2;

		// Token: 0x0400214C RID: 8524
		private FlowControl m_ctrl;

		// Token: 0x0400214D RID: 8525
		private bool m_endsUncondJmpBlk;

		// Token: 0x0400214E RID: 8526
		private int m_stackChange;

		// Token: 0x0400214F RID: 8527
		private static volatile string[] g_nameCache;
	}
}
