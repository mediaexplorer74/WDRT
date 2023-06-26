using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	/// <summary>Generates Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x0200063B RID: 1595
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ILGenerator))]
	[ComVisible(true)]
	public class ILGenerator : _ILGenerator
	{
		// Token: 0x06004AB2 RID: 19122 RVA: 0x0010F128 File Offset: 0x0010D328
		internal static int[] EnlargeArray(int[] incoming)
		{
			int[] array = new int[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0010F14C File Offset: 0x0010D34C
		private static byte[] EnlargeArray(byte[] incoming)
		{
			byte[] array = new byte[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0010F170 File Offset: 0x0010D370
		private static byte[] EnlargeArray(byte[] incoming, int requiredSize)
		{
			byte[] array = new byte[requiredSize];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0010F190 File Offset: 0x0010D390
		private static __FixupData[] EnlargeArray(__FixupData[] incoming)
		{
			__FixupData[] array = new __FixupData[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0010F1B4 File Offset: 0x0010D3B4
		private static __ExceptionInfo[] EnlargeArray(__ExceptionInfo[] incoming)
		{
			__ExceptionInfo[] array = new __ExceptionInfo[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x0010F1D7 File Offset: 0x0010D3D7
		internal int CurrExcStackCount
		{
			get
			{
				return this.m_currExcStackCount;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x0010F1DF File Offset: 0x0010D3DF
		internal __ExceptionInfo[] CurrExcStack
		{
			get
			{
				return this.m_currExcStack;
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x0010F1E7 File Offset: 0x0010D3E7
		internal ILGenerator(MethodInfo methodBuilder)
			: this(methodBuilder, 64)
		{
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x0010F1F4 File Offset: 0x0010D3F4
		internal ILGenerator(MethodInfo methodBuilder, int size)
		{
			if (size < 16)
			{
				this.m_ILStream = new byte[16];
			}
			else
			{
				this.m_ILStream = new byte[size];
			}
			this.m_length = 0;
			this.m_labelCount = 0;
			this.m_fixupCount = 0;
			this.m_labelList = null;
			this.m_fixupData = null;
			this.m_exceptions = null;
			this.m_exceptionCount = 0;
			this.m_currExcStack = null;
			this.m_currExcStackCount = 0;
			this.m_RelocFixupList = null;
			this.m_RelocFixupCount = 0;
			this.m_ScopeTree = new ScopeTree();
			this.m_LineNumberInfo = new LineNumberInfo();
			this.m_methodBuilder = methodBuilder;
			this.m_localCount = 0;
			MethodBuilder methodBuilder2 = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder2 == null)
			{
				this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(null);
				return;
			}
			this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(methodBuilder2.GetTypeBuilder().Module);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x0010F2D0 File Offset: 0x0010D4D0
		internal virtual void RecordTokenFixup()
		{
			if (this.m_RelocFixupList == null)
			{
				this.m_RelocFixupList = new int[8];
			}
			else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
			{
				this.m_RelocFixupList = ILGenerator.EnlargeArray(this.m_RelocFixupList);
			}
			int[] relocFixupList = this.m_RelocFixupList;
			int relocFixupCount = this.m_RelocFixupCount;
			this.m_RelocFixupCount = relocFixupCount + 1;
			relocFixupList[relocFixupCount] = this.m_length;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x0010F334 File Offset: 0x0010D534
		internal void InternalEmit(OpCode opcode)
		{
			int num;
			if (opcode.Size != 1)
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)(opcode.Value >> 8);
			}
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)opcode.Value;
			this.UpdateStackSize(opcode, opcode.StackChange());
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0010F39C File Offset: 0x0010D59C
		internal void UpdateStackSize(OpCode opcode, int stackchange)
		{
			this.m_maxMidStackCur += stackchange;
			if (this.m_maxMidStackCur > this.m_maxMidStack)
			{
				this.m_maxMidStack = this.m_maxMidStackCur;
			}
			else if (this.m_maxMidStackCur < 0)
			{
				this.m_maxMidStackCur = 0;
			}
			if (opcode.EndsUncondJmpBlk())
			{
				this.m_maxStackSize += this.m_maxMidStack;
				this.m_maxMidStack = 0;
				this.m_maxMidStackCur = 0;
			}
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0010F40D File Offset: 0x0010D60D
		[SecurityCritical]
		private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMethodTokenInternal(method, optionalParameterTypes, useMethodDef);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x0010F427 File Offset: 0x0010D627
		[SecurityCritical]
		internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x0010F435 File Offset: 0x0010D635
		[SecurityCritical]
		private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, cGenericParameters);
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x0010F454 File Offset: 0x0010D654
		internal byte[] BakeByteArray()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_length == 0)
			{
				return null;
			}
			int length = this.m_length;
			byte[] array = new byte[length];
			Array.Copy(this.m_ILStream, array, length);
			for (int i = 0; i < this.m_fixupCount; i++)
			{
				int num = this.GetLabelPos(this.m_fixupData[i].m_fixupLabel) - (this.m_fixupData[i].m_fixupPos + this.m_fixupData[i].m_fixupInstSize);
				if (this.m_fixupData[i].m_fixupInstSize == 1)
				{
					if (num < -128 || num > 127)
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_IllegalOneByteBranch", new object[]
						{
							this.m_fixupData[i].m_fixupPos,
							num
						}));
					}
					if (num < 0)
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)(256 + num);
					}
					else
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)num;
					}
				}
				else
				{
					ILGenerator.PutInteger4InArray(num, this.m_fixupData[i].m_fixupPos, array);
				}
			}
			return array;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x0010F59C File Offset: 0x0010D79C
		internal __ExceptionInfo[] GetExceptions()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_exceptionCount == 0)
			{
				return null;
			}
			__ExceptionInfo[] array = new __ExceptionInfo[this.m_exceptionCount];
			Array.Copy(this.m_exceptions, array, this.m_exceptionCount);
			ILGenerator.SortExceptions(array);
			return array;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x0010F5F0 File Offset: 0x0010D7F0
		internal void EnsureCapacity(int size)
		{
			if (this.m_length + size >= this.m_ILStream.Length)
			{
				if (this.m_length + size >= 2 * this.m_ILStream.Length)
				{
					this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream, this.m_length + size);
					return;
				}
				this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream);
			}
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0010F64E File Offset: 0x0010D84E
		internal void PutInteger4(int value)
		{
			this.m_length = ILGenerator.PutInteger4InArray(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x0010F668 File Offset: 0x0010D868
		private static int PutInteger4InArray(int value, int startPos, byte[] array)
		{
			array[startPos++] = (byte)value;
			array[startPos++] = (byte)(value >> 8);
			array[startPos++] = (byte)(value >> 16);
			array[startPos++] = (byte)(value >> 24);
			return startPos;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0010F69C File Offset: 0x0010D89C
		private int GetLabelPos(Label lbl)
		{
			int labelValue = lbl.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelCount)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabel"));
			}
			if (this.m_labelList[labelValue] < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabelContent"));
			}
			return this.m_labelList[labelValue];
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0010F6F4 File Offset: 0x0010D8F4
		private void AddFixup(Label lbl, int pos, int instSize)
		{
			if (this.m_fixupData == null)
			{
				this.m_fixupData = new __FixupData[8];
			}
			else if (this.m_fixupData.Length <= this.m_fixupCount)
			{
				this.m_fixupData = ILGenerator.EnlargeArray(this.m_fixupData);
			}
			this.m_fixupData[this.m_fixupCount].m_fixupPos = pos;
			this.m_fixupData[this.m_fixupCount].m_fixupLabel = lbl;
			this.m_fixupData[this.m_fixupCount].m_fixupInstSize = instSize;
			this.m_fixupCount++;
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x0010F78B File Offset: 0x0010D98B
		internal int GetMaxStackSize()
		{
			return this.m_maxStackSize;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0010F794 File Offset: 0x0010D994
		private static void SortExceptions(__ExceptionInfo[] exceptions)
		{
			int num = exceptions.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				for (int j = i + 1; j < num; j++)
				{
					if (exceptions[num2].IsInner(exceptions[j]))
					{
						num2 = j;
					}
				}
				__ExceptionInfo _ExceptionInfo = exceptions[i];
				exceptions[i] = exceptions[num2];
				exceptions[num2] = _ExceptionInfo;
			}
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0010F7E4 File Offset: 0x0010D9E4
		internal int[] GetTokenFixups()
		{
			if (this.m_RelocFixupCount == 0)
			{
				return null;
			}
			int[] array = new int[this.m_RelocFixupCount];
			Array.Copy(this.m_RelocFixupList, array, this.m_RelocFixupCount);
			return array;
		}

		/// <summary>Puts the specified instruction onto the stream of instructions.</summary>
		/// <param name="opcode">The Microsoft Intermediate Language (MSIL) instruction to be put onto the stream.</param>
		// Token: 0x06004ACB RID: 19147 RVA: 0x0010F81A File Offset: 0x0010DA1A
		public virtual void Emit(OpCode opcode)
		{
			this.EnsureCapacity(3);
			this.InternalEmit(opcode);
		}

		/// <summary>Puts the specified instruction and character argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The character argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004ACC RID: 19148 RVA: 0x0010F82C File Offset: 0x0010DA2C
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = arg;
		}

		/// <summary>Puts the specified instruction and character argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The character argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004ACD RID: 19149 RVA: 0x0010F860 File Offset: 0x0010DA60
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			int num;
			if (arg < 0)
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)(256 + (int)arg);
				return;
			}
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)arg;
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="arg">The <see langword="Int" /> argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004ACE RID: 19150 RVA: 0x0010F8BC File Offset: 0x0010DABC
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.EnsureCapacity(5);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int num = this.m_length;
			this.m_length = num + 1;
			ilstream[num] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)(arg >> 8);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004ACF RID: 19151 RVA: 0x0010F90D File Offset: 0x0010DB0D
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(arg);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given method.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="meth">A <see langword="MethodInfo" /> representing a method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="meth" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="meth" /> is a generic method for which the <see cref="P:System.Reflection.MethodBase.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06004AD0 RID: 19152 RVA: 0x0010F924 File Offset: 0x0010DB24
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
			{
				this.EmitCall(opcode, meth, null);
				return;
			}
			int num = 0;
			bool flag = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
			int methodToken = this.GetMethodToken(meth, null, flag);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		/// <summary>Puts a <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> instruction onto the Microsoft intermediate language (MSIL) stream, specifying a managed calling convention for the indirect call.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.</param>
		/// <param name="callingConvention">The managed calling convention to be used.</param>
		/// <param name="returnType">The <see cref="T:System.Type" /> of the result.</param>
		/// <param name="parameterTypes">The types of the required arguments to the instruction.</param>
		/// <param name="optionalParameterTypes">The types of the optional arguments for <see langword="varargs" /> calls.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="optionalParameterTypes" /> is not <see langword="null" />, but <paramref name="callingConvention" /> does not include the <see cref="F:System.Reflection.CallingConventions.VarArgs" /> flag.</exception>
		// Token: 0x06004AD1 RID: 19153 RVA: 0x0010F9D4 File Offset: 0x0010DBD4
		[SecuritySafeCritical]
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				num--;
			}
			num--;
			this.UpdateStackSize(OpCodes.Calli, num);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(memberRefSignature).Token);
		}

		/// <summary>Puts a <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> instruction onto the Microsoft intermediate language (MSIL) stream, specifying an unmanaged calling convention for the indirect call.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.</param>
		/// <param name="unmanagedCallConv">The unmanaged calling convention to be used.</param>
		/// <param name="returnType">The <see cref="T:System.Type" /> of the result.</param>
		/// <param name="parameterTypes">The types of the required arguments to the instruction.</param>
		// Token: 0x06004AD2 RID: 19154 RVA: 0x0010FA90 File Offset: 0x0010DC90
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(moduleBuilder, unmanagedCallConv, returnType);
			if (parameterTypes != null)
			{
				for (int i = 0; i < num2; i++)
				{
					methodSigHelper.AddArgument(parameterTypes[i]);
				}
			}
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= num2;
			}
			num--;
			this.UpdateStackSize(OpCodes.Calli, num);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(methodSigHelper).Token);
		}

		/// <summary>Puts a <see langword="call" /> or <see langword="callvirt" /> instruction onto the Microsoft intermediate language (MSIL) stream to call a <see langword="varargs" /> method.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream. Must be <see cref="F:System.Reflection.Emit.OpCodes.Call" />, <see cref="F:System.Reflection.Emit.OpCodes.Callvirt" />, or <see cref="F:System.Reflection.Emit.OpCodes.Newobj" />.</param>
		/// <param name="methodInfo">The <see langword="varargs" /> method to be called.</param>
		/// <param name="optionalParameterTypes">The types of the optional arguments if the method is a <see langword="varargs" /> method; otherwise, <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="opcode" /> does not specify a method call.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="methodInfo" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calling convention for the method is not <see langword="varargs" />, but optional parameter types are supplied. This exception is thrown in the .NET Framework versions 1.0 and 1.1, In subsequent versions, no exception is thrown.</exception>
		// Token: 0x06004AD3 RID: 19155 RVA: 0x0010FB40 File Offset: 0x0010DD40
		[SecuritySafeCritical]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), "opcode");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(methodInfo, optionalParameterTypes, false);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			Type[] parameterTypes = methodInfo.GetParameterTypes();
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		/// <summary>Puts the specified instruction and a signature token onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="signature">A helper for constructing a signature token.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x06004AD4 RID: 19156 RVA: 0x0010FC28 File Offset: 0x0010DE28
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetSignatureToken(signature).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				this.UpdateStackSize(opcode, num);
			}
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		/// <summary>Puts the specified instruction and metadata token for the specified constructor onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="con">A <see langword="ConstructorInfo" /> representing a constructor.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />. This exception is new in the .NET Framework 4.</exception>
		// Token: 0x06004AD5 RID: 19157 RVA: 0x0010FCA4 File Offset: 0x0010DEA4
		[SecuritySafeCritical]
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(con, null, true);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				Type[] parameterTypes = con.GetParameterTypes();
				if (parameterTypes != null)
				{
					num -= parameterTypes.Length;
				}
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given type.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="cls">A <see langword="Type" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cls" /> is <see langword="null" />.</exception>
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0010FD20 File Offset: 0x0010DF20
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, Type cls)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int num;
			if (opcode == OpCodes.Ldtoken && cls != null && cls.IsGenericTypeDefinition)
			{
				num = moduleBuilder.GetTypeToken(cls).Token;
			}
			else
			{
				num = moduleBuilder.GetTypeTokenInternal(cls).Token;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(num);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004AD7 RID: 19159 RVA: 0x0010FD9C File Offset: 0x0010DF9C
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int num = this.m_length;
			this.m_length = num + 1;
			ilstream[num] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)(arg >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream3[num] = (byte)(arg >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream4[num] = (byte)(arg >> 24);
			byte[] ilstream5 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream5[num] = (byte)(arg >> 32);
			byte[] ilstream6 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream6[num] = (byte)(arg >> 40);
			byte[] ilstream7 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream7[num] = (byte)(arg >> 48);
			byte[] ilstream8 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream8[num] = (byte)(arg >> 56);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream.</param>
		/// <param name="arg">The <see langword="Single" /> argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004AD8 RID: 19160 RVA: 0x0010FE9C File Offset: 0x0010E09C
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, float arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			uint num = *(uint*)(&arg);
			byte[] ilstream = this.m_ILStream;
			int num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream[num2] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream2[num2] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream3[num2] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream4[num2] = (byte)(num >> 24);
		}

		/// <summary>Puts the specified instruction and numerical argument onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be put onto the stream. Defined in the <see langword="OpCodes" /> enumeration.</param>
		/// <param name="arg">The numerical argument pushed onto the stream immediately after the instruction.</param>
		// Token: 0x06004AD9 RID: 19161 RVA: 0x0010FF2C File Offset: 0x0010E12C
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, double arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			ulong num = (ulong)(*(long*)(&arg));
			byte[] ilstream = this.m_ILStream;
			int num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream[num2] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream2[num2] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream3[num2] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream4[num2] = (byte)(num >> 24);
			byte[] ilstream5 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream5[num2] = (byte)(num >> 32);
			byte[] ilstream6 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream6[num2] = (byte)(num >> 40);
			byte[] ilstream7 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream7[num2] = (byte)(num >> 48);
			byte[] ilstream8 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream8[num2] = (byte)(num >> 56);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream and leaves space to include a label when fixes are done.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="label">The label to which to branch from this location.</param>
		// Token: 0x06004ADA RID: 19162 RVA: 0x00110034 File Offset: 0x0010E234
		public virtual void Emit(OpCode opcode, Label label)
		{
			int labelValue = label.GetLabelValue();
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (OpCodes.TakesSingleByteArgument(opcode))
			{
				this.AddFixup(label, this.m_length, 1);
				this.m_length++;
				return;
			}
			this.AddFixup(label, this.m_length, 4);
			this.m_length += 4;
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream and leaves space to include a label when fixes are done.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="labels">The array of label objects to which to branch from this location. All of the labels will be used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />. This exception is new in the .NET Framework 4.</exception>
		// Token: 0x06004ADB RID: 19163 RVA: 0x00110098 File Offset: 0x0010E298
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.EnsureCapacity(num * 4 + 7);
			this.InternalEmit(opcode);
			this.PutInteger4(num);
			int i = num * 4;
			int num2 = 0;
			while (i > 0)
			{
				this.AddFixup(labels[num2], this.m_length, i);
				this.m_length += 4;
				i -= 4;
				num2++;
			}
		}

		/// <summary>Puts the specified instruction and metadata token for the specified field onto the Microsoft intermediate language (MSIL) stream of instructions.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="field">A <see langword="FieldInfo" /> representing a field.</param>
		// Token: 0x06004ADC RID: 19164 RVA: 0x00110108 File Offset: 0x0010E308
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetFieldToken(field).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the metadata token for the given string.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="str">The <see langword="String" /> to be emitted.</param>
		// Token: 0x06004ADD RID: 19165 RVA: 0x00110154 File Offset: 0x0010E354
		public virtual void Emit(OpCode opcode, string str)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetStringConstant(str).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(token);
		}

		/// <summary>Puts the specified instruction onto the Microsoft intermediate language (MSIL) stream followed by the index of the given local variable.</summary>
		/// <param name="opcode">The MSIL instruction to be emitted onto the stream.</param>
		/// <param name="local">A local variable.</param>
		/// <exception cref="T:System.ArgumentException">The parent method of the <paramref name="local" /> parameter does not match the method associated with this <see cref="T:System.Reflection.Emit.ILGenerator" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="local" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="opcode" /> is a single-byte instruction, and <paramref name="local" /> represents a local variable with an index greater than <see langword="Byte.MaxValue" />.</exception>
		// Token: 0x06004ADE RID: 19166 RVA: 0x00110198 File Offset: 0x0010E398
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			int localIndex = local.GetLocalIndex();
			if (local.GetMethodBuilder() != this.m_methodBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchedMethodForLocal"), "local");
			}
			if (opcode.Equals(OpCodes.Ldloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Ldloc_0;
					break;
				case 1:
					opcode = OpCodes.Ldloc_1;
					break;
				case 2:
					opcode = OpCodes.Ldloc_2;
					break;
				case 3:
					opcode = OpCodes.Ldloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Ldloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Stloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Stloc_0;
					break;
				case 1:
					opcode = OpCodes.Stloc_1;
					break;
				case 2:
					opcode = OpCodes.Stloc_2;
					break;
				case 3:
					opcode = OpCodes.Stloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Stloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= 255)
			{
				opcode = OpCodes.Ldloca_S;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.OperandType == OperandType.InlineNone)
			{
				return;
			}
			int num;
			if (!OpCodes.TakesSingleByteArgument(opcode))
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)localIndex;
				byte[] ilstream2 = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream2[num] = (byte)(localIndex >> 8);
				return;
			}
			if (localIndex > 255)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound"));
			}
			byte[] ilstream3 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream3[num] = (byte)localIndex;
		}

		/// <summary>Begins an exception block for a non-filtered exception.</summary>
		/// <returns>The label for the end of the block. This will leave you in the correct place to execute finally blocks or to finish the try.</returns>
		// Token: 0x06004ADF RID: 19167 RVA: 0x00110350 File Offset: 0x0010E550
		public virtual Label BeginExceptionBlock()
		{
			if (this.m_exceptions == null)
			{
				this.m_exceptions = new __ExceptionInfo[2];
			}
			if (this.m_currExcStack == null)
			{
				this.m_currExcStack = new __ExceptionInfo[2];
			}
			if (this.m_exceptionCount >= this.m_exceptions.Length)
			{
				this.m_exceptions = ILGenerator.EnlargeArray(this.m_exceptions);
			}
			if (this.m_currExcStackCount >= this.m_currExcStack.Length)
			{
				this.m_currExcStack = ILGenerator.EnlargeArray(this.m_currExcStack);
			}
			Label label = this.DefineLabel();
			__ExceptionInfo _ExceptionInfo = new __ExceptionInfo(this.m_length, label);
			__ExceptionInfo[] exceptions = this.m_exceptions;
			int num = this.m_exceptionCount;
			this.m_exceptionCount = num + 1;
			exceptions[num] = _ExceptionInfo;
			__ExceptionInfo[] currExcStack = this.m_currExcStack;
			num = this.m_currExcStackCount;
			this.m_currExcStackCount = num + 1;
			currExcStack[num] = _ExceptionInfo;
			return label;
		}

		/// <summary>Ends an exception block.</summary>
		/// <exception cref="T:System.InvalidOperationException">The end exception block occurs in an unexpected place in the code stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.</exception>
		// Token: 0x06004AE0 RID: 19168 RVA: 0x00110410 File Offset: 0x0010E610
		public virtual void EndExceptionBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			this.m_currExcStack[this.m_currExcStackCount - 1] = null;
			this.m_currExcStackCount--;
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int currentState = _ExceptionInfo.GetCurrentState();
			if (currentState == 1 || currentState == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_BadExceptionCodeGen"));
			}
			if (currentState == 2)
			{
				this.Emit(OpCodes.Leave, endLabel);
			}
			else if (currentState == 3 || currentState == 4)
			{
				this.Emit(OpCodes.Endfinally);
			}
			if (this.m_labelList[endLabel.GetLabelValue()] == -1)
			{
				this.MarkLabel(endLabel);
			}
			else
			{
				this.MarkLabel(_ExceptionInfo.GetFinallyEndLabel());
			}
			_ExceptionInfo.Done(this.m_length);
		}

		/// <summary>Begins an exception block for a filtered exception.</summary>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.  
		///  -or-  
		///  This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AE1 RID: 19169 RVA: 0x001104E0 File Offset: 0x0010E6E0
		public virtual void BeginExceptFilterBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFilterAddr(this.m_length);
		}

		/// <summary>Begins a catch block.</summary>
		/// <param name="exceptionType">The <see cref="T:System.Type" /> object that represents the exception.</param>
		/// <exception cref="T:System.ArgumentException">The catch block is within a filtered exception.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exceptionType" /> is <see langword="null" />, and the exception filter block has not returned a value that indicates that finally blocks should be run until this catch block is located.</exception>
		/// <exception cref="T:System.NotSupportedException">The Microsoft intermediate language (MSIL) being generated is not currently in an exception block.</exception>
		// Token: 0x06004AE2 RID: 19170 RVA: 0x00110534 File Offset: 0x0010E734
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
				}
				this.Emit(OpCodes.Endfilter);
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
			}
			_ExceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
		}

		/// <summary>Begins an exception fault block in the Microsoft intermediate language (MSIL) stream.</summary>
		/// <exception cref="T:System.NotSupportedException">The MSIL being generated is not currently in an exception block.  
		///  -or-  
		///  This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AE3 RID: 19171 RVA: 0x001105CC File Offset: 0x0010E7CC
		public virtual void BeginFaultBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFaultAddr(this.m_length);
		}

		/// <summary>Begins a finally block in the Microsoft intermediate language (MSIL) instruction stream.</summary>
		/// <exception cref="T:System.NotSupportedException">The MSIL being generated is not currently in an exception block.</exception>
		// Token: 0x06004AE4 RID: 19172 RVA: 0x00110620 File Offset: 0x0010E820
		public virtual void BeginFinallyBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			int currentState = _ExceptionInfo.GetCurrentState();
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int num = 0;
			if (currentState != 0)
			{
				this.Emit(OpCodes.Leave, endLabel);
				num = this.m_length;
			}
			this.MarkLabel(endLabel);
			Label label = this.DefineLabel();
			_ExceptionInfo.SetFinallyEndLabel(label);
			this.Emit(OpCodes.Leave, label);
			if (num == 0)
			{
				num = this.m_length;
			}
			_ExceptionInfo.MarkFinallyAddr(this.m_length, num);
		}

		/// <summary>Declares a new label.</summary>
		/// <returns>A new label that can be used as a token for branching.</returns>
		// Token: 0x06004AE5 RID: 19173 RVA: 0x001106B8 File Offset: 0x0010E8B8
		public virtual Label DefineLabel()
		{
			if (this.m_labelList == null)
			{
				this.m_labelList = new int[4];
			}
			if (this.m_labelCount >= this.m_labelList.Length)
			{
				this.m_labelList = ILGenerator.EnlargeArray(this.m_labelList);
			}
			this.m_labelList[this.m_labelCount] = -1;
			int labelCount = this.m_labelCount;
			this.m_labelCount = labelCount + 1;
			return new Label(labelCount);
		}

		/// <summary>Marks the Microsoft intermediate language (MSIL) stream's current position with the given label.</summary>
		/// <param name="loc">The label for which to set an index.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="loc" /> represents an invalid index into the label array.  
		/// -or-  
		/// An index for <paramref name="loc" /> has already been defined.</exception>
		// Token: 0x06004AE6 RID: 19174 RVA: 0x00110720 File Offset: 0x0010E920
		public virtual void MarkLabel(Label loc)
		{
			int labelValue = loc.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelList.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLabel"));
			}
			if (this.m_labelList[labelValue] != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_RedefinedLabel"));
			}
			this.m_labelList[labelValue] = this.m_length;
		}

		/// <summary>Emits an instruction to throw an exception.</summary>
		/// <param name="excType">The class of the type of exception to throw.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="excType" /> is not the <see cref="T:System.Exception" /> class or a derived class of <see cref="T:System.Exception" />.  
		/// -or-  
		/// The type does not have a default constructor.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="excType" /> is <see langword="null" />.</exception>
		// Token: 0x06004AE7 RID: 19175 RVA: 0x00110780 File Offset: 0x0010E980
		public virtual void ThrowException(Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!excType.IsSubclassOf(typeof(Exception)) && excType != typeof(Exception))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotExceptionType"));
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MissingDefaultConstructor"));
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) to call <see cref="Overload:System.Console.WriteLine" /> with a string.</summary>
		/// <param name="value">The string to be printed.</param>
		// Token: 0x06004AE8 RID: 19176 RVA: 0x00110814 File Offset: 0x0010EA14
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			Type[] array = new Type[] { typeof(string) };
			MethodInfo method = typeof(Console).GetMethod("WriteLine", array);
			this.Emit(OpCodes.Call, method);
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) necessary to call <see cref="Overload:System.Console.WriteLine" /> with the given local variable.</summary>
		/// <param name="localBuilder">The local variable whose value is to be written to the console.</param>
		/// <exception cref="T:System.ArgumentException">The type of <paramref name="localBuilder" /> is <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.EnumBuilder" />, which are not supported.  
		///  -or-  
		///  There is no overload of <see cref="Overload:System.Console.WriteLine" /> that accepts the type of <paramref name="localBuilder" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x06004AE9 RID: 19177 RVA: 0x00110864 File Offset: 0x0010EA64
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (this.m_methodBuilder == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			this.Emit(OpCodes.Ldloc, localBuilder);
			Type[] array = new Type[1];
			object localType = localBuilder.LocalType;
			if (localType is TypeBuilder || localType is EnumBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)localType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "localBuilder");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		/// <summary>Emits the Microsoft intermediate language (MSIL) necessary to call <see cref="Overload:System.Console.WriteLine" /> with the given field.</summary>
		/// <param name="fld">The field whose value is to be written to the console.</param>
		/// <exception cref="T:System.ArgumentException">There is no overload of the <see cref="Overload:System.Console.WriteLine" /> method that accepts the type of the specified field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fld" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The type of the field is <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.EnumBuilder" />, which are not supported.</exception>
		// Token: 0x06004AEA RID: 19178 RVA: 0x00110934 File Offset: 0x0010EB34
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg, 0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			Type[] array = new Type[1];
			object fieldType = fld.FieldType;
			if (fieldType is TypeBuilder || fieldType is EnumBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)fieldType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "fld");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		/// <summary>Declares a local variable of the specified type.</summary>
		/// <param name="localType">A <see cref="T:System.Type" /> object that represents the type of the local variable.</param>
		/// <returns>The declared local variable.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created by the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.</exception>
		// Token: 0x06004AEB RID: 19179 RVA: 0x00110A1E File Offset: 0x0010EC1E
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		/// <summary>Declares a local variable of the specified type, optionally pinning the object referred to by the variable.</summary>
		/// <param name="localType">A <see cref="T:System.Type" /> object that represents the type of the local variable.</param>
		/// <param name="pinned">
		///   <see langword="true" /> to pin the object in memory; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.LocalBuilder" /> object that represents the local variable.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created by the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.  
		///  -or-  
		///  The method body of the enclosing method has been created by the <see cref="M:System.Reflection.Emit.MethodBuilder.CreateMethodBody(System.Byte[],System.Int32)" /> method.</exception>
		/// <exception cref="T:System.NotSupportedException">The method with which this <see cref="T:System.Reflection.Emit.ILGenerator" /> is associated is not represented by a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x06004AEC RID: 19180 RVA: 0x00110A28 File Offset: 0x0010EC28
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (methodBuilder.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_localSignature.AddArgument(localType, pinned);
			LocalBuilder localBuilder = new LocalBuilder(this.m_localCount, localType, methodBuilder, pinned);
			this.m_localCount++;
			return localBuilder;
		}

		/// <summary>Specifies the namespace to be used in evaluating locals and watches for the current active lexical scope.</summary>
		/// <param name="usingNamespace">The namespace to be used in evaluating locals and watches for the current active lexical scope</param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="usingNamespace" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usingNamespace" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AED RID: 19181 RVA: 0x00110AC0 File Offset: 0x0010ECC0
		public virtual void UsingNamespace(string usingNamespace)
		{
			if (usingNamespace == null)
			{
				throw new ArgumentNullException("usingNamespace");
			}
			if (usingNamespace.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "usingNamespace");
			}
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
				return;
			}
			this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
		}

		/// <summary>Marks a sequence point in the Microsoft intermediate language (MSIL) stream.</summary>
		/// <param name="document">The document for which the sequence point is being defined.</param>
		/// <param name="startLine">The line where the sequence point begins.</param>
		/// <param name="startColumn">The column in the line where the sequence point begins.</param>
		/// <param name="endLine">The line where the sequence point ends.</param>
		/// <param name="endColumn">The column in the line where the sequence point ends.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startLine" /> or <paramref name="endLine" /> is &lt;= 0.</exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AEE RID: 19182 RVA: 0x00110B41 File Offset: 0x0010ED41
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine == 0 || startLine < 0 || endLine == 0 || endLine < 0)
			{
				throw new ArgumentOutOfRangeException("startLine");
			}
			this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
		}

		/// <summary>Begins a lexical scope.</summary>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AEF RID: 19183 RVA: 0x00110B76 File Offset: 0x0010ED76
		public virtual void BeginScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
		}

		/// <summary>Ends a lexical scope.</summary>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Reflection.Emit.ILGenerator" /> belongs to a <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		// Token: 0x06004AF0 RID: 19184 RVA: 0x00110B8A File Offset: 0x0010ED8A
		public virtual void EndScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
		}

		/// <summary>Gets the current offset, in bytes, in the Microsoft intermediate language (MSIL) stream that is being emitted by the <see cref="T:System.Reflection.Emit.ILGenerator" />.</summary>
		/// <returns>The offset in the MSIL stream at which the next instruction will be emitted.</returns>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x00110B9E File Offset: 0x0010ED9E
		public virtual int ILOffset
		{
			get
			{
				return this.m_length;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004AF2 RID: 19186 RVA: 0x00110BA6 File Offset: 0x0010EDA6
		void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004AF3 RID: 19187 RVA: 0x00110BAD File Offset: 0x0010EDAD
		void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004AF4 RID: 19188 RVA: 0x00110BB4 File Offset: 0x0010EDB4
		void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004AF5 RID: 19189 RVA: 0x00110BBB File Offset: 0x0010EDBB
		void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001EC6 RID: 7878
		private const int defaultSize = 16;

		// Token: 0x04001EC7 RID: 7879
		private const int DefaultFixupArraySize = 8;

		// Token: 0x04001EC8 RID: 7880
		private const int DefaultLabelArraySize = 4;

		// Token: 0x04001EC9 RID: 7881
		private const int DefaultExceptionArraySize = 2;

		// Token: 0x04001ECA RID: 7882
		private int m_length;

		// Token: 0x04001ECB RID: 7883
		private byte[] m_ILStream;

		// Token: 0x04001ECC RID: 7884
		private int[] m_labelList;

		// Token: 0x04001ECD RID: 7885
		private int m_labelCount;

		// Token: 0x04001ECE RID: 7886
		private __FixupData[] m_fixupData;

		// Token: 0x04001ECF RID: 7887
		private int m_fixupCount;

		// Token: 0x04001ED0 RID: 7888
		private int[] m_RelocFixupList;

		// Token: 0x04001ED1 RID: 7889
		private int m_RelocFixupCount;

		// Token: 0x04001ED2 RID: 7890
		private int m_exceptionCount;

		// Token: 0x04001ED3 RID: 7891
		private int m_currExcStackCount;

		// Token: 0x04001ED4 RID: 7892
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001ED5 RID: 7893
		private __ExceptionInfo[] m_currExcStack;

		// Token: 0x04001ED6 RID: 7894
		internal ScopeTree m_ScopeTree;

		// Token: 0x04001ED7 RID: 7895
		internal LineNumberInfo m_LineNumberInfo;

		// Token: 0x04001ED8 RID: 7896
		internal MethodInfo m_methodBuilder;

		// Token: 0x04001ED9 RID: 7897
		internal int m_localCount;

		// Token: 0x04001EDA RID: 7898
		internal SignatureHelper m_localSignature;

		// Token: 0x04001EDB RID: 7899
		private int m_maxStackSize;

		// Token: 0x04001EDC RID: 7900
		private int m_maxMidStack;

		// Token: 0x04001EDD RID: 7901
		private int m_maxMidStackCur;
	}
}
