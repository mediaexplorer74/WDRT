using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200013A RID: 314
	internal class Signature
	{
		// Token: 0x060012D2 RID: 4818
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void GetSignature(void* pCorSig, int cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType);

		// Token: 0x060012D3 RID: 4819 RVA: 0x00037F04 File Offset: 0x00036104
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo method, RuntimeType[] arguments, RuntimeType returnType, CallingConventions callingConvention)
		{
			this.m_pMethod = method.Value;
			this.m_arguments = arguments;
			this.m_returnTypeORfieldType = returnType;
			this.m_managedCallingConventionAndArgIteratorFlags = (int)((byte)callingConvention);
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), method, null);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00037F50 File Offset: 0x00036150
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), methodHandle, declaringType);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00037F77 File Offset: 0x00036177
		[SecurityCritical]
		public Signature(IRuntimeFieldInfo fieldHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, fieldHandle.Value, null, declaringType);
			GC.KeepAlive(fieldHandle);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00037F98 File Offset: 0x00036198
		[SecurityCritical]
		public unsafe Signature(void* pCorSig, int cCorSig, RuntimeType declaringType)
		{
			this.GetSignature(pCorSig, cCorSig, default(RuntimeFieldHandleInternal), null, declaringType);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00037FBE File Offset: 0x000361BE
		internal CallingConventions CallingConvention
		{
			get
			{
				return (CallingConventions)((byte)this.m_managedCallingConventionAndArgIteratorFlags);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00037FC7 File Offset: 0x000361C7
		internal RuntimeType[] Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00037FCF File Offset: 0x000361CF
		internal RuntimeType ReturnType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00037FD7 File Offset: 0x000361D7
		internal RuntimeType FieldType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x060012DB RID: 4827
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareSig(Signature sig1, Signature sig2);

		// Token: 0x060012DC RID: 4828
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] GetCustomModifiers(int position, bool required);

		// Token: 0x04000677 RID: 1655
		internal RuntimeType[] m_arguments;

		// Token: 0x04000678 RID: 1656
		internal RuntimeType m_declaringType;

		// Token: 0x04000679 RID: 1657
		internal RuntimeType m_returnTypeORfieldType;

		// Token: 0x0400067A RID: 1658
		internal object m_keepalive;

		// Token: 0x0400067B RID: 1659
		[SecurityCritical]
		internal unsafe void* m_sig;

		// Token: 0x0400067C RID: 1660
		internal int m_managedCallingConventionAndArgIteratorFlags;

		// Token: 0x0400067D RID: 1661
		internal int m_nSizeOfArgStack;

		// Token: 0x0400067E RID: 1662
		internal int m_csig;

		// Token: 0x0400067F RID: 1663
		internal RuntimeMethodHandleInternal m_pMethod;

		// Token: 0x02000AF9 RID: 2809
		internal enum MdSigCallingConvention : byte
		{
			// Token: 0x040031F4 RID: 12788
			Generics = 16,
			// Token: 0x040031F5 RID: 12789
			HasThis = 32,
			// Token: 0x040031F6 RID: 12790
			ExplicitThis = 64,
			// Token: 0x040031F7 RID: 12791
			CallConvMask = 15,
			// Token: 0x040031F8 RID: 12792
			Default = 0,
			// Token: 0x040031F9 RID: 12793
			C,
			// Token: 0x040031FA RID: 12794
			StdCall,
			// Token: 0x040031FB RID: 12795
			ThisCall,
			// Token: 0x040031FC RID: 12796
			FastCall,
			// Token: 0x040031FD RID: 12797
			Vararg,
			// Token: 0x040031FE RID: 12798
			Field,
			// Token: 0x040031FF RID: 12799
			LocalSig,
			// Token: 0x04003200 RID: 12800
			Property,
			// Token: 0x04003201 RID: 12801
			Unmgd,
			// Token: 0x04003202 RID: 12802
			GenericInst,
			// Token: 0x04003203 RID: 12803
			Max
		}
	}
}
