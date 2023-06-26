using System;
using System.Runtime.CompilerServices;

namespace std
{
	// Token: 0x020001AE RID: 430
	[NativeCppClass]
	[UnsafeValueType]
	internal struct basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000028B0 File Offset: 0x00001CB0
		public unsafe static void <MarshalCopy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_1)
		{
			if (A_0 != null)
			{
				*(int*)A_0 = 0;
				try
				{
					*(int*)(A_0 + 16 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>)) = 0;
					*(int*)(A_0 + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>)) = 0;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), (void*)A_0);
					throw;
				}
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, A_1);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)A_0);
					throw;
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00002934 File Offset: 0x00001D34
		public unsafe static void <MarshalDestroy>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
		{
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)A_0);
				throw;
			}
		}
	}
}
