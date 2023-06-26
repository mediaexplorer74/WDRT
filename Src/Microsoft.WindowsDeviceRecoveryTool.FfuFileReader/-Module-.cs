using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using ?A0x3b496f95;
using ?A0x3d80193d;
using msclr.interop.details;
using std;
using std.?append@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQAEAAV12@ID@Z.__l2;
using std.?append@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQAEAAV12@QBDI@Z.__l2;
using std.?assign@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQAEAAV12@QBDI@Z.__l2;
using std.?push_back@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$FQAEXD@Z.__l2;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00001170 File Offset: 0x00000570
	internal unsafe static void* @new(uint _Size, void* _Where)
	{
		return _Where;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00001190 File Offset: 0x00000590
	internal static int std.numeric_limits<int>.max()
	{
		return int.MaxValue;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000011A4 File Offset: 0x000005A4
	internal unsafe static exception* std.exception.{ctor}(exception* A_0, sbyte* _Message, int __unnamed001)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		*ptr = _Message;
		return A_0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000011C8 File Offset: 0x000005C8
	internal unsafe static exception* std.exception.{ctor}(exception* A_0, exception* _Other)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		exception* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(_Other + 4, ptr);
		return A_0;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000011F8 File Offset: 0x000005F8
	internal unsafe static void std.exception.{dtor}(exception* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002EF0 File Offset: 0x000022F0
	internal unsafe static sbyte* std.exception.what(exception* A_0)
	{
		uint num = (uint)(*(A_0 + 4));
		return num != 0U ? num : ref <Module>.??_C@_0BC@EOODALEL@Unknown?5exception@;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002E90 File Offset: 0x00002290
	internal unsafe static void* std.exception.__vecDelDtor(exception* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			exception* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.exception.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				exception* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00001214 File Offset: 0x00000614
	internal unsafe static bad_alloc* std.bad_alloc.{ctor}(bad_alloc* A_0, sbyte* _Message)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_alloc* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		*ptr = _Message;
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002F20 File Offset: 0x00002320
	internal unsafe static void* std.bad_alloc.__vecDelDtor(bad_alloc* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_alloc* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.bad_alloc.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_alloc* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00001268 File Offset: 0x00000668
	internal unsafe static void std.bad_alloc.{dtor}(bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00001284 File Offset: 0x00000684
	internal unsafe static bad_array_new_length* std.bad_array_new_length.{ctor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_array_new_length* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		*ptr = ref <Module>.??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7bad_array_new_length@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.bad_alloc.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002F8C File Offset: 0x0000238C
	internal unsafe static void* std.bad_array_new_length.__vecDelDtor(bad_array_new_length* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_array_new_length* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.bad_array_new_length.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_array_new_length* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002FE4 File Offset: 0x000023E4
	internal unsafe static void std.bad_array_new_length.{dtor}(bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000130C File Offset: 0x0000070C
	internal unsafe static void std.exception_ptr.{dtor}(exception_ptr* A_0)
	{
		<Module>.__ExceptionPtrDestroy(A_0);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00001320 File Offset: 0x00000720
	internal unsafe static exception_ptr* std.exception_ptr.{ctor}(exception_ptr* A_0, exception_ptr* _Rhs)
	{
		<Module>.__ExceptionPtrCopy(A_0, _Rhs);
		return A_0;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000133C File Offset: 0x0000073C
	internal unsafe static void std._Throw_bad_array_new_length()
	{
		bad_array_new_length bad_array_new_length;
		<Module>.std.bad_array_new_length.{ctor}(ref bad_array_new_length);
		<Module>._CxxThrowException((void*)(&bad_array_new_length), (_s__ThrowInfo*)(&<Module>._TI3?AVbad_array_new_length@std@@));
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000013C4 File Offset: 0x000007C4
	internal unsafe static bad_array_new_length* std.bad_array_new_length.{ctor}(bad_array_new_length* A_0, bad_array_new_length* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_array_new_length* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7bad_array_new_length@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.bad_alloc.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00001360 File Offset: 0x00000760
	internal unsafe static bad_alloc* std.bad_alloc.{ctor}(bad_alloc* A_0, bad_alloc* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_alloc* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_alloc@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00001490 File Offset: 0x00000890
	internal unsafe static void* std._Default_allocate_traits._Allocate(uint _Bytes)
	{
		return <Module>.@new(_Bytes);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000014A4 File Offset: 0x000008A4
	internal unsafe static void std._Adjust_manually_vector_aligned(void** _Ptr, uint* _Bytes)
	{
		*_Bytes += 35;
		int num = *_Ptr;
		uint num2 = *(num - 4);
		if (num - num2 - 4 <= 31)
		{
			*_Ptr = num2;
		}
		else
		{
			<Module>._invalid_parameter_noinfo_noreturn();
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000014D4 File Offset: 0x000008D4
	internal unsafe static void std._Container_base0._Orphan_all(_Container_base0* A_0)
	{
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000014E4 File Offset: 0x000008E4
	internal unsafe static void std._Container_base0._Swap_proxy_and_iterators(_Container_base0* A_0, _Container_base0* A_0)
	{
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000014F4 File Offset: 0x000008F4
	internal unsafe static void std._Container_base0._Alloc_proxy(_Container_base0* A_0, _Fake_allocator* A_0)
	{
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00001EE4 File Offset: 0x000012E4
	internal unsafe static _Iterator_base12* std._Iterator_base12.{ctor}(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*(A_0 + 4) = 0;
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001504 File Offset: 0x00000904
	internal unsafe static _Iterator_base12* std._Iterator_base12.=(_Iterator_base12* A_0, _Iterator_base12* _Right)
	{
		*A_0 = *_Right;
		return A_0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00001518 File Offset: 0x00000918
	internal unsafe static _Fake_proxy_ptr_impl* std._Fake_proxy_ptr_impl.{ctor}(_Fake_proxy_ptr_impl* A_0, _Fake_allocator* A_0, _Container_base0* A_1)
	{
		return A_0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00001528 File Offset: 0x00000928
	internal unsafe static void std._Fake_proxy_ptr_impl._Release(_Fake_proxy_ptr_impl* A_0)
	{
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00001538 File Offset: 0x00000938
	internal unsafe static void std._Xlen_string()
	{
		<Module>.std._Xlength_error((sbyte*)(&<Module>.??_C@_0BA@JFNIOLAK@string?5too?5long@));
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00001550 File Offset: 0x00000950
	internal static ref char PtrToStringChars(string s)
	{
		ref byte ptr = s;
		if ((ref ptr) != null)
		{
			ptr = RuntimeHelpers.OffsetToStringData + (ref ptr);
		}
		return ref ptr;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003054 File Offset: 0x00002454
	internal unsafe static void* std.bad_cast.__vecDelDtor(bad_cast* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_cast* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.bad_cast.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_cast* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000030AC File Offset: 0x000024AC
	internal unsafe static void std.bad_cast.{dtor}(bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x0000156C File Offset: 0x0000096C
	internal unsafe static bad_cast* std.bad_cast.{ctor}(bad_cast* A_0, bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_cast* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_cast@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000314C File Offset: 0x0000254C
	internal unsafe static sbyte* std.bad_weak_ptr.what(bad_weak_ptr* A_0)
	{
		return ref <Module>.??_C@_0N@FJHHFFAF@bad_weak_ptr@;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000030D0 File Offset: 0x000024D0
	internal unsafe static void* std.bad_weak_ptr.__vecDelDtor(bad_weak_ptr* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			bad_weak_ptr* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.bad_weak_ptr.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				bad_weak_ptr* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00003128 File Offset: 0x00002528
	internal unsafe static void std.bad_weak_ptr.{dtor}(bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000015D0 File Offset: 0x000009D0
	internal unsafe static bad_weak_ptr* std.bad_weak_ptr.{ctor}(bad_weak_ptr* A_0, bad_weak_ptr* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_weak_ptr* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7bad_weak_ptr@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00001634 File Offset: 0x00000A34
	internal static uint msclr.interop.details.GetAnsiStringSize(string _str)
	{
		ref byte ptr = _str;
		if ((ref ptr) != null)
		{
			ptr = RuntimeHelpers.OffsetToStringData + (ref ptr);
		}
		ref char char_u0020modopt(IsConst)& = ref ptr;
		uint num = <Module>.WideCharToMultiByte(3U, 1024, ref char_u0020modopt(IsConst)&, _str.Length, null, 0, null, null);
		if (num == 0U && _str.Length != 0)
		{
			throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
		}
		return num + 1U;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00001680 File Offset: 0x00000A80
	internal unsafe static void msclr.interop.details.WriteAnsiString(sbyte* _buf, uint _size, string _str)
	{
		ref byte ptr = _str;
		if ((ref ptr) != null)
		{
			ptr = RuntimeHelpers.OffsetToStringData + (ref ptr);
		}
		ref char char_u0020modopt(IsConst)& = ref ptr;
		if (_size > 2147483647U)
		{
			throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		}
		uint num = <Module>.WideCharToMultiByte(3U, 1024, ref char_u0020modopt(IsConst)&, _str.Length, _buf, (int)_size, null, null);
		if (num < _size && (num != null || _size == 1U))
		{
			*(byte*)(num / sizeof(sbyte) + _buf) = 0;
			return;
		}
		throw new ArgumentException("Conversion from WideChar to MultiByte failed.  Please check the content of the string and/or locale settings.");
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000016E4 File Offset: 0x00000AE4
	internal unsafe static uint msclr.interop.details.GetUnicodeStringSize(sbyte* _str, uint _count)
	{
		if (_count > 2147483647U)
		{
			throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		}
		uint num = <Module>.MultiByteToWideChar(3U, 0, _str, (int)_count, null, 0);
		if (num == 0U && _count != 0U)
		{
			throw new ArgumentException("Conversion from MultiByte to WideChar failed.  Please check the content of the string and/or locale settings.");
		}
		return num + 1U;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00001724 File Offset: 0x00000B24
	internal unsafe static void msclr.interop.details.WriteUnicodeString(char* _dest, uint _size, sbyte* _src, uint _count)
	{
		if (_size > 2147483647U || _count > 2147483647U)
		{
			throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		}
		uint num = <Module>.MultiByteToWideChar(3U, 0, _src, (int)_count, _dest, (int)_size);
		if (num < _size && (num != 0U || _size == 1U))
		{
			num[_dest] = '\0';
			return;
		}
		throw new ArgumentException("Conversion from MultiByte to WideChar failed.  Please check the content of the string and/or locale settings.");
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00001F04 File Offset: 0x00001304
	internal unsafe static string msclr.interop.details.InternalAnsiToStringHelper(sbyte* _src, uint _count)
	{
		uint num = <Module>.msclr.interop.details.GetUnicodeStringSize(_src, _count);
		if (num - 1 <= 2147483646)
		{
			char* ptr = <Module>.new[](num << 1);
			char_buffer<wchar_t> char_buffer<wchar_t> = ptr;
			string text;
			try
			{
				if (ptr == null)
				{
					throw new InsufficientMemoryException();
				}
				<Module>.msclr.interop.details.WriteUnicodeString(ptr, num, _src, _count);
				text = new string(ptr, 0, num - 1);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<wchar_t>.{dtor}), (void*)(&char_buffer<wchar_t>));
				throw;
			}
			<Module>.delete[]((void*)ptr);
			return text;
		}
		throw new ArgumentOutOfRangeException("Size of string exceeds INT_MAX.");
		try
		{
		}
		catch
		{
			char_buffer<wchar_t> char_buffer<wchar_t>;
			<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<wchar_t>.{dtor}), (void*)(&char_buffer<wchar_t>));
			throw;
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002648 File Offset: 0x00001A48
	internal unsafe static string msclr.interop.marshal_as<class\u0020System::String\u0020^,class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _from_obj)
	{
		uint num = (uint)(*(_from_obj + 16));
		sbyte* ptr = _from_obj;
		if (((16 <= *(_from_obj + 20)) ? 1 : 0) != 0)
		{
			ptr = *_from_obj;
		}
		return <Module>.msclr.interop.details.InternalAnsiToStringHelper(ptr, num);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002A84 File Offset: 0x00001E84
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* msclr.interop.marshal_as<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>,class\u0020System::String\u0020^>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, string* _from_obj)
	{
		try
		{
			uint num = 0U;
			if (*_from_obj == null)
			{
				throw new ArgumentNullException("NULLPTR is not supported for this conversion.");
			}
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0);
			num = 1U;
			uint num2 = <Module>.msclr.interop.details.GetAnsiStringSize(*_from_obj);
			if (num2 > 1U)
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.resize(A_0, num2 - 1U, 0);
				sbyte* ptr = (sbyte*)A_0;
				if (((16 <= *(int*)(A_0 + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>))) ? 1 : 0) != 0)
				{
					ptr = *(int*)A_0;
				}
				<Module>.msclr.interop.details.WriteAnsiString(ptr, num2, *_from_obj);
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003184 File Offset: 0x00002584
	internal unsafe static void* std.runtime_error.__vecDelDtor(runtime_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			runtime_error* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.runtime_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				runtime_error* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000018A8 File Offset: 0x00000CA8
	internal unsafe static void std.runtime_error.{dtor}(runtime_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000031E4 File Offset: 0x000025E4
	internal unsafe static void* std.range_error.__vecDelDtor(range_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			range_error* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 12U, (uint)(*ptr), ldftn(std.range_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				range_error* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 12 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 12U);
		}
		return A_0;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000323C File Offset: 0x0000263C
	internal unsafe static void std.range_error.{dtor}(range_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00001928 File Offset: 0x00000D28
	internal unsafe static range_error* std.range_error.{ctor}(range_error* A_0, range_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		range_error* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7runtime_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7range_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000018C4 File Offset: 0x00000CC4
	internal unsafe static runtime_error* std.runtime_error.{ctor}(runtime_error* A_0, runtime_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		runtime_error* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7runtime_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000326C File Offset: 0x0000266C
	internal unsafe static void* std._System_error.__vecDelDtor(_System_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			_System_error* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 20U, (uint)(*ptr), ldftn(std._System_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				_System_error* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 20 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 20U);
		}
		return A_0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000019B8 File Offset: 0x00000DB8
	internal unsafe static void std._System_error.{dtor}(_System_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000032CC File Offset: 0x000026CC
	internal unsafe static void* std.system_error.__vecDelDtor(system_error* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			system_error* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 20U, (uint)(*ptr), ldftn(std.system_error.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				system_error* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 20 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 20U);
		}
		return A_0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000019D4 File Offset: 0x00000DD4
	internal unsafe static void std.system_error.{dtor}(system_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00001A8C File Offset: 0x00000E8C
	internal unsafe static system_error* std.system_error.{ctor}(system_error* A_0, system_error* A_0)
	{
		<Module>.std._System_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7system_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._System_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000019F0 File Offset: 0x00000DF0
	internal unsafe static _System_error* std._System_error.{ctor}(_System_error* A_0, _System_error* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		_System_error* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		<Module>.__std_exception_copy(A_0 + 4, ptr);
		try
		{
			*A_0 = ref <Module>.??_7runtime_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7_System_error@std@@6B@;
			cpblk(A_0 + 12, A_0 + 12, 8);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.runtime_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00001ADC File Offset: 0x00000EDC
	internal unsafe static locale* std.locale.{ctor}(locale* A_0, locale* _Right)
	{
		int num = *(_Right + 4);
		*(A_0 + 4) = num;
		int num2 = num;
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 4)));
		return A_0;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000332C File Offset: 0x0000272C
	internal unsafe static void* std.ios_base.failure.__vecDelDtor(ios_base.failure* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			ios_base.failure* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 20U, (uint)(*ptr), ldftn(std.ios_base.failure.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				ios_base.failure* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 20 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 20U);
		}
		return A_0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003384 File Offset: 0x00002784
	internal unsafe static void std.ios_base.failure.{dtor}(ios_base.failure* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 + 4);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00001B08 File Offset: 0x00000F08
	internal unsafe static ios_base.failure* std.ios_base.failure.{ctor}(ios_base.failure* A_0, ios_base.failure* A_0)
	{
		<Module>.std._System_error.{ctor}(A_0, A_0);
		try
		{
			*A_0 = ref <Module>.??_7system_error@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._System_error.{dtor}), A_0);
			throw;
		}
		try
		{
			*A_0 = ref <Module>.??_7failure@ios_base@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.system_error.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000297C File Offset: 0x00001D7C
	internal unsafe static void FfuReaderResult.{dtor}(FfuReaderResult* A_0)
	{
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 4;
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
			throw;
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00001BC4 File Offset: 0x00000FC4
	internal unsafe static sbyte* msclr.interop.details.char_buffer<char>.release(char_buffer<char>* A_0)
	{
		sbyte* ptr = *A_0;
		*A_0 = 0;
		return ptr;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00001BD8 File Offset: 0x00000FD8
	internal unsafe static sbyte* msclr.interop.details.char_buffer<char>.get(char_buffer<char>* A_0)
	{
		return *A_0;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00001BE8 File Offset: 0x00000FE8
	internal unsafe static void msclr.interop.details.char_buffer<char>.{dtor}(char_buffer<char>* A_0)
	{
		<Module>.delete[](*A_0);
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00001BFC File Offset: 0x00000FFC
	internal unsafe static char_buffer<char>* msclr.interop.details.char_buffer<char>.{ctor}(char_buffer<char>* A_0, uint _size)
	{
		*A_0 = <Module>.new[](_size);
		return A_0;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00001C14 File Offset: 0x00001014
	internal unsafe static char* msclr.interop.details.char_buffer<wchar_t>.get(char_buffer<wchar_t>* A_0)
	{
		return *A_0;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00001C24 File Offset: 0x00001024
	internal unsafe static void msclr.interop.details.char_buffer<wchar_t>.{dtor}(char_buffer<wchar_t>* A_0)
	{
		<Module>.delete[](*A_0);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00001C38 File Offset: 0x00001038
	internal unsafe static char_buffer<wchar_t>* msclr.interop.details.char_buffer<wchar_t>.{ctor}(char_buffer<wchar_t>* A_0, uint _size)
	{
		uint num;
		if (_size <= 2147483647U)
		{
			num = _size << 1;
		}
		else
		{
			num = uint.MaxValue;
		}
		*A_0 = <Module>.new[](num);
		return A_0;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000029C0 File Offset: 0x00001DC0
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.resize(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Newsize, sbyte _Ch)
	{
		uint num = *(A_0 + 16);
		if (_Newsize <= num)
		{
			*(A_0 + 16) = _Newsize;
			sbyte* ptr = A_0;
			if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
			{
				ptr = *A_0;
			}
			*(byte*)(ptr + _Newsize / sizeof(sbyte)) = 0;
		}
		else
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(A_0, _Newsize - num, _Ch);
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00001C60 File Offset: 0x00001060
	internal unsafe static uint std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.length(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return *(A_0 + 16);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000023B0 File Offset: 0x000017B0
	internal unsafe static sbyte* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.c_str(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000023D4 File Offset: 0x000017D4
	internal unsafe static sbyte* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.[](basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Off)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr + _Off / sizeof(sbyte);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002678 File Offset: 0x00001A78
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000026B8 File Offset: 0x00001AB8
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, _Right);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002730 File Offset: 0x00001B30
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		*A_0 = 0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2;
		try
		{
			ptr = A_0 + 16;
			*ptr = 0;
			ptr2 = A_0 + 20;
			*ptr2 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			*ptr = 0;
			*ptr2 = 15;
			*A_0 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002A04 File Offset: 0x00001E04
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(A_0, _Right);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000023FC File Offset: 0x000017FC
	internal unsafe static void std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002180 File Offset: 0x00001580
	internal unsafe static void std._String_val<std::_Simple_types<char>\u0020>.{dtor}(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002190 File Offset: 0x00001590
	internal unsafe static allocator<char>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Getal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000021A0 File Offset: 0x000015A0
	internal unsafe static allocator<char>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Getal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000240C File Offset: 0x0000180C
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		uint num = (uint)(*(A_0 + 20));
		if (((16U <= num) ? 1 : 0) != 0)
		{
			uint num2 = num + 1U;
			void* ptr = *A_0;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num2);
			}
			<Module>.delete(ptr, num2);
		}
		*(A_0 + 16) = 0;
		*(A_0 + 20) = 15;
		*A_0 = 0;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000021B0 File Offset: 0x000015B0
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_init(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		*(A_0 + 16) = 0;
		*(A_0 + 20) = 15;
		*A_0 = 0;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000245C File Offset: 0x0000185C
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Eos(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Newsize)
	{
		*(A_0 + 16) = _Newsize;
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		*(byte*)(ptr + _Newsize / sizeof(sbyte)) = 0;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00001C74 File Offset: 0x00001074
	internal unsafe static uint std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.size(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return *(A_0 + 16);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000027B0 File Offset: 0x00001BB0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Count, sbyte _Ch)
	{
		uint num = *(A_0 + 16);
		uint num2 = (uint)(*(A_0 + 20));
		if (_Count <= num2 - num)
		{
			*(A_0 + 16) = num + _Count;
			sbyte* ptr = A_0;
			if (((16U <= num2) ? 1 : 0) != 0)
			{
				ptr = *A_0;
			}
			int num3 = num / sizeof(sbyte) + ptr;
			initblk(num3, _Ch, _Count);
			*(num3 + _Count) = 0;
			return A_0;
		}
		<lambda_b520e6e7dd2c85f4b83ca9ec1210796f> <lambda_b520e6e7dd2c85f4b83ca9ec1210796f>;
		initblk(ref <lambda_b520e6e7dd2c85f4b83ca9ec1210796f>, 0, 1);
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_b520e6e7dd2c85f4b83ca9ec1210796f>,unsigned\u0020int,char>(A_0, _Count, <lambda_b520e6e7dd2c85f4b83ca9ec1210796f>, _Count, _Ch);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000021D0 File Offset: 0x000015D0
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append.<lambda_b520e6e7dd2c85f4b83ca9ec1210796f>.()(<lambda_b520e6e7dd2c85f4b83ca9ec1210796f>* A_0, sbyte* _New_ptr, sbyte* _Old_ptr, uint _Old_size, uint _Count, sbyte _Ch)
	{
		cpblk(_New_ptr, _Old_ptr, _Old_size);
		int num = _Old_size + _New_ptr;
		initblk(num, _Ch, _Count);
		*(num + _Count) = 0;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00002810 File Offset: 0x00001C10
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		uint num = *(_Right + 16);
		sbyte* ptr = _Right;
		if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
		{
			ptr = *_Right;
		}
		if (num < 16)
		{
			cpblk(A_0, ptr, 16);
			*(A_0 + 16) = num;
			*(A_0 + 20) = 15;
		}
		else
		{
			uint num2 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0);
			uint num3 = num | 15;
			uint num4 = num3;
			uint num5 = *(ref num2 < num3 ? ref num2 : ref num4);
			uint num6 = num5 + 1;
			void* ptr2;
			if (num6 >= 4096)
			{
				ptr2 = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num6);
			}
			else if (num6 != null)
			{
				ptr2 = <Module>.@new(num6);
			}
			else
			{
				ptr2 = null;
			}
			*A_0 = ptr2;
			cpblk(ptr2, ptr, num + 1);
			*(A_0 + 16) = num;
			*(A_0 + 20) = num5;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000248C File Offset: 0x0000188C
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		cpblk(A_0, _Right, 24);
		*(_Right + 16) = 0;
		*(_Right + 20) = 15;
		*_Right = 0;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00001C88 File Offset: 0x00001088
	internal unsafe static void std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}(_String_val<std::_Simple_types<char>\u0020>._Bxty* A_0)
	{
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000021F4 File Offset: 0x000015F4
	internal unsafe static sbyte* std._String_val<std::_Simple_types<char>\u0020>._Myptr(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002218 File Offset: 0x00001618
	internal unsafe static sbyte* std._String_val<std::_Simple_types<char>\u0020>._Myptr(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00001C98 File Offset: 0x00001098
	internal unsafe static allocator<char>* std._Default_allocator_traits<std::allocator<char>\u0020>.select_on_container_copy_construction(allocator<char>* A_0, allocator<char>* _Al)
	{
		return A_0;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00001CA8 File Offset: 0x000010A8
	internal unsafe static sbyte* std._Char_traits<char,int>.copy(sbyte* _First1, sbyte* _First2, uint _Count)
	{
		cpblk(_First1, _First2, _Count);
		return _First1;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00001CBC File Offset: 0x000010BC
	internal unsafe static uint* std.max<unsigned\u0020int>(uint* _Left, uint* _Right)
	{
		return (*_Left < *_Right) ? _Right : _Left;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00001CD4 File Offset: 0x000010D4
	internal unsafe static allocator<char>* std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00001CE4 File Offset: 0x000010E4
	internal unsafe static allocator<char>* std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00001CF4 File Offset: 0x000010F4
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Swap_proxy_and_iterators(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000223C File Offset: 0x0000163C
	internal unsafe static uint std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		return int.MaxValue;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00002250 File Offset: 0x00001650
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Memcpy_val_from(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		cpblk(A_0, _Right, 24);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00001D04 File Offset: 0x00001104
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._String_val<std::_Simple_types<char>\u0020>._Large_string_engaged(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		return (16 <= *(A_0 + 20)) ? 1 : 0;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000024B0 File Offset: 0x000018B0
	internal unsafe static sbyte* std.allocator<char>.allocate(allocator<char>* A_0, uint _Count)
	{
		void* ptr;
		if (_Count >= 4096)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(_Count);
		}
		else if (_Count != null)
		{
			ptr = <Module>.@new(_Count);
		}
		else
		{
			ptr = null;
		}
		return ptr;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00002264 File Offset: 0x00001664
	internal unsafe static void std.allocator<char>.deallocate(allocator<char>* A_0, sbyte* _Ptr, uint _Count)
	{
		uint num = _Count;
		void* ptr = _Ptr;
		if (_Count >= 4096)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00001D20 File Offset: 0x00001120
	internal unsafe static void std._Narrow_char_traits<char,int>.assign(sbyte* _Left, sbyte* _Right)
	{
		*_Left = (byte)(*_Right);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00001D34 File Offset: 0x00001134
	internal unsafe static sbyte* std._Narrow_char_traits<char,int>.assign(sbyte* _First, uint _Count, sbyte _Ch)
	{
		initblk(_First, _Ch, _Count);
		return _First;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00001D4C File Offset: 0x0000114C
	internal unsafe static uint std._Default_allocator_traits<std::allocator<char>\u0020>.max_size(allocator<char>* A_0)
	{
		return -1;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00001D5C File Offset: 0x0000115C
	internal unsafe static uint* std.min<unsigned\u0020int>(uint* _Left, uint* _Right)
	{
		return (*_Right < *_Left) ? _Right : _Left;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00001D74 File Offset: 0x00001174
	internal unsafe static allocator<char>* std.move<class\u0020std::allocator<char>\u0020&>(allocator<char>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000024E0 File Offset: 0x000018E0
	internal unsafe static _Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{ctor}<class\u0020std::allocator<char>\u0020>(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0, _One_then_variadic_args_t __unnamed000, allocator<char>* _Val1)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00002528 File Offset: 0x00001928
	internal unsafe static _Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{ctor}<>(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0, _Zero_then_variadic_args_t A_0)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00001D84 File Offset: 0x00001184
	internal unsafe static void std._Destroy_in_place<char\u0020*>(sbyte** _Obj)
	{
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00002570 File Offset: 0x00001970
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_b520e6e7dd2c85f4b83ca9ec1210796f>,unsigned\u0020int,char>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Size_increase, <lambda_b520e6e7dd2c85f4b83ca9ec1210796f> _Fn, uint <_Args_0>, sbyte <_Args_1>)
	{
		uint num = *(A_0 + 16);
		if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0) - num < _Size_increase)
		{
			<Module>.std._Xlen_string();
		}
		uint num2 = num + _Size_increase;
		uint num3 = *(A_0 + 20);
		uint num4 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(num2, *(A_0 + 20), <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0));
		uint num5 = num4 + 1;
		void* ptr;
		if (num5 >= 4096)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num5);
		}
		else if (num5 != null)
		{
			ptr = <Module>.@new(num5);
		}
		else
		{
			ptr = null;
		}
		*(A_0 + 16) = num2;
		*(A_0 + 20) = num4;
		if (16 <= num3)
		{
			sbyte* ptr2 = *A_0;
			cpblk(ptr, ptr2, num);
			initblk(num + (byte*)ptr, <_Args_1>, <_Args_0>);
			((byte*)(num + (byte*)ptr))[<_Args_0>] = 0;
			uint num6 = num3 + 1;
			void* ptr3 = ptr2;
			if (num6 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num6);
			}
			<Module>.delete(ptr3, num6);
			*A_0 = ptr;
		}
		else
		{
			cpblk(ptr, A_0, num);
			int num7 = num + (byte*)ptr;
			initblk(num7, <_Args_1>, <_Args_0>);
			*(num7 + (int)<_Args_0>) = 0;
			*A_0 = ptr;
		}
		return A_0;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00002290 File Offset: 0x00001690
	internal unsafe static void std._Construct_in_place<char\u0020*,char\u0020*\u0020const\u0020&>(sbyte** _Obj, sbyte** <_Args_0>)
	{
		*_Obj = *<_Args_0>;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00001D94 File Offset: 0x00001194
	internal unsafe static sbyte* std._Unfancy<char>(sbyte* _Ptr)
	{
		return _Ptr;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000022A4 File Offset: 0x000016A4
	internal unsafe static void std._Construct_in_place<char\u0020*,char\u0020*\u0020&>(sbyte** _Obj, sbyte** <_Args_0>)
	{
		*_Obj = *<_Args_0>;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00001DA4 File Offset: 0x000011A4
	internal unsafe static void std._Deallocate<8,0>(void* _Ptr, uint _Bytes)
	{
		if (_Bytes >= 4096U)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref _Ptr, ref _Bytes);
		}
		<Module>.delete(_Ptr, _Bytes);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00001DCC File Offset: 0x000011CC
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>* std.addressof<class\u0020std::_String_val<struct\u0020std::_Simple_types<char>\u0020>\u0020>(_String_val<std::_Simple_types<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00001DDC File Offset: 0x000011DC
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>* std.addressof<class\u0020std::_String_val<struct\u0020std::_Simple_types<char>\u0020>\u0020const\u0020>(_String_val<std::_Simple_types<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00001DEC File Offset: 0x000011EC
	internal static uint std._Get_size_of_n<1>(uint _Count)
	{
		return _Count;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000022B8 File Offset: 0x000016B8
	internal unsafe static void* std._Allocate<8,struct\u0020std::_Default_allocate_traits,0>(uint _Bytes)
	{
		if (_Bytes >= 4096)
		{
			return <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(_Bytes);
		}
		if (_Bytes != null)
		{
			return <Module>.@new(_Bytes);
		}
		return 0;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000022E0 File Offset: 0x000016E0
	internal unsafe static uint std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Requested)
	{
		uint num = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0);
		uint num2 = *(A_0 + 20);
		uint num3 = _Requested | 15;
		uint num4;
		if (num3 > num)
		{
			num4 = num;
		}
		else
		{
			uint num5 = num2 >> 1;
			if (num2 > num - num5)
			{
				num4 = num;
			}
			else
			{
				uint num6 = num2 + num5;
				uint num7 = num6;
				num4 = *(ref num3 < num6 ? ref num7 : ref num3);
			}
		}
		return num4;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00002330 File Offset: 0x00001730
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>* std._String_val<std::_Simple_types<char>\u0020>.{ctor}(_String_val<std::_Simple_types<char>\u0020>* A_0)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00001DFC File Offset: 0x000011FC
	internal unsafe static allocator<char>* std.allocator<char>.{ctor}(allocator<char>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00001E0C File Offset: 0x0000120C
	internal unsafe static uint std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(uint _Requested, uint _Old, uint _Max)
	{
		uint num = _Requested | 15;
		if (num > _Max)
		{
			return _Max;
		}
		uint num2 = _Old >> 1;
		if (_Old > _Max - num2)
		{
			return _Max;
		}
		uint num3 = _Old + num2;
		uint num4 = num3;
		return *(ref num < num3 ? ref num4 : ref num);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00001E44 File Offset: 0x00001244
	internal unsafe static _String_val<std::_Simple_types<char>\u0020>._Bxty* std._String_val<std::_Simple_types<char>\u0020>._Bxty.{ctor}(_String_val<std::_Simple_types<char>\u0020>._Bxty* A_0)
	{
		*A_0 = 0;
		return A_0;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00001E58 File Offset: 0x00001258
	internal unsafe static allocator<char>* std.forward<class\u0020std::allocator<char>\u0020>(allocator<char>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00001E68 File Offset: 0x00001268
	internal unsafe static sbyte** std.addressof<char\u0020*>(sbyte** _Val)
	{
		return _Val;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00001E78 File Offset: 0x00001278
	internal unsafe static void* std._Voidify_iter<char\u0020*\u0020*>(sbyte** _It)
	{
		return _It;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00001E88 File Offset: 0x00001288
	internal unsafe static sbyte** std.forward<char\u0020*\u0020const\u0020&>(sbyte** _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00001E98 File Offset: 0x00001298
	internal unsafe static sbyte** std.forward<char\u0020*\u0020&>(sbyte** _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00001EA8 File Offset: 0x000012A8
	internal unsafe static void* std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(uint _Bytes)
	{
		uint num = _Bytes + 35;
		if (num <= _Bytes)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		uint num2 = <Module>.@new(num);
		if (num2 != null)
		{
			void* ptr = (num2 + 35) & -32;
			*(ptr - 4) = num2;
			return ptr;
		}
		<Module>._invalid_parameter_noinfo_noreturn();
		return 0;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00001180 File Offset: 0x00000580
	internal unsafe static void* @new(uint __unnamed000, void* _Where, ?A0x3b496f95.__clr_placement_new_t __unnamed002)
	{
		return _Where;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003570 File Offset: 0x00002970
	internal unsafe static void* @new(uint __unnamed000, void* _Where, ?A0x3d80193d.__clr_placement_new_t __unnamed002)
	{
		return _Where;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003594 File Offset: 0x00002994
	internal unsafe static long ?A0x3d80193d.time(long* _Time)
	{
		return <Module>._time64(_Time);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000035A8 File Offset: 0x000029A8
	internal unsafe static bad_cast* std.bad_cast.{ctor}(bad_cast* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		bad_cast* ptr = A_0 + 4;
		initblk(ptr, 0, 8);
		*ptr = ref <Module>.??_C@_08EPJLHIJG@bad?5cast@;
		try
		{
			*A_0 = ref <Module>.??_7bad_cast@std@@6B@;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.exception.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003600 File Offset: 0x00002A00
	internal unsafe static void std._Throw_bad_cast()
	{
		bad_cast bad_cast;
		<Module>.std.bad_cast.{ctor}(ref bad_cast);
		<Module>._CxxThrowException((void*)(&bad_cast), (_s__ThrowInfo*)(&<Module>._TI2?AVbad_cast@std@@));
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00003624 File Offset: 0x00002A24
	internal unsafe static void std.locale.{dtor}(locale* A_0)
	{
		uint num = (uint)(*(A_0 + 4));
		if (num != 0U)
		{
			uint num2 = num;
			_Facet_base* ptr = calli(std._Facet_base* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 8)));
			if (ptr != null)
			{
				void* ptr2 = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt32), ptr, 1U, (IntPtr)(*(*(int*)ptr)));
			}
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003654 File Offset: 0x00002A54
	internal unsafe static locale.facet* std.locale._Getfacet(locale* A_0, uint _Id)
	{
		int num = *(A_0 + 4);
		locale.facet* ptr;
		if (_Id < (uint)(*(num + 12)))
		{
			ptr = *(_Id * 4U + (uint)(*(num + 8)));
			if (ptr != null)
			{
				return ptr;
			}
		}
		else
		{
			ptr = null;
		}
		if (*(num + 20) != 0)
		{
			locale._Locimp* ptr2 = <Module>.std.locale._Getgloballocale();
			if (_Id < (uint)(*(int*)(ptr2 + 12 / sizeof(locale._Locimp))))
			{
				return *(_Id * 4U + (uint)(*(int*)(ptr2 + 8 / sizeof(locale._Locimp))));
			}
			return 0;
		}
		return ptr;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000036A4 File Offset: 0x00002AA4
	internal unsafe static ios_base* std.hex(ios_base* _Iosbase)
	{
		<Module>.std.ios_base.setf(_Iosbase, 2048, 3584);
		return _Iosbase;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000FAC4 File Offset: 0x0000EEC4
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Fgetc<char>(sbyte* _Byte, _iobuf* _File)
	{
		int num = <Module>.fgetc(_File);
		if (num == -1)
		{
			return 0;
		}
		*_Byte = (byte)num;
		return 1;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000F4F4 File Offset: 0x0000E8F4
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Fputc<char>(sbyte _Byte, _iobuf* _File)
	{
		return (<Module>.fputc(_Byte, _File) != -1) ? 1 : 0;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000F5BC File Offset: 0x0000E9BC
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Ungetc<char>(sbyte* _Byte, _iobuf* _File)
	{
		return (<Module>.ungetc((int)((byte)(*_Byte)), _File) != -1) ? 1 : 0;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00006158 File Offset: 0x00005558
	internal unsafe static void IFfuReader.{dtor}(IFfuReader* A_0)
	{
		*A_0 = ref <Module>.??_7IFfuReader@@6B@;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 4;
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
			throw;
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00010904 File Offset: 0x0000FD04
	internal unsafe static void* IFfuReader.__vecDelDtor(IFfuReader* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			IFfuReader* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 28U, (uint)(*ptr), ldftn(IFfuReader.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				IFfuReader* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 28 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7IFfuReader@@6B@;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = A_0 + 4;
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr3);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr3);
			throw;
		}
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 28U);
		}
		return A_0;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000036C4 File Offset: 0x00002AC4
	internal unsafe static FfuReader.WriteRequest* FfuReader.WriteRequest.{ctor}(FfuReader.WriteRequest* A_0)
	{
		*A_0 = 0L;
		*(A_0 + 8) = 0;
		*(A_0 + 16) = 0L;
		*(A_0 + 24) = 0;
		*(A_0 + 32) = 0L;
		return A_0;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000036F0 File Offset: 0x00002AF0
	internal unsafe static FfuReader.BlockDataEntry* FfuReader.BlockDataEntry.{ctor}(FfuReader.BlockDataEntry* A_0)
	{
		*A_0 = 0L;
		*(A_0 + 12) = 0;
		*(A_0 + 16) = 0L;
		return A_0;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x000061A4 File Offset: 0x000055A4
	internal unsafe static FfuReader.partition* FfuReader.partition.{ctor}(FfuReader.partition* A_0)
	{
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0);
		try
		{
			*(A_0 + 56) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00001000 File Offset: 0x00000400
	internal unsafe static void ?A0x3d80193d.??__E?A0x3d80193d@IMAGE_HEADER_SIGNATURE@@YMXXZ()
	{
		try
		{
			*((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 16) = 0;
			*((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 20) = 15;
			<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE = 0;
			sbyte* ptr = ref <Module>.??_C@_0N@EFJOMLHH@ImageFlash?5?5@;
			do
			{
				ptr++;
			}
			while (*ptr != 0);
			int num = ptr - (ref <Module>.??_C@_0N@EFJOMLHH@ImageFlash?5?5@);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE, ref <Module>.??_C@_0N@EFJOMLHH@ImageFlash?5?5@, num);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE));
			throw;
		}
		<Module>._atexit_m(ldftn(?A0x3d80193d.??__F?A0x3d80193d@IMAGE_HEADER_SIGNATURE@@YMXXZ));
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000147DC File Offset: 0x00013BDC
	internal unsafe static void ?A0x3d80193d.??__F?A0x3d80193d@IMAGE_HEADER_SIGNATURE@@YMXXZ()
	{
		try
		{
			if (((16 <= *((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 20)) ? 1 : 0) != 0)
			{
				uint num = (uint)(*((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 20) + 1);
				void* ptr = <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE;
				if (num >= 4096U)
				{
					num += 35U;
					uint num2 = *(<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE - 4);
					if (<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE - num2 - 4 <= 31)
					{
						ptr = num2;
					}
					else
					{
						<Module>._invalid_parameter_noinfo_noreturn();
					}
				}
				<Module>.delete(ptr, num);
			}
			*((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 16) = 0;
			*((ref <Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE) + 20) = 15;
			<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&<Module>.?A0x3d80193d.IMAGE_HEADER_SIGNATURE));
			throw;
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00006838 File Offset: 0x00005C38
	internal unsafe static void stringToLower(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* s)
	{
		sbyte* ptr = <Module>.new[]((uint)(*(s + 16) + 1));
		sbyte* ptr2 = s;
		if (((16 <= *(s + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *s;
		}
		sbyte* ptr3 = ptr2;
		int num = (int)(ptr - ptr2);
		sbyte b;
		do
		{
			b = *(sbyte*)ptr3;
			*(byte*)(num / sizeof(sbyte) + ptr3) = (byte)b;
			ptr3 += 1 / sizeof(sbyte);
		}
		while (b != 0);
		uint num2 = 0U;
		if (0 < *(s + 16))
		{
			do
			{
				*(byte*)(ptr + num2 / (uint)sizeof(sbyte)) = <Module>.tolower((int)(*(sbyte*)(ptr + num2 / (uint)sizeof(sbyte))));
				num2 += 1U;
			}
			while (num2 < (uint)(*(s + 16)));
		}
		sbyte* ptr4 = ptr;
		if (*(sbyte*)ptr != 0)
		{
			do
			{
				ptr4 += 1 / sizeof(sbyte);
			}
			while (*(sbyte*)ptr4 != 0);
		}
		int num3 = (int)(ptr4 - ptr);
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(s, ptr, num3);
		<Module>.delete[]((void*)ptr);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000AEC4 File Offset: 0x0000A2C4
	internal unsafe static FfuReader* FfuReader.{ctor}(FfuReader* A_0)
	{
		<Module>.IFfuReader.{ctor}(A_0);
		try
		{
			*A_0 = ref <Module>.??_7FfuReader@@6B@;
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0 + 28);
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0 + 52);
				try
				{
					*(A_0 + 80) = 2013103101L;
					vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* ptr = A_0 + 92;
					*ptr = 0;
					*(ptr + 4) = 0;
					*(ptr + 8) = 0;
					try
					{
						vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* ptr2 = A_0 + 104;
						*ptr2 = 0;
						*(ptr2 + 4) = 0;
						*(ptr2 + 8) = 0;
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0 + 17012);
							try
							{
								*(A_0 + 17036) = 0;
								*(A_0 + 17232) = 0;
								*(A_0 + 17236) = 0;
								*(A_0 + 17240) = 0L;
								*(A_0 + 17248) = 0L;
								vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* ptr3 = A_0 + 17268;
								*ptr3 = 0;
								*(ptr3 + 4) = 0;
								*(ptr3 + 8) = 0;
								try
								{
									*(A_0 + 17280) = 0;
									*(A_0 + 18340) = 0;
									*(A_0 + 17264) = 0;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.{dtor}), A_0 + 17268);
									throw;
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 17012);
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.{dtor}), A_0 + 104);
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.{dtor}), A_0 + 92);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 52);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 28);
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(IFfuReader.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00010AE8 File Offset: 0x0000FEE8
	internal unsafe static void* FfuReader.__vecDelDtor(FfuReader* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			FfuReader* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 18344U, (uint)(*ptr), ldftn(FfuReader.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				FfuReader* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 18344 + 4));
			}
			return ptr;
		}
		<Module>.FfuReader.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 18344U);
		}
		return A_0;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000061EC File Offset: 0x000055EC
	internal unsafe static IFfuReader* IFfuReader.{ctor}(IFfuReader* A_0)
	{
		*A_0 = ref <Module>.??_7IFfuReader@@6B@;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0 + 4);
		return A_0;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000B0C4 File Offset: 0x0000A4C4
	internal unsafe static void FfuReader.{dtor}(FfuReader* A_0)
	{
		*A_0 = ref <Module>.??_7FfuReader@@6B@;
		try
		{
			try
			{
				try
				{
					try
					{
						try
						{
							try
							{
								<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Tidy(A_0 + 17268);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 17012);
								throw;
							}
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 17012;
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.{dtor}), A_0 + 104);
							throw;
						}
						<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Tidy(A_0 + 104);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.{dtor}), A_0 + 92);
						throw;
					}
					<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Tidy(A_0 + 92);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 52);
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = A_0 + 52;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr2);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr2);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0 + 28);
				throw;
			}
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = A_0 + 28;
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr3);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr3);
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(IFfuReader.{dtor}), A_0);
			throw;
		}
		*A_0 = ref <Module>.??_7IFfuReader@@6B@;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr4 = A_0 + 4;
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr4);
			throw;
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003710 File Offset: 0x00002B10
	internal unsafe static void FfuReader.setProgress(FfuReader* A_0, IFfuReaderProgress* progress)
	{
		*(A_0 + 17280) = progress;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000372C File Offset: 0x00002B2C
	internal unsafe static void FfuReader.setDiagnostic(FfuReader* A_0, IDiagnostic* pIdiagnostic)
	{
		*(A_0 + 18340) = pIdiagnostic;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003748 File Offset: 0x00002B48
	internal unsafe static long FfuReader.SkipChunk(FfuReader* A_0, long currentPos, long chunkSize)
	{
		long num = currentPos % chunkSize;
		if (num != 0L)
		{
			currentPos += chunkSize - num;
		}
		return currentPos;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00009850 File Offset: 0x00008C50
	internal unsafe static FfuReaderResult* FfuReader.readFfuPlatformId(FfuReader* A_0, FfuReaderResult* A_1, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* filename)
	{
		uint num = 0U;
		FfuReaderResult* ptr28;
		try
		{
			try
			{
				FfuReaderResult ffuReaderResult;
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult) + 4);
				try
				{
					uint num2 = (uint)(*(A_0 + 18340));
					if (0U != num2)
					{
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num2, (sbyte*)(&<Module>.??_C@_0BB@MKPNLBEM@readFfu?$CI?$CJ?5Start?4@), (IntPtr)(*(*num2 + 4)));
					}
					initblk(A_0 + 628, 0, 16384);
					*(A_0 + 17036) = 0;
					*(A_0 + 17040) = 0;
					*(A_0 + 17232) = 0;
					*(A_0 + 17236) = 0;
					*(A_0 + 17240) = 0L;
					*(A_0 + 17248) = 0L;
					sbyte* ptr = (sbyte*)filename;
					if (((16 <= *(int*)(filename + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>))) ? 1 : 0) != 0)
					{
						ptr = *(int*)filename;
					}
					_iobuf* ptr2 = <Module>.fopen(ptr, (sbyte*)(&<Module>.??_C@_02JDPG@rb@));
					if (ptr2 != null)
					{
						goto IL_152;
					}
					ffuReaderResult = 2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_0CD@GAPHJKMF@Could?5not?5open?5FFU?5file?0?5filena@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr4 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr4);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_152:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					FfuReaderResult ffuReaderResult2;
					FfuReaderResult* ptr5 = <Module>.FfuReader.readSecurityHdrAndCheckValidity(A_0, &ffuReaderResult2, ptr2);
					try
					{
						ffuReaderResult = *(int*)ptr5;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr5 + 4 / sizeof(FfuReaderResult));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult2));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr6 = (ref ffuReaderResult2) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult2) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr6);
						throw;
					}
					if (0 == ffuReaderResult)
					{
						goto IL_21F;
					}
					<Module>.fclose(ptr2);
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr7 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr7);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_21F:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					uint num3 = (uint)(*(A_0 + 17300) * 1024);
					*(A_0 + 17036) = (int)num3;
					_iobuf* ptr2;
					long num4 = <Module>._ftelli64(ptr2) + (ulong)(*(A_0 + 17312));
					*(A_0 + 17256) = (long)((ulong)(*(A_0 + 17308)) + (ulong)num4);
					long num5 = *(A_0 + 17256);
					long num6 = (long)((ulong)num3);
					long num7 = num5 % num6;
					if (num7 != 0L)
					{
						*(A_0 + 17256) = num6 - num7 + num5;
					}
					if (0 == <Module>._fseeki64(ptr2, *(A_0 + 17256), 0))
					{
						goto IL_360;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref <Module>.??_C@_0DA@OLEAJDFH@Corrupted?5FFU?0?5incorrect?5header@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr8);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr9 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr9);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_360:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					*(A_0 + 17232) = 0;
					_iobuf* ptr2;
					ImageHeader imageHeader;
					if (1 == <Module>.fread((void*)(&imageHeader), 24U, 1U, ptr2))
					{
						goto IL_43A;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr10 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_0DG@LJEFNDMN@Corrupted?5FFU?0?5could?5not?5read?5i@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr10);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr11 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr11);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_43A:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					ImageHeader imageHeader;
					if (imageHeader == 24)
					{
						goto IL_504;
					}
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr12 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4, ref <Module>.??_C@_0DI@OMEKDBC@Corrupted?5FFU?0?5size?5of?5image?5he@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr12);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr13 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr13);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_504:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					ImageHeader imageHeader;
					if (0 == <Module>._fseeki64(ptr2, (long)((ulong)(*((ref imageHeader) + 16))), 1))
					{
						goto IL_5D8;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr14 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5, ref <Module>.??_C@_0CP@NOMNLDJO@Corrupted?5FFU?0?5cannot?5skip?5mani@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr14);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr15 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr15);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_5D8:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					long num8 = (long)((ulong)(*(A_0 + 17300) * 1024));
					_iobuf* ptr2;
					long num9 = <Module>._ftelli64(ptr2);
					long num10 = num9 % num8;
					if (num10 != 0L)
					{
						num9 += num8 - num10;
					}
					if (0 == <Module>._fseeki64(ptr2, num9, 0))
					{
						goto IL_6D7;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr16 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6, ref <Module>.??_C@_0CP@JCLBHCCP@Corrupted?5FFU?0?5cannot?5skip?5padd@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr16);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr17 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr17);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_6D7:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					StoreHeader storeHeader;
					if (<Module>.fread((void*)(&storeHeader), 248U, 1U, ptr2) == 1)
					{
						goto IL_81C;
					}
					FfuReaderResult ffuReaderResult3;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult3) + 4);
					try
					{
						<Module>.fclose(ptr2);
						ffuReaderResult3 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr18 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr19 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8, ptr18, ref <Module>.??_C@_0DF@NIFEHAIP@fread?$CI?$CGstoreHeader?0?5sizeof?$CIStor@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult3) + 4, ptr19);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7));
							throw;
						}
						*(int*)A_1 = ffuReaderResult3;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult3) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult3));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr20 = (ref ffuReaderResult3) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult3) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr20);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr21 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr21);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_81C:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					StoreHeader storeHeader;
					if (*((ref storeHeader) + 8) != 2)
					{
						goto IL_A3E;
					}
					if (*((ref storeHeader) + 10) != 0)
					{
						goto IL_A3E;
					}
					if (storeHeader == null)
					{
						goto IL_905;
					}
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 7;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr22 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9, ref <Module>.??_C@_0EL@FHOOFPLA@The?5FFU?5is?5not?5a?5full?5flash?5FFU@, filename);
					try
					{
						if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr22))
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr22);
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9));
						throw;
					}
					<Module>.FfuReaderResult.{ctor}(A_1, ref ffuReaderResult);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr23 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr23);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_905:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					StoreHeader storeHeader;
					long num11 = <Module>._ftelli64(ptr2) + (ulong)(*((ref storeHeader) + 212));
					long num12 = (long)((ulong)(*((ref storeHeader) + 204)));
					long num13 = num11 % num12;
					if (num13 != 0L)
					{
						num11 += num12 - num13;
					}
					cpblk(A_0 + 17040, (ref storeHeader) + 12, 192);
					cpblk(A_0 + 88, (ref storeHeader) + 232, 4);
					*(A_0 + 17236) = (int)num11 - *(A_0 + 17232);
					*(A_0 + 17240) = num11;
					<Module>.fclose(ptr2);
					ffuReaderResult = 0;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign((ref ffuReaderResult) + 4, ref <Module>.??_C@_00CNPNBAHC@@, 0);
					uint num2 = (uint)(*(A_0 + 18340));
					if (0U != num2)
					{
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num2, (sbyte*)(&<Module>.??_C@_0P@FGKADOIP@readFfu?$CI?$CJ?5End?4@), (IntPtr)(*(*num2 + 4)));
					}
					*(int*)A_1 = ffuReaderResult;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr24 = A_1 + 4 / sizeof(FfuReaderResult);
					<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ptr24);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr24, (ref ffuReaderResult) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr24);
						throw;
					}
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr25 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr25);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_A3E:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>;
					<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, 1);
					try
					{
						StoreHeader storeHeader;
						<Module>.std.operator<<<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 16, (sbyte*)(&<Module>.??_C@_0DN@EDLJMDMN@The?5FFU?5has?5wrong?5version?0?5must@)), *((ref storeHeader) + 8)), (sbyte*)(&<Module>.??_C@_01LFCBOECM@?4@)), *((ref storeHeader) + 10)), (sbyte*)(&<Module>.??_C@_0N@LPLCCEEL@?0?5filename?3?5@)), filename);
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult = 3;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr26 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10);
						try
						{
							if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr26))
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr26);
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10));
							throw;
						}
						*(int*)A_1 = ffuReaderResult;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
					<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr27 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr27);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			ptr28 = A_1;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return ptr28;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000620C File Offset: 0x0000560C
	internal unsafe static FfuReaderResult* FfuReaderResult.{ctor}(FfuReaderResult* A_0)
	{
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0 + 4);
		return A_0;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00006224 File Offset: 0x00005624
	internal unsafe static FfuReaderResult* FfuReaderResult.{ctor}(FfuReaderResult* A_0, FfuReaderResult* A_0)
	{
		*A_0 = *A_0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 4;
		*ptr = 0;
		try
		{
			*(ptr + 16) = 0;
			*(ptr + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), ptr);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr, A_0 + 4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000062A8 File Offset: 0x000056A8
	internal unsafe static FfuReaderResult* FfuReaderResult.=(FfuReaderResult* A_0, FfuReaderResult* A_0)
	{
		*A_0 = *A_0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 4;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = A_0 + 4;
		if (ptr2 != ptr)
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr2);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr2, ptr);
		}
		return A_0;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000062D4 File Offset: 0x000056D4
	internal unsafe static void std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 104;
		<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ptr);
		<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}(ptr);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000B77C File Offset: 0x0000AB7C
	internal unsafe static FfuReaderResult* FfuReader.readFfu(FfuReader* A_0, FfuReaderResult* A_1, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* filename, uint maxBlockSizeInBytes, [MarshalAs(UnmanagedType.U1)] bool secureFFU, [MarshalAs(UnmanagedType.U1)] bool dumpPartitions, [MarshalAs(UnmanagedType.U1)] bool dumpGpt)
	{
		uint num = 0U;
		FfuReaderResult* ptr85;
		try
		{
			try
			{
				uint num2 = 0U;
				FfuReaderResult ffuReaderResult;
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult) + 4);
				try
				{
					uint num3 = (uint)(*(A_0 + 18340));
					if (0U != num3)
					{
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BB@MKPNLBEM@readFfu?$CI?$CJ?5Start?4@), (IntPtr)(*(*num3 + 4)));
					}
					initblk(A_0 + 628, 0, 16384);
					*(A_0 + 17036) = 0;
					*(A_0 + 17040) = 0;
					*(A_0 + 17232) = 0;
					*(A_0 + 17236) = 0;
					*(A_0 + 17240) = 0L;
					*(A_0 + 17248) = 0L;
					sbyte* ptr = (sbyte*)filename;
					if (((16 <= *(int*)(filename + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>))) ? 1 : 0) != 0)
					{
						ptr = *(int*)filename;
					}
					_iobuf* ptr2 = <Module>.fopen(ptr, (sbyte*)(&<Module>.??_C@_02JDPG@rb@));
					if (ptr2 != null)
					{
						goto IL_176;
					}
					ffuReaderResult = 2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_0CD@GAPHJKMF@Could?5not?5open?5FFU?5file?0?5filena@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr4 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr4);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_176:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					FfuReaderResult ffuReaderResult2;
					FfuReaderResult* ptr5 = <Module>.FfuReader.readSecurityHdrAndCheckValidity(A_0, &ffuReaderResult2, ptr2);
					try
					{
						ffuReaderResult = *(int*)ptr5;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr5 + 4 / sizeof(FfuReaderResult));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult2));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr6 = (ref ffuReaderResult2) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult2) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr6);
						throw;
					}
					if (0 == ffuReaderResult)
					{
						goto IL_25E;
					}
					<Module>.fclose(ptr2);
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr7 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr7);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_25E:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					uint num4 = (uint)(*(A_0 + 17300) * 1024);
					*(A_0 + 17036) = (int)num4;
					_iobuf* ptr2;
					long num5 = <Module>._ftelli64(ptr2) + (ulong)(*(A_0 + 17312));
					*(A_0 + 17256) = (long)((ulong)(*(A_0 + 17308)) + (ulong)num5);
					long num6 = (long)((ulong)num4);
					long num7 = *(A_0 + 17256);
					long num8 = num7 % num6;
					if (num8 != 0L)
					{
						*(A_0 + 17256) = num7 - num8 + num6;
					}
					if (0 == <Module>._fseeki64(ptr2, *(A_0 + 17256), 0))
					{
						goto IL_3B5;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref <Module>.??_C@_0DA@OLEAJDFH@Corrupted?5FFU?0?5incorrect?5header@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr8);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr9 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr9);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_3B5:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					*(A_0 + 17232) = 0;
					_iobuf* ptr2;
					ImageHeader imageHeader;
					if (1 == <Module>.fread((void*)(&imageHeader), 24U, 1U, ptr2))
					{
						goto IL_4A7;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr10 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_0DG@LJEFNDMN@Corrupted?5FFU?0?5could?5not?5read?5i@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr10);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr11 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr11);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_4A7:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					ImageHeader imageHeader;
					if (imageHeader == 24)
					{
						goto IL_589;
					}
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr12 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4, ref <Module>.??_C@_0DI@OMEKDBC@Corrupted?5FFU?0?5size?5of?5image?5he@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr12);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr13 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr13);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_589:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					ImageHeader imageHeader;
					if (0 == <Module>._fseeki64(ptr2, (long)((ulong)(*((ref imageHeader) + 16))), 1))
					{
						goto IL_675;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr14 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5, ref <Module>.??_C@_0CP@NOMNLDJO@Corrupted?5FFU?0?5cannot?5skip?5mani@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr14);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr15 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr15);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_675:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					long num9 = (long)((ulong)(*(A_0 + 17300) * 1024));
					_iobuf* ptr2;
					long num10 = <Module>._ftelli64(ptr2);
					long num11 = num10 % num9;
					if (num11 != 0L)
					{
						num10 += num9 - num11;
					}
					if (0 == <Module>._fseeki64(ptr2, num10, 0))
					{
						goto IL_78A;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 4;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr16 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6, ref <Module>.??_C@_0CP@JCLBHCCP@Corrupted?5FFU?0?5cannot?5skip?5padd@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr16);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr17 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr17);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_78A:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					StoreHeader storeHeader;
					if (<Module>.fread((void*)(&storeHeader), 248U, 1U, ptr2) == 1)
					{
						goto IL_8F7;
					}
					FfuReaderResult ffuReaderResult3;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult3) + 4);
					try
					{
						<Module>.fclose(ptr2);
						ffuReaderResult3 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr18 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr19 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8, ptr18, ref <Module>.??_C@_0DF@NIFEHAIP@fread?$CI?$CGstoreHeader?0?5sizeof?$CIStor@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult3) + 4, ptr19);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>8));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>7));
							throw;
						}
						*(int*)A_1 = ffuReaderResult3;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult3) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult3));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr20 = (ref ffuReaderResult3) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult3) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr20);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr21 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr21);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_8F7:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					StoreHeader storeHeader;
					if (*((ref storeHeader) + 8) != 2)
					{
						goto IL_262B;
					}
					if (*((ref storeHeader) + 10) != 0)
					{
						goto IL_262B;
					}
					if (storeHeader == null)
					{
						goto IL_9F5;
					}
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 7;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr22 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9, ref <Module>.??_C@_0EL@FHOOFPLA@The?5FFU?5is?5not?5a?5full?5flash?5FFU@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr22);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>9));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr23 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr23);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_9F5:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					StoreHeader storeHeader;
					long num12 = <Module>._ftelli64(ptr2) + (ulong)(*((ref storeHeader) + 212));
					long num13 = (long)((ulong)(*((ref storeHeader) + 204)));
					long num14 = num12 % num13;
					if (num14 != 0L)
					{
						num12 += num13 - num14;
					}
					cpblk(A_0 + 17040, (ref storeHeader) + 12, 192);
					cpblk(A_0 + 88, (ref storeHeader) + 232, 4);
					*(A_0 + 17236) = (int)num12 - *(A_0 + 17232);
					*(A_0 + 17240) = num12;
					if (*((ref storeHeader) + 204) <= (int)maxBlockSizeInBytes)
					{
						goto IL_B5A;
					}
					<Module>.fclose(ptr2);
					ffuReaderResult = 6;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr24 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10, ref <Module>.??_C@_0CJ@EDDAJGLL@Too?5small?5maximum?5block?5size?0?5f@, filename);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr24);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>10));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr25 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr25);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_B5A:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					StoreHeader storeHeader;
					if (*((ref storeHeader) + 204) == (int)maxBlockSizeInBytes)
					{
						goto IL_CEA;
					}
					_iobuf* ptr2;
					long num12;
					if (0 != <Module>.FfuReader.readDescriptors(A_0, ptr2, num12, maxBlockSizeInBytes, (uint)(*((ref storeHeader) + 204)), (uint)(*((ref storeHeader) + 208))))
					{
						goto IL_1211;
					}
					FfuReaderResult ffuReaderResult4;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult4) + 4);
					try
					{
						<Module>.fclose(ptr2);
						ffuReaderResult4 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>11;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr26 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>11, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>12;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr27 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>12, ptr26, ref <Module>.??_C@_0IB@KAMICEBB@0?5?$CB?$DN?5readDescriptors?$CIfp?0?5payloa@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult4) + 4, ptr27);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>12));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>12);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>12));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>11));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>11);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>11));
							throw;
						}
						*(int*)A_1 = ffuReaderResult4;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult4) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult4));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr28 = (ref ffuReaderResult4) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult4) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr28);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr29 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr29);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_CEA:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					uint num15 = 0U;
					StoreHeader storeHeader;
					if (0 >= *((ref storeHeader) + 208))
					{
						goto IL_1211;
					}
					for (;;)
					{
						_iobuf* ptr2;
						long num16 = <Module>._ftelli64(ptr2);
						DataBlock dataBlock;
						if (<Module>.fread((void*)(&dataBlock), 8U, 1U, ptr2) != 1)
						{
							break;
						}
						if (dataBlock <= 0)
						{
							goto Block_270;
						}
						if (*((ref dataBlock) + 4) == 0)
						{
							goto Block_271;
						}
						num15 += 1U;
						long num12;
						do
						{
							dataBlock--;
							BlockLocation blockLocation;
							if (1 != <Module>.fread((void*)(&blockLocation), 8U, 1U, ptr2))
							{
								goto IL_E13;
							}
							FfuReader.AccessMethod accessMethod = ((blockLocation == 0) ? ((FfuReader.AccessMethod)0) : ((FfuReader.AccessMethod)2));
							FfuReader.WriteRequest writeRequest;
							*((ref writeRequest) + 24) = (int)accessMethod;
							writeRequest = num12;
							uint num17 = (uint)(*((ref storeHeader) + 204)) >> 9;
							*((ref writeRequest) + 16) = (long)((ulong)(num17 * (uint)(*((ref blockLocation) + 4))));
							*((ref writeRequest) + 8) = (int)(num17 * (uint)(*((ref dataBlock) + 4)));
							*((ref writeRequest) + 32) = num16;
							<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.emplace_back<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(A_0 + 92, ref writeRequest);
						}
						while (dataBlock > 0);
						dataBlock--;
						num12 += (long)((ulong)(*((ref dataBlock) + 4) * *((ref storeHeader) + 204)));
						if (num15 >= (uint)(*((ref storeHeader) + 208)))
						{
							goto Block_275;
						}
					}
					goto IL_10BC;
					Block_270:
					Block_271:
					goto IL_F67;
					Block_275:
					goto IL_1211;
					IL_E13:
					FfuReaderResult ffuReaderResult5;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult5) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult5 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>13;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr30 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>13, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>14;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr31 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>14, ptr30, ref <Module>.??_C@_0DE@OEBOFPPN@1?5?$DN?$DN?5fread?$CI?$CGlocation?0?5sizeof?$CIBl@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult5) + 4, ptr31);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>14));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>14);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>14));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>13));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>13);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>13));
							throw;
						}
						*(int*)A_1 = ffuReaderResult5;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult5) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult5));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr32 = (ref ffuReaderResult5) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult5) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr32);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr33 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr33);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_F67:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					FfuReaderResult ffuReaderResult6;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult6) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult6 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>15;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr34 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>15, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>16;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr35 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>16, ptr34, ref <Module>.??_C@_0DJ@NAHGMBCO@block?4blockLocationCount?5?$DO?50?5?$CG?$CG@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult6) + 4, ptr35);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>16));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>16);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>16));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>15));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>15);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>15));
							throw;
						}
						*(int*)A_1 = ffuReaderResult6;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult6) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult6));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr36 = (ref ffuReaderResult6) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult6) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr36);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr37 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr37);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_10BC:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					FfuReaderResult ffuReaderResult7;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult7) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult7 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>17;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr38 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>17, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>18;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr39 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>18, ptr38, ref <Module>.??_C@_0CN@KKAJNADJ@fread?$CI?$CGblock?0?5sizeof?$CIDataBlock?$CJ@);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult7) + 4, ptr39);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>18));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>18);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>18));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>17));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>17);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>17));
							throw;
						}
						*(int*)A_1 = ffuReaderResult7;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult7) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult7));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr40 = (ref ffuReaderResult7) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult7) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr40);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr41 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr41);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_1211:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					if (true != secureFFU)
					{
						goto IL_196D;
					}
					long num18 = (long)((ulong)(*(A_0 + 17300) * 1024));
					long num19 = num18;
					long num20 = (long)((ulong)(*(A_0 + 17312) + *(A_0 + 17308) + 32));
					long num21 = num20 % num19;
					if (num21 != 0L)
					{
						num20 += num19 - num21;
					}
					long num22 = num18;
					ImageHeader imageHeader;
					long num23 = (long)((ulong)(*((ref imageHeader) + 16) + 24));
					long num24 = num23 % num22;
					if (num24 != 0L)
					{
						num23 += num22 - num24;
					}
					_iobuf* ptr2;
					StoreHeader storeHeader;
					<Module>._fseeki64(ptr2, (long)((ulong)(*((ref storeHeader) + 220) + (int)num23 + (int)num20 + 248)), 0);
					uint num25 = 0U;
					if (0 >= *((ref storeHeader) + 208))
					{
						goto IL_196D;
					}
					for (;;)
					{
						long num26 = <Module>._ftelli64(ptr2);
						DataBlock dataBlock2;
						if (<Module>.fread((void*)(&dataBlock2), 8U, 1U, ptr2) != 1)
						{
							break;
						}
						if (dataBlock2 <= 0)
						{
							goto Block_362;
						}
						if (*((ref dataBlock2) + 4) == 0)
						{
							goto Block_363;
						}
						BlockLocation blockLocation2;
						if (1 != <Module>.fread((void*)(&blockLocation2), 8U, 1U, ptr2))
						{
							goto Block_364;
						}
						long num12;
						FfuReader.BlockDataEntry blockDataEntry = num12;
						*((ref blockDataEntry) + 8) = dataBlock2;
						*((ref blockDataEntry) + 12) = *((ref dataBlock2) + 4) * *((ref storeHeader) + 204);
						*((ref blockDataEntry) + 16) = num26;
						<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.emplace_back<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(A_0 + 104, ref blockDataEntry);
						dataBlock2--;
						if (dataBlock2 != null)
						{
							do
							{
								dataBlock2--;
								if (1 != <Module>.fread((void*)(&blockLocation2), 8U, 1U, ptr2))
								{
									goto IL_13CA;
								}
							}
							while (dataBlock2 != null);
						}
						dataBlock2--;
						num12 += (long)((ulong)(*((ref dataBlock2) + 4) * *((ref storeHeader) + 204)));
						num25 += 1U;
						if (num25 >= (uint)(*((ref storeHeader) + 208)))
						{
							goto Block_366;
						}
					}
					goto IL_1804;
					Block_362:
					Block_363:
					goto IL_169B;
					Block_364:
					goto IL_1532;
					Block_366:
					goto IL_196D;
					IL_13CA:
					FfuReaderResult ffuReaderResult8;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult8) + 4);
					try
					{
						<Module>.fclose(ptr2);
						ffuReaderResult8 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>19;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr42 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>19, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>20;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr43 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>20, ptr42, ref <Module>.??_C@_0DE@OEBOFPPN@1?5?$DN?$DN?5fread?$CI?$CGlocation?0?5sizeof?$CIBl@);
							try
							{
								if ((ref ffuReaderResult8) + 4 != (ref *(FfuReaderResult*)ptr43))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult8) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult8) + 4, ptr43);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>20));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>20);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>20));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>19));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>19);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>19));
							throw;
						}
						*(int*)A_1 = ffuReaderResult8;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult8) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult8));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr44 = (ref ffuReaderResult8) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult8) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr44);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr45 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr45);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_1532:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					FfuReaderResult ffuReaderResult9;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult9) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult9 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>21;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr46 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>21, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>22;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr47 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>22, ptr46, ref <Module>.??_C@_0DE@OEBOFPPN@1?5?$DN?$DN?5fread?$CI?$CGlocation?0?5sizeof?$CIBl@);
							try
							{
								if ((ref ffuReaderResult9) + 4 != (ref *(FfuReaderResult*)ptr47))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult9) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult9) + 4, ptr47);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>22));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>22);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>22));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>21));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>21);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>21));
							throw;
						}
						*(int*)A_1 = ffuReaderResult9;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult9) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult9));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr48 = (ref ffuReaderResult9) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult9) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr48);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr49 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr49);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_169B:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					FfuReaderResult ffuReaderResult10;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult10) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult10 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>23;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr50 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>23, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>24;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr51 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>24, ptr50, ref <Module>.??_C@_0DJ@NAHGMBCO@block?4blockLocationCount?5?$DO?50?5?$CG?$CG@);
							try
							{
								if ((ref ffuReaderResult10) + 4 != (ref *(FfuReaderResult*)ptr51))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult10) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult10) + 4, ptr51);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>24));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>24);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>24));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>23));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>23);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>23));
							throw;
						}
						*(int*)A_1 = ffuReaderResult10;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult10) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult10));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr52 = (ref ffuReaderResult10) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult10) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr52);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr53 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr53);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_1804:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					FfuReaderResult ffuReaderResult11;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}((ref ffuReaderResult11) + 4);
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult11 = 5;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>25;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr54 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>25, filename, ref <Module>.??_C@_02KEGNLNML@?0?5@);
						try
						{
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>26;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr55 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>26, ptr54, ref <Module>.??_C@_0CN@KKAJNADJ@fread?$CI?$CGblock?0?5sizeof?$CIDataBlock?$CJ@);
							try
							{
								if ((ref ffuReaderResult11) + 4 != (ref *(FfuReaderResult*)ptr55))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult11) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult11) + 4, ptr55);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>26));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>26);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>26));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>25));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>25);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>25));
							throw;
						}
						*(int*)A_1 = ffuReaderResult11;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult11) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult11));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr56 = (ref ffuReaderResult11) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult11) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr56);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr57 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr57);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_196D:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					<Module>._fseeki64(ptr2, 0L, 2);
					*(A_0 + 17248) = <Module>._ftelli64(ptr2) - *(A_0 + 17240);
					<Module>._fseeki64(ptr2, 0L, 0);
					uint num27 = 0U;
					_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr58 = A_0 + 92;
					if (0 >= (*(ptr58 + 4) - *ptr58) / 40)
					{
						goto IL_2441;
					}
					int num28 = 0;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27;
					for (;;)
					{
						_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr59 = A_0 + 92;
						if (*(num28 + *ptr59 + 16) == 0L)
						{
							_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr60 = A_0 + 92;
							FfuReader.WriteRequest* ptr61 = num28 + *ptr60;
							if (0 != <Module>._fseeki64(ptr2, *ptr61 + 512L, 0))
							{
								break;
							}
							EFI_PARTITION_TABLE_HEADER efi_PARTITION_TABLE_HEADER;
							if (1 != <Module>.fread((void*)(&efi_PARTITION_TABLE_HEADER), 512U, 1U, ptr2))
							{
								goto Block_482;
							}
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27, ref <Module>.??_C@_08BOGKMBPC@EFI?5PART@);
							try
							{
								sbyte* ptr62 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27 : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
								sbyte* ptr63 = ptr62;
								EFI_PARTITION_TABLE_HEADER* ptr64 = &efi_PARTITION_TABLE_HEADER;
								byte b = efi_PARTITION_TABLE_HEADER;
								byte b2 = *(byte*)ptr62;
								if (efi_PARTITION_TABLE_HEADER >= b2)
								{
									while (b <= b2)
									{
										if (b != 0)
										{
											ptr64 += 1 / sizeof(EFI_PARTITION_TABLE_HEADER);
											ptr63 += 1 / sizeof(sbyte);
											b = *(byte*)ptr64;
											b2 = *(byte*)ptr63;
											if (b < b2)
											{
												break;
											}
										}
										else
										{
											if (128 != *((ref efi_PARTITION_TABLE_HEADER) + 80))
											{
												goto IL_2158;
											}
											if (128 != *((ref efi_PARTITION_TABLE_HEADER) + 84))
											{
												goto IL_204C;
											}
											$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@ $ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@;
											if (128 != <Module>.fread((void*)(&$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@), 128U, 128U, ptr2))
											{
												goto IL_1F40;
											}
											uint num29 = 0U;
											EFI_PARTITION_TABLE_HEADER efi_PARTITION_TABLE_HEADER2;
											if (0 < *((ref efi_PARTITION_TABLE_HEADER) + 80))
											{
												int num30 = (ref $ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@) + 56;
												while (*num30 != 0)
												{
													num29 += 1U;
													num30 += 128;
													if (num29 >= (uint)(*((ref efi_PARTITION_TABLE_HEADER) + 80)))
													{
														break;
													}
												}
												uint num2;
												if (num2 < num29)
												{
													num2 = num29;
													cpblk(A_0 + 116, ref efi_PARTITION_TABLE_HEADER, 512);
													efi_PARTITION_TABLE_HEADER2 = efi_PARTITION_TABLE_HEADER;
													cpblk(A_0 + 628, ref $ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@, 16384);
												}
											}
											*((ref efi_PARTITION_TABLE_HEADER2) + 16) = 0;
											uint num31 = <Module>.FfuReader.calculate_crc32(A_0, (void*)(&efi_PARTITION_TABLE_HEADER2), (uint)(*((ref efi_PARTITION_TABLE_HEADER2) + 12)));
											if (*((ref efi_PARTITION_TABLE_HEADER) + 16) != (int)num31)
											{
												goto IL_1DA5;
											}
											uint num32 = <Module>.FfuReader.calculate_crc32(A_0, (void*)(&$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@), (uint)(*((ref efi_PARTITION_TABLE_HEADER) + 84) * *((ref efi_PARTITION_TABLE_HEADER) + 80)));
											if (*((ref efi_PARTITION_TABLE_HEADER) + 88) != (int)num32)
											{
												goto IL_1BE4;
											}
											break;
										}
									}
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
								throw;
							}
						}
						num27 += 1U;
						num28 += 40;
						ptr58 = A_0 + 92;
						if (num27 >= (uint)((*(ptr58 + 4) - *ptr58) / 40))
						{
							goto Block_485;
						}
					}
					goto IL_2358;
					Block_482:
					goto IL_226F;
					Block_485:
					goto IL_2441;
					IL_1BE4:
					try
					{
						basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>;
						<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, 1);
						try
						{
							int _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z = <Module>.__unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z;
							EFI_PARTITION_TABLE_HEADER efi_PARTITION_TABLE_HEADER;
							uint num32;
							<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 16, (sbyte*)(&<Module>.??_C@_0ED@DFFBNCAK@CRC32?5mismatch?5of?5GPT?5partition@)), _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z), (uint)(*((ref efi_PARTITION_TABLE_HEADER) + 88))), (sbyte*)(&<Module>.??_C@_0CA@MGBAEBFD@?5Calculated?5CRC32?5of?5header?3?50x@)), _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z), num32), (sbyte*)(&<Module>.??_C@_0N@LPLCCEEL@?0?5filename?3?5@)), filename), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
							<Module>.fclose(ptr2);
							ffuReaderResult = 8;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>28;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr65 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>28);
							try
							{
								if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr65))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr65);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>28));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>28);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>28));
								throw;
							}
							*(int*)A_1 = ffuReaderResult;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr66 = A_1 + 4 / sizeof(FfuReaderResult);
							<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ptr66);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr66, (ref ffuReaderResult) + 4);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr66);
								throw;
							}
							num = 1U;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>));
							throw;
						}
						<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
						<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr67 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr67);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_1DA5:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27;
					try
					{
						basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2;
						<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2, 1);
						try
						{
							int _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z2 = <Module>.__unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z;
							EFI_PARTITION_TABLE_HEADER efi_PARTITION_TABLE_HEADER;
							uint num31;
							<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 16, (sbyte*)(&<Module>.??_C@_0DE@NAEAEFBF@CRC32?5mismatch?5of?5GPT?5header?$CB?5C@)), _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z2), (uint)(*((ref efi_PARTITION_TABLE_HEADER) + 16))), (sbyte*)(&<Module>.??_C@_0CA@MGBAEBFD@?5Calculated?5CRC32?5of?5header?3?50x@)), _unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z2), num31), (sbyte*)(&<Module>.??_C@_0N@LPLCCEEL@?0?5filename?3?5@)), filename), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
							_iobuf* ptr2;
							<Module>.fclose(ptr2);
							ffuReaderResult = 8;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>29;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr68 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>29);
							try
							{
								if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr68))
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr68);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>29));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>29);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>29));
								throw;
							}
							<Module>.FfuReaderResult.{ctor}(A_1, ref ffuReaderResult);
							num = 1U;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2));
							throw;
						}
						<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 104);
						<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 104);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr69 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr69);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_1F40:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27;
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult = 10;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>30;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr70 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>30, ref <Module>.??_C@_0CN@EGEGKNHO@Cannot?5read?5GPT?5partition?5entri@, filename);
						try
						{
							if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr70))
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr70);
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>30));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>30);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>30));
							throw;
						}
						<Module>.FfuReaderResult.{ctor}(A_1, ref ffuReaderResult);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr71 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr71);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_204C:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27;
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult = 10;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>31;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr72 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>31, ref <Module>.??_C@_0CN@BBLDFMCJ@Wrong?5size?5of?5GPT?5partition?5ent@, filename);
						try
						{
							if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr72))
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr72);
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>31));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>31);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>31));
							throw;
						}
						<Module>.FfuReaderResult.{ctor}(A_1, ref ffuReaderResult);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr73 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr73);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_2158:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27;
					try
					{
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult = 10;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>32;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr74 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>32, ref <Module>.??_C@_0DB@NLCGKNEK@Wrong?5number?5of?5GPT?5partition?5e@, filename);
						try
						{
							if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr74))
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr74);
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>32));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>32);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>32));
							throw;
						}
						*(int*)A_1 = ffuReaderResult;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>27));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr75 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr75);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_226F:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 10;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>33;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr76 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>33, ref <Module>.??_C@_0CE@CGHHDHDD@Read?5table?5header?5failed?0?5filen@, filename);
					try
					{
						if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr76))
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr76);
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>33));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>33);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>33));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr77 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr77);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_2358:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					<Module>.fclose(ptr2);
					ffuReaderResult = 10;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>34;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr78 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>34, ref <Module>.??_C@_0DE@KMDNEMNG@Seek?5to?5the?5GPT?5header?5?$CL?5MBR?5si@, filename);
					try
					{
						if ((ref ffuReaderResult) + 4 != (ref *(FfuReaderResult*)ptr78))
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents((ref ffuReaderResult) + 4, ptr78);
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>34));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>34);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>34));
						throw;
					}
					*(int*)A_1 = ffuReaderResult;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr79 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr79);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			IL_2441:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					_iobuf* ptr2;
					<Module>._fseeki64(ptr2, 0L, 0);
					<Module>.FfuReader.readGpt(A_0);
					byte* ptr80 = <Module>.new[](10485760U);
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>35;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr81 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>35, ref <Module>.??_C@_04MIKOFOFJ@SBL1@);
					<Module>.FfuReader.readRkh(A_0, ptr2, maxBlockSizeInBytes, ptr81, ptr80);
					uint num33 = (uint)(*(int*)(ptr80 + 10240 + 44) - *(int*)(ptr80 + 10240 + 24) + 10240);
					if (*(int*)(ptr80 + 10240 + 4) == 1943474228 && *(int*)(ptr80 + 10240 + 40) == 256)
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(A_0 + 28);
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0 + 28, ptr80 + num33 + 305, 64);
					}
					else
					{
						num33 = (uint)(*(int*)(ptr80 + 44) - *(int*)(ptr80 + 24));
						if (*(int*)(ptr80 + 4) == 1943474228 && *(int*)(ptr80 + 40) == 256)
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(A_0 + 28);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0 + 28, ptr80 + num33 + 305, 64);
						}
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>36;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr82 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>36, ref <Module>.??_C@_04DAEIOFGI@UEFI@);
					<Module>.FfuReader.readRkh(A_0, ptr2, maxBlockSizeInBytes, ptr82, ptr80);
					uint num34 = (uint)(*(int*)(ptr80 + 12) - *(int*)(ptr80 + 32) - 256 + 517);
					if (*(int*)(ptr80 + 24) == 256)
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(A_0 + 52);
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0 + 52, num34 + ptr80, 64);
					}
					<Module>.delete[]((void*)ptr80);
					if (true == dumpGpt)
					{
						<Module>._fseeki64(ptr2, 0L, 0);
						<Module>.FfuReader.DumpGpt(A_0, ptr2);
					}
					if (true == dumpPartitions)
					{
						<Module>._fseeki64(ptr2, 0L, 0);
						<Module>.FfuReader.DumpPartitions(A_0, ptr2, maxBlockSizeInBytes);
					}
					<Module>.FfuReader.checkDppPartition(A_0);
					<Module>.fclose(ptr2);
					ffuReaderResult = 0;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ref <Module>.??_C@_00CNPNBAHC@@);
					<Module>.FfuReader.trace(A_0, (sbyte*)(&<Module>.??_C@_0P@FGKADOIP@readFfu?$CI?$CJ?5End?4@));
					<Module>.FfuReaderResult.{ctor}(A_1, ref ffuReaderResult);
					num = 1U;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				<Module>.FfuReaderResult.{dtor}(ref ffuReaderResult);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(filename);
			return A_1;
			IL_262B:
			try
			{
				FfuReaderResult ffuReaderResult;
				try
				{
					basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3;
					<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3, 1);
					try
					{
						StoreHeader storeHeader;
						<Module>.std.operator<<<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 16, (sbyte*)(&<Module>.??_C@_0DN@EDLJMDMN@The?5FFU?5has?5wrong?5version?0?5must@)), *((ref storeHeader) + 8)), (sbyte*)(&<Module>.??_C@_01LFCBOECM@?4@)), *((ref storeHeader) + 10)), (sbyte*)(&<Module>.??_C@_0N@LPLCCEEL@?0?5filename?3?5@)), filename);
						_iobuf* ptr2;
						<Module>.fclose(ptr2);
						ffuReaderResult = 3;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>37;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr83 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>37);
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=((ref ffuReaderResult) + 4, ptr83);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>37));
							throw;
						}
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>37);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>37));
							throw;
						}
						*(int*)A_1 = ffuReaderResult;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult) + 4);
						num = 1U;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 104);
					<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 104);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
					throw;
				}
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr84 = (ref ffuReaderResult) + 4;
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr84);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			ptr85 = A_1;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return ptr85;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00007418 File Offset: 0x00006818
	internal unsafe static FfuReaderResult* FfuReader.readPartImage(FfuReader* A_0, FfuReaderResult* A_1, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* filename, uint maxBlockSizeInBytes)
	{
		try
		{
			uint num = 0U;
			try
			{
				FfuReaderResult* ptr = A_1 + 4 / sizeof(FfuReaderResult);
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ptr);
				num = 1U;
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
				basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, filename, ref <Module>.??_C@_0BM@GBJGKMED@?5image?5parsed?5successfully?4@);
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = ptr;
					if (ptr3 != ptr2)
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr3);
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr3, ptr2);
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
					throw;
				}
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
					throw;
				}
				*(int*)A_1 = 0;
				sbyte* ptr4 = (sbyte*)filename;
				if (((16 <= *(int*)(filename + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>))) ? 1 : 0) != 0)
				{
					ptr4 = *(int*)filename;
				}
				_iobuf* ptr5 = <Module>.fopen(ptr4, (sbyte*)(&<Module>.??_C@_02JDPG@rb@));
				if (ptr5 == null)
				{
					*(int*)A_1 = 2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr6 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref <Module>.??_C@_0CA@JFGCLPEA@Could?5not?5open?5file?0?5filename?3?5@, filename);
					try
					{
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr7 = ptr;
						if (ptr7 != ptr6)
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr7);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr7, ptr6);
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
				}
				else
				{
					uint num2 = 0U;
					FfuReaderResult ffuReaderResult;
					FfuReaderResult* ptr8 = <Module>.FfuReader.readImageId(A_0, &ffuReaderResult, ptr5, ref num2);
					try
					{
						*(int*)A_1 = *(int*)ptr8;
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(ptr, ptr8 + 4 / sizeof(FfuReaderResult));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr9 = (ref ffuReaderResult) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr9);
						throw;
					}
					if (*(int*)A_1 == 0)
					{
						if (<Module>.FfuReader.isValidBootImage(A_0, ref num2) == null)
						{
							*(int*)A_1 = 12;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr10 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_0CN@CDANHBKI@The?5file?5is?5not?5valid?5boot?5imag@, filename);
							try
							{
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr11 = ptr;
								if (ptr11 != ptr10)
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr11);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr11, ptr10);
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
								throw;
							}
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
								throw;
							}
						}
						else
						{
							byte* ptr12 = <Module>.new[](10485760U);
							if (2219564241U == num2)
							{
								uint num3 = <Module>.fread((void*)ptr12, 1U, 10320U, ptr5);
								uint num4 = (uint)(*(int*)(ptr12 + 10240 + 44) - *(int*)(ptr12 + 10240 + 24) + 10240);
								if (*(int*)(ptr12 + 10240 + 4) == 1943474228 && *(int*)(ptr12 + 10240 + 40) == 256)
								{
									<Module>.fread((void*)(num3 + ptr12), 1U, num4 - num3 + 369U, ptr5);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(A_0 + 28);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0 + 28, ptr12 + num4 + 305, 64);
								}
								else
								{
									uint num5 = (uint)(*(int*)(ptr12 + 44) - *(int*)(ptr12 + 24));
									if (*(int*)(ptr12 + 4) == 1943474228 && *(int*)(ptr12 + 40) == 256)
									{
										<Module>.fread((void*)(num3 + ptr12), 1U, num5 - num3 + 369U, ptr5);
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(A_0 + 28);
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0 + 28, ptr12 + num5 + 305, 64);
									}
								}
							}
							else
							{
								uint num6 = <Module>.fread((void*)ptr12, 1U, 36U, ptr5);
								uint num7 = (uint)(*(int*)(ptr12 + 12) - *(int*)(ptr12 + 32) - 256 + 517);
								if (*(int*)(ptr12 + 24) == 256)
								{
									<Module>.fread((void*)(num6 + ptr12), 1U, num7 - num6 + 64U, ptr5);
									FfuReader* ptr13 = A_0 + 28;
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(ptr13);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr13, num7 + ptr12, 64);
								}
							}
							if (ptr12 != null)
							{
								<Module>.delete[]((void*)ptr12);
							}
						}
					}
					<Module>.fclose(ptr5);
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000B4A4 File Offset: 0x0000A8A4
	internal unsafe static void FfuReader.readGpt(FfuReader* A_0)
	{
		FfuReader.partition partition;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref partition);
		try
		{
			*((ref partition) + 56) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&partition));
			throw;
		}
		try
		{
			uint num = (uint)(*(A_0 + 18340));
			if (0U != num)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BB@DFMAPPBF@readGpt?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
			}
			uint num2 = 0U;
			if (0 < *(A_0 + 196))
			{
				FfuReader* ptr = A_0 + 692;
				while (*(ptr - 8) != 0)
				{
					int num3 = 0;
					FfuReader* ptr2 = ptr + 2;
					FfuReader* ptr3 = ptr;
					FfuReader* ptr4 = ptr - 2;
					FfuReader* ptr5 = ptr - 4;
					FfuReader* ptr6 = ptr - 6;
					FfuReader* ptr7 = ptr - 8;
					$ArrayType$$$BY0EI@D $ArrayType$$$BY0EI@D;
					do
					{
						short num4 = *ptr7;
						if (num4 == 0)
						{
							break;
						}
						*(num3 + (ref $ArrayType$$$BY0EI@D)) = (byte)num4;
						short num5 = *ptr6;
						if (num5 == 0)
						{
							goto IL_15F;
						}
						*(num3 + ((ref $ArrayType$$$BY0EI@D) + 1)) = (byte)num5;
						short num6 = *ptr5;
						if (num6 == 0)
						{
							goto IL_159;
						}
						*(num3 + ((ref $ArrayType$$$BY0EI@D) + 2)) = (byte)num6;
						short num7 = *ptr4;
						if (num7 == 0)
						{
							goto IL_153;
						}
						*(num3 + ((ref $ArrayType$$$BY0EI@D) + 3)) = (byte)num7;
						short num8 = *ptr3;
						if (num8 == 0)
						{
							goto IL_14D;
						}
						*(num3 + ((ref $ArrayType$$$BY0EI@D) + 4)) = (byte)num8;
						short num9 = *ptr2;
						if (num9 == 0)
						{
							goto IL_147;
						}
						*(num3 + ((ref $ArrayType$$$BY0EI@D) + 5)) = (byte)num9;
						num3 += 6;
						ptr7 += 12;
						ptr6 += 12;
						ptr5 += 12;
						ptr4 += 12;
						ptr3 += 12;
						ptr2 += 12;
					}
					while (num3 < 36);
					IL_163:
					*(num3 + (ref $ArrayType$$$BY0EI@D)) = 0;
					sbyte* ptr8 = ref $ArrayType$$$BY0EI@D;
					if ($ArrayType$$$BY0EI@D != null)
					{
						do
						{
							ptr8++;
						}
						while (*ptr8 != 0);
					}
					int num10 = ptr8 - (ref $ArrayType$$$BY0EI@D);
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref partition, ref $ArrayType$$$BY0EI@D, num10);
					ulong num11 = (ulong)(*(ptr - 32));
					*((ref partition) + 24) = (long)num11;
					ulong num12 = (ulong)(*(ptr - 24));
					*((ref partition) + 32) = (long)num12;
					*((ref partition) + 40) = (long)(num12 - num11 + 1UL);
					*((ref partition) + 48) = *(ptr - 16);
					<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.emplace_back<struct\u0020FfuReader::partition\u0020const\u0020&>(A_0 + 17268, ref partition);
					num2 += 1U;
					ptr += 128;
					if (num2 >= (uint)(*(A_0 + 196)))
					{
						break;
					}
					continue;
					goto IL_163;
					IL_147:
					num3 += 5;
					goto IL_163;
					IL_14D:
					num3 += 4;
					goto IL_163;
					IL_153:
					num3 += 3;
					goto IL_163;
					IL_159:
					num3 += 2;
					goto IL_163;
					IL_15F:
					num3++;
					goto IL_163;
				}
			}
			sbyte* ptr9 = ref <Module>.??_C@_08KJPBNJGC@Overflow@;
			do
			{
				ptr9++;
			}
			while (*ptr9 != 0);
			int num13 = ptr9 - (ref <Module>.??_C@_08KJPBNJGC@Overflow@);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref partition, ref <Module>.??_C@_08KJPBNJGC@Overflow@, num13);
			<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.emplace_back<struct\u0020FfuReader::partition\u0020const\u0020&>(A_0 + 17268, ref partition);
			uint num14 = (uint)(*(A_0 + 18340));
			if (0U != num14)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num14, (sbyte*)(&<Module>.??_C@_0P@KKOFAEKD@readGpt?$CI?$CJ?5End?4@), (IntPtr)(*(*num14 + 4)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(FfuReader.partition.{dtor}), (void*)(&partition));
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref partition);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&partition));
			throw;
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000062F4 File Offset: 0x000056F4
	internal unsafe static void FfuReader.partition.{dtor}(FfuReader.partition* A_0)
	{
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000AA9C File Offset: 0x00009E9C
	internal unsafe static uint FfuReader.readDescriptors(FfuReader* A_0, _iobuf* fp, long fileOffset, uint maxBlockSizeInBytes, uint dwBlockSizeInBytes, uint writeDescriptorCount)
	{
		uint num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BJ@EAKABFJM@readDescriptors?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
		}
		FfuReader.WriteRequest writeRequest = 0L;
		*((ref writeRequest) + 8) = 0;
		*((ref writeRequest) + 16) = 0L;
		*((ref writeRequest) + 24) = 0;
		*((ref writeRequest) + 32) = 0L;
		uint num2 = 0U;
		if (0U < writeDescriptorCount)
		{
			for (;;)
			{
				IL_49:
				long num3 = <Module>._ftelli64(fp);
				DataBlock dataBlock;
				if (<Module>.fread((void*)(&dataBlock), 8U, 1U, fp) != 1)
				{
					return 0;
				}
				if (dataBlock > 0 && *((ref dataBlock) + 4) != 0)
				{
					num2 += 1U;
					BlockLocation blockLocation;
					while (1 == <Module>.fread((void*)(&blockLocation), 8U, 1U, fp))
					{
						if (*((ref writeRequest) + 8) == 0)
						{
							FfuReader.AccessMethod accessMethod = ((blockLocation == 0) ? ((FfuReader.AccessMethod)0) : ((FfuReader.AccessMethod)2));
							*((ref writeRequest) + 24) = (int)accessMethod;
							writeRequest = fileOffset;
							uint num4 = dwBlockSizeInBytes >> 9;
							*((ref writeRequest) + 16) = (long)((ulong)(num4 * (uint)(*((ref blockLocation) + 4))));
							*((ref writeRequest) + 8) = (int)(num4 * (uint)(*((ref dataBlock) + 4)));
							*((ref writeRequest) + 32) = num3;
							dataBlock--;
						}
						else
						{
							uint num4 = dwBlockSizeInBytes >> 9;
							uint num5 = (uint)(*((ref writeRequest) + 8) + (int)(num4 * (uint)(*((ref dataBlock) + 4))));
							if (num5 <= maxBlockSizeInBytes >> 9)
							{
								FfuReader.AccessMethod accessMethod2 = ((blockLocation == 0) ? ((FfuReader.AccessMethod)0) : ((FfuReader.AccessMethod)2));
								if (*((ref writeRequest) + 24) == (int)accessMethod2 && (ulong)(*((ref writeRequest) + 8)) + (ulong)(*((ref writeRequest) + 16)) == (ulong)(num4 * (uint)(*((ref blockLocation) + 4))))
								{
									*((ref writeRequest) + 8) = (int)num5;
									dataBlock--;
									goto IL_15B;
								}
							}
							<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.emplace_back<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(A_0 + 92, ref writeRequest);
							*((ref writeRequest) + 8) = 0;
							if (0 != <Module>._fseeki64(fp, -8L, 1))
							{
								return 0;
							}
						}
						IL_15B:
						if (dataBlock <= 0)
						{
							fileOffset += (long)((ulong)(*((ref dataBlock) + 4) * (int)dwBlockSizeInBytes));
							if (num2 >= writeDescriptorCount)
							{
								goto Block_13;
							}
							goto IL_49;
						}
					}
					return 0;
				}
				return 0;
			}
			Block_13:
			if (*((ref writeRequest) + 8) != 0)
			{
				<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.emplace_back<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(A_0 + 92, ref writeRequest);
			}
		}
		IL_195:
		num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BH@ODFOOBCN@readDescriptors?$CI?$CJ?5End?4@), (IntPtr)(*(*num + 4)));
		}
		return 1;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000376C File Offset: 0x00002B6C
	internal unsafe static int FfuReader.calculateSectorIndexInPartition(FfuReader* A_0, EFI_PARTITION_ENTRY* partitionEntries, FfuReader.WriteRequest* writeRequest, uint iSectorInBlock, uint* indexSectorInPartition)
	{
		ulong num = (ulong)iSectorInBlock;
		ulong num2 = (ulong)(*(writeRequest + 16));
		ulong num3 = num2 + num;
		if (num3 < (ulong)(*(partitionEntries + 32)))
		{
			*indexSectorInPartition = (int)iSectorInBlock;
			return -1;
		}
		uint num4 = 0U;
		EFI_PARTITION_ENTRY* ptr = partitionEntries + 32;
		while (*(ptr + 24) != 0)
		{
			if (num3 >= (ulong)(*ptr) && num3 <= (ulong)(*(ptr + 8)))
			{
				*indexSectorInPartition = (int)((uint)(num2 - (ulong)(*(partitionEntries + num4 * 128U + 32)) + num));
				return num4;
			}
			num4 += 1U;
			ptr += 128;
			if (num4 >= 128U)
			{
				return 1;
			}
		}
		*indexSectorInPartition = (int)((uint)(num2 - (ulong)(*(partitionEntries + num4 * 128U - 88)) + num + ulong.MaxValue));
		return num4;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000068CC File Offset: 0x00005CCC
	internal unsafe static uint FfuReader.readRkh(FfuReader* A_0, _iobuf* fp, uint maxBlockSizeInBytes, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* name, byte* pDumpPartition)
	{
		try
		{
			uint num = (uint)(*(A_0 + 18340));
			if (0U != num)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BB@EFIOPFCC@readRkh?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
			}
			byte* ptr = <Module>.new[](maxBlockSizeInBytes);
			int i = 0;
			uint num2 = 0U;
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_init(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			try
			{
				uint num3 = 0U;
				_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr2 = A_0 + 92;
				if (0 >= (*(ptr2 + 4) - *ptr2) / 40)
				{
					goto IL_2AF;
				}
				int num4 = 0;
				while (i < 1)
				{
					bool flag = false;
					uint num5 = 0U;
					_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr3 = A_0 + 92;
					if (0 < *(*ptr3 + num4 + 8))
					{
						FfuReader* ptr4 = A_0 + 628;
						byte* ptr5 = ptr;
						for (;;)
						{
							num2 = 0U;
							FfuReader.WriteRequest* ptr6 = *(A_0 + 92) + num4;
							int num6 = <Module>.FfuReader.calculateSectorIndexInPartition(A_0, ptr4, ptr6, num5, ref num2);
							sbyte* ptr7;
							if (num6 == -1)
							{
								ptr7 = (sbyte*)(&<Module>.??_C@_03GKBDPGIH@GPT@);
							}
							else
							{
								_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr8 = A_0 + 17268;
								FfuReader.partition* ptr9 = num6 * 64 + *ptr8;
								sbyte* ptr10 = ptr9;
								if (((16 <= *(ptr9 + 20)) ? 1 : 0) != 0)
								{
									ptr10 = *ptr9;
								}
								ptr7 = ptr10;
							}
							sbyte* ptr11 = ptr7;
							if (*(sbyte*)ptr7 != 0)
							{
								do
								{
									ptr11 += 1 / sizeof(sbyte);
								}
								while (*(sbyte*)ptr11 != 0);
							}
							int num7 = (int)(ptr11 - ptr7);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ptr7, num7);
							if (flag)
							{
								goto IL_176;
							}
							if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, name) != null)
							{
								_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr12 = A_0 + 92;
								FfuReader.WriteRequest* ptr13 = num4 + *ptr12;
								if (0 != <Module>._fseeki64(fp, *ptr13, 0))
								{
									break;
								}
								FfuReader.WriteRequest* ptr14 = *(A_0 + 92) + num4;
								int num8 = <Module>.fread((void*)ptr, 512U, (uint)(*(ptr14 + 8)), fp);
								_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr15 = A_0 + 92;
								if (num8 == *(num4 + *ptr15 + 8))
								{
									flag = true;
									goto IL_176;
								}
								goto IL_202;
							}
							IL_1B5:
							num5 += 1U;
							ptr5 += 512;
							ptr3 = A_0 + 92;
							if (num5 >= (uint)(*(*ptr3 + num4 + 8)))
							{
								goto IL_1DA;
							}
							continue;
							IL_176:
							if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_08KJPBNJGC@Overflow@) == 0) ? 1 : 0) == 0)
							{
								goto IL_1B5;
							}
							if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, name) == 0) ? 1 : 0) != 0)
							{
								flag = false;
								i++;
								goto IL_1B5;
							}
							cpblk(num2 * 512U + pDumpPartition, ptr5, 512);
							goto IL_1B5;
						}
						goto IL_258;
						IL_202:
						<Module>.delete[]((void*)ptr);
						goto IL_219;
					}
					IL_1DA:
					num3 += 1U;
					num4 += 40;
					ptr2 = A_0 + 92;
					if (num3 >= (uint)((*(ptr2 + 4) - *ptr2) / 40))
					{
						break;
					}
				}
				goto IL_2AF;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			IL_219:
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)name);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(name);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)name);
			throw;
		}
		return 1;
		IL_258:
		try
		{
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			try
			{
				byte* ptr;
				<Module>.delete[]((void*)ptr);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)name);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(name);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)name);
			throw;
		}
		return 1;
		IL_2AF:
		try
		{
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			try
			{
				byte* ptr;
				<Module>.delete[]((void*)ptr);
				uint num = (uint)(*(A_0 + 18340));
				if (0U != num)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0P@OAPOKHFA@readRkh?$CI?$CJ?5End?4@), (IntPtr)(*(*num + 4)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)name);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(name);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)name);
			throw;
		}
		return 0;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000788C File Offset: 0x00006C8C
	internal unsafe static FfuReaderResult* FfuReader.readSecurityHdrAndCheckValidity(FfuReader* A_0, FfuReaderResult* A_1, _iobuf* fp)
	{
		try
		{
			uint num = 0U;
			FfuReaderResult* ptr = A_1 + 4 / sizeof(FfuReaderResult);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ptr);
			num = 1U;
			*(int*)A_1 = 0;
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr, ref <Module>.??_C@_00CNPNBAHC@@, 0);
			uint num2 = (uint)(*(A_0 + 18340));
			if (0U != num2)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num2, (sbyte*)(&<Module>.??_C@_0CJ@KHIPEPBA@readSecurityHdrAndCheckValidity@), (IntPtr)(*(*num2 + 4)));
			}
			if (1 != <Module>.fread(A_0 + 17284, 32U, 1U, fp))
			{
				*(int*)A_1 = 4;
				sbyte* ptr2 = ref <Module>.??_C@_0CO@NPKMMIFF@Corrupted?5FFU?0?5could?5not?5read?5s@;
				do
				{
					ptr2++;
				}
				while (*ptr2 != 0);
				int num3 = ptr2 - (ref <Module>.??_C@_0CO@NPKMMIFF@Corrupted?5FFU?0?5could?5not?5read?5s@);
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr, ref <Module>.??_C@_0CO@NPKMMIFF@Corrupted?5FFU?0?5could?5not?5read?5s@, num3);
			}
			else
			{
				uint num4 = 12U;
				sbyte* ptr3 = ref <Module>.??_C@_0N@LJGGPJIJ@SignedImage?5@;
				FfuReader* ptr4 = A_0 + 17288;
				byte b = *ptr4;
				byte b2 = 83;
				if (b >= 83)
				{
					while (b <= b2)
					{
						if (num4 != 1U)
						{
							num4 -= 1U;
							ptr4++;
							ptr3++;
							b = *ptr4;
							b2 = *ptr3;
							if (b < b2)
							{
								break;
							}
						}
						else
						{
							if (32 != *(A_0 + 17284))
							{
								basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>;
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, 1);
								try
								{
									<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 16, (sbyte*)(&<Module>.??_C@_0BG@MMHJALCK@Invalid?5struct?5size?3?5@)), (uint)(*(A_0 + 17284))), (sbyte*)(&<Module>.??_C@_0BC@LJPEICOC@?4?5Expected?5size?3?5@)), 32U), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr5 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(ptr, ptr5);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
										throw;
									}
									*(int*)A_1 = 4;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>));
									throw;
								}
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
								<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>) + 104);
								goto IL_48C;
							}
							if (32780 != *(A_0 + 17304))
							{
								basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2;
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2, 1);
								try
								{
									<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 16, (sbyte*)(&<Module>.??_C@_0BI@NPNBLBND@Unsupported?5algorithm?3?5@)), (uint)(*(A_0 + 17304))), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr6 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(ptr, ptr6);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
										throw;
									}
									*(int*)A_1 = 4;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2));
									throw;
								}
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 104);
								<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 104);
								goto IL_48C;
							}
							if (0 == *(A_0 + 17300))
							{
								basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3;
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3, 1);
								try
								{
									<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 16, (sbyte*)(&<Module>.??_C@_0BF@CHEBECHJ@Invalid?5chunk?5size?3?5@)), (uint)(*(A_0 + 17300))), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr7 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
									try
									{
										basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = ptr;
										if (ptr8 != ptr7)
										{
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr8);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr8, ptr7);
										}
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
										throw;
									}
									*(int*)A_1 = 4;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3));
									throw;
								}
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 104);
								<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 104);
								goto IL_48C;
							}
							if (0 == *(A_0 + 17308))
							{
								basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4;
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4, 1);
								try
								{
									<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4) + 16, (sbyte*)(&<Module>.??_C@_0BH@EMBEMOOJ@Invalid?5catalog?5size?3?5@)), (uint)(*(A_0 + 17308))), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4;
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr9 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
									try
									{
										basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr10 = ptr;
										if (ptr10 != ptr9)
										{
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr10);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr10, ptr9);
										}
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
										throw;
									}
									*(int*)A_1 = 4;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4));
									throw;
								}
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4) + 104);
								<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>4) + 104);
								goto IL_48C;
							}
							uint num5 = (uint)(*(A_0 + 17312));
							if (0U == num5 || (num5 & 31U) != 0U)
							{
								basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020> basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5;
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5, 1);
								try
								{
									<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.<<(<Module>.std.operator<<<struct\u0020std::char_traits<char>\u0020>((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5) + 16, (sbyte*)(&<Module>.??_C@_0BK@HOBHKCKM@Invalid?5hash?5table?5size?3?5@)), (uint)(*(A_0 + 17312))), <Module>.__unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z);
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
									basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr11 = <Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5, &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
									try
									{
										basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr12 = ptr;
										if (ptr12 != ptr11)
										{
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr12);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr12, ptr11);
										}
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
										throw;
									}
									*(int*)A_1 = 4;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor), (void*)(&basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5));
									throw;
								}
								<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5) + 104);
								<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_stringstream<char,std::char_traits<char>,std::allocator<char>_u0020>5) + 104);
								goto IL_48C;
							}
							goto IL_48C;
						}
					}
				}
				*(int*)A_1 = 4;
				sbyte* ptr13 = ref <Module>.??_C@_0CP@MDFGNDNN@Corrupted?5FFU?0?5image?5header?5sig@;
				do
				{
					ptr13++;
				}
				while (*ptr13 != 0);
				int num6 = ptr13 - (ref <Module>.??_C@_0CP@MDFGNDNN@Corrupted?5FFU?0?5image?5header?5sig@);
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr, ref <Module>.??_C@_0CP@MDFGNDNN@Corrupted?5FFU?0?5image?5header?5sig@, num6);
			}
			IL_48C:
			uint num7 = (uint)(*(A_0 + 18340));
			if (0U != num7)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num7, (sbyte*)(&<Module>.??_C@_0CH@EGAMGHCG@readSecurityHdrAndCheckValidity@), (IntPtr)(*(*num7 + 4)));
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000381C File Offset: 0x00002C1C
	internal unsafe static void FfuReader.crc32_gentab(FfuReader* A_0)
	{
		uint num = 0U;
		FfuReader* ptr = A_0 + 17316;
		do
		{
			uint num2;
			if ((num & 1U) != 0U)
			{
				num2 = (num >> 1) ^ 3988292384U;
			}
			else
			{
				num2 = num >> 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			if ((num2 & 1U) != 0U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
			}
			else
			{
				num2 >>= 1;
			}
			*ptr = (int)num2;
			num += 1U;
			ptr += 4;
		}
		while (num < 256U);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000038F8 File Offset: 0x00002CF8
	internal unsafe static uint FfuReader.calculate_crc32(FfuReader* A_0, void* data, uint size)
	{
		<Module>.FfuReader.crc32_gentab(A_0);
		uint num = uint.MaxValue;
		uint num2 = 0U;
		if (0U < size)
		{
			do
			{
				num = (uint)(*((((uint)(*(byte*)(num2 / (uint)sizeof(void) + data)) ^ num) & 255U) * 4U + A_0 + 17316) ^ (int)(num >> 8));
				num2 += 1U;
			}
			while (num2 < size);
		}
		return ~num;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00007EF4 File Offset: 0x000072F4
	internal unsafe static uint FfuReader.DumpPartitions(FfuReader* A_0, _iobuf* fp, uint maxBlockSizeInBytes)
	{
		uint num = 0U;
		uint num2 = (uint)(*(A_0 + 18340));
		if (0U != num2)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num2, (sbyte*)(&<Module>.??_C@_0BI@FFBKAKPK@DumpPartitions?$CI?$CJ?5Start?4@), (IntPtr)(*(*num2 + 4)));
		}
		byte* ptr = <Module>.new[](maxBlockSizeInBytes);
		int num3 = 0;
		uint num4 = 0U;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
		<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_init(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		uint num15;
		try
		{
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
			<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_init(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
				throw;
			}
			try
			{
				basic_ofstream<char,std::char_traits<char>\u0020> basic_ofstream<char,std::char_traits<char>_u0020>;
				<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{ctor}(ref basic_ofstream<char,std::char_traits<char>_u0020>, 1);
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
					<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_init(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					try
					{
						uint num5 = 0U;
						_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr2 = A_0 + 92;
						if (0 < (*(ptr2 + 4) - *ptr2) / 40)
						{
							int num6 = 0;
							for (;;)
							{
								bool flag = false;
								uint num7 = 0U;
								_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr3 = A_0 + 92;
								if (0 < *(num6 + *ptr3 + 8))
								{
									FfuReader* ptr4 = A_0 + 628;
									byte* ptr5 = ptr;
									for (;;)
									{
										num4 = 0U;
										_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr6 = A_0 + 92;
										FfuReader.WriteRequest* ptr7 = num6 + *ptr6;
										num3 = <Module>.FfuReader.calculateSectorIndexInPartition(A_0, ptr4, ptr7, num7, ref num4);
										basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4;
										if (num3 == -1)
										{
											basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4, ref <Module>.??_C@_03GKBDPGIH@GPT@);
											try
											{
												num |= 1U;
												goto IL_1A5;
											}
											catch
											{
												if ((num & 1U) != 0U)
												{
													num &= 4294967294U;
													<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
												}
												throw;
											}
											goto IL_130;
										}
										goto IL_130;
										IL_1A5:
										basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
										try
										{
											try
											{
												basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8;
												<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ptr8);
											}
											catch
											{
												if ((num & 2U) != 0U)
												{
													num &= 4294967293U;
													<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
												}
												throw;
											}
											if ((num & 2U) != 0U)
											{
												num &= 4294967293U;
												try
												{
													<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
												}
												catch
												{
													<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
													throw;
												}
											}
										}
										catch
										{
											if ((num & 1U) != 0U)
											{
												num &= 4294967294U;
												<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
											}
											throw;
										}
										if ((num & 1U) != 0U)
										{
											num &= 4294967294U;
											try
											{
												<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
											}
											catch
											{
												<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
												throw;
											}
										}
										if (*((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 80) == 0)
										{
											sbyte* ptr9 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
											sbyte* ptr10 = ptr9;
											if (*(sbyte*)ptr9 != 0)
											{
												do
												{
													ptr10 += 1 / sizeof(sbyte);
												}
												while (*(sbyte*)ptr10 != 0);
											}
											int num8 = (int)(ptr10 - ptr9);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ptr9, num8);
											sbyte* ptr11 = ref <Module>.??_C@_04GKHLBAIJ@?4bin@;
											do
											{
												ptr11++;
											}
											while (*ptr11 != 0);
											int num9 = ptr11 - (ref <Module>.??_C@_04GKHLBAIJ@?4bin@);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_04GKHLBAIJ@?4bin@, num9);
											sbyte* ptr12 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3 : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
											<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.open(ref basic_ofstream<char,std::char_traits<char>_u0020>, ptr12, 34, 64);
											sbyte* ptr13 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
											sbyte* ptr14 = ptr13;
											if (*(sbyte*)ptr13 != 0)
											{
												do
												{
													ptr14 += 1 / sizeof(sbyte);
												}
												while (*(sbyte*)ptr14 != 0);
											}
											int num10 = (int)(ptr14 - ptr13);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ptr13, num10);
										}
										if (flag)
										{
											goto IL_37D;
										}
										if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_08KJPBNJGC@Overflow@) == 0) ? 1 : 0) != 0)
										{
											FfuReader.WriteRequest* ptr15 = *(A_0 + 92) + num6;
											if (0 != <Module>._fseeki64(fp, *ptr15, 0))
											{
												goto IL_51A;
											}
											_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr16 = A_0 + 92;
											FfuReader.WriteRequest* ptr17 = num6 + *ptr16;
											int num11 = <Module>.fread((void*)ptr, 512U, (uint)(*(ptr17 + 8)), fp);
											_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr18 = A_0 + 92;
											if (num11 == *(num6 + *ptr18 + 8))
											{
												flag = true;
												goto IL_37D;
											}
											goto IL_511;
										}
										IL_4C0:
										num7 += 1U;
										ptr5 += 512;
										ptr3 = A_0 + 92;
										if (num7 >= (uint)(*(num6 + *ptr3 + 8)))
										{
											break;
										}
										continue;
										IL_37D:
										if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_08KJPBNJGC@Overflow@) == 0) ? 1 : 0) == 0)
										{
											goto IL_4C0;
										}
										if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2) == 0) ? 1 : 0) != 0)
										{
											flag = false;
											if (<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.close((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 4) == null)
											{
												<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(basic_ofstream<char,std::char_traits<char>_u0020> + 4) + (ref basic_ofstream<char,std::char_traits<char>_u0020>), 2, false);
											}
											sbyte* ptr19 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
											sbyte* ptr20 = ptr19;
											if (*(sbyte*)ptr19 != 0)
											{
												do
												{
													ptr20 += 1 / sizeof(sbyte);
												}
												while (*(sbyte*)ptr20 != 0);
											}
											int num12 = (int)(ptr20 - ptr19);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ptr19, num12);
											sbyte* ptr21 = ref <Module>.??_C@_04GKHLBAIJ@?4bin@;
											do
											{
												ptr21++;
											}
											while (*ptr21 != 0);
											int num13 = ptr21 - (ref <Module>.??_C@_04GKHLBAIJ@?4bin@);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_04GKHLBAIJ@?4bin@, num13);
											sbyte* ptr22 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3 : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
											<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.open(ref basic_ofstream<char,std::char_traits<char>_u0020>, ptr22, 34, 64);
											sbyte* ptr23 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
											sbyte* ptr24 = ptr23;
											if (*(sbyte*)ptr23 != 0)
											{
												do
												{
													ptr24 += 1 / sizeof(sbyte);
												}
												while (*(sbyte*)ptr24 != 0);
											}
											int num14 = (int)(ptr24 - ptr23);
											<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ptr23, num14);
											<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.write(ref basic_ofstream<char,std::char_traits<char>_u0020>, (sbyte*)ptr5, 512L);
											goto IL_4C0;
										}
										<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.write(ref basic_ofstream<char,std::char_traits<char>_u0020>, (sbyte*)ptr5, 512L);
										goto IL_4C0;
										IL_130:
										_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr25 = A_0 + 17268;
										FfuReader.partition* ptr26 = num3 * 64 + *ptr25;
										_One_then_variadic_args_t one_then_variadic_args_t;
										allocator<char> allocator<char>;
										<Module>.std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{ctor}<class\u0020std::allocator<char>\u0020>(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5, one_then_variadic_args_t, ref allocator<char>);
										try
										{
											try
											{
												<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5, ptr26);
											}
											catch
											{
												<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
												throw;
											}
											basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = &basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
											try
											{
												num |= 2U;
											}
											catch
											{
												if ((num & 2U) != 0U)
												{
													num &= 4294967293U;
													<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
												}
												throw;
											}
										}
										catch
										{
											if ((num & 1U) != 0U)
											{
												num &= 4294967294U;
												<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
											}
											throw;
										}
										goto IL_1A5;
									}
								}
								num5 += 1U;
								num6 += 40;
								num3++;
								ptr2 = A_0 + 92;
								if (num5 >= (uint)((*(ptr2 + 4) - *ptr2) / 40))
								{
									goto IL_526;
								}
							}
							IL_511:
							<Module>.delete[]((void*)ptr);
							goto IL_521;
							IL_51A:
							<Module>.delete[]((void*)ptr);
							IL_521:
							num15 = 1U;
							goto IL_54D;
						}
						IL_526:
						<Module>.delete[]((void*)ptr);
						num2 = (uint)(*(A_0 + 18340));
						if (0U != num2)
						{
							calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num2, (sbyte*)(&<Module>.??_C@_0BG@GBPPGJLC@DumpPartitions?$CI?$CJ?5End?4@), (IntPtr)(*(*num2 + 4)));
						}
						num15 = 0U;
						IL_54D:;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ofstream<char,std::char_traits<char>\u0020>.__vbaseDtor), (void*)(&basic_ofstream<char,std::char_traits<char>_u0020>));
					throw;
				}
				<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{dtor}((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 104);
				<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 104);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		return num15;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000058C0 File Offset: 0x00004CC0
	internal unsafe static void std.basic_ofstream<char,std::char_traits<char>\u0020>.__vbaseDtor(basic_ofstream<char,std::char_traits<char>\u0020>* A_0)
	{
		basic_ofstream<char,std::char_traits<char>\u0020>* ptr = A_0 + 104;
		<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{dtor}(ptr);
		<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}(ptr);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000086A8 File Offset: 0x00007AA8
	internal unsafe static uint FfuReader.DumpGpt(FfuReader* A_0, _iobuf* fp)
	{
		uint num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BB@HHMKMHJH@DumpGpt?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
		}
		int num2 = 0;
		uint num3 = 0U;
		_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr = A_0 + 92;
		if (0 < (*(ptr + 4) - *ptr) / 40)
		{
			int num4 = 0;
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
			for (;;)
			{
				_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr2 = A_0 + 92;
				if (*(num4 + *ptr2 + 16) == 0L)
				{
					$ArrayType$$$BY0CAA@E $ArrayType$$$BY0CAA@E;
					initblk(ref $ArrayType$$$BY0CAA@E, 0, 512);
					_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr3 = A_0 + 92;
					FfuReader.WriteRequest* ptr4 = num4 + *ptr3;
					if (0 != <Module>._fseeki64(fp, *ptr4, 0))
					{
						goto IL_3BA;
					}
					if (1 != <Module>.fread((void*)(&$ArrayType$$$BY0CAA@E), 512U, 1U, fp))
					{
						goto IL_3B0;
					}
					EFI_PARTITION_TABLE_HEADER efi_PARTITION_TABLE_HEADER;
					if (1 != <Module>.fread((void*)(&efi_PARTITION_TABLE_HEADER), 512U, 1U, fp))
					{
						goto IL_3A6;
					}
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_08BOGKMBPC@EFI?5PART@);
					try
					{
						sbyte* ptr5 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
						sbyte* ptr6 = ptr5;
						EFI_PARTITION_TABLE_HEADER* ptr7 = &efi_PARTITION_TABLE_HEADER;
						byte b = efi_PARTITION_TABLE_HEADER;
						byte b2 = *(byte*)ptr5;
						if (efi_PARTITION_TABLE_HEADER >= b2)
						{
							while (b <= b2)
							{
								if (b == 0)
								{
									$ArrayType$$$BY0BJ@D $ArrayType$$$BY0BJ@D;
									initblk(ref $ArrayType$$$BY0BJ@D, 0, 25);
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref <Module>.??_C@_03GKBDPGIH@GPT@);
									try
									{
										<Module>.sprintf(ref $ArrayType$$$BY0BJ@D, ref <Module>.??_C@_06DPNFGDJN@?$CFd?4bin@, __arglist(num2));
										sbyte* ptr8 = ref $ArrayType$$$BY0BJ@D;
										if ($ArrayType$$$BY0BJ@D != null)
										{
											do
											{
												ptr8++;
											}
											while (*ptr8 != 0);
										}
										int num5 = ptr8 - (ref $ArrayType$$$BY0BJ@D);
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref $ArrayType$$$BY0BJ@D, num5);
										num2++;
										if (128 != *((ref efi_PARTITION_TABLE_HEADER) + 80))
										{
											goto IL_32A;
										}
										if (128 != *((ref efi_PARTITION_TABLE_HEADER) + 84))
										{
											goto IL_300;
										}
										$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@ $ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@;
										if (128 != <Module>.fread((void*)(&$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@), 128U, 128U, fp))
										{
											goto IL_2D6;
										}
										basic_ofstream<char,std::char_traits<char>\u0020> basic_ofstream<char,std::char_traits<char>_u0020>;
										<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{ctor}(ref basic_ofstream<char,std::char_traits<char>_u0020>, 1);
										try
										{
											sbyte* ptr9 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2 : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
											<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.open(ref basic_ofstream<char,std::char_traits<char>_u0020>, ptr9, 34, 64);
											<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.write(ref basic_ofstream<char,std::char_traits<char>_u0020>, (sbyte*)(&$ArrayType$$$BY0CAA@E), 512L);
											<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.write(ref basic_ofstream<char,std::char_traits<char>_u0020>, (sbyte*)(&efi_PARTITION_TABLE_HEADER), 512L);
											<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.write(ref basic_ofstream<char,std::char_traits<char>_u0020>, (sbyte*)(&$ArrayType$$$BY0IA@UEFI_PARTITION_ENTRY@@), 16384L);
											if (<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.close((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 4) == null)
											{
												<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(basic_ofstream<char,std::char_traits<char>_u0020> + 4) + (ref basic_ofstream<char,std::char_traits<char>_u0020>), 2, false);
											}
										}
										catch
										{
											<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ofstream<char,std::char_traits<char>\u0020>.__vbaseDtor), (void*)(&basic_ofstream<char,std::char_traits<char>_u0020>));
											throw;
										}
										<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{dtor}((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 104);
										<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}((ref basic_ofstream<char,std::char_traits<char>_u0020>) + 104);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
										throw;
									}
									try
									{
										<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
										throw;
									}
									break;
								}
								ptr7 += 1 / sizeof(EFI_PARTITION_TABLE_HEADER);
								ptr6 += 1 / sizeof(sbyte);
								b = *(byte*)ptr7;
								b2 = *(byte*)ptr6;
								if (b < b2)
								{
									break;
								}
							}
						}
						try
						{
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
						throw;
					}
				}
				num3 += 1U;
				num4 += 40;
				ptr = A_0 + 92;
				if (num3 >= (uint)((*(ptr + 4) - *ptr) / 40))
				{
					goto IL_3C4;
				}
			}
			IL_2D6:
			try
			{
				try
				{
					<Module>.fclose(fp);
					goto IL_352;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			IL_300:
			try
			{
				try
				{
					<Module>.fclose(fp);
					goto IL_352;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			IL_32A:
			try
			{
				try
				{
					<Module>.fclose(fp);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			IL_352:
			try
			{
				try
				{
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					throw;
				}
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				throw;
			}
			return 10;
			IL_3A6:
			<Module>.fclose(fp);
			return 10;
			IL_3B0:
			<Module>.fclose(fp);
			return 10;
			IL_3BA:
			<Module>.fclose(fp);
			return 10;
		}
		IL_3C4:
		num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0P@LHKOCCJE@DumpGpt?$CI?$CJ?5End?4@), (IntPtr)(*(*num + 4)));
		}
		return 0;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003940 File Offset: 0x00002D40
	internal unsafe static sbyte* FfuReader.ffu_get_hash_by_index(FfuReader* A_0, sbyte* hashTable, uint dwHashIndex)
	{
		sbyte* ptr = null;
		if (0 != A_0 + 17284 && dwHashIndex < (uint)(*(A_0 + 17312)) >> 5)
		{
			ptr = dwHashIndex * 32U / (uint)sizeof(sbyte) + hashTable;
		}
		return ptr;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00008C24 File Offset: 0x00008024
	internal unsafe static FfuReaderResult* FfuReader.integrityCheck(FfuReader* A_0, FfuReaderResult* A_1, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* filename, bool* terminate)
	{
		try
		{
			uint num = 0U;
			int num2 = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
			try
			{
				uint num3 = (uint)(*(A_0 + 18340));
				if (0U != num3)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BI@MDMCKOFE@integrityCheck?$CI?$CJ?5Start?4@), (IntPtr)(*(*num3 + 4)));
				}
				<Module>.FfuReaderResult.{ctor}(A_1);
				num = 1U;
				uint num4 = 0U;
				sbyte* ptr = null;
				sbyte* ptr2 = null;
				ulong num5 = 0UL;
				bool flag = false;
				if (null != terminate)
				{
					flag = *(byte*)terminate != 0;
				}
				*(int*)A_1 = 0;
				FfuReaderResult* ptr3 = A_1 + 4 / sizeof(FfuReaderResult);
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr3, ref <Module>.??_C@_00CNPNBAHC@@, 0);
				sbyte* ptr4 = (sbyte*)filename;
				if (((16 <= *(int*)(filename + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>))) ? 1 : 0) != 0)
				{
					ptr4 = *(int*)filename;
				}
				_iobuf* ptr5 = <Module>.fopen(ptr4, (sbyte*)(&<Module>.??_C@_02JDPG@rb@));
				if (null != ptr5)
				{
					FfuReaderResult ffuReaderResult8;
					try
					{
						FfuReaderResult ffuReaderResult;
						FfuReaderResult* ptr6 = <Module>.FfuReader.readSecurityHdrAndCheckValidity(A_0, &ffuReaderResult, ptr5);
						try
						{
							<Module>.FfuReaderResult.=(A_1, ptr6);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
							throw;
						}
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr7 = (ref ffuReaderResult) + 4;
						try
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr7);
							throw;
						}
						if (0 != *(int*)A_1)
						{
							FfuReaderResult ffuReaderResult2;
							<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult2, A_1);
							<Module>._CxxThrowException((void*)(&ffuReaderResult2), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
						}
						long num6 = <Module>.?A0x3d80193d.time(0);
						uint num7 = (uint)(*(A_0 + 17300) * 1024);
						*(A_0 + 17036) = (int)num7;
						long num8 = <Module>._ftelli64(ptr5) + (ulong)(*(A_0 + 17312));
						*(A_0 + 17256) = (long)((ulong)(*(A_0 + 17308)) + (ulong)num8);
						long num9 = *(A_0 + 17256);
						long num10 = (long)((ulong)num7);
						long num11 = num9 % num10;
						if (num11 != 0L)
						{
							*(A_0 + 17256) = num10 - num11 + num9;
						}
						<Module>._fseeki64(ptr5, 0L, 2);
						ulong num12 = <Module>._ftelli64(ptr5);
						num12 -= (ulong)(*(A_0 + 17256));
						ulong num13 = num12;
						if (0 != <Module>._fseeki64(ptr5, (long)((ulong)(*(A_0 + 17284) + *(A_0 + 17308))), 0))
						{
							*(int*)A_1 = 4;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr8 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_0EB@NNMJIEIB@Corrupted?5FFU?0?5cannot?5seek?5to?5b@, filename);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr3, ptr8);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
								throw;
							}
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
							FfuReaderResult ffuReaderResult3;
							<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult3, A_1);
							<Module>._CxxThrowException((void*)(&ffuReaderResult3), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
						}
						sbyte* ptr9 = <Module>.new[]((uint)(*(A_0 + 17312)));
						ptr2 = ptr9;
						if (1 != <Module>.fread((void*)ptr9, (uint)(*(A_0 + 17312)), 1U, ptr5))
						{
							*(int*)A_1 = 4;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr10 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref <Module>.??_C@_0DC@DBIHALHK@Corrupted?5FFU?0?5could?5not?5read?5F@, filename);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr3, ptr10);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
								throw;
							}
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
							FfuReaderResult ffuReaderResult4;
							<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult4, A_1);
							<Module>._CxxThrowException((void*)(&ffuReaderResult4), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
						}
						if (0 != <Module>._fseeki64(ptr5, *(A_0 + 17256), 0))
						{
							*(int*)A_1 = 4;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3;
							basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr11 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3, ref <Module>.??_C@_0DJ@BLCILKMJ@Corrupted?5FFU?0?5cannot?5seek?5to?5s@, filename);
							try
							{
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr3, ptr11);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3));
								throw;
							}
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>3);
							FfuReaderResult ffuReaderResult5;
							<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult5, A_1);
							<Module>._CxxThrowException((void*)(&ffuReaderResult5), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
						}
						NC_SHA256_CTX nc_SHA256_CTX;
						<Module>.sha256_init(&nc_SHA256_CTX);
						uint num14;
						uint num15;
						if (52428800UL < num12)
						{
							num14 = (uint)(*(A_0 + 17036));
							num15 = 52428800U / num14;
						}
						else
						{
							num14 = (uint)(*(A_0 + 17036));
							num15 = (uint)num12 / num14;
						}
						num15 = num14 * num15;
						ptr = <Module>.new[](num15);
						while (num12 > 0UL && false == flag)
						{
							if (num12 < (ulong)num15)
							{
								num15 = (uint)num12;
							}
							if (1 != <Module>.fread((void*)ptr, num15, 1U, ptr5))
							{
								*(int*)A_1 = 4;
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4;
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr12 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4, ref <Module>.??_C@_0DC@DBIHALHK@Corrupted?5FFU?0?5could?5not?5read?5F@, filename);
								try
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_1 + 4 / sizeof(FfuReaderResult), ptr12);
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4));
									throw;
								}
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>4);
								FfuReaderResult ffuReaderResult6;
								<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult6, A_1);
								<Module>._CxxThrowException((void*)(&ffuReaderResult6), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
							}
							uint num16 = 0U;
							while (num16 < num15)
							{
								<Module>.sha256_update(&nc_SHA256_CTX, (sbyte*)(num16 / (uint)sizeof(sbyte) + ptr), (uint)(*(A_0 + 17036)));
								$ArrayType$$$BY07I $ArrayType$$$BY07I;
								<Module>.sha256_final(&nc_SHA256_CTX, ref $ArrayType$$$BY07I);
								uint num17 = num4;
								num4 += 1U;
								sbyte* ptr13 = null;
								uint num18 = (uint)(*(A_0 + 17312)) >> 5;
								if (0 != A_0 + 17284 && num17 < num18)
								{
									ptr13 = num17 * 32U / (uint)sizeof(sbyte) + ptr2;
								}
								uint num19 = 32U;
								sbyte* ptr14 = ptr13;
								$ArrayType$$$BY07I* ptr15 = &$ArrayType$$$BY07I;
								int num20 = 0;
								for (;;)
								{
									byte b = *(byte*)ptr15;
									byte b2 = *(byte*)ptr14;
									if (b < b2 || b > b2)
									{
										goto IL_455;
									}
									if (num19 == 1U)
									{
										goto IL_450;
									}
									num19 -= 1U;
									ptr15 += 1 / sizeof($ArrayType$$$BY07I);
									ptr14 += 1 / sizeof(sbyte);
								}
								IL_49E:
								num14 = (uint)(*(A_0 + 17036));
								num16 = num14 + num16;
								uint num21 = (uint)(*(A_0 + 17280));
								if (num21 != 0U)
								{
									num5 += (ulong)num14;
									calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt64,System.UInt64), (IntPtr)num21, num13, num5, (IntPtr)(*(*num21 + 4)));
								}
								continue;
								IL_450:
								if (0 == num20)
								{
									goto IL_49E;
								}
								IL_455:
								*(int*)A_1 = 4;
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5;
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr16 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5, ref <Module>.??_C@_0CI@CGNAALBM@Corrupted?5FFU?0?5hash?5mismatch?0?5f@, filename);
								try
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_1 + 4 / sizeof(FfuReaderResult), ptr16);
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5));
									throw;
								}
								<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>5);
								FfuReaderResult ffuReaderResult7;
								<Module>.FfuReaderResult.{ctor}(ref ffuReaderResult7, A_1);
								<Module>._CxxThrowException((void*)(&ffuReaderResult7), (_s__ThrowInfo*)(&<Module>._TI1?AUFfuReaderResult@@));
								goto IL_49E;
							}
							num12 -= (ulong)num15;
							if (null != terminate)
							{
								flag = *(byte*)terminate != 0;
							}
						}
						if (true == flag)
						{
							*(int*)A_1 = 11;
							sbyte* ptr17 = ref <Module>.??_C@_0CI@PNDJCJNO@Calculation?5of?5hash?5terminated?5@;
							while (*ptr17 != 0)
							{
								ptr17++;
							}
							uint num22 = ptr17 - (ref <Module>.??_C@_0CI@PNDJCJNO@Calculation?5of?5hash?5terminated?5@);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_1 + 4 / sizeof(FfuReaderResult), ref <Module>.??_C@_0CI@PNDJCJNO@Calculation?5of?5hash?5terminated?5@, num22);
						}
					}
					catch when (endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), (void*)(&<Module>.??_R0?AUFfuReaderResult@@@8), 0, (void*)(&ffuReaderResult8)) != null))
					{
						uint num23 = 0U;
						<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num2);
						try
						{
							try
							{
								try
								{
									*(int*)A_1 = ffuReaderResult8;
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(A_1 + 4 / sizeof(FfuReaderResult), (ref ffuReaderResult8) + 4);
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult8));
									throw;
								}
								basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr18 = (ref ffuReaderResult8) + 4;
								try
								{
									<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult8) + 4);
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr18);
									throw;
								}
								goto IL_5B9;
							}
							catch when (delegate
							{
								// Failed to create a 'catch-when' expression
								num23 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
								endfilter(num23 != 0U);
							})
							{
							}
							if (num23 != 0U)
							{
								throw;
							}
						}
						finally
						{
							<Module>.__CxxUnregisterExceptionObject(num2, (int)num23);
						}
					}
					IL_5B9:
					if (null != ptr5)
					{
						<Module>.fclose(ptr5);
					}
					if (null != ptr)
					{
						<Module>.delete[]((void*)ptr);
					}
					if (null != ptr2)
					{
						<Module>.delete[]((void*)ptr2);
					}
				}
				else
				{
					*(int*)A_1 = 2;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6;
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr19 = <Module>.std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6, ref <Module>.??_C@_0CD@GAPHJKMF@Could?5not?5open?5FFU?5file?0?5filena@, filename);
					try
					{
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr20 = ptr3;
						if (ptr20 != ptr19)
						{
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr20);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(ptr20, ptr19);
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>6));
						throw;
					}
				}
				num3 = (uint)(*(A_0 + 18340));
				if (0U != num3)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BG@NGLINCBG@integrityCheck?$CI?$CJ?5End?4@), (IntPtr)(*(*num3 + 4)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(filename);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)filename);
				throw;
			}
			return A_1;
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)filename);
				throw;
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00006D40 File Offset: 0x00006140
	internal unsafe static FfuReaderResult* FfuReaderResult.=(FfuReaderResult* A_0, FfuReaderResult* A_0)
	{
		*A_0 = *A_0;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(A_0 + 4, A_0 + 4);
		return A_0;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00006D60 File Offset: 0x00006160
	internal unsafe static FfuReaderResult* FfuReaderResult.{ctor}(FfuReaderResult* A_0, FfuReaderResult* A_0)
	{
		*A_0 = *A_0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 4;
		*ptr = 0;
		try
		{
			*(ptr + 16) = 0;
			*(ptr + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), ptr);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(ptr, A_0 + 4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
			throw;
		}
		return A_0;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00009444 File Offset: 0x00008844
	internal unsafe static void FfuReader.checkDppPartition(FfuReader* A_0)
	{
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref <Module>.??_C@_03BBDBFBBB@dpp@);
		try
		{
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
			try
			{
				uint num = (uint)(*(A_0 + 18340));
				if (0U != num)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BL@EMPFHFAH@checkDppPartition?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
				}
				uint num2 = 0U;
				if (0 < *(A_0 + 196))
				{
					int num3 = 0;
					for (;;)
					{
						_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr = A_0 + 17268;
						if (((<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(num3 + *ptr, ref <Module>.??_C@_08KJPBNJGC@Overflow@) == 0) ? 1 : 0) == 0)
						{
							goto IL_1C9;
						}
						_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr2 = A_0 + 17268;
						basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = num3 + *ptr2;
						if ((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2) != ptr3)
						{
							integral_constant<bool,0> integral_constant<bool,0>;
							initblk(ref integral_constant<bool,0>, 0, 1);
							<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Copy_assign(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ptr3, integral_constant<bool,0>);
						}
						<Module>.stringToLower(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
						if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.compare(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) == null)
						{
							uint num4 = 0U;
							_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr4 = A_0 + 92;
							if (0 < (*(ptr4 + 4) - *ptr4) / 40)
							{
								int num5 = 0;
								do
								{
									_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr5 = A_0 + 92;
									int num6 = *ptr5;
									if ((*(ptr5 + 4) - num6) / 40 <= (int)num4)
									{
										goto IL_1C4;
									}
									FfuReader.WriteRequest* ptr6 = num5 + num6;
									_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr7 = A_0 + 17268;
									int num7 = *ptr7;
									if (*(ptr7 + 4) - num7 >> 6 <= (int)num2)
									{
										goto IL_1BF;
									}
									FfuReader.partition* ptr8 = num3 + num7;
									if (*(ptr6 + 16) >= *(ptr8 + 24))
									{
										_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* ptr9 = A_0 + 92;
										int num8 = *ptr9;
										if ((*(ptr9 + 4) - num8) / 40 <= (int)num4)
										{
											goto IL_1BA;
										}
										FfuReader.WriteRequest* ptr10 = num5 + num8;
										_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* ptr11 = A_0 + 17268;
										int num9 = *ptr11;
										if (*(ptr11 + 4) - num9 >> 6 <= (int)num2)
										{
											goto IL_1B5;
										}
										FfuReader.partition* ptr12 = num3 + num9;
										if (*(ptr10 + 16) <= *(ptr12 + 32))
										{
											*(A_0 + 17264) = 1;
										}
									}
									num4 += 1U;
									num5 += 40;
									ptr4 = A_0 + 92;
								}
								while (num4 < (uint)((*(ptr4 + 4) - *ptr4) / 40));
							}
						}
						num2 += 1U;
						num3 += 64;
						if (num2 >= (uint)(*(A_0 + 196)))
						{
							goto IL_1C9;
						}
					}
					IL_1B5:
					<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xrange();
					IL_1BA:
					<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xrange();
					IL_1BF:
					<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xrange();
					IL_1C4:
					<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xrange();
				}
				IL_1C9:
				num = (uint)(*(A_0 + 18340));
				if (0U != num)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BJ@PLPGDIKF@checkDppPartition?$CI?$CJ?5End?4@), (IntPtr)(*(*num + 4)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
				throw;
			}
			try
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00003974 File Offset: 0x00002D74
	internal unsafe static void FfuReader.trace(FfuReader* A_0, sbyte* msg)
	{
		uint num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, msg, (IntPtr)(*(*num + 4)));
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000039A0 File Offset: 0x00002DA0
	internal unsafe static void FfuReader.traceError(FfuReader* A_0, sbyte* msg)
	{
		uint num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, msg, (IntPtr)(*(*num + 8)));
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00006DEC File Offset: 0x000061EC
	internal unsafe static FfuReaderResult* FfuReader.readImageId(FfuReader* A_0, FfuReaderResult* A_1, _iobuf* fp, uint* imageId)
	{
		try
		{
			uint num = 0U;
			<Module>.FfuReaderResult.{ctor}(A_1);
			num = 1U;
			*(int*)A_1 = 0;
			sbyte* ptr = ref <Module>.??_C@_0BB@CMIDDDDM@ImageId?5read?5ok?4@;
			do
			{
				ptr++;
			}
			while (*ptr != 0);
			int num2 = ptr - (ref <Module>.??_C@_0BB@CMIDDDDM@ImageId?5read?5ok?4@);
			FfuReaderResult* ptr2 = A_1 + 4 / sizeof(FfuReaderResult);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr2, ref <Module>.??_C@_0BB@CMIDDDDM@ImageId?5read?5ok?4@, num2);
			uint num3 = (uint)(*(A_0 + 18340));
			if (0U != num3)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BF@FCPPKGEC@readImageId?$CI?$CJ?5Start?4@), (IntPtr)(*(*num3 + 4)));
			}
			$ArrayType$$$BY07E $ArrayType$$$BY07E = 0;
			*((ref $ArrayType$$$BY07E) + 1) = 0;
			*((ref $ArrayType$$$BY07E) + 2) = 0;
			*((ref $ArrayType$$$BY07E) + 3) = 0;
			*((ref $ArrayType$$$BY07E) + 4) = 0;
			*((ref $ArrayType$$$BY07E) + 5) = 0;
			*((ref $ArrayType$$$BY07E) + 6) = 0;
			*((ref $ArrayType$$$BY07E) + 7) = 0;
			if (<Module>.fread((void*)(&$ArrayType$$$BY07E), 1U, 8U, fp) != 8)
			{
				num3 = (uint)(*(A_0 + 18340));
				if (0U != num3)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0CI@FEMMPJOB@readImageId?0?5Unable?5to?5read?5fil@), (IntPtr)(*(*num3 + 8)));
				}
				*(int*)A_1 = 10;
				sbyte* ptr3 = ref <Module>.??_C@_0CI@FEMMPJOB@readImageId?0?5Unable?5to?5read?5fil@;
				do
				{
					ptr3++;
				}
				while (*ptr3 != 0);
				int num4 = ptr3 - (ref <Module>.??_C@_0CI@FEMMPJOB@readImageId?0?5Unable?5to?5read?5fil@);
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr2, ref <Module>.??_C@_0CI@FEMMPJOB@readImageId?0?5Unable?5to?5read?5fil@, num4);
			}
			else
			{
				uint num5 = ((uint)(((((int)(*((ref $ArrayType$$$BY07E) + 3)) << 8) | (int)(*((ref $ArrayType$$$BY07E) + 2))) << 8) | (int)(*((ref $ArrayType$$$BY07E) + 1))) << 8) | $ArrayType$$$BY07E;
				if ((((ulong)(((((((int)(*((ref $ArrayType$$$BY07E) + 7)) << 8) | (int)(*((ref $ArrayType$$$BY07E) + 6))) << 8) | (int)(*((ref $ArrayType$$$BY07E) + 5))) << 8) | (int)(*((ref $ArrayType$$$BY07E) + 4))) << 32) | (ulong)num5) == 7452728838522632820UL)
				{
					num3 = (uint)(*(A_0 + 18340));
					if (0U != num3)
					{
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BO@EIIEAOIB@Encrypted?5image?5not?5supported@), (IntPtr)(*(*num3 + 8)));
					}
					*(int*)A_1 = 13;
					sbyte* ptr4 = ref <Module>.??_C@_0CC@DMHIICGH@Encrypted?5image?5is?5not?5supporte@;
					do
					{
						ptr4++;
					}
					while (*ptr4 != 0);
					int num6 = ptr4 - (ref <Module>.??_C@_0CC@DMHIICGH@Encrypted?5image?5is?5not?5supporte@);
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(ptr2, ref <Module>.??_C@_0CC@DMHIICGH@Encrypted?5image?5is?5not?5supporte@, num6);
				}
				else
				{
					*imageId = (int)num5;
				}
			}
			<Module>._fseeki64(fp, 0L, 0);
			uint num7 = (uint)(*(A_0 + 18340));
			if (0U != num7)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num7, (sbyte*)(&<Module>.??_C@_0BD@BAEILGK@readImageId?$CI?$CJ?5End?4@), (IntPtr)(*(*num7 + 4)));
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x000039CC File Offset: 0x00002DCC
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool FfuReader.isValidBootImage(FfuReader* A_0, uint* imageId)
	{
		bool flag = false;
		uint num = (uint)(*(A_0 + 18340));
		if (0U != num)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num, (sbyte*)(&<Module>.??_C@_0BK@CONNOIJG@isValidBootImage?$CI?$CJ?5Start?4@), (IntPtr)(*(*num + 4)));
		}
		uint num2 = (uint)(*imageId);
		if ((num2 > 0U && num2 < 28U) || num2 == 2219564241U)
		{
			flag = true;
		}
		uint num3 = (uint)(*(A_0 + 18340));
		if (0U != num3)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.SByte modopt(System.Runtime.CompilerServices.IsSignUnspecifiedByte) modopt(System.Runtime.CompilerServices.IsConst)*), (IntPtr)num3, (sbyte*)(&<Module>.??_C@_0BI@HEHMDDPC@isValidBootImage?$CI?$CJ?5End?4@), (IntPtr)(*(*num3 + 4)));
		}
		return flag;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000FE5C File Offset: 0x0000F25C
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>.imbue(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, locale* _Loc)
	{
		codecvt<char,char,_Mbstatet>* ptr = <Module>.std.use_facet<class\u0020std::codecvt<char,char,struct\u0020_Mbstatet>\u0020>(_Loc);
		if (<Module>.std.codecvt_base.always_noconv(ptr) != null)
		{
			*(A_0 + 56) = 0;
		}
		else
		{
			*(A_0 + 56) = ptr;
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(A_0);
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000FE1C File Offset: 0x0000F21C
	internal unsafe static int std.basic_filebuf<char,std::char_traits<char>\u0020>.sync(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		if (*(A_0 + 76) != 0 && ((-1 == calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.Int32), A_0, -1, (IntPtr)(*(*A_0 + 12)))) ? 1 : 0) == 0 && 0 > <Module>.fflush(*(A_0 + 76)))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000FDD4 File Offset: 0x0000F1D4
	internal unsafe static basic_streambuf<char,std::char_traits<char>\u0020>* std.basic_filebuf<char,std::char_traits<char>\u0020>.setbuf(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, sbyte* _Buffer, long _Count)
	{
		int num;
		if (_Buffer == null && _Count == 0L)
		{
			num = 4;
		}
		else
		{
			num = 0;
		}
		uint num2 = (uint)(*(A_0 + 76));
		if (num2 != 0U && <Module>.setvbuf(num2, _Buffer, num, (uint)((int)_Count)) == null)
		{
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(A_0, *(A_0 + 76), (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)1);
			return A_0;
		}
		return 0;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000FD14 File Offset: 0x0000F114
	internal unsafe static fpos<_Mbstatet>* std.basic_filebuf<char,std::char_traits<char>\u0020>.seekpos(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, fpos<_Mbstatet>* A_1, fpos<_Mbstatet> _Pos, int __unnamed001)
	{
		long num = *((ref _Pos) + 8) + _Pos;
		if (*(A_0 + 76) != 0 && <Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Endwrite(A_0) != null && <Module>.fsetpos(*(A_0 + 76), (long*)(&num)) == null)
		{
			_Mbstatet mbstatet;
			cpblk(ref mbstatet, (ref _Pos) + 16, 8);
			basic_filebuf<char,std::char_traits<char>\u0020>* ptr = A_0 + 64;
			cpblk(ptr, ref mbstatet, 8);
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
			_Mbstatet mbstatet2 = *ptr;
			*(long*)A_1 = num;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			cpblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), ref mbstatet2, 8);
			return A_1;
		}
		*(long*)A_1 = -1L;
		*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
		initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
		return A_1;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000FC28 File Offset: 0x0000F028
	internal unsafe static fpos<_Mbstatet>* std.basic_filebuf<char,std::char_traits<char>\u0020>.seekoff(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, fpos<_Mbstatet>* A_1, long _Off, int _Way, int __unnamed002)
	{
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) == A_0 + 60 && _Way == 1 && *(A_0 + 56) == 0)
		{
			_Off += -1L;
		}
		long num;
		if (*(A_0 + 76) != 0 && <Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Endwrite(A_0) != null && ((_Off == 0L && _Way == 1) || <Module>._fseeki64(*(A_0 + 76), _Off, _Way) == null) && <Module>.fgetpos(*(A_0 + 76), &num) == null)
		{
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
			_Mbstatet mbstatet = *(A_0 + 64);
			*(long*)A_1 = num;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			cpblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), ref mbstatet, 8);
			return A_1;
		}
		*(long*)A_1 = -1L;
		*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
		initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
		return A_1;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000FBAC File Offset: 0x0000EFAC
	internal unsafe static long std.basic_filebuf<char,std::char_traits<char>\u0020>.xsputn(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, sbyte* _Ptr, long _Count)
	{
		if (*(A_0 + 56) != 0)
		{
			return <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.xsputn(A_0, _Ptr, _Count);
		}
		long num = _Count;
		long num2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Pnavail(A_0);
		if (0L < _Count)
		{
			if (0L < num2)
			{
				if (_Count < num2)
				{
					num2 = _Count;
				}
				cpblk(<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0), _Ptr, (int)num2);
				int num3 = (int)num2;
				_Ptr = num3 / sizeof(sbyte) + _Ptr;
				_Count -= num2;
				<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pbump(A_0, num3);
			}
			if (0L < _Count)
			{
				uint num4 = (uint)(*(A_0 + 76));
				if (num4 != 0U)
				{
					_Count -= (long)<Module>.fwrite((void*)_Ptr, 1U, (uint)((int)_Count), num4);
				}
			}
		}
		return num - _Count;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000FAE8 File Offset: 0x0000EEE8
	internal unsafe static long std.basic_filebuf<char,std::char_traits<char>\u0020>.xsgetn(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, sbyte* _Ptr, long _Count)
	{
		if (_Count <= 0L)
		{
			return 0L;
		}
		if (*(A_0 + 56) != 0)
		{
			return <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.xsgetn(A_0, _Ptr, _Count);
		}
		uint num = (uint)((int)_Count);
		uint num2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Gnavail(A_0);
		if (0 < num2)
		{
			uint num3 = *(ref num2 < num ? ref num2 : ref num);
			sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0);
			cpblk(_Ptr, ptr, num3);
			_Ptr = num3 / sizeof(sbyte) + _Ptr;
			num -= num3;
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gbump(A_0, num3);
		}
		if (*(A_0 + 76) != 0)
		{
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
			if (4095U < num)
			{
				do
				{
					uint num4 = <Module>.fread((void*)_Ptr, 1U, 4095U, *(A_0 + 76));
					_Ptr = num4 / sizeof(sbyte) + _Ptr;
					num -= num4;
					if (num4 != 4095)
					{
						goto IL_93;
					}
				}
				while (4095U < num);
				goto IL_98;
				IL_93:
				return _Count - (long)((ulong)num);
			}
			IL_98:
			if (0U < num)
			{
				num -= <Module>.fread((void*)_Ptr, 1U, num, *(A_0 + 76));
			}
		}
		return _Count - (long)((ulong)num);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000F678 File Offset: 0x0000EA78
	internal unsafe static int std.basic_filebuf<char,std::char_traits<char>\u0020>.uflow(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) != null && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) < <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0))
		{
			return (byte)(*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Gninc(A_0));
		}
		if (*(A_0 + 76) == 0)
		{
			return -1;
		}
		<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
		if (*(A_0 + 56) == 0)
		{
			int num = <Module>.fgetc(*(A_0 + 76));
			int num2;
			if (num == -1)
			{
				num2 = -1;
			}
			else
			{
				sbyte b = (sbyte)num;
				num2 = b;
			}
			return num2;
		}
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		int num6;
		try
		{
			int num3 = <Module>.fgetc(*(A_0 + 76));
			int num7;
			int num9;
			if (num3 != -1)
			{
				basic_filebuf<char,std::char_traits<char>\u0020>* ptr = A_0 + 64;
				sbyte* ptr4;
				sbyte b2;
				int num4;
				for (;;)
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.push_back(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, (sbyte)num3);
					sbyte* ptr2 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					sbyte* ptr3 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					sbyte* ptr5;
					num4 = <Module>.std.codecvt<char,char,_Mbstatet>.in(*(A_0 + 56), ptr, ptr3, *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 16) / sizeof(sbyte) + ptr2, ref ptr4, &b2, (ref b2) + 1, ref ptr5);
					if (num4 != 0 && num4 != 1)
					{
						break;
					}
					if (ptr5 != &b2)
					{
						goto IL_142;
					}
					sbyte* ptr6 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					uint num5 = ptr4 - ptr6;
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Erase_noexcept(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, 0, num5);
					num3 = <Module>.fgetc(*(A_0 + 76));
					if (num3 == -1)
					{
						goto IL_18D;
					}
				}
				if (num4 != 3)
				{
					num6 = -1;
					goto IL_198;
				}
				num7 = (int)(*<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.front(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
				goto IL_194;
				IL_142:
				sbyte* ptr7 = (((16 <= *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 20)) ? 1 : 0) != 0 ? basic_string<char,std::char_traits<char>,std::allocator<char>_u0020> : ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
				int num8 = *((ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>) + 16) - ptr4 + ptr7 / sizeof(sbyte);
				if (0 < num8)
				{
					do
					{
						num8--;
						<Module>.ungetc((int)(*(sbyte*)(num8 / sizeof(sbyte) + ptr4)), *(A_0 + 76));
					}
					while (num8 > 0);
				}
				num9 = (int)b2;
				goto IL_190;
			}
			IL_18D:
			num9 = -1;
			IL_190:
			num7 = num9;
			IL_194:
			num6 = num7;
			IL_198:;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>));
			throw;
		}
		return num6;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000F620 File Offset: 0x0000EA20
	internal unsafe static int std.basic_filebuf<char,std::char_traits<char>\u0020>.underflow(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) != null && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) < <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0))
		{
			return (byte)(*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0));
		}
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), A_0, (IntPtr)(*(*A_0 + 28)));
		if (((-1 == num) ? 1 : 0) != 0)
		{
			return num;
		}
		int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.Int32), A_0, num, (IntPtr)(*(*A_0 + 16)));
		return num;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000F518 File Offset: 0x0000E918
	internal unsafe static int std.basic_filebuf<char,std::char_traits<char>\u0020>.pbackfail(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, int _Meta)
	{
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) != null && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0) < <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) && (((-1 == _Meta) ? 1 : 0) != 0 || (((int)((byte)(*(<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) - 1))) == _Meta) ? 1 : 0) != 0))
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Gndec(A_0);
			return (_Meta != -1) ? _Meta : 0;
		}
		uint num = (uint)(*(A_0 + 76));
		if (num == 0U || ((-1 == _Meta) ? 1 : 0) != 0)
		{
			return -1;
		}
		if (*(A_0 + 56) == 0)
		{
			_iobuf* ptr = num;
			if (((<Module>.ungetc((int)((byte)_Meta), ptr) != -1) ? 1 : 0) != 0)
			{
				return _Meta;
			}
		}
		basic_filebuf<char,std::char_traits<char>\u0020>* ptr2 = A_0 + 60;
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) != ptr2)
		{
			*ptr2 = (byte)_Meta;
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Set_back(A_0);
			return _Meta;
		}
		return -1;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000F3DC File Offset: 0x0000E7DC
	internal unsafe static int std.basic_filebuf<char,std::char_traits<char>\u0020>.overflow(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, int _Meta)
	{
		if (((-1 == _Meta) ? 1 : 0) != 0)
		{
			return (_Meta != -1) ? _Meta : 0;
		}
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0) != null && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0) < <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0))
		{
			*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Pninc(A_0) = (byte)_Meta;
			return _Meta;
		}
		if (*(A_0 + 76) == 0)
		{
			return -1;
		}
		<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
		uint num = (uint)(*(A_0 + 56));
		if (num == 0U)
		{
			_iobuf* ptr = *(A_0 + 76);
			return (((<Module>.fputc((int)((sbyte)_Meta), ptr) != -1) ? 1 : 0) != 0) ? _Meta : (-1);
		}
		sbyte b = (sbyte)_Meta;
		sbyte* ptr2;
		$ArrayType$$$BY0CA@D $ArrayType$$$BY0CA@D;
		sbyte* ptr3;
		int num2 = <Module>.std.codecvt<char,char,_Mbstatet>.out(num, A_0 + 64, &b, (ref b) + 1, ref ptr2, (sbyte*)(&$ArrayType$$$BY0CA@D), (ref $ArrayType$$$BY0CA@D) + 32, ref ptr3);
		if (num2 != 0 && num2 != 1)
		{
			if (num2 != 3)
			{
				return -1;
			}
			_iobuf* ptr4 = *(A_0 + 76);
			return (((<Module>.fputc(b, ptr4) != -1) ? 1 : 0) != 0) ? _Meta : (-1);
		}
		else
		{
			uint num3 = ptr3 - (ref $ArrayType$$$BY0CA@D) / sizeof(sbyte);
			if (0 < num3 && num3 != <Module>.fwrite((void*)(&$ArrayType$$$BY0CA@D), 1U, num3, *(A_0 + 76)))
			{
				return -1;
			}
			*(A_0 + 61) = 1;
			return (ptr2 != &b) ? _Meta : (-1);
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000F4CC File Offset: 0x0000E8CC
	internal unsafe static sbyte std._Narrow_char_traits<char,int>.to_char_type(int* _Meta)
	{
		return (sbyte)(*_Meta);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000F3B8 File Offset: 0x0000E7B8
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Unlock(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		uint num = (uint)(*(A_0 + 76));
		if (num != 0U)
		{
			<Module>._unlock_file(num);
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000F394 File Offset: 0x0000E794
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Lock(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		uint num = (uint)(*(A_0 + 76));
		if (num != 0U)
		{
			<Module>._lock_file(num);
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00004FD8 File Offset: 0x000043D8
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>.{dtor}(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		*A_0 = ref <Module>.??_7?$basic_filebuf@DU?$char_traits@D@std@@@std@@6B@;
		try
		{
			if (*(A_0 + 76) != 0)
			{
				<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
			}
			if (*(A_0 + 72) != 0)
			{
				<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.close(A_0);
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), A_0);
			throw;
		}
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}(A_0);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000503C File Offset: 0x0000443C
	internal unsafe static void std.basic_ofstream<char,std::char_traits<char>\u0020>.close(basic_ofstream<char,std::char_traits<char>\u0020>* A_0)
	{
		if (<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.close(A_0 + 4) == null)
		{
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*A_0 + 4) + A_0, 2, false);
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00006FE4 File Offset: 0x000063E4
	internal unsafe static void std.basic_ofstream<char,std::char_traits<char>\u0020>.open(basic_ofstream<char,std::char_traits<char>\u0020>* A_0, sbyte* _Filename, int _Mode, int _Prot)
	{
		if (<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.open(A_0 + 4, _Filename, _Mode | 2, _Prot) != null)
		{
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.clear(*(*A_0 + 4) + A_0, 0, false);
		}
		else
		{
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*A_0 + 4) + A_0, 2, false);
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000042E8 File Offset: 0x000036E8
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_ofstream<char,std::char_traits<char>\u0020>.is_open(basic_ofstream<char,std::char_traits<char>\u0020>* A_0)
	{
		return (*(A_0 + 80) != 0) ? 1 : 0;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005064 File Offset: 0x00004464
	internal unsafe static void std.basic_ofstream<char,std::char_traits<char>\u0020>.{dtor}(basic_ofstream<char,std::char_traits<char>\u0020>* A_0)
	{
		basic_ofstream<char,std::char_traits<char>\u0020>* ptr = A_0 - 104;
		*(A_0 + *(*ptr + 4) - 104) = ref <Module>.??_7?$basic_ofstream@DU?$char_traits@D@std@@@std@@6B@;
		int num = *(*ptr + 4);
		*(A_0 + num - 108) = num - 104;
		try
		{
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.{dtor}(A_0 - 100);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>.{dtor}), A_0 - 104 + 8);
			throw;
		}
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.{dtor}(A_0 - 96);
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000050DC File Offset: 0x000044DC
	internal unsafe static basic_ofstream<char,std::char_traits<char>\u0020>* std.basic_ofstream<char,std::char_traits<char>\u0020>.{ctor}(basic_ofstream<char,std::char_traits<char>\u0020>* A_0, int A_1)
	{
		uint num = 0U;
		if (A_1 != 0)
		{
			*A_0 = ref <Module>.??_8?$basic_ofstream@DU?$char_traits@D@std@@@std@@7B@;
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{ctor}(A_0 + 104);
			try
			{
				num = 1U;
			}
			catch
			{
				if ((num & 1U) != 0U)
				{
					num &= 4294967294U;
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 104);
				}
				throw;
			}
		}
		try
		{
			basic_ofstream<char,std::char_traits<char>\u0020>* ptr = A_0 + 4;
			<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.{ctor}(A_0, ptr, false, 0);
			try
			{
				*(*(*A_0 + 4) + A_0) = ref <Module>.??_7?$basic_ofstream@DU?$char_traits@D@std@@@std@@6B@;
				int num2 = *(*A_0 + 4);
				*(A_0 + num2 - 4) = num2 - 104;
				basic_filebuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
				<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{ctor}(ptr2);
				try
				{
					*ptr2 = ref <Module>.??_7?$basic_filebuf@DU?$char_traits@D@std@@@std@@6B@;
					<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(ptr2, null, (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)0);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), ptr2);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 8);
				throw;
			}
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 104);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000104E8 File Offset: 0x0000F8E8
	internal unsafe static fpos<_Mbstatet>* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.seekpos(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, fpos<_Mbstatet>* A_1, fpos<_Mbstatet> _Pos, int _Mode)
	{
		long num = *((ref _Pos) + 8) + _Pos;
		sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0);
		sbyte* ptr2;
		if ((*(A_0 + 60) & 2) != 0)
		{
			ptr2 = null;
		}
		else
		{
			ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0);
			if (ptr2 != null && *(A_0 + 56) < ptr2)
			{
				*(A_0 + 56) = ptr2;
			}
		}
		sbyte* ptr3 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
		uint num2 = (uint)(*(A_0 + 56));
		if (num > num2 - ptr3)
		{
			*(long*)A_1 = -1L;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
			return A_1;
		}
		if (num != 0L && (((_Mode & 1) != 0 && ptr == null) || ((_Mode & 2) != 0 && ptr2 == null)))
		{
			*(long*)A_1 = -1L;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
			return A_1;
		}
		sbyte* ptr4 = num + ptr3;
		if ((_Mode & 1) != 0 && ptr != null)
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, ptr3, ptr4, num2);
		}
		if ((_Mode & 2) != 0 && ptr2 != null)
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(A_0, ptr3, ptr4, <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0));
		}
		*(long*)A_1 = num;
		*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
		initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
		return A_1;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00010398 File Offset: 0x0000F798
	internal unsafe static fpos<_Mbstatet>* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.seekoff(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, fpos<_Mbstatet>* A_1, long _Off, int _Way, int _Mode)
	{
		sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0);
		sbyte* ptr2;
		if ((*(A_0 + 60) & 2) != 0)
		{
			ptr2 = null;
		}
		else
		{
			ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0);
			if (ptr2 != null && *(A_0 + 56) < ptr2)
			{
				*(A_0 + 56) = ptr2;
			}
		}
		sbyte* ptr3 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
		uint num = (uint)(*(A_0 + 56));
		int num2 = num - ptr3;
		long num3;
		if (_Way != 0)
		{
			if (_Way != 1)
			{
				if (_Way == 2)
				{
					num3 = num2;
					goto IL_9C;
				}
			}
			else if ((_Mode & 3) != 3)
			{
				if ((_Mode & 1) != 0)
				{
					if (ptr != null || ptr3 == null)
					{
						num3 = ptr - ptr3;
						goto IL_9C;
					}
				}
				else if ((_Mode & 2) != 0 && (ptr2 != null || ptr3 == null))
				{
					num3 = ptr2 - ptr3 / sizeof(sbyte);
					goto IL_9C;
				}
			}
			*(long*)A_1 = -1L;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
			return A_1;
		}
		num3 = 0L;
		IL_9C:
		long num4 = num3 + _Off;
		if (num4 > num2)
		{
			*(long*)A_1 = -1L;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
			return A_1;
		}
		_Off = num4;
		if (_Off != 0L && (((_Mode & 1) != 0 && ptr == null) || ((_Mode & 2) != 0 && ptr2 == null)))
		{
			*(long*)A_1 = -1L;
			*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
			initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
			return A_1;
		}
		sbyte* ptr4 = (int)_Off + ptr3;
		if ((_Mode & 1) != 0 && ptr != null)
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, ptr3, ptr4, num);
		}
		if ((_Mode & 2) != 0 && ptr2 != null)
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(A_0, ptr3, ptr4, <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0));
		}
		*(long*)A_1 = _Off;
		*(long*)(A_1 + 8 / sizeof(fpos<_Mbstatet>)) = 0L;
		initblk(A_1 + 16 / sizeof(fpos<_Mbstatet>), 0, 8);
		return A_1;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0001031C File Offset: 0x0000F71C
	internal unsafe static int std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.underflow(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0);
		if (ptr == null)
		{
			return -1;
		}
		if (ptr < <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0))
		{
			return (byte)(*ptr);
		}
		sbyte* ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0);
		if (ptr2 == null || (*(A_0 + 60) & 4) != 0)
		{
			return -1;
		}
		basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = A_0 + 56;
		sbyte** ptr4 = ptr3;
		sbyte* ptr5 = *(ref *ptr4 < ptr2 ? ref ptr2 : ptr4);
		if (ptr5 == ptr)
		{
			return -1;
		}
		*ptr3 = ptr5;
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0), <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0), ptr5);
		return (byte)(*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0));
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000102A4 File Offset: 0x0000F6A4
	internal unsafe static int std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.pbackfail(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, int _Meta)
	{
		sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0);
		if (ptr != null && ptr != <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0) && (((-1 == _Meta) ? 1 : 0) != 0 || (((sbyte)_Meta == *(ptr - 1)) ? 1 : 0) != 0 || (*(A_0 + 60) & 2) == 0))
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gbump(A_0, -1);
			if (((-1 == _Meta) ? 1 : 0) == 0)
			{
				*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) = (byte)_Meta;
			}
			return (_Meta != -1) ? _Meta : 0;
		}
		return -1;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00010164 File Offset: 0x0000F564
	internal unsafe static int std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.overflow(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, int _Meta)
	{
		if ((*(A_0 + 60) & 2) != 0)
		{
			return -1;
		}
		if (((-1 == _Meta) ? 1 : 0) != 0)
		{
			return (_Meta != -1) ? _Meta : 0;
		}
		sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0);
		sbyte* ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0);
		if (ptr != null && ptr < ptr2)
		{
			*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Pninc(A_0) = (byte)_Meta;
			*(A_0 + 56) = ptr + 1;
			return _Meta;
		}
		uint num = 0U;
		sbyte* ptr3 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
		uint num2;
		void* ptr4;
		if (ptr != null)
		{
			num = ptr2 - ptr3;
			if (num >= 32U)
			{
				if (num < 1073741823U)
				{
					num2 = num << 1;
					if (num2 < 4096U)
					{
						if (num2 != 0U)
						{
							goto IL_99;
						}
						ptr4 = null;
						goto IL_A4;
					}
				}
				else
				{
					if (num >= 2147483647U)
					{
						return -1;
					}
					num2 = 2147483647U;
				}
				ptr4 = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num2);
				goto IL_A4;
			}
		}
		num2 = 32U;
		IL_99:
		ptr4 = <Module>.@new(num2);
		IL_A4:
		cpblk(ptr4, ptr3, num);
		sbyte* ptr5 = (byte*)ptr4 + num;
		*(A_0 + 56) = ptr5 + 1;
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(A_0, (sbyte*)ptr4, ptr5, (sbyte*)((byte*)ptr4 + num2));
		if ((*(A_0 + 60) & 4) != 0)
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, (sbyte*)ptr4, null, (sbyte*)ptr4);
		}
		else
		{
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, (sbyte*)ptr4, (sbyte*)(<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) + (byte*)((byte*)ptr4 - ptr3)), *(A_0 + 56));
		}
		if ((*(A_0 + 60) & 1) != 0)
		{
			uint num3 = num;
			void* ptr6 = ptr3;
			if (num >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr6, ref num3);
			}
			<Module>.delete(ptr6, num3);
		}
		*(A_0 + 60) = *(A_0 + 60) | 1;
		*<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Pninc(A_0) = (byte)_Meta;
		return _Meta;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000058E0 File Offset: 0x00004CE0
	internal unsafe static void std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		*A_0 = ref <Module>.??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
		try
		{
			<Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), A_0);
			throw;
		}
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}(A_0);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00007020 File Offset: 0x00006420
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.str(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_1)
	{
		uint num = 0U;
		<Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.str(A_0 + 24, A_1);
		try
		{
			num = 1U;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000592C File Offset: 0x00004D2C
	internal unsafe static void std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 - 104;
		*(A_0 + *(*ptr + 4) - 104) = ref <Module>.??_7?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
		int num = *(*ptr + 4);
		*(A_0 + num - 108) = num - 104;
		try
		{
			basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = A_0 - 80;
			*ptr2 = ref <Module>.??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
			try
			{
				<Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy(ptr2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), ptr2);
				throw;
			}
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}(ptr2);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_iostream<char,std::char_traits<char>\u0020>.{dtor}), A_0 - 104 + 32);
			throw;
		}
		<Module>.std.basic_iostream<char,std::char_traits<char>\u0020>.{dtor}(A_0 - 72);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000059D8 File Offset: 0x00004DD8
	internal unsafe static basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, int A_1)
	{
		uint num = 0U;
		if (A_1 != 0)
		{
			*A_0 = ref <Module>.??_8?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@7B?$basic_istream@DU?$char_traits@D@std@@@1@@;
			*(A_0 + 16) = ref <Module>.??_8?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@7B?$basic_ostream@DU?$char_traits@D@std@@@1@@;
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{ctor}(A_0 + 104);
			try
			{
				num = 1U;
			}
			catch
			{
				if ((num & 1U) != 0U)
				{
					num &= 4294967294U;
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 104);
				}
				throw;
			}
		}
		try
		{
			basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 + 24;
			<Module>.std.basic_iostream<char,std::char_traits<char>\u0020>.{ctor}(A_0, ptr, 0);
			try
			{
				*(*(*A_0 + 4) + A_0) = ref <Module>.??_7?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
				int num2 = *(*A_0 + 4);
				*(A_0 + num2 - 4) = num2 - 104;
				basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = ptr;
				<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{ctor}(ptr2);
				try
				{
					*ptr2 = ref <Module>.??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
					*(ptr2 + 56) = 0;
					*(ptr2 + 60) = <Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Getstate(3);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), ptr2);
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_iostream<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 32);
				throw;
			}
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}), A_0 + 104);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00004304 File Offset: 0x00003704
	internal unsafe static FfuReader.partition* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.at(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, uint _Pos)
	{
		int num = *A_0;
		if (*(A_0 + 4) - num >> 6 <= _Pos)
		{
			<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xrange();
		}
		return _Pos * 64 + num;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00003A34 File Offset: 0x00002E34
	internal unsafe static FfuReader.partition* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.[](vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, uint _Pos)
	{
		return _Pos * 64 + *A_0;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000B490 File Offset: 0x0000A890
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.push_back(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _Val)
	{
		<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.emplace_back<struct\u0020FfuReader::partition\u0020const\u0020&>(A_0, _Val);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000B458 File Offset: 0x0000A858
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.emplace_back<struct\u0020FfuReader::partition\u0020const\u0020&>(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* <_Val_0>)
	{
		vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* ptr = A_0 + 4;
		uint num = (uint)(*ptr);
		if (num != (uint)(*(A_0 + 8)))
		{
			FfuReader.partition** ptr2 = ptr;
			<Module>.FfuReader.partition.{ctor}(*ptr2, <_Val_0>);
			*ptr2 += 64;
		}
		else
		{
			<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Emplace_reallocate<struct\u0020FfuReader::partition\u0020const\u0020&>(A_0, num, <_Val_0>);
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000096F4 File Offset: 0x00008AF4
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Emplace_back_with_unused_capacity<struct\u0020FfuReader::partition\u0020const\u0020&>(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* <_Val_0>)
	{
		<Module>.FfuReader.partition.{ctor}(*(A_0 + 4), <_Val_0>);
		*(A_0 + 4) = *(A_0 + 4) + 64;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000AE98 File Offset: 0x0000A298
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.{dtor}(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Tidy(A_0);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00005204 File Offset: 0x00004604
	internal unsafe static vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.{ctor}(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00009750 File Offset: 0x00008B50
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.push_back(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _Val)
	{
		<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.emplace_back<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(A_0, _Val);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00009718 File Offset: 0x00008B18
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.emplace_back<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* <_Val_0>)
	{
		vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* ptr = A_0 + 4;
		uint num = (uint)(*ptr);
		if (num != (uint)(*(A_0 + 8)))
		{
			FfuReader.BlockDataEntry** ptr2 = ptr;
			cpblk(*ptr2, <_Val_0>, 24);
			*ptr2 += 24;
		}
		else
		{
			<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Emplace_reallocate<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(A_0, num, <_Val_0>);
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00005220 File Offset: 0x00004620
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Emplace_back_with_unused_capacity<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* <_Val_0>)
	{
		cpblk(*(A_0 + 4), <_Val_0>, 24);
		*(A_0 + 4) = *(A_0 + 4) + 24;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00005B14 File Offset: 0x00004F14
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.{dtor}(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Tidy(A_0);
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00005244 File Offset: 0x00004644
	internal unsafe static vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.{ctor}(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000432C File Offset: 0x0000372C
	internal unsafe static FfuReader.WriteRequest* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.at(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, uint _Pos)
	{
		int num = *A_0;
		if ((*(A_0 + 4) - num) / 40 <= _Pos)
		{
			<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xrange();
		}
		return _Pos * 40 + num;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00003A48 File Offset: 0x00002E48
	internal unsafe static FfuReader.WriteRequest* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.[](vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, uint _Pos)
	{
		return _Pos * 40 + *A_0;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00003A5C File Offset: 0x00002E5C
	internal unsafe static uint std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.size(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		return (*(A_0 + 4) - *A_0) / 40;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000979C File Offset: 0x00008B9C
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.push_back(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _Val)
	{
		<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.emplace_back<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(A_0, _Val);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00009764 File Offset: 0x00008B64
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.emplace_back<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* <_Val_0>)
	{
		vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* ptr = A_0 + 4;
		uint num = (uint)(*ptr);
		if (num != (uint)(*(A_0 + 8)))
		{
			FfuReader.WriteRequest** ptr2 = ptr;
			cpblk(*ptr2, <_Val_0>, 40);
			*ptr2 += 40;
		}
		else
		{
			<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Emplace_reallocate<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(A_0, num, <_Val_0>);
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00005260 File Offset: 0x00004660
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Emplace_back_with_unused_capacity<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* <_Val_0>)
	{
		cpblk(*(A_0 + 4), <_Val_0>, 40);
		*(A_0 + 4) = *(A_0 + 4) + 40;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00005B28 File Offset: 0x00004F28
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.{dtor}(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Tidy(A_0);
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005284 File Offset: 0x00004684
	internal unsafe static vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.{ctor}(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000052A0 File Offset: 0x000046A0
	internal unsafe static int std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.compare(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		sbyte* ptr = _Right;
		if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
		{
			ptr = *_Right;
		}
		sbyte* ptr2 = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *A_0;
		}
		return <Module>.std._Traits_compare<struct\u0020std::char_traits<char>\u0020>(ptr2, *(A_0 + 16), ptr, *(_Right + 16));
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000052E8 File Offset: 0x000046E8
	internal unsafe static sbyte* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.data(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00005B3C File Offset: 0x00004F3C
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.clear(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		*(A_0 + 16) = 0;
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		*(byte*)ptr = 0;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006334 File Offset: 0x00005734
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr)
	{
		sbyte* ptr = _Ptr;
		if (*_Ptr != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		int num = ptr - _Ptr;
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0, _Ptr, num);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00005B68 File Offset: 0x00004F68
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr, uint _Count)
	{
		uint num = (uint)(*(A_0 + 20));
		if (_Count <= num)
		{
			sbyte* ptr = A_0;
			if (((16U <= num) ? 1 : 0) != 0)
			{
				ptr = *A_0;
			}
			*(A_0 + 16) = _Count;
			<Module>.memmove((void*)ptr, _Ptr, _Count);
			*(byte*)(ptr + _Count / sizeof(sbyte)) = 0;
			return A_0;
		}
		<lambda_88acb98cb1d3e807ff08d7ebe077788e> <lambda_88acb98cb1d3e807ff08d7ebe077788e>;
		initblk(ref <lambda_88acb98cb1d3e807ff08d7ebe077788e>, 0, 1);
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_for<class\u0020<lambda_88acb98cb1d3e807ff08d7ebe077788e>,char\u0020const\u0020*>(A_0, _Count, <lambda_88acb98cb1d3e807ff08d7ebe077788e>, _Ptr);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00004354 File Offset: 0x00003754
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign.<lambda_88acb98cb1d3e807ff08d7ebe077788e>.()(<lambda_88acb98cb1d3e807ff08d7ebe077788e>* A_0, sbyte* _New_ptr, uint _Count, sbyte* _Ptr)
	{
		cpblk(_New_ptr, _Ptr, _Count);
		*(_New_ptr + _Count) = 0;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00007074 File Offset: 0x00006474
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(A_0, _Right);
		return A_0;
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000635C File Offset: 0x0000575C
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr)
	{
		sbyte* ptr = _Ptr;
		if (*_Ptr != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		int num = ptr - _Ptr;
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(A_0, _Ptr, num);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00006384 File Offset: 0x00005784
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr)
	{
		sbyte* ptr = _Ptr;
		if (*_Ptr != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		int num = ptr - _Ptr;
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0, _Ptr, num);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x000063AC File Offset: 0x000057AC
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		if (A_0 != _Right)
		{
			sbyte* ptr = _Right;
			if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
			{
				ptr = *_Right;
			}
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0, ptr, *(_Right + 16));
		}
		return A_0;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000063E0 File Offset: 0x000057E0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		if (A_0 != _Right)
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, _Right);
		}
		return A_0;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00005BBC File Offset: 0x00004FBC
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.=(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		if (A_0 != _Right)
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, _Right);
		}
		return A_0;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00006400 File Offset: 0x00005800
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr)
	{
		*A_0 = 0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2;
		try
		{
			ptr = A_0 + 16;
			*ptr = 0;
			ptr2 = A_0 + 20;
			*ptr2 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			*ptr = 0;
			*ptr2 = 15;
			*A_0 = 0;
			sbyte* ptr3 = _Ptr;
			if (*_Ptr != 0)
			{
				do
				{
					ptr3++;
				}
				while (*ptr3 != 0);
			}
			int num = ptr3 - _Ptr;
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0, _Ptr, num);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000F33C File Offset: 0x0000E73C
	internal unsafe static void* std.basic_filebuf<char,std::char_traits<char>\u0020>.__vecDelDtor(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			basic_filebuf<char,std::char_traits<char>\u0020>* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 88U, (uint)(*ptr), ldftn(std.basic_filebuf<char,std::char_traits<char>\u0020>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				basic_filebuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 88 + 4));
			}
			return ptr;
		}
		<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 88U);
		}
		return A_0;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x000106D0 File Offset: 0x0000FAD0
	internal unsafe static void* std.basic_ofstream<char,std::char_traits<char>\u0020>.__vecDelDtor(basic_ofstream<char,std::char_traits<char>\u0020>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			basic_ofstream<char,std::char_traits<char>\u0020>* ptr = A_0 - 108;
			<Module>.__ehvec_dtor(A_0 - 104, 176U, (uint)(*ptr), ldftn(std.basic_ofstream<char,std::char_traits<char>\u0020>.__vbaseDtor));
			if ((A_0 & 1U) != 0U)
			{
				basic_ofstream<char,std::char_traits<char>\u0020>* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 176 + 4));
			}
			return ptr;
		}
		basic_ofstream<char,std::char_traits<char>\u0020>* ptr3 = A_0 - 104;
		basic_ofstream<char,std::char_traits<char>\u0020>* ptr4 = ptr3 + 104;
		<Module>.std.basic_ofstream<char,std::char_traits<char>\u0020>.{dtor}(ptr4);
		<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}(ptr4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(ptr3, 176U);
		}
		return ptr3;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000100D4 File Offset: 0x0000F4D4
	internal unsafe static void* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.__vecDelDtor(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 - 4;
			<Module>.__ehvec_dtor(A_0, 68U, (uint)(*ptr), ldftn(std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 68 + 4));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
		try
		{
			<Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), A_0);
			throw;
		}
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 68U);
		}
		return A_0;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0001080C File Offset: 0x0000FC0C
	internal unsafe static void* std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vecDelDtor(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0 - 108;
			<Module>.__ehvec_dtor(A_0 - 104, 176U, (uint)(*ptr), ldftn(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vbaseDtor));
			if ((A_0 & 1U) != 0U)
			{
				basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = ptr;
				<Module>.delete[](ptr2, (uint)(*ptr2 * 176 + 4));
			}
			return ptr;
		}
		basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr3 = A_0 - 104;
		basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr4 = ptr3 + 104;
		<Module>.std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(ptr4);
		<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}(ptr4);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(ptr3, 176U);
		}
		return ptr3;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00003A74 File Offset: 0x00002E74
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.._N(basic_ostream<char,std::char_traits<char>\u0020>.sentry* A_0)
	{
		return *(A_0 + 4);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000436C File Offset: 0x0000376C
	internal unsafe static void std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{dtor}(basic_ostream<char,std::char_traits<char>\u0020>.sentry* A_0)
	{
		try
		{
			if (<Module>.std.uncaught_exception() == null)
			{
				<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>._Osfx(*A_0);
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base.{dtor}), A_0);
			throw;
		}
		int num = *A_0;
		basic_streambuf<char,std::char_traits<char>\u0020>* ptr = <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*num + 4) + num);
		if (ptr != null)
		{
			basic_streambuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), ptr2, (IntPtr)(*(*ptr2 + 8)));
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000043D4 File Offset: 0x000037D4
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>.sentry* std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{ctor}(basic_ostream<char,std::char_traits<char>\u0020>.sentry* A_0, basic_ostream<char,std::char_traits<char>\u0020>* _Ostr)
	{
		*A_0 = _Ostr;
		basic_streambuf<char,std::char_traits<char>\u0020>* ptr = <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr);
		if (ptr != null)
		{
			basic_streambuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), ptr2, (IntPtr)(*(*ptr2 + 4)));
		}
		try
		{
			if (<Module>.std.ios_base.good(*(*_Ostr + 4) + _Ostr) == null)
			{
				*(A_0 + 4) = 0;
			}
			else
			{
				basic_ostream<char,std::char_traits<char>\u0020>* ptr3 = <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.tie(*(*_Ostr + 4) + _Ostr);
				if (ptr3 != null && ptr3 != _Ostr)
				{
					<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.flush(ptr3);
					*(A_0 + 4) = <Module>.std.ios_base.good(*(*_Ostr + 4) + _Ostr);
				}
				else
				{
					*(A_0 + 4) = 1;
				}
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00004474 File Offset: 0x00003874
	internal unsafe static sbyte* std.pointer_traits<char\u0020*>.pointer_to(sbyte* _Val)
	{
		return _Val;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000FDB8 File Offset: 0x0000F1B8
	internal unsafe static long std.fpos<_Mbstatet>.._J(fpos<_Mbstatet>* A_0)
	{
		return *(A_0 + 8) + *A_0;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000FDA0 File Offset: 0x0000F1A0
	internal unsafe static _Mbstatet* std.fpos<_Mbstatet>.state(fpos<_Mbstatet>* A_0, _Mbstatet* A_1)
	{
		cpblk(A_1, A_0 + 16, 8);
		return A_1;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000FCEC File Offset: 0x0000F0EC
	internal unsafe static fpos<_Mbstatet>* std.fpos<_Mbstatet>.{ctor}(fpos<_Mbstatet>* A_0, _Mbstatet _State, long _Fileposition)
	{
		*A_0 = _Fileposition;
		*(A_0 + 8) = 0L;
		cpblk(A_0 + 16, ref _State, 8);
		return A_0;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000FCCC File Offset: 0x0000F0CC
	internal unsafe static fpos<_Mbstatet>* std.fpos<_Mbstatet>.{ctor}(fpos<_Mbstatet>* A_0, long _Off)
	{
		*A_0 = _Off;
		*(A_0 + 8) = 0L;
		initblk(A_0 + 16, 0, 8);
		return A_0;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000F5DC File Offset: 0x0000E9DC
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Set_back(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		basic_filebuf<char,std::char_traits<char>\u0020>* ptr = A_0 + 60;
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0) != ptr)
		{
			*(A_0 + 80) = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
			*(A_0 + 84) = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0);
		}
		basic_filebuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, ptr2, ptr2, A_0 + 61);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00003A88 File Offset: 0x00002E88
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0) == A_0 + 60)
		{
			int num = *(A_0 + 80);
			int num2 = num;
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, num2, num2, *(A_0 + 84));
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00004484 File Offset: 0x00003884
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Initcvt(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, codecvt<char,char,_Mbstatet>* _Newcvt)
	{
		if (<Module>.std.codecvt_base.always_noconv(_Newcvt) != null)
		{
			*(A_0 + 56) = 0;
		}
		else
		{
			*(A_0 + 56) = _Newcvt;
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(A_0);
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000044B0 File Offset: 0x000038B0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_filebuf<char,std::char_traits<char>\u0020>._Endwrite(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		if (*(A_0 + 56) == 0 || *(A_0 + 61) == 0)
		{
			return 1;
		}
		if (((-1 == calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.Int32), A_0, -1, (IntPtr)(*(*A_0 + 12)))) ? 1 : 0) != 0)
		{
			return 0;
		}
		$ArrayType$$$BY0CA@D $ArrayType$$$BY0CA@D;
		sbyte* ptr;
		int num = <Module>.std.codecvt<char,char,_Mbstatet>.unshift(*(A_0 + 56), A_0 + 64, (sbyte*)(&$ArrayType$$$BY0CA@D), (ref $ArrayType$$$BY0CA@D) + 32, ref ptr);
		if (num != 0)
		{
			if (num != 1)
			{
				if (num != 3)
				{
					return 0;
				}
				*(A_0 + 61) = 0;
				return 1;
			}
		}
		else
		{
			*(A_0 + 61) = 0;
		}
		uint num2 = ptr - (ref $ArrayType$$$BY0CA@D) / sizeof(sbyte);
		if (0 < num2 && num2 != <Module>.fwrite((void*)(&$ArrayType$$$BY0CA@D), 1U, num2, *(A_0 + 76)))
		{
			return 0;
		}
		return (*(A_0 + 61) == 0) ? 1 : 0;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00003AB4 File Offset: 0x00002EB4
	internal unsafe static void std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, _iobuf* _File, basic_filebuf<char,std::char_traits<char>\u0020>._Initfl _Which)
	{
		int num = ((_Which == (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)1) ? 1 : 0);
		*(A_0 + 72) = (byte)num;
		*(A_0 + 61) = 0;
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(A_0);
		if (_File != null)
		{
			sbyte** ptr = null;
			sbyte** ptr2 = null;
			int* ptr3 = null;
			<Module>._get_stream_buffer_pointers(_File, &ptr, &ptr2, &ptr3);
			<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(A_0, ptr, ptr2, ptr3, ptr, ptr2, ptr3);
		}
		*(A_0 + 76) = _File;
		cpblk(A_0 + 64, ref <Module>.?_Stinit@?1??_Init@?$basic_filebuf@DU?$char_traits@D@std@@@std@@IAEXPAU_iobuf@@W4_Initfl@23@@Z@4U_Mbstatet@@A, 8);
		*(A_0 + 56) = 0;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00004540 File Offset: 0x00003940
	internal unsafe static basic_filebuf<char,std::char_traits<char>\u0020>* std.basic_filebuf<char,std::char_traits<char>\u0020>.close(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		basic_filebuf<char,std::char_traits<char>\u0020>* ptr;
		if (*(A_0 + 76) != 0)
		{
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Reset_back(A_0);
			ptr = ((<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Endwrite(A_0) == 0) ? null : A_0);
			if (<Module>.fclose(*(A_0 + 76)) != null)
			{
				ptr = null;
			}
		}
		else
		{
			ptr = null;
		}
		<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(A_0, null, (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)2);
		return ptr;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000649C File Offset: 0x0000589C
	internal unsafe static basic_filebuf<char,std::char_traits<char>\u0020>* std.basic_filebuf<char,std::char_traits<char>\u0020>.open(basic_filebuf<char,std::char_traits<char>\u0020>* A_0, sbyte* _Filename, int _Mode, int _Prot)
	{
		if (*(A_0 + 76) != 0)
		{
			return 0;
		}
		_iobuf* ptr = <Module>.std._Fiopen(_Filename, _Mode, _Prot);
		if (ptr == null)
		{
			return 0;
		}
		<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(A_0, ptr, (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)1);
		locale locale;
		locale* ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.getloc(A_0, &locale);
		try
		{
			codecvt<char,char,_Mbstatet>* ptr3 = <Module>.std.use_facet<class\u0020std::codecvt<char,char,struct\u0020_Mbstatet>\u0020>(ptr2);
			if (<Module>.std.codecvt_base.always_noconv(ptr3) != null)
			{
				*(A_0 + 56) = 0;
			}
			else
			{
				*(A_0 + 56) = ptr3;
				<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(A_0);
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.locale.{dtor}), (void*)(&locale));
			throw;
		}
		<Module>.std.locale.{dtor}(ref locale);
		return A_0;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00003B14 File Offset: 0x00002F14
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_filebuf<char,std::char_traits<char>\u0020>.is_open(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		return (*(A_0 + 76) != 0) ? 1 : 0;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00003B30 File Offset: 0x00002F30
	internal unsafe static basic_filebuf<char,std::char_traits<char>\u0020>* std.basic_filebuf<char,std::char_traits<char>\u0020>.{ctor}(basic_filebuf<char,std::char_traits<char>\u0020>* A_0)
	{
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{ctor}(A_0);
		try
		{
			*A_0 = ref <Module>.??_7?$basic_filebuf@DU?$char_traits@D@std@@@std@@6B@;
			<Module>.std.basic_filebuf<char,std::char_traits<char>\u0020>._Init(A_0, null, (basic_filebuf<char,std::char_traits<char>\u0020>._Initfl)0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000530C File Offset: 0x0000470C
	internal unsafe static void std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		if ((*(A_0 + 60) & 1) != 0)
		{
			sbyte* ptr;
			if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0) != null)
			{
				ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0);
			}
			else
			{
				ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0);
			}
			void* ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
			uint num = ptr - <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0) / sizeof(sbyte);
			void* ptr3 = ptr2;
			if (num >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num);
			}
			<Module>.delete(ptr3, num);
		}
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(A_0, null, null, null);
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(A_0, null, null);
		*(A_0 + 56) = 0;
		*(A_0 + 60) = *(A_0 + 60) & -2;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00006528 File Offset: 0x00005928
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.str(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_1)
	{
		try
		{
			uint num = 0U;
			*(int*)A_1 = 0;
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr;
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2;
			try
			{
				ptr = A_1 + 16 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>);
				*(int*)ptr = 0;
				ptr2 = A_1 + 20 / sizeof(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>);
				*(int*)ptr2 = 0;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), (void*)A_1);
				throw;
			}
			try
			{
				*(int*)ptr = 0;
				*(int*)ptr2 = 15;
				*(byte*)A_1 = 0;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)A_1);
				throw;
			}
			num = 1U;
			basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view buffer_view;
			<Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Get_buffer_view(A_0, (basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view*)(&buffer_view));
			if (buffer_view != null)
			{
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_1, buffer_view, *((ref buffer_view) + 4));
			}
		}
		catch
		{
			uint num;
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_1);
			}
			throw;
		}
		return A_1;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00004588 File Offset: 0x00003988
	internal unsafe static basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, int _Mode)
	{
		<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.{ctor}(A_0);
		try
		{
			*A_0 = ref <Module>.??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;
			*(A_0 + 56) = 0;
			*(A_0 + 60) = <Module>.std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Getstate(_Mode);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000045E0 File Offset: 0x000039E0
	internal unsafe static allocator<FfuReader::partition>* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Getal(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00003B80 File Offset: 0x00002F80
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Orphan_range(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* A_0, FfuReader.partition* A_1)
	{
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00003B90 File Offset: 0x00002F90
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xrange()
	{
		<Module>.std._Xout_of_range((sbyte*)(&<Module>.??_C@_0BJ@DHFDPMIM@invalid?5vector?5subscript@));
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000ADD8 File Offset: 0x0000A1D8
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Tidy(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			FfuReader.partition* ptr = *(A_0 + 4);
			FfuReader.partition* ptr2 = num;
			if (ptr2 != ptr)
			{
				do
				{
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr2);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)ptr2);
						throw;
					}
					ptr2 += 64 / sizeof(FfuReader.partition);
				}
				while (ptr2 != ptr);
			}
			num = (uint)(*A_0);
			uint num2 = (uint)((*(A_0 + 8) - (int)num >> 6) * 64);
			void* ptr3 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num2);
			}
			<Module>.delete(ptr3, num2);
			*A_0 = 0;
			*(A_0 + 4) = 0;
			*(A_0 + 8) = 0;
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000045F0 File Offset: 0x000039F0
	internal unsafe static allocator<FfuReader::BlockDataEntry>* std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Getal(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00003BA8 File Offset: 0x00002FA8
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Orphan_range(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* A_0, FfuReader.BlockDataEntry* A_1)
	{
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00005388 File Offset: 0x00004788
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Tidy(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		FfuReader.BlockDataEntry** ptr = A_0 + 4;
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			uint num2 = (uint)((*(A_0 + 8) - (int)num) / 24 * 24);
			void* ptr2 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr2, ref num2);
			}
			<Module>.delete(ptr2, num2);
			*A_0 = 0;
			*ptr = 0;
			*(A_0 + 8) = 0;
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00004600 File Offset: 0x00003A00
	internal unsafe static allocator<FfuReader::WriteRequest>* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Getal(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00003BB8 File Offset: 0x00002FB8
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Orphan_range(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* A_0, FfuReader.WriteRequest* A_1)
	{
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00003BC8 File Offset: 0x00002FC8
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xrange()
	{
		<Module>.std._Xout_of_range((sbyte*)(&<Module>.??_C@_0BJ@DHFDPMIM@invalid?5vector?5subscript@));
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000053D4 File Offset: 0x000047D4
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Tidy(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		FfuReader.WriteRequest** ptr = A_0 + 4;
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			uint num2 = (uint)((*(A_0 + 8) - (int)num) / 40 * 40);
			void* ptr2 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr2, ref num2);
			}
			<Module>.delete(ptr2, num2);
			*A_0 = 0;
			*ptr = 0;
			*(A_0 + 8) = 0;
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000FAA0 File Offset: 0x0000EEA0
	internal unsafe static sbyte* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.front(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0)
	{
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		return ptr;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000F954 File Offset: 0x0000ED54
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.push_back(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte _Ch)
	{
		uint num = *(A_0 + 16);
		uint num2 = (uint)(*(A_0 + 20));
		if (num < num2)
		{
			*(A_0 + 16) = num + 1;
			sbyte* ptr = A_0;
			if (((16U <= num2) ? 1 : 0) != 0)
			{
				ptr = *A_0;
			}
			*(byte*)(num / sizeof(sbyte) + ptr) = _Ch;
			*(byte*)(num / sizeof(sbyte) + ptr + 1 / sizeof(sbyte)) = 0;
		}
		else
		{
			<lambda_6f04b3841c6ace20ee4fd1b3dbc592d0> <lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>;
			initblk(ref <lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>, 0, 1);
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>,char>(A_0, 1, <lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>, _Ch);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000FA80 File Offset: 0x0000EE80
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.push_back.<lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>.()(<lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>* A_0, sbyte* _New_ptr, sbyte* _Old_ptr, uint _Old_size, sbyte _Ch)
	{
		cpblk(_New_ptr, _Old_ptr, _Old_size);
		*(_Old_size + _New_ptr) = _Ch;
		*(_Old_size + _New_ptr + 1) = 0;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000F87C File Offset: 0x0000EC7C
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.erase(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Off, uint _Count)
	{
		if (*(A_0 + 16) < _Off)
		{
			<Module>.std._String_val<std::_Simple_types<char>\u0020>._Xran();
		}
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Erase_noexcept(A_0, _Off, _Count);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00005BDC File Offset: 0x00004FDC
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr, uint _Count)
	{
		uint num = *(A_0 + 16);
		uint num2 = (uint)(*(A_0 + 20));
		if (_Count <= num2 - num)
		{
			*(A_0 + 16) = num + _Count;
			sbyte* ptr = A_0;
			if (((16U <= num2) ? 1 : 0) != 0)
			{
				ptr = *A_0;
			}
			int num3 = num / sizeof(sbyte) + ptr;
			<Module>.memmove(num3, _Ptr, _Count);
			*(num3 + _Count) = 0;
			return A_0;
		}
		<lambda_4c04651f1675f62dd3603168f157397a> <lambda_4c04651f1675f62dd3603168f157397a>;
		initblk(ref <lambda_4c04651f1675f62dd3603168f157397a>, 0, 1);
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_4c04651f1675f62dd3603168f157397a>,char\u0020const\u0020*,unsigned\u0020int>(A_0, _Count, <lambda_4c04651f1675f62dd3603168f157397a>, _Ptr, _Count);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00004610 File Offset: 0x00003A10
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append.<lambda_4c04651f1675f62dd3603168f157397a>.()(<lambda_4c04651f1675f62dd3603168f157397a>* A_0, sbyte* _New_ptr, sbyte* _Old_ptr, uint _Old_size, sbyte* _Ptr, uint _Count)
	{
		cpblk(_New_ptr, _Old_ptr, _Old_size);
		int num = _Old_size + _New_ptr;
		cpblk(num, _Ptr, _Count);
		*(num + _Count) = 0;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00005C40 File Offset: 0x00005040
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Copy_assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right, integral_constant<bool,0> __unnamed001)
	{
		sbyte* ptr = _Right;
		if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
		{
			ptr = *_Right;
		}
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.assign(A_0, ptr, *(_Right + 16));
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00005420 File Offset: 0x00004820
	internal unsafe static void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Move_assign(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right, _Equal_allocators __unnamed001)
	{
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, _Right);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00003BE0 File Offset: 0x00002FE0
	internal static int std._Narrow_char_traits<char,int>.eof()
	{
		return -1;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000F4DC File Offset: 0x0000E8DC
	internal unsafe static int std._Narrow_char_traits<char,int>.not_eof(int* _Meta)
	{
		int num = *_Meta;
		return (num != -1) ? num : 0;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00003BF0 File Offset: 0x00002FF0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Narrow_char_traits<char,int>.eq_int_type(int* _Left, int* _Right)
	{
		return (*_Left == *_Right) ? 1 : 0;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000F5AC File Offset: 0x0000E9AC
	internal unsafe static int std._Narrow_char_traits<char,int>.to_int_type(sbyte* _Ch)
	{
		return (byte)(*_Ch);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00010300 File Offset: 0x0000F700
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Narrow_char_traits<char,int>.eq(sbyte* _Left, sbyte* _Right)
	{
		return (*_Left == *_Right) ? 1 : 0;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00003C04 File Offset: 0x00003004
	internal unsafe static uint std._Narrow_char_traits<char,int>.length(sbyte* _First)
	{
		sbyte* ptr = _First;
		if (*_First != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		return ptr - _First;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00003C24 File Offset: 0x00003024
	internal unsafe static sbyte* std._Char_traits<char,int>.move(sbyte* _First1, sbyte* _First2, uint _Count)
	{
		<Module>.memmove(_First1, _First2, _Count);
		return _First1;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00003C3C File Offset: 0x0000303C
	internal unsafe static void std.basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base.{dtor}(basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base* A_0)
	{
		int num = *A_0;
		basic_streambuf<char,std::char_traits<char>\u0020>* ptr = <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*num + 4) + num);
		if (ptr != null)
		{
			basic_streambuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), ptr2, (IntPtr)(*(*ptr2 + 8)));
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00003C68 File Offset: 0x00003068
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base* std.basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base.{ctor}(basic_ostream<char,std::char_traits<char>\u0020>._Sentry_base* A_0, basic_ostream<char,std::char_traits<char>\u0020>* _Ostr)
	{
		*A_0 = _Ostr;
		basic_streambuf<char,std::char_traits<char>\u0020>* ptr = <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr);
		if (ptr != null)
		{
			basic_streambuf<char,std::char_traits<char>\u0020>* ptr2 = ptr;
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), ptr2, (IntPtr)(*(*ptr2 + 4)));
		}
		return A_0;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00003C94 File Offset: 0x00003094
	internal static int std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Getstate(int _Mode)
	{
		int num = 0;
		num = (((_Mode & 1) == 0) ? 4 : num);
		if ((_Mode & 2) == 0)
		{
			num |= 2;
		}
		if ((_Mode & 8) != 0)
		{
			num |= 8;
		}
		if ((_Mode & 4) != 0)
		{
			num |= 16;
		}
		return num;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00004634 File Offset: 0x00003A34
	internal unsafe static basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view* std.basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Get_buffer_view(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view* A_1)
	{
		initblk(A_1, 0, 12);
		if ((*(A_0 + 60) & 34) != 2 && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0) != null)
		{
			sbyte* ptr = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pbase(A_0);
			*(int*)A_1 = ptr;
			sbyte* ptr2 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(A_0);
			sbyte** ptr3 = A_0 + 56;
			sbyte** ptr4 = (ptr2 < *ptr3 ? ptr3 : ref ptr2);
			*(int*)(A_1 + 4 / sizeof(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view)) = *(int*)ptr4 - ptr;
			*(int*)(A_1 + 8 / sizeof(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view)) = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(A_0) - ptr;
		}
		else if ((*(A_0 + 60) & 4) == 0 && <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(A_0) != null)
		{
			sbyte* ptr5 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(A_0);
			*(int*)A_1 = ptr5;
			uint num = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(A_0) - ptr5;
			*(int*)(A_1 + 4 / sizeof(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view)) = (int)num;
			*(int*)(A_1 + 8 / sizeof(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>._Buffer_view)) = (int)num;
		}
		return A_1;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00003CCC File Offset: 0x000030CC
	internal unsafe static allocator<FfuReader::partition>* std._Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000AC64 File Offset: 0x0000A064
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Destroy(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _First, FfuReader.partition* _Last)
	{
		FfuReader.partition* ptr = _First;
		if (_First != _Last)
		{
			do
			{
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)ptr);
					throw;
				}
				ptr += 64 / sizeof(FfuReader.partition);
			}
			while (ptr != _Last);
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000046C4 File Offset: 0x00003AC4
	internal unsafe static void std.allocator<FfuReader::partition>.deallocate(allocator<FfuReader::partition>* A_0, FfuReader.partition* _Ptr, uint _Count)
	{
		uint num = _Count * 64;
		void* ptr = _Ptr;
		if (num >= 4096U)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00003CDC File Offset: 0x000030DC
	internal unsafe static allocator<FfuReader::BlockDataEntry>* std._Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000046F0 File Offset: 0x00003AF0
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Destroy(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last)
	{
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00004700 File Offset: 0x00003B00
	internal unsafe static void std.allocator<FfuReader::BlockDataEntry>.deallocate(allocator<FfuReader::BlockDataEntry>* A_0, FfuReader.BlockDataEntry* _Ptr, uint _Count)
	{
		uint num = _Count * 24;
		void* ptr = _Ptr;
		if (num >= 4096U)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00003CEC File Offset: 0x000030EC
	internal unsafe static allocator<FfuReader::WriteRequest>* std._Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000472C File Offset: 0x00003B2C
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Destroy(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last)
	{
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000473C File Offset: 0x00003B3C
	internal unsafe static void std.allocator<FfuReader::WriteRequest>.deallocate(allocator<FfuReader::WriteRequest>* A_0, FfuReader.WriteRequest* _Ptr, uint _Count)
	{
		uint num = _Count * 40;
		void* ptr = _Ptr;
		if (num >= 4096U)
		{
			<Module>.std._Adjust_manually_vector_aligned(ref ptr, ref num);
		}
		<Module>.delete(ptr, num);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000F8D4 File Offset: 0x0000ECD4
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Erase_noexcept(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Off, uint _Count)
	{
		uint num = _Count;
		uint num2 = (uint)(*(A_0 + 16));
		uint num3 = num2 - _Off;
		uint num4 = (uint)(*(ref num3 < _Count ? ref num3 : ref num));
		uint num5 = num2;
		sbyte* ptr = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr = *A_0;
		}
		sbyte* ptr2 = ptr + _Off / sizeof(sbyte);
		uint num6 = num5 - num4;
		*(A_0 + 16) = num6;
		<Module>.memmove((void*)ptr2, (void*)(ptr2 + num4 / (uint)sizeof(sbyte)), num6 - _Off + 1);
		return A_0;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00005C70 File Offset: 0x00005070
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, allocator<char>* _Al)
	{
		*A_0 = 0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2;
		try
		{
			ptr = A_0 + 16;
			*ptr = 0;
			ptr2 = A_0 + 20;
			*ptr2 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			*ptr = 0;
			*ptr2 = 15;
			*A_0 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000F8A0 File Offset: 0x0000ECA0
	internal unsafe static void std._String_val<std::_Simple_types<char>\u0020>._Check_offset(_String_val<std::_Simple_types<char>\u0020>* A_0, uint _Off)
	{
		if (*(A_0 + 16) < _Off)
		{
			<Module>.std._String_val<std::_Simple_types<char>\u0020>._Xran();
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000F934 File Offset: 0x0000ED34
	internal unsafe static uint std._String_val<std::_Simple_types<char>\u0020>._Clamp_suffix_size(_String_val<std::_Simple_types<char>\u0020>* A_0, uint _Off, uint _Size)
	{
		uint num = *(A_0 + 16) - _Off;
		return *(ref num < _Size ? ref num : ref _Size);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000F8BC File Offset: 0x0000ECBC
	internal unsafe static void std._String_val<std::_Simple_types<char>\u0020>._Xran()
	{
		<Module>.std._Xout_of_range((sbyte*)(&<Module>.??_C@_0BI@CFPLBAOH@invalid?5string?5position@));
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00005CF0 File Offset: 0x000050F0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Left, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		uint num = 0U;
		sbyte* ptr = _Left;
		if (*_Left != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		uint num2 = ptr - _Left;
		uint num3 = *(_Right + 16);
		if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(_Right) - num3 < num2)
		{
			<Module>.std._Xlen_string();
		}
		sbyte* ptr2 = _Right;
		if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *_Right;
		}
		_String_constructor_concat_tag string_constructor_concat_tag;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0, string_constructor_concat_tag, _Right, _Left, num2, ptr2, num3);
		try
		{
			num = 1U;
			return A_0;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		try
		{
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00003CFC File Offset: 0x000030FC
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.move<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020&>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000065FC File Offset: 0x000059FC
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, sbyte* _Right)
	{
		uint num = 0U;
		sbyte* ptr = _Right;
		if (*_Right != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		int num2 = ptr - _Right;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.append(_Left, _Right, num2);
		*(int*)A_0 = 0;
		try
		{
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
				<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, ptr2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)A_0);
				throw;
			}
			num = 1U;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00005DC0 File Offset: 0x000051C0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.operator+<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, sbyte* _Right)
	{
		uint num = 0U;
		uint num2 = *(_Left + 16);
		sbyte* ptr = _Right;
		if (*_Right != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		uint num3 = ptr - _Right;
		if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(_Left) - num2 < num3)
		{
			<Module>.std._Xlen_string();
		}
		sbyte* ptr2 = _Left;
		if (((16 <= *(_Left + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *_Left;
		}
		_String_constructor_concat_tag string_constructor_concat_tag;
		<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(A_0, string_constructor_concat_tag, _Left, ptr2, num2, _Right, num3);
		try
		{
			num = 1U;
			return A_0;
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		try
		{
		}
		catch
		{
			if ((num & 1U) != 0U)
			{
				num &= 4294967294U;
				<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), (void*)A_0);
			}
			throw;
		}
		return A_0;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00004768 File Offset: 0x00003B68
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>* std.operator<<<struct\u0020std::char_traits<char>\u0020>(basic_ostream<char,std::char_traits<char>\u0020>* _Ostr, sbyte* _Val)
	{
		int num = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		int num2 = 0;
		sbyte* ptr = _Val;
		if (*(sbyte*)_Val != 0)
		{
			do
			{
				ptr += 1 / sizeof(sbyte);
			}
			while (*(sbyte*)ptr != 0);
		}
		long num3 = (long)((ulong)(ptr - _Val));
		long num4;
		if (<Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) > 0L && <Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) > num3)
		{
			num4 = <Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) - num3;
		}
		else
		{
			num4 = 0L;
		}
		long num5 = num4;
		basic_ostream<char,std::char_traits<char>\u0020>.sentry sentry;
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{ctor}(ref sentry, _Ostr);
		try
		{
			if (*((ref sentry) + 4) != 0)
			{
				uint exceptionCode;
				try
				{
					if ((<Module>.std.ios_base.flags(*(*_Ostr + 4) + _Ostr) & 448) != 64)
					{
						while (0L < num5)
						{
							int num6 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputc(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.fill(*(*_Ostr + 4) + _Ostr));
							if (((-1 == num6) ? 1 : 0) != 0)
							{
								num2 |= 4;
								break;
							}
							num5 += -1L;
						}
						if (num2 != 0)
						{
							goto IL_12E;
						}
					}
					if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputn(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), _Val, num3) != num3)
					{
						num2 = 4;
					}
					else
					{
						while (0L < num5)
						{
							int num7 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputc(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.fill(*(*_Ostr + 4) + _Ostr));
							if (((-1 == num7) ? 1 : 0) != 0)
							{
								num2 |= 4;
								break;
							}
							num5 += -1L;
						}
					}
					IL_12E:
					<Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr, 0L);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					exceptionCode = (uint)Marshal.GetExceptionCode();
					endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
				})
				{
					uint num8 = 0U;
					<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
					try
					{
						try
						{
							<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*_Ostr + 4) + _Ostr, 4, true);
							goto IL_19A;
						}
						catch when (delegate
						{
							// Failed to create a 'catch-when' expression
							num8 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
							endfilter(num8 != 0U);
						})
						{
						}
						if (num8 != 0U)
						{
							throw;
						}
					}
					finally
					{
						<Module>.__CxxUnregisterExceptionObject(num, (int)num8);
					}
				}
			}
			else
			{
				num2 = 4;
			}
			IL_19A:
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*_Ostr + 4) + _Ostr, num2, false);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{dtor}), (void*)(&sentry));
			throw;
		}
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{dtor}(ref sentry);
		return _Ostr;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000543C File Offset: 0x0000483C
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>* std.operator<<<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_ostream<char,std::char_traits<char>\u0020>* _Ostr, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Str)
	{
		uint num = (uint)(*(_Str + 16));
		sbyte* ptr = _Str;
		if (((16 <= *(_Str + 20)) ? 1 : 0) != 0)
		{
			ptr = *_Str;
		}
		return <Module>.std._Insert_string<char,struct\u0020std::char_traits<char>,unsigned\u0020int>(_Ostr, ptr, num);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00003D0C File Offset: 0x0000310C
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>* std.endl<char,struct\u0020std::char_traits<char>\u0020>(basic_ostream<char,std::char_traits<char>\u0020>* _Ostr)
	{
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.put(_Ostr, <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.widen(*(*_Ostr + 4) + _Ostr, 10));
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.flush(_Ostr);
		return _Ostr;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000546C File Offset: 0x0000486C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.operator==<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(_Left, _Right);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00005E90 File Offset: 0x00005290
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.operator!=<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, sbyte* _Right)
	{
		return (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(_Left, _Right) == 0) ? 1 : 0;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00005480 File Offset: 0x00004880
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.operator!=<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		return (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(_Left, _Right) == 0) ? 1 : 0;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00005EA8 File Offset: 0x000052A8
	internal unsafe static codecvt<char,char,_Mbstatet>* std.use_facet<class\u0020std::codecvt<char,char,struct\u0020_Mbstatet>\u0020>(locale* _Loc)
	{
		_Lockit lockit;
		<Module>.std._Lockit.{ctor}(ref lockit, 0);
		locale.facet* ptr2;
		try
		{
			locale.facet* ptr = <Module>.?_Psave@?$_Facetptr@V?$codecvt@DDU_Mbstatet@@@std@@@std@@2PBVfacet@locale@2@B;
			uint num = <Module>.std.locale.id..I(<Module>.__imp_?id@?$codecvt@DDU_Mbstatet@@@std@@2V0locale@2@A);
			ptr2 = <Module>.std.locale._Getfacet(_Loc, num);
			if (ptr2 == null)
			{
				if (ptr != null)
				{
					ptr2 = ptr;
				}
				else
				{
					if (<Module>.std.codecvt<char,char,_Mbstatet>._Getcat(&ptr, _Loc) == -1)
					{
						<Module>.std._Throw_bad_cast();
					}
					locale.facet* ptr3 = (locale.facet*)ptr;
					unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020> unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>_u0020> = ptr;
					try
					{
						<Module>.std._Facet_Register_m((_Facet_base*)ptr3);
						locale.facet* ptr4 = ptr3;
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), ptr4, (IntPtr)(*(*(int*)ptr4 + 4)));
						<Module>.?_Psave@?$_Facetptr@V?$codecvt@DDU_Mbstatet@@@std@@@std@@2PBVfacet@locale@2@B = ptr;
						ptr2 = ptr;
						unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>_u0020> = 0;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std.unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>.{dtor}), (void*)(&unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>_u0020>));
						throw;
					}
					<Module>.std.unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>.{dtor}(ref unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>_u0020>);
				}
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Lockit.{dtor}), (void*)(&lockit));
			throw;
		}
		<Module>.std._Lockit.{dtor}(ref lockit);
		return ptr2;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00003D38 File Offset: 0x00003138
	internal unsafe static basic_filebuf<char,std::char_traits<char>\u0020>* std.addressof<class\u0020std::basic_filebuf<char,struct\u0020std::char_traits<char>\u0020>\u0020>(basic_filebuf<char,std::char_traits<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00003D48 File Offset: 0x00003148
	internal unsafe static sbyte** std.max<char\u0020*>(sbyte** _Left, sbyte** _Right)
	{
		return (*_Left < *_Right) ? _Right : _Left;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00003D60 File Offset: 0x00003160
	internal unsafe static basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* std.addressof<class\u0020std::basic_stringbuf<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020>(basic_stringbuf<char,std::char_traits<char>,std::allocator<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00003D70 File Offset: 0x00003170
	internal unsafe static FfuReader.partition* std.forward<struct\u0020FfuReader::partition\u0020const\u0020&>(FfuReader.partition* _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00003D80 File Offset: 0x00003180
	internal unsafe static FfuReader.partition* std._Unfancy<struct\u0020FfuReader::partition>(FfuReader.partition* _Ptr)
	{
		return _Ptr;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000708C File Offset: 0x0000648C
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::partition>\u0020>.construct<struct\u0020FfuReader::partition,struct\u0020FfuReader::partition\u0020const\u0020&>(allocator<FfuReader::partition>* __unnamed000, FfuReader.partition* _Ptr, FfuReader.partition* <_Args_0>)
	{
		<Module>.FfuReader.partition.{ctor}(_Ptr, <_Args_0>);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000B2D0 File Offset: 0x0000A6D0
	internal unsafe static FfuReader.partition* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Emplace_reallocate<struct\u0020FfuReader::partition\u0020const\u0020&>(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _Whereptr, FfuReader.partition* <_Val_0>)
	{
		int num = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		int num2 = *A_0;
		uint num3 = _Whereptr - num2 >> 6;
		uint num4 = *(A_0 + 4) - num2 >> 6;
		if (num4 == 67108863)
		{
			<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xlength();
		}
		uint num5 = num4 + 1;
		uint num6 = <Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Calculate_growth(A_0, num5);
		FfuReader.partition* ptr = <Module>.std.allocator<FfuReader::partition>.allocate(A_0, num6);
		FfuReader.partition* ptr2 = ptr + num3 * 64;
		FfuReader.partition* ptr3 = ptr2 + 64;
		FfuReader.partition* ptr4 = ptr3;
		uint exceptionCode;
		try
		{
			<Module>.FfuReader.partition.{ctor}(ptr2, <_Val_0>);
			ptr4 = ptr2;
			int num7 = *(A_0 + 4);
			if (_Whereptr == num7)
			{
				FfuReader.partition* ptr5 = num7;
				FfuReader.partition* ptr6 = *A_0;
				integral_constant<bool,1> integral_constant<bool,1>;
				initblk(ref integral_constant<bool,1>, 0, 1);
				integral_constant<bool,1> integral_constant<bool,1>2;
				cpblk(ref integral_constant<bool,1>2, ref integral_constant<bool,1>, 1);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(ptr6, ptr5, ptr, A_0);
			}
			else
			{
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(*A_0, _Whereptr, ptr, A_0);
				ptr4 = ptr;
				FfuReader.partition* ptr7 = *(A_0 + 4);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(_Whereptr, ptr7, ptr2 + 64, A_0);
			}
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num8 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Destroy(A_0, ptr4, ptr3);
					<Module>.std.allocator<FfuReader::partition>.deallocate(A_0, ptr, num6);
					<Module>._CxxThrowException(null, null);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num8 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num8 != 0U);
				})
				{
				}
				if (num8 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num8);
			}
		}
		<Module>.std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Change_array(A_0, ptr, num5, num6);
		return ptr + num3 * 64;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000499C File Offset: 0x00003D9C
	internal unsafe static _Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>* std._Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>.{ctor}<>(_Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>* A_0, _Zero_then_variadic_args_t A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00003D90 File Offset: 0x00003190
	internal unsafe static FfuReader.BlockDataEntry* std.forward<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(FfuReader.BlockDataEntry* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00003DA0 File Offset: 0x000031A0
	internal unsafe static FfuReader.BlockDataEntry* std._Unfancy<struct\u0020FfuReader::BlockDataEntry>(FfuReader.BlockDataEntry* _Ptr)
	{
		return _Ptr;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000049B8 File Offset: 0x00003DB8
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::BlockDataEntry>\u0020>.construct<struct\u0020FfuReader::BlockDataEntry,struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(allocator<FfuReader::BlockDataEntry>* __unnamed000, FfuReader.BlockDataEntry* _Ptr, FfuReader.BlockDataEntry* <_Args_0>)
	{
		cpblk(_Ptr, <_Args_0>, 24);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000070A4 File Offset: 0x000064A4
	internal unsafe static FfuReader.BlockDataEntry* std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Emplace_reallocate<struct\u0020FfuReader::BlockDataEntry\u0020const\u0020&>(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _Whereptr, FfuReader.BlockDataEntry* <_Val_0>)
	{
		int num = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		int num2 = *A_0;
		uint num3 = (_Whereptr - num2) / 24;
		uint num4 = (*(A_0 + 4) - num2) / 24;
		if (num4 == 178956970)
		{
			<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Xlength();
		}
		uint num5 = num4 + 1;
		uint num6 = <Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Calculate_growth(A_0, num5);
		FfuReader.BlockDataEntry* ptr = <Module>.std.allocator<FfuReader::BlockDataEntry>.allocate(A_0, num6);
		FfuReader.BlockDataEntry* ptr2 = ptr + num3 * 24;
		FfuReader.BlockDataEntry* ptr3 = ptr2 + 24;
		FfuReader.BlockDataEntry* ptr4 = ptr3;
		uint exceptionCode;
		try
		{
			cpblk(ptr2, <_Val_0>, 24);
			ptr4 = ptr2;
			int num7 = *(A_0 + 4);
			if (_Whereptr == num7)
			{
				FfuReader.BlockDataEntry* ptr5 = num7;
				FfuReader.BlockDataEntry* ptr6 = *A_0;
				integral_constant<bool,1> integral_constant<bool,1>;
				initblk(ref integral_constant<bool,1>, 0, 1);
				integral_constant<bool,1> integral_constant<bool,1>2;
				cpblk(ref integral_constant<bool,1>2, ref integral_constant<bool,1>, 1);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(ptr6, ptr5, ptr, A_0);
			}
			else
			{
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(*A_0, _Whereptr, ptr, A_0);
				ptr4 = ptr;
				FfuReader.BlockDataEntry* ptr7 = *(A_0 + 4);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(_Whereptr, ptr7, ptr2 + 24, A_0);
			}
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num8 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Destroy(A_0, ptr4, ptr3);
					<Module>.std.allocator<FfuReader::BlockDataEntry>.deallocate(A_0, ptr, num6);
					<Module>._CxxThrowException(null, null);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num8 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num8 != 0U);
				})
				{
				}
				if (num8 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num8);
			}
		}
		<Module>.std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Change_array(A_0, ptr, num5, num6);
		return ptr + num3 * 24;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000049CC File Offset: 0x00003DCC
	internal unsafe static _Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>* std._Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>.{ctor}<>(_Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>* A_0, _Zero_then_variadic_args_t A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00003DB0 File Offset: 0x000031B0
	internal unsafe static FfuReader.WriteRequest* std.forward<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(FfuReader.WriteRequest* _Arg)
	{
		return _Arg;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00003DC0 File Offset: 0x000031C0
	internal unsafe static FfuReader.WriteRequest* std._Unfancy<struct\u0020FfuReader::WriteRequest>(FfuReader.WriteRequest* _Ptr)
	{
		return _Ptr;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000049E8 File Offset: 0x00003DE8
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::WriteRequest>\u0020>.construct<struct\u0020FfuReader::WriteRequest,struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(allocator<FfuReader::WriteRequest>* __unnamed000, FfuReader.WriteRequest* _Ptr, FfuReader.WriteRequest* <_Args_0>)
	{
		cpblk(_Ptr, <_Args_0>, 40);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000722C File Offset: 0x0000662C
	internal unsafe static FfuReader.WriteRequest* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Emplace_reallocate<struct\u0020FfuReader::WriteRequest\u0020const\u0020&>(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _Whereptr, FfuReader.WriteRequest* <_Val_0>)
	{
		int num = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		int num2 = *A_0;
		uint num3 = (_Whereptr - num2) / 40;
		uint num4 = (*(A_0 + 4) - num2) / 40;
		if (num4 == 107374182)
		{
			<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xlength();
		}
		uint num5 = num4 + 1;
		uint num6 = <Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Calculate_growth(A_0, num5);
		FfuReader.WriteRequest* ptr = <Module>.std.allocator<FfuReader::WriteRequest>.allocate(A_0, num6);
		FfuReader.WriteRequest* ptr2 = ptr + num3 * 40;
		FfuReader.WriteRequest* ptr3 = ptr2 + 40;
		FfuReader.WriteRequest* ptr4 = ptr3;
		uint exceptionCode;
		try
		{
			cpblk(ptr2, <_Val_0>, 40);
			ptr4 = ptr2;
			int num7 = *(A_0 + 4);
			if (_Whereptr == num7)
			{
				FfuReader.WriteRequest* ptr5 = num7;
				FfuReader.WriteRequest* ptr6 = *A_0;
				integral_constant<bool,1> integral_constant<bool,1>;
				initblk(ref integral_constant<bool,1>, 0, 1);
				integral_constant<bool,1> integral_constant<bool,1>2;
				cpblk(ref integral_constant<bool,1>2, ref integral_constant<bool,1>, 1);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(ptr6, ptr5, ptr, A_0);
			}
			else
			{
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(*A_0, _Whereptr, ptr, A_0);
				ptr4 = ptr;
				FfuReader.WriteRequest* ptr7 = *(A_0 + 4);
				<Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(_Whereptr, ptr7, ptr2 + 40, A_0);
			}
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num8 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Destroy(A_0, ptr4, ptr3);
					<Module>.std.allocator<FfuReader::WriteRequest>.deallocate(A_0, ptr, num6);
					<Module>._CxxThrowException(null, null);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num8 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num8 != 0U);
				})
				{
				}
				if (num8 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num8);
			}
		}
		<Module>.std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Change_array(A_0, ptr, num5, num6);
		return ptr + num3 * 40;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000049FC File Offset: 0x00003DFC
	internal unsafe static _Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>* std._Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>.{ctor}<>(_Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>* A_0, _Zero_then_variadic_args_t A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00004A18 File Offset: 0x00003E18
	internal unsafe static int std._Traits_compare<struct\u0020std::char_traits<char>\u0020>(sbyte* _Left, uint _Left_size, sbyte* _Right, uint _Right_size)
	{
		uint num = (uint)(*(ref _Right_size < _Left_size ? ref _Right_size : ref _Left_size));
		sbyte* ptr = _Right;
		int num2 = 0;
		if (num != 0U)
		{
			byte b = *_Left;
			byte b2 = *_Right;
			if (b >= b2)
			{
				int num3 = _Left - _Right;
				while (b <= b2)
				{
					if (num == 1U)
					{
						goto IL_4A;
					}
					num -= 1U;
					ptr++;
					b = *(num3 + ptr);
					b2 = *ptr;
					if (b < b2)
					{
						goto IL_47;
					}
				}
				num2 = 1;
				goto IL_4A;
			}
			IL_47:
			num2 = -1;
		}
		IL_4A:
		int num4 = num2;
		if (num4 != null)
		{
			return num4;
		}
		if (_Left_size < _Right_size)
		{
			return -1;
		}
		return _Left_size > _Right_size;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00003DD0 File Offset: 0x000031D0
	internal static uint std._Convert_size<unsigned\u0020int>(uint _Len)
	{
		return _Len;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00005498 File Offset: 0x00004898
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_for<class\u0020<lambda_88acb98cb1d3e807ff08d7ebe077788e>,char\u0020const\u0020*>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _New_size, <lambda_88acb98cb1d3e807ff08d7ebe077788e> _Fn, sbyte* <_Args_0>)
	{
		if (_New_size > <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0))
		{
			<Module>.std._Xlen_string();
		}
		uint num = *(A_0 + 20);
		uint num2 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(A_0, _New_size);
		uint num3 = num2 + 1;
		void* ptr;
		if (num3 >= 4096)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num3);
		}
		else if (num3 != null)
		{
			ptr = <Module>.@new(num3);
		}
		else
		{
			ptr = null;
		}
		*(A_0 + 16) = _New_size;
		*(A_0 + 20) = num2;
		cpblk(ptr, <_Args_0>, _New_size);
		((byte*)ptr)[_New_size] = 0;
		if (16 <= num)
		{
			uint num4 = num + 1;
			void* ptr2 = *A_0;
			if (num4 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr2, ref num4);
			}
			<Module>.delete(ptr2, num4);
			*A_0 = ptr;
		}
		else
		{
			*A_0 = ptr;
		}
		return A_0;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00003DE0 File Offset: 0x000031E0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.addressof<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020const\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00003DF0 File Offset: 0x000031F0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.addressof<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Val)
	{
		return _Val;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00003E00 File Offset: 0x00003200
	internal unsafe static sbyte* std.addressof<char>(sbyte* _Val)
	{
		return _Val;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00003E10 File Offset: 0x00003210
	internal unsafe static codecvt<char,char,_Mbstatet>* std.addressof<class\u0020std::codecvt<char,char,struct\u0020_Mbstatet>\u0020const\u0020>(codecvt<char,char,_Mbstatet>* _Val)
	{
		return _Val;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000F9B0 File Offset: 0x0000EDB0
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_6f04b3841c6ace20ee4fd1b3dbc592d0>,char>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Size_increase, <lambda_6f04b3841c6ace20ee4fd1b3dbc592d0> _Fn, sbyte <_Args_0>)
	{
		uint num = *(A_0 + 16);
		if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0) - num < _Size_increase)
		{
			<Module>.std._Xlen_string();
		}
		uint num2 = num + _Size_increase;
		uint num3 = *(A_0 + 20);
		uint num4 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(num2, *(A_0 + 20), <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0));
		uint num5 = num4 + 1;
		void* ptr;
		if (num5 >= 4096)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num5);
		}
		else if (num5 != null)
		{
			ptr = <Module>.@new(num5);
		}
		else
		{
			ptr = null;
		}
		*(A_0 + 16) = num2;
		*(A_0 + 20) = num4;
		if (16 <= num3)
		{
			sbyte* ptr2 = *A_0;
			cpblk(ptr, ptr2, num);
			num[(byte*)ptr] = <_Args_0>;
			((byte*)(num + (byte*)ptr))[1] = 0;
			uint num6 = num3 + 1;
			void* ptr3 = ptr2;
			if (num6 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num6);
			}
			<Module>.delete(ptr3, num6);
			*A_0 = ptr;
		}
		else
		{
			cpblk(ptr, A_0, num);
			num[(byte*)ptr] = <_Args_0>;
			((byte*)(num + (byte*)ptr))[1] = 0;
			*A_0 = ptr;
		}
		return A_0;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000552C File Offset: 0x0000492C
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Reallocate_grow_by<class\u0020<lambda_4c04651f1675f62dd3603168f157397a>,char\u0020const\u0020*,unsigned\u0020int>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint _Size_increase, <lambda_4c04651f1675f62dd3603168f157397a> _Fn, sbyte* <_Args_0>, uint <_Args_1>)
	{
		uint num = *(A_0 + 16);
		if (<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0) - num < _Size_increase)
		{
			<Module>.std._Xlen_string();
		}
		uint num2 = num + _Size_increase;
		uint num3 = *(A_0 + 20);
		uint num4 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Calculate_growth(num2, *(A_0 + 20), <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0));
		uint num5 = num4 + 1;
		void* ptr;
		if (num5 >= 4096)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num5);
		}
		else if (num5 != null)
		{
			ptr = <Module>.@new(num5);
		}
		else
		{
			ptr = null;
		}
		*(A_0 + 16) = num2;
		*(A_0 + 20) = num4;
		if (16 <= num3)
		{
			sbyte* ptr2 = *A_0;
			cpblk(ptr, ptr2, num);
			cpblk(num + (byte*)ptr, <_Args_0>, <_Args_1>);
			((byte*)(num + (byte*)ptr))[<_Args_1>] = 0;
			uint num6 = num3 + 1;
			void* ptr3 = ptr2;
			if (num6 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num6);
			}
			<Module>.delete(ptr3, num6);
			*A_0 = ptr;
		}
		else
		{
			cpblk(ptr, A_0, num);
			int num7 = num + (byte*)ptr;
			cpblk(num7, <_Args_0>, <_Args_1>);
			*(num7 + (int)<_Args_1>) = 0;
			*A_0 = ptr;
		}
		return A_0;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00003E20 File Offset: 0x00003220
	internal unsafe static void std._Pocca<class\u0020std::allocator<char>\u0020>(allocator<char>* _Left, allocator<char>* _Right)
	{
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00003E30 File Offset: 0x00003230
	internal unsafe static void std._Pocma<class\u0020std::allocator<char>\u0020>(allocator<char>* _Left, allocator<char>* _Right)
	{
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000097B0 File Offset: 0x00008BB0
	internal unsafe static void std._Destroy_range<class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(FfuReader.partition* _First, FfuReader.partition* _Last, allocator<FfuReader::partition>* _Al)
	{
		if (_First != _Last)
		{
			do
			{
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(_First);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)_First);
					throw;
				}
				_First += 64 / sizeof(FfuReader.partition);
			}
			while (_First != _Last);
		}
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00003E40 File Offset: 0x00003240
	internal unsafe static void std._Destroy_range<class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last, allocator<FfuReader::BlockDataEntry>* _Al)
	{
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00003E50 File Offset: 0x00003250
	internal unsafe static void std._Destroy_range<class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last, allocator<FfuReader::WriteRequest>* _Al)
	{
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00005604 File Offset: 0x00004A04
	internal unsafe static _Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{ctor}<class\u0020std::allocator<char>\u0020const\u0020&>(_Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>* A_0, _One_then_variadic_args_t __unnamed000, allocator<char>* _Val1)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000066C8 File Offset: 0x00005AC8
	internal unsafe static FfuReader.partition* FfuReader.partition.{ctor}(FfuReader.partition* A_0, FfuReader.partition* A_0)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(A_0, A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		try
		{
			*(A_0 + 24) = *(A_0 + 24);
			*(A_0 + 32) = *(A_0 + 32);
			*(A_0 + 40) = *(A_0 + 40);
			*(A_0 + 48) = *(A_0 + 48);
			*(A_0 + 56) = *(A_0 + 56);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00004A84 File Offset: 0x00003E84
	internal unsafe static _Facet_base* std.unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>.release(unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>* A_0)
	{
		_Facet_base* ptr = *A_0;
		*A_0 = 0;
		return ptr;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00004A98 File Offset: 0x00003E98
	internal unsafe static void std.unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>.{dtor}(unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>* A_0)
	{
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			_Facet_base* ptr = num;
			void* ptr2 = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt32), ptr, 1U, (IntPtr)(*(*(int*)ptr)));
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00003E60 File Offset: 0x00003260
	internal unsafe static _Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* std._Vector_val<std::_Simple_types<FfuReader::partition>\u0020>.{ctor}(_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00003E7C File Offset: 0x0000327C
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Xlength()
	{
		<Module>.std._Xlength_error((sbyte*)(&<Module>.??_C@_0BA@FOIKENOD@vector?5too?5long@));
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000ACB4 File Offset: 0x0000A0B4
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Change_array(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _Newvec, uint _Newsize, uint _Newcapacity)
	{
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			FfuReader.partition* ptr = *(A_0 + 4);
			FfuReader.partition* ptr2 = num;
			if (ptr2 != ptr)
			{
				do
				{
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr2);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)ptr2);
						throw;
					}
					ptr2 += 64 / sizeof(FfuReader.partition);
				}
				while (ptr2 != ptr);
			}
			num = (uint)(*A_0);
			uint num2 = (uint)((*(A_0 + 8) - (int)num >> 6) * 64);
			void* ptr3 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr3, ref num2);
			}
			<Module>.delete(ptr3, num2);
		}
		*A_0 = _Newvec;
		*(A_0 + 4) = _Newsize * 64 + _Newvec;
		*(A_0 + 8) = _Newcapacity * 64 + _Newvec;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00005F7C File Offset: 0x0000537C
	internal unsafe static uint std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Calculate_growth(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, uint _Newsize)
	{
		uint num = *(A_0 + 8) - *A_0 >> 6;
		uint num2 = num >> 1;
		if (num > 67108863U - num2)
		{
			return 67108863;
		}
		uint num3 = num2 + num;
		return (num3 < _Newsize) ? _Newsize : num3;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000AEAC File Offset: 0x0000A2AC
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Umove_if_noexcept(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _First, FfuReader.partition* _Last, FfuReader.partition* _Dest)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000AE68 File Offset: 0x0000A268
	internal unsafe static FfuReader.partition* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Umove(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _First, FfuReader.partition* _Last, FfuReader.partition* _Dest)
	{
		return <Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000564C File Offset: 0x00004A4C
	internal unsafe static uint std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.max_size(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		return 67108863;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00004AB8 File Offset: 0x00003EB8
	internal unsafe static FfuReader.partition* std.allocator<FfuReader::partition>.allocate(allocator<FfuReader::partition>* A_0, uint _Count)
	{
		if (_Count > 67108863)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		uint num = _Count * 64;
		void* ptr;
		if (num >= 4096U)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num);
		}
		else if (num != 0U)
		{
			ptr = <Module>.@new(num);
		}
		else
		{
			ptr = null;
		}
		return ptr;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00003E94 File Offset: 0x00003294
	internal unsafe static allocator<FfuReader::partition>* std.allocator<FfuReader::partition>.{ctor}(allocator<FfuReader::partition>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00003EA4 File Offset: 0x000032A4
	internal unsafe static _Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>* std._Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>.{ctor}(_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00003EC0 File Offset: 0x000032C0
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Xlength()
	{
		<Module>.std._Xlength_error((sbyte*)(&<Module>.??_C@_0BA@FOIKENOD@vector?5too?5long@));
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00004AF8 File Offset: 0x00003EF8
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Change_array(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _Newvec, uint _Newsize, uint _Newcapacity)
	{
		ref int ptr = A_0 + 4;
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			uint num2 = (uint)((*(A_0 + 8) - (int)num) / 24 * 24);
			void* ptr2 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr2, ref num2);
			}
			<Module>.delete(ptr2, num2);
		}
		*A_0 = _Newvec;
		ptr = _Newsize * 24 + _Newvec;
		*(A_0 + 8) = _Newcapacity * 24 + _Newvec;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00005FB4 File Offset: 0x000053B4
	internal unsafe static uint std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Calculate_growth(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, uint _Newsize)
	{
		uint num = (*(A_0 + 8) - *A_0) / 24;
		uint num2 = num >> 1;
		if (num > 178956970U - num2)
		{
			return 178956970;
		}
		uint num3 = num2 + num;
		return (num3 < _Newsize) ? _Newsize : num3;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000067A4 File Offset: 0x00005BA4
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Umove_if_noexcept(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last, FfuReader.BlockDataEntry* _Dest)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00005FEC File Offset: 0x000053EC
	internal unsafe static FfuReader.BlockDataEntry* std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Umove(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last, FfuReader.BlockDataEntry* _Dest)
	{
		return <Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00005660 File Offset: 0x00004A60
	internal unsafe static uint std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.max_size(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		return 178956970;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00004B4C File Offset: 0x00003F4C
	internal unsafe static FfuReader.BlockDataEntry* std.allocator<FfuReader::BlockDataEntry>.allocate(allocator<FfuReader::BlockDataEntry>* A_0, uint _Count)
	{
		if (_Count > 178956970)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		uint num = _Count * 24;
		void* ptr;
		if (num >= 4096U)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num);
		}
		else if (num != 0U)
		{
			ptr = <Module>.@new(num);
		}
		else
		{
			ptr = null;
		}
		return ptr;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00003ED8 File Offset: 0x000032D8
	internal unsafe static allocator<FfuReader::BlockDataEntry>* std.allocator<FfuReader::BlockDataEntry>.{ctor}(allocator<FfuReader::BlockDataEntry>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00003EE8 File Offset: 0x000032E8
	internal unsafe static _Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* std._Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>.{ctor}(_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>* A_0)
	{
		*A_0 = 0;
		*(A_0 + 4) = 0;
		*(A_0 + 8) = 0;
		return A_0;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00003F04 File Offset: 0x00003304
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Xlength()
	{
		<Module>.std._Xlength_error((sbyte*)(&<Module>.??_C@_0BA@FOIKENOD@vector?5too?5long@));
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00004B8C File Offset: 0x00003F8C
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Change_array(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _Newvec, uint _Newsize, uint _Newcapacity)
	{
		ref int ptr = A_0 + 4;
		uint num = (uint)(*A_0);
		if (num != 0U)
		{
			uint num2 = (uint)((*(A_0 + 8) - (int)num) / 40 * 40);
			void* ptr2 = num;
			if (num2 >= 4096U)
			{
				<Module>.std._Adjust_manually_vector_aligned(ref ptr2, ref num2);
			}
			<Module>.delete(ptr2, num2);
		}
		*A_0 = _Newvec;
		ptr = _Newsize * 40 + _Newvec;
		*(A_0 + 8) = _Newcapacity * 40 + _Newvec;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00006004 File Offset: 0x00005404
	internal unsafe static uint std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Calculate_growth(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, uint _Newsize)
	{
		uint num = (*(A_0 + 8) - *A_0) / 40;
		uint num2 = num >> 1;
		if (num > 107374182U - num2)
		{
			return 107374182;
		}
		uint num3 = num2 + num;
		return (num3 < _Newsize) ? _Newsize : num3;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000067BC File Offset: 0x00005BBC
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Umove_if_noexcept(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last, FfuReader.WriteRequest* _Dest)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000603C File Offset: 0x0000543C
	internal unsafe static FfuReader.WriteRequest* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Umove(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last, FfuReader.WriteRequest* _Dest)
	{
		return <Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00005674 File Offset: 0x00004A74
	internal unsafe static uint std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.max_size(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		return 107374182;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00004BE0 File Offset: 0x00003FE0
	internal unsafe static FfuReader.WriteRequest* std.allocator<FfuReader::WriteRequest>.allocate(allocator<FfuReader::WriteRequest>* A_0, uint _Count)
	{
		if (_Count > 107374182)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		uint num = _Count * 40;
		void* ptr;
		if (num >= 4096U)
		{
			ptr = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num);
		}
		else if (num != 0U)
		{
			ptr = <Module>.@new(num);
		}
		else
		{
			ptr = null;
		}
		return ptr;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00003F1C File Offset: 0x0000331C
	internal unsafe static allocator<FfuReader::WriteRequest>* std.allocator<FfuReader::WriteRequest>.{ctor}(allocator<FfuReader::WriteRequest>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00004C20 File Offset: 0x00004020
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Right)
	{
		sbyte* ptr = _Right;
		if (((16 <= *(_Right + 20)) ? 1 : 0) != 0)
		{
			ptr = *_Right;
		}
		sbyte* ptr2 = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *A_0;
		}
		uint num = *(A_0 + 16);
		int num4;
		if (num == *(_Right + 16))
		{
			uint num2 = num;
			sbyte* ptr3 = ptr;
			if (num != null)
			{
				byte b = *(byte*)ptr2;
				byte b2 = *(byte*)ptr;
				if (b >= b2)
				{
					int num3 = (int)(ptr2 - ptr);
					while (b <= b2)
					{
						if (num2 == 1)
						{
							goto IL_79;
						}
						num2--;
						ptr3 += 1 / sizeof(sbyte);
						b = *(byte*)(num3 / sizeof(sbyte) + ptr3);
						b2 = *(byte*)ptr3;
						if (b < b2)
						{
							break;
						}
					}
					goto IL_7E;
				}
				goto IL_7E;
			}
			IL_79:
			num4 = 1;
			goto IL_81;
		}
		IL_7E:
		num4 = 0;
		IL_81:
		return (byte)num4;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00005688 File Offset: 0x00004A88
	internal unsafe static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, _String_constructor_concat_tag __unnamed000, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Source_of_al, sbyte* _Left_ptr, uint _Left_size, sbyte* _Right_ptr, uint _Right_size)
	{
		*A_0 = 0;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr;
		basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr2;
		try
		{
			ptr = A_0 + 16;
			*ptr = 0;
			ptr2 = A_0 + 20;
			*ptr2 = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			uint num = _Left_size + _Right_size;
			uint num2 = 15U;
			sbyte* ptr3 = A_0;
			if (15 < num)
			{
				uint num3 = *(ref num < 16 ? ref <Module>.?_BUF_SIZE@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@0IB : ref num);
				uint num4 = <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.max_size(A_0);
				uint num5 = num3 | 15;
				if (num5 > num4)
				{
					num2 = num4;
				}
				else if (15 > num4 - 7)
				{
					num2 = num4;
				}
				else
				{
					uint num6 = 22;
					num2 = (uint)(*(ref num5 < 22 ? ref num6 : ref num5));
				}
				uint num7 = num2 + 1U;
				void* ptr4;
				if (num7 >= 4096)
				{
					ptr4 = <Module>.std._Allocate_manually_vector_aligned<struct\u0020std::_Default_allocate_traits>(num7);
				}
				else if (num7 != null)
				{
					ptr4 = <Module>.@new(num7);
				}
				else
				{
					ptr4 = null;
				}
				ptr3 = (sbyte*)ptr4;
				*A_0 = ptr4;
			}
			*ptr = num;
			*ptr2 = (int)num2;
			cpblk(ptr3, _Left_ptr, _Left_size);
			cpblk(ptr3 + _Left_size / sizeof(sbyte), _Right_ptr, _Right_size);
			*(byte*)(ptr3 + num / sizeof(sbyte)) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00003F2C File Offset: 0x0000332C
	internal unsafe static int std._Narrow_char_traits<char,int>.compare(sbyte* _First1, sbyte* _First2, uint _Count)
	{
		uint num = _Count;
		sbyte* ptr = _First2;
		int num2 = 0;
		if (_Count != null)
		{
			byte b = *_First1;
			byte b2 = *_First2;
			if (b >= b2)
			{
				int num3 = _First1 - _First2;
				while (b <= b2)
				{
					if (num == 1)
					{
						return num2;
					}
					num--;
					ptr++;
					b = *(num3 + ptr);
					b2 = *ptr;
					if (b < b2)
					{
						goto IL_3F;
					}
				}
				return 1;
			}
			IL_3F:
			num2 = -1;
		}
		return num2;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00003F7C File Offset: 0x0000337C
	internal unsafe static default_delete<std::_Facet_base>* std._Compressed_pair<std::default_delete<std::_Facet_base>,std::_Facet_base\u0020*,1>._Get_first(_Compressed_pair<std::default_delete<std::_Facet_base>,std::_Facet_base\u0020*,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00003F8C File Offset: 0x0000338C
	internal unsafe static void std.default_delete<std::_Facet_base>.()(default_delete<std::_Facet_base>* A_0, _Facet_base* _Ptr)
	{
		if (_Ptr != null)
		{
			void* ptr = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt32), _Ptr, 1U, (IntPtr)(*(*(int*)_Ptr)));
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00004CB4 File Offset: 0x000040B4
	internal unsafe static allocator<FfuReader::partition>* std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Getal(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000AE80 File Offset: 0x0000A280
	internal unsafe static void std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>._Umove_if_noexcept1(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _First, FfuReader.partition* _Last, FfuReader.partition* _Dest, integral_constant<bool,1> __unnamed003)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00003FA8 File Offset: 0x000033A8
	internal unsafe static uint std.vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>.capacity(vector<FfuReader::partition,std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		return *(A_0 + 8) - *A_0 >> 6;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00003FC0 File Offset: 0x000033C0
	internal unsafe static uint std._Default_allocator_traits<std::allocator<FfuReader::partition>\u0020>.max_size(allocator<FfuReader::partition>* A_0)
	{
		return 67108863;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00004CC4 File Offset: 0x000040C4
	internal unsafe static allocator<FfuReader::BlockDataEntry>* std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Getal(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00006054 File Offset: 0x00005454
	internal unsafe static void std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>._Umove_if_noexcept1(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last, FfuReader.BlockDataEntry* _Dest, integral_constant<bool,1> __unnamed003)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00003FD4 File Offset: 0x000033D4
	internal unsafe static uint std.vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>.capacity(vector<FfuReader::BlockDataEntry,std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		return (*(A_0 + 8) - *A_0) / 24;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00003FEC File Offset: 0x000033EC
	internal unsafe static uint std._Default_allocator_traits<std::allocator<FfuReader::BlockDataEntry>\u0020>.max_size(allocator<FfuReader::BlockDataEntry>* A_0)
	{
		return 178956970;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00004CD4 File Offset: 0x000040D4
	internal unsafe static allocator<FfuReader::WriteRequest>* std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Getal(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000606C File Offset: 0x0000546C
	internal unsafe static void std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>._Umove_if_noexcept1(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last, FfuReader.WriteRequest* _Dest, integral_constant<bool,1> __unnamed003)
	{
		<Module>.std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(_First, _Last, _Dest, A_0);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00004000 File Offset: 0x00003400
	internal unsafe static uint std.vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>.capacity(vector<FfuReader::WriteRequest,std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		return (*(A_0 + 8) - *A_0) / 40;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00004018 File Offset: 0x00003418
	internal unsafe static uint std._Default_allocator_traits<std::allocator<FfuReader::WriteRequest>\u0020>.max_size(allocator<FfuReader::WriteRequest>* A_0)
	{
		return 107374182;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000402C File Offset: 0x0000342C
	internal unsafe static allocator<FfuReader::partition>* std._Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::partition>,std::_Vector_val<std::_Simple_types<FfuReader::partition>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000403C File Offset: 0x0000343C
	internal unsafe static allocator<FfuReader::BlockDataEntry>* std._Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::BlockDataEntry>,std::_Vector_val<std::_Simple_types<FfuReader::BlockDataEntry>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000404C File Offset: 0x0000344C
	internal unsafe static allocator<FfuReader::WriteRequest>* std._Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>._Get_first(_Compressed_pair<std::allocator<FfuReader::WriteRequest>,std::_Vector_val<std::_Simple_types<FfuReader::WriteRequest>\u0020>,1>* A_0)
	{
		return A_0;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000057A0 File Offset: 0x00004BA0
	internal unsafe static unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>* std.unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>.{ctor}<struct\u0020std::default_delete<class\u0020std::_Facet_base>,0>(unique_ptr<std::_Facet_base,std::default_delete<std::_Facet_base>\u0020>* A_0, _Facet_base* _Ptr)
	{
		*A_0 = _Ptr;
		return A_0;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00004CE4 File Offset: 0x000040E4
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>* std._Insert_string<char,struct\u0020std::char_traits<char>,unsigned\u0020int>(basic_ostream<char,std::char_traits<char>\u0020>* _Ostr, sbyte* _Data, uint _Size)
	{
		int num = (int)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		int num2 = 0;
		uint num3;
		if (<Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) > 0L && <Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) > _Size)
		{
			num3 = <Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr) - _Size;
		}
		else
		{
			num3 = 0U;
		}
		basic_ostream<char,std::char_traits<char>\u0020>.sentry sentry;
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{ctor}(ref sentry, _Ostr);
		try
		{
			if (*((ref sentry) + 4) != 0)
			{
				uint exceptionCode;
				try
				{
					if ((<Module>.std.ios_base.flags(*(*_Ostr + 4) + _Ostr) & 448) == 64)
					{
						goto IL_AC;
					}
					while (0U < num3)
					{
						int num4 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputc(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.fill(*(*_Ostr + 4) + _Ostr));
						if (((-1 == num4) ? 1 : 0) != 0)
						{
							num2 |= 4;
							break;
						}
						num3 -= 1U;
					}
					if (num2 == 0)
					{
						goto IL_AC;
					}
					IL_C9:
					while (0U < num3)
					{
						int num5 = <Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputc(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), <Module>.std.basic_ios<char,std::char_traits<char>\u0020>.fill(*(*_Ostr + 4) + _Ostr));
						if (((-1 == num5) ? 1 : 0) != 0)
						{
							num2 |= 4;
							break;
						}
						num3 -= 1U;
					}
					goto IL_103;
					IL_AC:
					long num6 = (long)_Size;
					if (<Module>.std.basic_streambuf<char,std::char_traits<char>\u0020>.sputn(<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(*(*_Ostr + 4) + _Ostr), _Data, num6) == num6)
					{
						goto IL_C9;
					}
					num2 = 4;
					IL_103:
					<Module>.std.ios_base.width(*(*_Ostr + 4) + _Ostr, 0L);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					exceptionCode = (uint)Marshal.GetExceptionCode();
					endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
				})
				{
					uint num7 = 0U;
					<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
					try
					{
						try
						{
							<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*_Ostr + 4) + _Ostr, 4, true);
							goto IL_16F;
						}
						catch when (delegate
						{
							// Failed to create a 'catch-when' expression
							num7 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
							endfilter(num7 != 0U);
						})
						{
						}
						if (num7 != 0U)
						{
							throw;
						}
					}
					finally
					{
						<Module>.__CxxUnregisterExceptionObject(num, (int)num7);
					}
				}
			}
			else
			{
				num2 = 4;
			}
			IL_16F:
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*_Ostr + 4) + _Ostr, num2, false);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{dtor}), (void*)(&sentry));
			throw;
		}
		<Module>.std.basic_ostream<char,std::char_traits<char>\u0020>.sentry.{dtor}(ref sentry);
		return _Ostr;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000057B4 File Offset: 0x00004BB4
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.operator==<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* _Left, sbyte* _Right)
	{
		return <Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(_Left, _Right);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000405C File Offset: 0x0000345C
	internal unsafe static void* std._Voidify_iter<struct\u0020FfuReader::partition\u0020*>(FfuReader.partition* _It)
	{
		return _It;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000406C File Offset: 0x0000346C
	internal unsafe static void* std._Voidify_iter<struct\u0020FfuReader::BlockDataEntry\u0020*>(FfuReader.BlockDataEntry* _It)
	{
		return _It;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000407C File Offset: 0x0000347C
	internal unsafe static void* std._Voidify_iter<struct\u0020FfuReader::WriteRequest\u0020*>(FfuReader.WriteRequest* _It)
	{
		return _It;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000073B4 File Offset: 0x000067B4
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::partition>\u0020>.destroy<struct\u0020FfuReader::partition>(allocator<FfuReader::partition>* __unnamed000, FfuReader.partition* _Ptr)
	{
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(_Ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), _Ptr);
			throw;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000408C File Offset: 0x0000348C
	internal unsafe static allocator<char>* std.forward<class\u0020std::allocator<char>\u0020const\u0020&>(allocator<char>* _Arg)
	{
		return _Arg;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000409C File Offset: 0x0000349C
	internal unsafe static _Facet_base* std.exchange<class\u0020std::_Facet_base\u0020*,std::nullptr_t>(_Facet_base** _Val, void** _New_val)
	{
		_Facet_base* ptr = *_Val;
		*_Val = *_New_val;
		return ptr;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000AD50 File Offset: 0x0000A150
	internal unsafe static FfuReader.partition* std._Uninitialized_move<struct\u0020FfuReader::partition\u0020*,class\u0020std::allocator<struct\u0020FfuReader::partition>\u0020>(FfuReader.partition* _First, FfuReader.partition* _Last, FfuReader.partition* _Dest, allocator<FfuReader::partition>* _Al)
	{
		FfuReader.partition* ptr = _First;
		_Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020> uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020> = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4) = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 8) = _Al;
		FfuReader.partition* ptr2;
		try
		{
			if (_First != _Last)
			{
				do
				{
					<Module>.FfuReader.partition.{ctor}(*((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4), ptr);
					*((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4) = *((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4) + 64;
					ptr += 64 / sizeof(FfuReader.partition);
				}
				while (ptr != _Last);
			}
			uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020> = *((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4);
			ptr2 = *((ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>) + 4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>.{dtor}), (void*)(&uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>));
			throw;
		}
		<Module>.std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>.{dtor}(ref uninitialized_backout_al<std::allocator<FfuReader::partition>_u0020>);
		return ptr2;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000040B0 File Offset: 0x000034B0
	internal unsafe static FfuReader.partition* std._Get_unwrapped<struct\u0020FfuReader::partition\u0020*\u0020const\u0020&>(FfuReader.partition** _It)
	{
		return *_It;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000040C0 File Offset: 0x000034C0
	internal static uint std._Get_size_of_n<64>(uint _Count)
	{
		if (_Count > 67108863)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		return _Count * 64;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000057C8 File Offset: 0x00004BC8
	internal unsafe static FfuReader.BlockDataEntry* std._Uninitialized_move<struct\u0020FfuReader::BlockDataEntry\u0020*,class\u0020std::allocator<struct\u0020FfuReader::BlockDataEntry>\u0020>(FfuReader.BlockDataEntry* _First, FfuReader.BlockDataEntry* _Last, FfuReader.BlockDataEntry* _Dest, allocator<FfuReader::BlockDataEntry>* _Al)
	{
		FfuReader.BlockDataEntry* ptr = _First;
		_Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020> uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020> = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4) = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 8) = _Al;
		try
		{
			if (_First != _Last)
			{
				do
				{
					cpblk(*((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4), ptr, 24);
					*((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4) = *((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4) + 24;
					ptr += 24 / sizeof(FfuReader.BlockDataEntry);
				}
				while (ptr != _Last);
			}
			uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020> = *((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>.{dtor}), (void*)(&uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>));
			throw;
		}
		return *((ref uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>_u0020>) + 4);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000040E0 File Offset: 0x000034E0
	internal unsafe static FfuReader.BlockDataEntry* std._Get_unwrapped<struct\u0020FfuReader::BlockDataEntry\u0020*\u0020const\u0020&>(FfuReader.BlockDataEntry** _It)
	{
		return *_It;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x000040F0 File Offset: 0x000034F0
	internal static uint std._Get_size_of_n<24>(uint _Count)
	{
		if (_Count > 178956970)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		return _Count * 24;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00005844 File Offset: 0x00004C44
	internal unsafe static FfuReader.WriteRequest* std._Uninitialized_move<struct\u0020FfuReader::WriteRequest\u0020*,class\u0020std::allocator<struct\u0020FfuReader::WriteRequest>\u0020>(FfuReader.WriteRequest* _First, FfuReader.WriteRequest* _Last, FfuReader.WriteRequest* _Dest, allocator<FfuReader::WriteRequest>* _Al)
	{
		FfuReader.WriteRequest* ptr = _First;
		_Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020> uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020> = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4) = _Dest;
		*((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 8) = _Al;
		try
		{
			if (_First != _Last)
			{
				do
				{
					cpblk(*((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4), ptr, 40);
					*((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4) = *((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4) + 40;
					ptr += 40 / sizeof(FfuReader.WriteRequest);
				}
				while (ptr != _Last);
			}
			uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020> = *((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>.{dtor}), (void*)(&uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>));
			throw;
		}
		return *((ref uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>_u0020>) + 4);
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00004110 File Offset: 0x00003510
	internal unsafe static FfuReader.WriteRequest* std._Get_unwrapped<struct\u0020FfuReader::WriteRequest\u0020*\u0020const\u0020&>(FfuReader.WriteRequest** _It)
	{
		return *_It;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00004120 File Offset: 0x00003520
	internal static uint std._Get_size_of_n<40>(uint _Count)
	{
		if (_Count > 107374182)
		{
			<Module>.std._Throw_bad_array_new_length();
		}
		return _Count * 40;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00004140 File Offset: 0x00003540
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std._Traits_equal<struct\u0020std::char_traits<char>\u0020>(sbyte* _Left, uint _Left_size, sbyte* _Right, uint _Right_size)
	{
		int num3;
		if (_Left_size == _Right_size)
		{
			uint num = _Left_size;
			sbyte* ptr = _Right;
			if (_Left_size != null)
			{
				byte b = *_Left;
				byte b2 = *_Right;
				if (b >= b2)
				{
					int num2 = _Left - _Right;
					while (b <= b2)
					{
						if (num == 1)
						{
							goto IL_39;
						}
						num--;
						ptr++;
						b = *(num2 + ptr);
						b2 = *ptr;
						if (b < b2)
						{
							break;
						}
					}
					goto IL_3E;
				}
				goto IL_3E;
			}
			IL_39:
			num3 = 1;
			goto IL_41;
		}
		IL_3E:
		num3 = 0;
		IL_41:
		return (byte)num3;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000067D4 File Offset: 0x00005BD4
	internal unsafe static void* FfuReader.partition.__delDtor(FfuReader.partition* A_0, uint A_0)
	{
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 64U);
		}
		return A_0;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00004194 File Offset: 0x00003594
	internal unsafe static FfuReader.WriteRequest* std._Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>._Release(_Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
		int num = *(A_0 + 4);
		*A_0 = num;
		return num;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000041AC File Offset: 0x000035AC
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>.{dtor}(_Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>* A_0)
	{
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000041BC File Offset: 0x000035BC
	internal unsafe static _Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>* std._Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>.{ctor}(_Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* _Dest, allocator<FfuReader::WriteRequest>* _Al_)
	{
		*A_0 = _Dest;
		*(A_0 + 4) = _Dest;
		*(A_0 + 8) = _Al_;
		return A_0;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000041D8 File Offset: 0x000035D8
	internal unsafe static FfuReader.BlockDataEntry* std._Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>._Release(_Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
		int num = *(A_0 + 4);
		*A_0 = num;
		return num;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000041F0 File Offset: 0x000035F0
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>.{dtor}(_Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0)
	{
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00004200 File Offset: 0x00003600
	internal unsafe static _Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>* std._Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>.{ctor}(_Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* _Dest, allocator<FfuReader::BlockDataEntry>* _Al_)
	{
		*A_0 = _Dest;
		*(A_0 + 4) = _Dest;
		*(A_0 + 8) = _Al_;
		return A_0;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000421C File Offset: 0x0000361C
	internal unsafe static FfuReader.partition* std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>._Release(_Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		int num = *(A_0 + 4);
		*A_0 = num;
		return num;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000097FC File Offset: 0x00008BFC
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>.{dtor}(_Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>* A_0)
	{
		FfuReader.partition* ptr = *(A_0 + 4);
		FfuReader.partition* ptr2 = *A_0;
		if (ptr2 != ptr)
		{
			do
			{
				try
				{
					<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate(ptr2);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)ptr2);
					throw;
				}
				ptr2 += 64 / sizeof(FfuReader.partition);
			}
			while (ptr2 != ptr);
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00004234 File Offset: 0x00003634
	internal unsafe static _Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>* std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>.{ctor}(_Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* _Dest, allocator<FfuReader::partition>* _Al_)
	{
		*A_0 = _Dest;
		*(A_0 + 4) = _Dest;
		*(A_0 + 8) = _Al_;
		return A_0;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00004EEC File Offset: 0x000042EC
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Equal(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, sbyte* _Ptr)
	{
		sbyte* ptr = _Ptr;
		sbyte b = *_Ptr;
		if (b != 0)
		{
			do
			{
				ptr++;
			}
			while (*ptr != 0);
		}
		int num = ptr - _Ptr;
		sbyte* ptr2 = A_0;
		if (((16 <= *(A_0 + 20)) ? 1 : 0) != 0)
		{
			ptr2 = *A_0;
		}
		uint num2 = *(A_0 + 16);
		int num5;
		if (num2 == num)
		{
			uint num3 = num2;
			sbyte* ptr3 = _Ptr;
			if (num2 != null)
			{
				byte b2 = *(byte*)ptr2;
				byte b3 = (byte)b;
				if (b2 >= b3)
				{
					int num4 = ptr2 - _Ptr / sizeof(sbyte);
					while (b2 <= b3)
					{
						if (num3 == 1)
						{
							goto IL_78;
						}
						num3--;
						ptr3++;
						b2 = *(num4 + ptr3);
						b3 = *ptr3;
						if (b2 < b3)
						{
							break;
						}
					}
					goto IL_7D;
				}
				goto IL_7D;
			}
			IL_78:
			num5 = 1;
			goto IL_80;
		}
		IL_7D:
		num5 = 0;
		IL_80:
		return (byte)num5;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00004F7C File Offset: 0x0000437C
	internal unsafe static _Compressed_pair<std::default_delete<std::_Facet_base>,std::_Facet_base\u0020*,1>* std._Compressed_pair<std::default_delete<std::_Facet_base>,std::_Facet_base\u0020*,1>.{ctor}<class\u0020std::_Facet_base\u0020*\u0020&>(_Compressed_pair<std::default_delete<std::_Facet_base>,std::_Facet_base\u0020*,1>* A_0, _Zero_then_variadic_args_t __unnamed000, _Facet_base** <_Val2_0>)
	{
		*A_0 = *<_Val2_0>;
		return A_0;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00004250 File Offset: 0x00003650
	internal unsafe static FfuReader.partition* std.move<struct\u0020FfuReader::partition\u0020&>(FfuReader.partition* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000073F4 File Offset: 0x000067F4
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>._Emplace_back<struct\u0020FfuReader::partition>(_Uninitialized_backout_al<std::allocator<FfuReader::partition>\u0020>* A_0, FfuReader.partition* <_Vals_0>)
	{
		<Module>.FfuReader.partition.{ctor}(*(A_0 + 4), <_Vals_0>);
		*(A_0 + 4) = *(A_0 + 4) + 64;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00004260 File Offset: 0x00003660
	internal unsafe static FfuReader.BlockDataEntry* std.move<struct\u0020FfuReader::BlockDataEntry\u0020&>(FfuReader.BlockDataEntry* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00004F90 File Offset: 0x00004390
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>._Emplace_back<struct\u0020FfuReader::BlockDataEntry>(_Uninitialized_backout_al<std::allocator<FfuReader::BlockDataEntry>\u0020>* A_0, FfuReader.BlockDataEntry* <_Vals_0>)
	{
		cpblk(*(A_0 + 4), <_Vals_0>, 24);
		*(A_0 + 4) = *(A_0 + 4) + 24;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00004270 File Offset: 0x00003670
	internal unsafe static FfuReader.WriteRequest* std.move<struct\u0020FfuReader::WriteRequest\u0020&>(FfuReader.WriteRequest* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00004FB4 File Offset: 0x000043B4
	internal unsafe static void std._Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>._Emplace_back<struct\u0020FfuReader::WriteRequest>(_Uninitialized_backout_al<std::allocator<FfuReader::WriteRequest>\u0020>* A_0, FfuReader.WriteRequest* <_Vals_0>)
	{
		cpblk(*(A_0 + 4), <_Vals_0>, 40);
		*(A_0 + 4) = *(A_0 + 4) + 40;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00004280 File Offset: 0x00003680
	internal unsafe static _Facet_base** std.forward<class\u0020std::_Facet_base\u0020*\u0020&>(_Facet_base** _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00004290 File Offset: 0x00003690
	internal unsafe static FfuReader.partition* std.forward<struct\u0020FfuReader::partition>(FfuReader.partition* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00006820 File Offset: 0x00005C20
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::partition>\u0020>.construct<struct\u0020FfuReader::partition,struct\u0020FfuReader::partition>(allocator<FfuReader::partition>* __unnamed000, FfuReader.partition* _Ptr, FfuReader.partition* <_Args_0>)
	{
		<Module>.FfuReader.partition.{ctor}(_Ptr, <_Args_0>);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000042A0 File Offset: 0x000036A0
	internal unsafe static FfuReader.BlockDataEntry* std.forward<struct\u0020FfuReader::BlockDataEntry>(FfuReader.BlockDataEntry* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x000042B0 File Offset: 0x000036B0
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::BlockDataEntry>\u0020>.construct<struct\u0020FfuReader::BlockDataEntry,struct\u0020FfuReader::BlockDataEntry>(allocator<FfuReader::BlockDataEntry>* __unnamed000, FfuReader.BlockDataEntry* _Ptr, FfuReader.BlockDataEntry* <_Args_0>)
	{
		cpblk(_Ptr, <_Args_0>, 24);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x000042C4 File Offset: 0x000036C4
	internal unsafe static FfuReader.WriteRequest* std.forward<struct\u0020FfuReader::WriteRequest>(FfuReader.WriteRequest* _Arg)
	{
		return _Arg;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x000042D4 File Offset: 0x000036D4
	internal unsafe static void std._Default_allocator_traits<std::allocator<FfuReader::WriteRequest>\u0020>.construct<struct\u0020FfuReader::WriteRequest,struct\u0020FfuReader::WriteRequest>(allocator<FfuReader::WriteRequest>* __unnamed000, FfuReader.WriteRequest* _Ptr, FfuReader.WriteRequest* <_Args_0>)
	{
		cpblk(_Ptr, <_Args_0>, 40);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00006084 File Offset: 0x00005484
	internal unsafe static FfuReader.partition* FfuReader.partition.{ctor}(FfuReader.partition* A_0, FfuReader.partition* A_0)
	{
		*A_0 = 0;
		try
		{
			*(A_0 + 16) = 0;
			*(A_0 + 20) = 0;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._String_val<std::_Simple_types<char>\u0020>._Bxty.{dtor}), A_0);
			throw;
		}
		try
		{
			<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Take_contents(A_0, A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), A_0);
			throw;
		}
		try
		{
			*(A_0 + 24) = *(A_0 + 24);
			*(A_0 + 32) = *(A_0 + 32);
			*(A_0 + 40) = *(A_0 + 40);
			*(A_0 + 48) = *(A_0 + 48);
			*(A_0 + 56) = *(A_0 + 56);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000107F0 File Offset: 0x0000FBF0
	internal unsafe static void* ??_E?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@$$F$4PPPPPPPM@A@AEPAXI@Z(basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* A_0, uint A_0)
	{
		basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = A_0;
		A_0 = ptr - *(ptr + -4);
		jmp(std.basic_stringstream<char,std::char_traits<char>,std::allocator<char>\u0020>.__vecDelDtor());
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000106B4 File Offset: 0x0000FAB4
	internal unsafe static void* ??_E?$basic_ofstream@DU?$char_traits@D@std@@@std@@$$F$4PPPPPPPM@A@AEPAXI@Z(basic_ofstream<char,std::char_traits<char>\u0020>* A_0, uint A_0)
	{
		basic_ofstream<char,std::char_traits<char>\u0020>* ptr = A_0;
		A_0 = ptr - *(ptr + -4);
		jmp(std.basic_ofstream<char,std::char_traits<char>\u0020>.__vecDelDtor());
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00013304 File Offset: 0x00012704
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00012CC8 File Offset: 0x000120C8
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00012CDC File Offset: 0x000120DC
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00012DF8 File Offset: 0x000121F8
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00012E10 File Offset: 0x00012210
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4), *(guid + 6), *(guid + 8), *(guid + 9), *(guid + 10), *(guid + 11), *(guid + 12), *(guid + 13), *(guid + 14), *(guid + 15));
		return guid2;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00012E58 File Offset: 0x00012258
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), guid).ToPointer();
			goto IL_35;
		}
		catch (System.Exception ex)
		{
			num = Marshal.GetHRForException(ex);
		}
		if (num < 0)
		{
			return num;
		}
		IL_35:
		int num2 = *(*(int*)ptr + 52);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr,IUnknown**), ptr, ppUnk, (IntPtr)num2);
		ICorRuntimeHost* ptr2 = ptr;
		uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr), ptr2, (IntPtr)(*(*(int*)ptr2 + 8)));
		return num;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00012ED4 File Offset: 0x000122D4
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr), ppUnk, (IntPtr)(*(*(int*)ppUnk + 8)));
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00012EF0 File Offset: 0x000122F0
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00012F50 File Offset: 0x00012350
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), guid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			int num = *(*(int*)ptr + 32);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall) (System.Void*),System.Void*), ptr, id, function, cookie, (IntPtr)num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr), ptr2, (IntPtr)(*(*(int*)ptr2 + 8)));
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00013004 File Offset: 0x00012404
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0001303C File Offset: 0x0001243C
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0001305C File Offset: 0x0001245C
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if ((ref <Module>.__xc_mp_a) < (ref <Module>.__xc_mp_z))
			{
				while (*(int*)ptr == 0)
				{
					ptr += 4 / sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_34;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_34:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000130B0 File Offset: 0x000124B0
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if ((ref <Module>.__xi_a) < (ref <Module>.__xi_z))
			{
				while (*(int*)ptr == 0)
				{
					ptr += 4 / sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_34;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_34:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if ((ref <Module>.__xc_a) < (ref <Module>.__xc_z))
			{
				while (*(int*)ptr2 == 0)
				{
					ptr2 += 4 / sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_60;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_60:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00013130 File Offset: 0x00012530
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00013168 File Offset: 0x00012568
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0001317C File Offset: 0x0001257C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCGJPAX@Z, null);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00001094 File Offset: 0x00000494
	internal static void ?A0x98f5405c.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000010A8 File Offset: 0x000004A8
	internal static void ?A0x98f5405c.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000010BC File Offset: 0x000004BC
	internal static void ?A0x98f5405c.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000010D0 File Offset: 0x000004D0
	internal static void ?A0x98f5405c.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x000010E4 File Offset: 0x000004E4
	internal static void ?A0x98f5405c.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x000010F8 File Offset: 0x000004F8
	internal static void ?A0x98f5405c.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000110C File Offset: 0x0000050C
	internal static void ?A0x98f5405c.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00013358 File Offset: 0x00012758
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0001338C File Offset: 0x0001278C
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x000133AC File Offset: 0x000127AC
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..P$AAVString@System@@(A_0));
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0001343C File Offset: 0x0001283C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.__xc_mp_a), (method*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0001347C File Offset: 0x0001287C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.__xc_ma_a), (method*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x000134B8 File Offset: 0x000128B8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000134E4 File Offset: 0x000128E4
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
		{
			<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		}
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					IntPtr intPtr = (IntPtr)0;
					IntPtr intPtr2 = (IntPtr)ptr;
					IntPtr intPtr3 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, intPtr2, intPtr);
					void* ptr2 = (void*)intPtr3;
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != null)
			{
				num3 = 1;
			}
		}
		finally
		{
			if (num == 0)
			{
				IntPtr intPtr4 = (IntPtr)0;
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, intPtr4);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00013194 File Offset: 0x00012594
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x000131A8 File Offset: 0x000125A8
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000131E0 File Offset: 0x000125E0
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCGJPAX@Z, null);
			}
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00013214 File Offset: 0x00012614
	[SecurityCritical]
	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0);
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00013608 File Offset: 0x00012A08
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (System.Exception ex)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, ex);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0001367C File Offset: 0x00012A7C
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00013694 File Offset: 0x00012A94
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x000136A8 File Offset: 0x00012AA8
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception ex)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, ex);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..P$AAVString@System@@(A_0), ex);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..P$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00013764 File Offset: 0x00012B64
	[SecurityCritical]
	[DebuggerStepThrough]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00013250 File Offset: 0x00012650
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..P$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00013274 File Offset: 0x00012674
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0001329C File Offset: 0x0001269C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000132C4 File Offset: 0x000126C4
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x000138C4 File Offset: 0x00012CC4
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00013910 File Offset: 0x00012D10
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDelDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0001395C File Offset: 0x00012D5C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindVecDtor(method pVecDtor, void* ptr, uint size, int count, method pDtor)
	{
		try
		{
			calli(System.Void(System.Void*,System.UInt32,System.Int32,System.Void (System.Void*)), ptr, size, count, pDtor, pVecDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00013A54 File Offset: 0x00012E54
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void __ehvec_dtor(void* ptr, uint size, uint count, method destructor)
	{
		bool flag = false;
		ptr = (void*)(size * count + (byte*)ptr);
		try
		{
			for (;;)
			{
				int num = (int)count;
				count -= 1U;
				if (num == 0)
				{
					break;
				}
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
			flag = true;
		}
		finally
		{
			if (!flag)
			{
				<Module>.__ArrayUnwind(ptr, size, count, destructor);
			}
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000139C0 File Offset: 0x00012DC0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int ?A0x83b0f00c.ArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		EHExceptionRecord* ptr = *(int*)pExPtrs;
		if (*(int*)ptr != -529697949)
		{
			return 0;
		}
		*<Module>.__current_exception() = ptr;
		int num = *(int*)(pExPtrs + 4 / sizeof(_EXCEPTION_POINTERS));
		*<Module>.__current_exception_context() = num;
		<Module>.terminate();
		return 0;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x000139F4 File Offset: 0x00012DF4
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ArrayUnwind(void* ptr, uint size, uint count, method destructor)
	{
		try
		{
			for (uint num = 0U; num != count; num += 1U)
			{
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
		}
		catch when (endfilter(<Module>.?A0x83b0f00c.ArrayUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00013AB8 File Offset: 0x00012EB8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ehvec_dtor(void* ptr, uint size, int count, method destructor)
	{
		<Module>.__ehvec_dtor(ptr, size, (uint)count, destructor);
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00013AF0 File Offset: 0x00012EF0
	internal unsafe static _Fac_node* std._Fac_node.{ctor}(_Fac_node* A_0, _Fac_node* _Nextarg, _Facet_base* _Facptrarg)
	{
		*A_0 = _Nextarg;
		*(A_0 + 4) = _Facptrarg;
		return A_0;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00013B08 File Offset: 0x00012F08
	internal unsafe static void std._Fac_node.{dtor}(_Fac_node* A_0)
	{
		int num = *(A_0 + 4);
		_Facet_base* ptr = calli(std._Facet_base* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), (IntPtr)num, (IntPtr)(*(*num + 8)));
		if (ptr != null)
		{
			int num2 = *(*(int*)ptr);
			void* ptr2 = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt32), ptr, 1U, (IntPtr)num2);
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00013BB4 File Offset: 0x00012FB4
	internal unsafe static void std._Fac_tidy_reg_t.{dtor}(_Fac_tidy_reg_t* A_0)
	{
		if (<Module>.std.?A0x3e6dbf51._Fac_head != null)
		{
			do
			{
				_Fac_node* ptr = <Module>.std.?A0x3e6dbf51._Fac_head;
				<Module>.std.?A0x3e6dbf51._Fac_head = *(int*)<Module>.std.?A0x3e6dbf51._Fac_head;
				<Module>.std._Fac_node.{dtor}(ptr);
				<Module>.delete((void*)ptr, 8U);
			}
			while (<Module>.std.?A0x3e6dbf51._Fac_head != null);
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00013B34 File Offset: 0x00012F34
	internal unsafe static void* std._Fac_node.__delDtor(_Fac_node* A_0, uint A_0)
	{
		<Module>.std._Fac_node.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8U);
		}
		return A_0;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000114C File Offset: 0x0000054C
	internal static void ?A0x3e6dbf51.??__E?A0x3e6dbf51@_Fac_tidy_reg@std@@YMXXZ()
	{
		<Module>._atexit_m(ldftn(?A0x3e6dbf51.??__F?A0x3e6dbf51@_Fac_tidy_reg@std@@YMXXZ));
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00014898 File Offset: 0x00013C98
	internal unsafe static void ?A0x3e6dbf51.??__F?A0x3e6dbf51@_Fac_tidy_reg@std@@YMXXZ()
	{
		if (<Module>.std.?A0x3e6dbf51._Fac_head != null)
		{
			do
			{
				_Fac_node* ptr = <Module>.std.?A0x3e6dbf51._Fac_head;
				<Module>.std.?A0x3e6dbf51._Fac_head = *(int*)<Module>.std.?A0x3e6dbf51._Fac_head;
				int num = *(int*)(ptr + 4 / sizeof(_Fac_node));
				_Facet_base* ptr2 = calli(std._Facet_base* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr), (IntPtr)num, (IntPtr)(*(*num + 8)));
				if (ptr2 != null)
				{
					int num2 = *(*(int*)ptr2);
					void* ptr3 = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvThiscall)(System.IntPtr,System.UInt32), ptr2, 1U, (IntPtr)num2);
				}
				<Module>.delete((void*)ptr, 8U);
			}
			while (<Module>.std.?A0x3e6dbf51._Fac_head != null);
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00013B54 File Offset: 0x00012F54
	internal unsafe static void std._Facet_Register_m(_Facet_base* _This)
	{
		_Fac_node* ptr = <Module>.@new(8U);
		_Fac_node* ptr2;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = <Module>.std.?A0x3e6dbf51._Fac_head;
				*(int*)(ptr + 4 / sizeof(_Fac_node)) = _This;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			<Module>.delete((void*)ptr, 8U);
			throw;
		}
		<Module>.std.?A0x3e6dbf51._Fac_head = ptr2;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00013C1C File Offset: 0x0001301C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA != null)
		{
			IntPtr intPtr = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA);
			return GCHandle.FromIntPtr(intPtr);
		}
		return null;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00014120 File Offset: 0x00013520
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00013C4C File Offset: 0x0001304C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00013C9C File Offset: 0x0001309C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00013CC0 File Offset: 0x000130C0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA = null;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00013CE8 File Offset: 0x000130E8
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0001413C File Offset: 0x0001353C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00013D04 File Offset: 0x00013104
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA--;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00013D2C File Offset: 0x0001312C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00013D44 File Offset: 0x00013144
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00013D5C File Offset: 0x0001315C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0xac88cc7b.__global_lock()
	{
		bool flag = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			flag = true;
		}
		return flag;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00013D7C File Offset: 0x0001317C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0xac88cc7b.__global_unlock()
	{
		bool flag = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			flag = true;
		}
		return flag;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0001416C File Offset: 0x0001356C
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0xac88cc7b.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00013D9C File Offset: 0x0001319C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0xac88cc7b.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00013DB0 File Offset: 0x000131B0
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, uint* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method system.Void_u0020() = 0;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0xac88cc7b.__global_lock() == 1)
		{
			try
			{
				method* ptr = (method*)<Module>.DecodePointer(*(int*)__ponexitbegin_e);
				method* ptr2 = (method*)<Module>.DecodePointer(*(int*)__ponexitend_e);
				int num = (int)(ptr2 - ptr);
				if (*__pexit_list_size - 1U < (uint)num >> 2)
				{
					try
					{
						uint num2 = *__pexit_list_size * 4U;
						uint num3;
						if (num2 < 2048U)
						{
							num3 = num2;
						}
						else
						{
							num3 = 2048U;
						}
						IntPtr intPtr = new IntPtr((int)(num2 + num3));
						IntPtr intPtr2 = new IntPtr((void*)ptr);
						IntPtr intPtr3 = Marshal.ReAllocHGlobal(intPtr2, intPtr);
						IntPtr intPtr4 = intPtr3;
						ptr2 = (method*)((byte*)intPtr4.ToPointer() + num);
						ptr = (method*)intPtr4.ToPointer();
						uint num4 = *__pexit_list_size;
						uint num5;
						if (512U < num4)
						{
							num5 = 512U;
						}
						else
						{
							num5 = num4;
						}
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr intPtr5 = new IntPtr((int)(*__pexit_list_size * 4U + 8U));
						IntPtr intPtr6 = new IntPtr((void*)ptr);
						IntPtr intPtr7 = Marshal.ReAllocHGlobal(intPtr6, intPtr5);
						IntPtr intPtr8 = intPtr7;
						ptr2 = (intPtr8.ToPointer() - ptr) / (IntPtr)sizeof(method) + ptr2;
						ptr = (method*)intPtr8.ToPointer();
						*__pexit_list_size += 4U;
					}
				}
				*(int*)ptr2 = func;
				ptr2 += 4 / sizeof(method);
				system.Void_u0020() = func;
				*(int*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
				*(int*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0xac88cc7b.__global_unlock();
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00013F34 File Offset: 0x00013334
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0xac88cc7b.__exit_list_size != 0U)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.?A0xac88cc7b.__onexitbegin_m);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xac88cc7b.__onexitend_m);
			if (ptr != -1 && ptr != null && ptr2 != null)
			{
				method* ptr3 = ptr;
				method* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 4 / sizeof(method);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(int*)ptr2 != <Module>.EncodePointer(null))
					{
						IntPtr intPtr = <Module>.DecodePointer(*(int*)ptr2);
						*(int*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), intPtr);
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xac88cc7b.__onexitbegin_m);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xac88cc7b.__onexitend_m);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
				IntPtr intPtr2 = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr2);
			}
			<Module>.?A0xac88cc7b.__dealloc_global_lock();
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00014184 File Offset: 0x00013584
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_m()
	{
		int num = 0;
		if (<Module>.?A0xac88cc7b.__alloc_global_lock() == 1)
		{
			<Module>.?A0xac88cc7b.__onexitbegin_m = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(128).ToPointer());
			<Module>.?A0xac88cc7b.__onexitend_m = <Module>.?A0xac88cc7b.__onexitbegin_m;
			<Module>.?A0xac88cc7b.__exit_list_size = 32U;
			num = 1;
		}
		return num;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000141D0 File Offset: 0x000135D0
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0 : _Function;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00013FE0 File Offset: 0x000133E0
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xac88cc7b.__exit_list_size, &<Module>.?A0xac88cc7b.__onexitend_m, &<Module>.?A0xac88cc7b.__onexitbegin_m);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000141F0 File Offset: 0x000135F0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0xac88cc7b.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(128).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32U;
		}
		return 1;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00014010 File Offset: 0x00013410
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0U)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1 && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 4 / sizeof(method);
						}
						while (ptr2 >= ptr && *(int*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						method system.Void_u0020() = <Module>.DecodePointer(*(int*)ptr2);
						*(int*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr);
				<Module>.?A0xac88cc7b.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0001423C File Offset: 0x0001363C
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0 : _Function;
	}

	// Token: 0x06000215 RID: 533 RVA: 0x000140F0 File Offset: 0x000134F0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x06000216 RID: 534
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x06000217 RID: 535
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x06000218 RID: 536 RVA: 0x000142C4 File Offset: 0x000136C4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				uint num2 = (uint)(*(int*)pfbegin);
				if (num2 != 0U)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num2);
				}
				pfbegin += 4 / sizeof(method);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x000142F8 File Offset: 0x000136F8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				uint num = (uint)(*(int*)pfbegin);
				if (num != 0U)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num);
				}
				pfbegin += 4 / sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00014324 File Offset: 0x00013724
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00014374 File Offset: 0x00013774
	[DebuggerStepThrough]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				uint num = (uint)(*(int*)pfbegin);
				if (num != 0U)
				{
					void* ptr = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 4 / sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00014348 File Offset: 0x00013748
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x0600021D RID: 541 RVA: 0x000145AE File Offset: 0x000139AE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_ios<char,std::char_traits<char>\u0020>.setstate(basic_ios<char,std::char_traits<char>\u0020>*, int, [MarshalAs(UnmanagedType.U1)] bool);

	// Token: 0x0600021E RID: 542 RVA: 0x00014585 File Offset: 0x00013985
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](uint);

	// Token: 0x0600021F RID: 543 RVA: 0x00014580 File Offset: 0x00013980
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x06000220 RID: 544 RVA: 0x0001471C File Offset: 0x00013B1C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int MultiByteToWideChar(uint, uint, sbyte*, int, char*, int);

	// Token: 0x06000221 RID: 545 RVA: 0x00014716 File Offset: 0x00013B16
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int WideCharToMultiByte(uint, uint, char*, int, sbyte*, int, sbyte*, int*);

	// Token: 0x06000222 RID: 546 RVA: 0x000145A8 File Offset: 0x000139A8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std._Xlength_error(sbyte*);

	// Token: 0x06000223 RID: 547 RVA: 0x00014752 File Offset: 0x00013B52
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _invalid_parameter_noinfo_noreturn();

	// Token: 0x06000224 RID: 548 RVA: 0x00014550 File Offset: 0x00013950
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(uint);

	// Token: 0x06000225 RID: 549 RVA: 0x00014454 File Offset: 0x00013854
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x06000226 RID: 550 RVA: 0x000145A2 File Offset: 0x000139A2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __ExceptionPtrCopy(void*, void*);

	// Token: 0x06000227 RID: 551 RVA: 0x0001459C File Offset: 0x0001399C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __ExceptionPtrDestroy(void*);

	// Token: 0x06000228 RID: 552 RVA: 0x0001458E File Offset: 0x0001398E
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*, uint);

	// Token: 0x06000229 RID: 553 RVA: 0x00011CE8 File Offset: 0x000110E8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, uint);

	// Token: 0x0600022A RID: 554 RVA: 0x0001444E File Offset: 0x0001384E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x0600022B RID: 555 RVA: 0x00014448 File Offset: 0x00013848
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x0600022C RID: 556 RVA: 0x00010A50 File Offset: 0x0000FE50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int sprintf(sbyte*, sbyte*, __arglist);

	// Token: 0x0600022D RID: 557 RVA: 0x00014698 File Offset: 0x00013A98
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint std.locale.id..I(locale.id*);

	// Token: 0x0600022E RID: 558 RVA: 0x0001461A File Offset: 0x00013A1A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool std.codecvt_base.always_noconv(codecvt_base*);

	// Token: 0x0600022F RID: 559 RVA: 0x0001460E File Offset: 0x00013A0E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool std.ios_base.good(ios_base*);

	// Token: 0x06000230 RID: 560 RVA: 0x0001464A File Offset: 0x00013A4A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.ios_base.flags(ios_base*);

	// Token: 0x06000231 RID: 561 RVA: 0x000145BA File Offset: 0x000139BA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.ios_base.setf(ios_base*, int, int);

	// Token: 0x06000232 RID: 562 RVA: 0x00014644 File Offset: 0x00013A44
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.ios_base.width(ios_base*);

	// Token: 0x06000233 RID: 563 RVA: 0x00014662 File Offset: 0x00013A62
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.ios_base.width(ios_base*, long);

	// Token: 0x06000234 RID: 564 RVA: 0x00014686 File Offset: 0x00013A86
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_iostream<char,std::char_traits<char>\u0020>.{dtor}(basic_iostream<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000235 RID: 565 RVA: 0x000146C8 File Offset: 0x00013AC8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.write(basic_ostream<char,std::char_traits<char>\u0020>*, sbyte*, long);

	// Token: 0x06000236 RID: 566 RVA: 0x000146BC File Offset: 0x00013ABC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.<<(basic_ostream<char,std::char_traits<char>\u0020>*, uint);

	// Token: 0x06000237 RID: 567 RVA: 0x000146CE File Offset: 0x00013ACE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.<<(basic_ostream<char,std::char_traits<char>\u0020>*, ushort);

	// Token: 0x06000238 RID: 568 RVA: 0x000146D4 File Offset: 0x00013AD4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.<<(basic_ostream<char,std::char_traits<char>\u0020>*, method);

	// Token: 0x06000239 RID: 569 RVA: 0x000146C2 File Offset: 0x00013AC2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.<<(basic_ostream<char,std::char_traits<char>\u0020>*, method);

	// Token: 0x0600023A RID: 570 RVA: 0x00014668 File Offset: 0x00013A68
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_ostream<char,std::char_traits<char>\u0020>.{dtor}(basic_ostream<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600023B RID: 571 RVA: 0x00014674 File Offset: 0x00013A74
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_ios<char,std::char_traits<char>\u0020>.{dtor}(basic_ios<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600023C RID: 572 RVA: 0x000143D0 File Offset: 0x000137D0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.basic_streambuf<char,std::char_traits<char>\u0020>.xsputn(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, long);

	// Token: 0x0600023D RID: 573 RVA: 0x000143CA File Offset: 0x000137CA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.basic_streambuf<char,std::char_traits<char>\u0020>.xsgetn(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, long);

	// Token: 0x0600023E RID: 574 RVA: 0x000145DE File Offset: 0x000139DE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.{dtor}(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600023F RID: 575 RVA: 0x0001468C File Offset: 0x00013A8C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_iostream<char,std::char_traits<char>\u0020>* std.basic_iostream<char,std::char_traits<char>\u0020>.{ctor}(basic_iostream<char,std::char_traits<char>\u0020>*, basic_streambuf<char,std::char_traits<char>\u0020>*, int);

	// Token: 0x06000240 RID: 576 RVA: 0x0001467A File Offset: 0x00013A7A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.{ctor}(basic_ostream<char,std::char_traits<char>\u0020>*, basic_streambuf<char,std::char_traits<char>\u0020>*, [MarshalAs(UnmanagedType.U1)] bool, int);

	// Token: 0x06000241 RID: 577 RVA: 0x0001466E File Offset: 0x00013A6E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ios<char,std::char_traits<char>\u0020>* std.basic_ios<char,std::char_traits<char>\u0020>.{ctor}(basic_ios<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000242 RID: 578 RVA: 0x00014650 File Offset: 0x00013A50
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte std.basic_ios<char,std::char_traits<char>\u0020>.fill(basic_ios<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000243 RID: 579 RVA: 0x000145EA File Offset: 0x000139EA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_streambuf<char,std::char_traits<char>\u0020>* std.basic_ios<char,std::char_traits<char>\u0020>.rdbuf(basic_ios<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000244 RID: 580 RVA: 0x000146B6 File Offset: 0x00013AB6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_ios<char,std::char_traits<char>\u0020>.clear(basic_ios<char,std::char_traits<char>\u0020>*, int, [MarshalAs(UnmanagedType.U1)] bool);

	// Token: 0x06000245 RID: 581 RVA: 0x00014704 File Offset: 0x00013B04
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.basic_streambuf<char,std::char_traits<char>\u0020>._Pnavail(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000246 RID: 582 RVA: 0x000146DA File Offset: 0x00013ADA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>._Pninc(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000247 RID: 583 RVA: 0x00014710 File Offset: 0x00013B10
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, sbyte*, sbyte*);

	// Token: 0x06000248 RID: 584 RVA: 0x0001470A File Offset: 0x00013B0A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.pbump(basic_streambuf<char,std::char_traits<char>\u0020>*, int);

	// Token: 0x06000249 RID: 585 RVA: 0x000146F8 File Offset: 0x00013AF8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.basic_streambuf<char,std::char_traits<char>\u0020>._Gnavail(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600024A RID: 586 RVA: 0x000146EC File Offset: 0x00013AEC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>._Gninc(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600024B RID: 587 RVA: 0x000146E6 File Offset: 0x00013AE6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>._Gndec(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600024C RID: 588 RVA: 0x00014632 File Offset: 0x00013A32
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.epptr(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600024D RID: 589 RVA: 0x000145C6 File Offset: 0x000139C6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.setg(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, sbyte*, sbyte*);

	// Token: 0x0600024E RID: 590 RVA: 0x000146FE File Offset: 0x00013AFE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.gbump(basic_streambuf<char,std::char_traits<char>\u0020>*, int);

	// Token: 0x0600024F RID: 591 RVA: 0x0001463E File Offset: 0x00013A3E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.egptr(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000250 RID: 592 RVA: 0x00014626 File Offset: 0x00013A26
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.pptr(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000251 RID: 593 RVA: 0x00014638 File Offset: 0x00013A38
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.gptr(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000252 RID: 594 RVA: 0x000145C0 File Offset: 0x000139C0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.eback(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000253 RID: 595 RVA: 0x0001465C File Offset: 0x00013A5C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long std.basic_streambuf<char,std::char_traits<char>\u0020>.sputn(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, long);

	// Token: 0x06000254 RID: 596 RVA: 0x000146E0 File Offset: 0x00013AE0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.codecvt<char,char,_Mbstatet>.out(codecvt<char,char,_Mbstatet>*, _Mbstatet*, sbyte*, sbyte*, sbyte**, sbyte*, sbyte*, sbyte**);

	// Token: 0x06000255 RID: 597 RVA: 0x000146F2 File Offset: 0x00013AF2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.codecvt<char,char,_Mbstatet>.in(codecvt<char,char,_Mbstatet>*, _Mbstatet*, sbyte*, sbyte*, sbyte**, sbyte*, sbyte*, sbyte**);

	// Token: 0x06000256 RID: 598 RVA: 0x000145FC File Offset: 0x000139FC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.flush(basic_ostream<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000257 RID: 599 RVA: 0x00014608 File Offset: 0x00013A08
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_ostream<char,std::char_traits<char>\u0020>._Osfx(basic_ostream<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000258 RID: 600 RVA: 0x00014614 File Offset: 0x00013A14
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ios<char,std::char_traits<char>\u0020>.tie(basic_ios<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000259 RID: 601 RVA: 0x000145D2 File Offset: 0x000139D2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte**, sbyte**, int*, sbyte**, sbyte**, int*);

	// Token: 0x0600025A RID: 602 RVA: 0x000145CC File Offset: 0x000139CC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>._Init(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600025B RID: 603 RVA: 0x00014680 File Offset: 0x00013A80
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_streambuf<char,std::char_traits<char>\u0020>.setp(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte*, sbyte*);

	// Token: 0x0600025C RID: 604 RVA: 0x000146B0 File Offset: 0x00013AB0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern locale* std.basic_streambuf<char,std::char_traits<char>\u0020>.getloc(basic_streambuf<char,std::char_traits<char>\u0020>*, locale*);

	// Token: 0x0600025D RID: 605 RVA: 0x000145D8 File Offset: 0x000139D8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_streambuf<char,std::char_traits<char>\u0020>* std.basic_streambuf<char,std::char_traits<char>\u0020>.{ctor}(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x0600025E RID: 606 RVA: 0x00014620 File Offset: 0x00013A20
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.codecvt<char,char,_Mbstatet>.unshift(codecvt<char,char,_Mbstatet>*, _Mbstatet*, sbyte*, sbyte*, sbyte**);

	// Token: 0x0600025F RID: 607 RVA: 0x000145F0 File Offset: 0x000139F0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte std.basic_ios<char,std::char_traits<char>\u0020>.widen(basic_ios<char,std::char_traits<char>\u0020>*, sbyte);

	// Token: 0x06000260 RID: 608 RVA: 0x0001462C File Offset: 0x00013A2C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern sbyte* std.basic_streambuf<char,std::char_traits<char>\u0020>.pbase(basic_streambuf<char,std::char_traits<char>\u0020>*);

	// Token: 0x06000261 RID: 609 RVA: 0x00014656 File Offset: 0x00013A56
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int std.basic_streambuf<char,std::char_traits<char>\u0020>.sputc(basic_streambuf<char,std::char_traits<char>\u0020>*, sbyte);

	// Token: 0x06000262 RID: 610 RVA: 0x000145F6 File Offset: 0x000139F6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_ostream<char,std::char_traits<char>\u0020>* std.basic_ostream<char,std::char_traits<char>\u0020>.put(basic_ostream<char,std::char_traits<char>\u0020>*, sbyte);

	// Token: 0x06000263 RID: 611 RVA: 0x0001469E File Offset: 0x00013A9E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint std.codecvt<char,char,_Mbstatet>._Getcat(locale.facet**, locale*);

	// Token: 0x06000264 RID: 612 RVA: 0x0001475E File Offset: 0x00013B5E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int _get_stream_buffer_pointers(_iobuf*, sbyte***, sbyte***, int**);

	// Token: 0x06000265 RID: 613 RVA: 0x00014776 File Offset: 0x00013B76
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int _fseeki64(_iobuf*, long, int);

	// Token: 0x06000266 RID: 614 RVA: 0x0001477C File Offset: 0x00013B7C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint fread(void*, uint, uint, _iobuf*);

	// Token: 0x06000267 RID: 615 RVA: 0x00014788 File Offset: 0x00013B88
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long _ftelli64(_iobuf*);

	// Token: 0x06000268 RID: 616 RVA: 0x000147B2 File Offset: 0x00013BB2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fsetpos(_iobuf*, long*);

	// Token: 0x06000269 RID: 617 RVA: 0x000147A0 File Offset: 0x00013BA0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int ungetc(int, _iobuf*);

	// Token: 0x0600026A RID: 618 RVA: 0x0001472E File Offset: 0x00013B2E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern int __CxxQueryExceptionSize();

	// Token: 0x0600026B RID: 619 RVA: 0x00014758 File Offset: 0x00013B58
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern long _time64(long*);

	// Token: 0x0600026C RID: 620 RVA: 0x000147B8 File Offset: 0x00013BB8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int setvbuf(_iobuf*, sbyte*, int, uint);

	// Token: 0x0600026D RID: 621 RVA: 0x000147AC File Offset: 0x00013BAC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fgetpos(_iobuf*, long*);

	// Token: 0x0600026E RID: 622 RVA: 0x00014740 File Offset: 0x00013B40
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxDetectRethrow(void*);

	// Token: 0x0600026F RID: 623 RVA: 0x00014746 File Offset: 0x00013B46
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __CxxUnregisterExceptionObject(void*, int);

	// Token: 0x06000270 RID: 624 RVA: 0x00014734 File Offset: 0x00013B34
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxExceptionFilter(void*, void*, int, void*);

	// Token: 0x06000271 RID: 625 RVA: 0x00014764 File Offset: 0x00013B64
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint fwrite(void*, uint, uint, _iobuf*);

	// Token: 0x06000272 RID: 626 RVA: 0x0001478E File Offset: 0x00013B8E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _lock_file(_iobuf*);

	// Token: 0x06000273 RID: 627 RVA: 0x00014794 File Offset: 0x00013B94
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _unlock_file(_iobuf*);

	// Token: 0x06000274 RID: 628 RVA: 0x000147A6 File Offset: 0x00013BA6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fgetc(_iobuf*);

	// Token: 0x06000275 RID: 629 RVA: 0x00014770 File Offset: 0x00013B70
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern int tolower(int);

	// Token: 0x06000276 RID: 630 RVA: 0x00011690 File Offset: 0x00010A90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void sha256_init(NC_SHA256_CTX*);

	// Token: 0x06000277 RID: 631 RVA: 0x00011570 File Offset: 0x00010970
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void sha256_final(NC_SHA256_CTX*, uint*);

	// Token: 0x06000278 RID: 632 RVA: 0x000146AA File Offset: 0x00013AAA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern _iobuf* std._Fiopen(sbyte*, int, int);

	// Token: 0x06000279 RID: 633 RVA: 0x000145E4 File Offset: 0x000139E4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std._Xout_of_range(sbyte*);

	// Token: 0x0600027A RID: 634 RVA: 0x00014602 File Offset: 0x00013A02
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static extern bool std.uncaught_exception();

	// Token: 0x0600027B RID: 635 RVA: 0x000145B4 File Offset: 0x000139B4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern locale._Locimp* std.locale._Getgloballocale();

	// Token: 0x0600027C RID: 636 RVA: 0x00014692 File Offset: 0x00013A92
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern _Lockit* std._Lockit.{ctor}(_Lockit*, int);

	// Token: 0x0600027D RID: 637 RVA: 0x000146A4 File Offset: 0x00013AA4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.ThisCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std._Lockit.{dtor}(_Lockit*);

	// Token: 0x0600027E RID: 638 RVA: 0x0001476A File Offset: 0x00013B6A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fclose(_iobuf*);

	// Token: 0x0600027F RID: 639 RVA: 0x000147BE File Offset: 0x00013BBE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fflush(_iobuf*);

	// Token: 0x06000280 RID: 640 RVA: 0x00014782 File Offset: 0x00013B82
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern _iobuf* fopen(sbyte*, sbyte*);

	// Token: 0x06000281 RID: 641 RVA: 0x0001473A File Offset: 0x00013B3A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxRegisterExceptionObject(void*, void*);

	// Token: 0x06000282 RID: 642 RVA: 0x000116F0 File Offset: 0x00010AF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void sha256_update(NC_SHA256_CTX*, sbyte*, uint);

	// Token: 0x06000283 RID: 643 RVA: 0x00014728 File Offset: 0x00013B28
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* memmove(void*, void*, uint);

	// Token: 0x06000284 RID: 644 RVA: 0x0001479A File Offset: 0x00013B9A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int fputc(int, _iobuf*);

	// Token: 0x06000285 RID: 645 RVA: 0x00013030 File Offset: 0x00012430
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000286 RID: 646 RVA: 0x000144B4 File Offset: 0x000138B4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x06000287 RID: 647 RVA: 0x00014722 File Offset: 0x00013B22
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x06000288 RID: 648 RVA: 0x000147C4 File Offset: 0x00013BC4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x06000289 RID: 649 RVA: 0x00012058 File Offset: 0x00011458
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x0600028A RID: 650 RVA: 0x0001474C File Offset: 0x00013B4C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x0600028B RID: 651 RVA: 0x00014466 File Offset: 0x00013866
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception_context();

	// Token: 0x0600028C RID: 652 RVA: 0x000144BA File Offset: 0x000138BA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void terminate();

	// Token: 0x0600028D RID: 653 RVA: 0x00014460 File Offset: 0x00013860
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception();

	// Token: 0x04000001 RID: 1 RVA: 0x00015344 File Offset: 0x00014144
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x0001531C File Offset: 0x0001411C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;

	// Token: 0x04000003 RID: 3 RVA: 0x00015334 File Offset: 0x00014134
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@$$CBD ??_C@_0BA@JFNIOLAK@string?5too?5long@;

	// Token: 0x04000004 RID: 4 RVA: 0x00015358 File Offset: 0x00014158
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@FJHHFFAF@bad_weak_ptr@;

	// Token: 0x04000005 RID: 5 RVA: 0x0003CA90 File Offset: 0x0003B890
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_20 ??_R2failure@ios_base@std@@8;

	// Token: 0x04000006 RID: 6 RVA: 0x0003CA3C File Offset: 0x0003B83C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2system_error@std@@8;

	// Token: 0x04000007 RID: 7 RVA: 0x0003C9EC File Offset: 0x0003B7EC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_12 ??_R2_System_error@std@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x0003C99C File Offset: 0x0003B79C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_12 ??_R2range_error@std@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x0003C950 File Offset: 0x0003B750
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2runtime_error@std@@8;

	// Token: 0x0400000A RID: 10 RVA: 0x0003C904 File Offset: 0x0003B704
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2bad_weak_ptr@std@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x0003C8B8 File Offset: 0x0003B6B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2bad_cast@std@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x0003C868 File Offset: 0x0003B668
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_12 ??_R2bad_array_new_length@std@@8;

	// Token: 0x0400000D RID: 13 RVA: 0x0003C81C File Offset: 0x0003B61C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2bad_alloc@std@@8;

	// Token: 0x0400000E RID: 14 RVA: 0x0003C7D4 File Offset: 0x0003B5D4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_4 ??_R2exception@std@@8;

	// Token: 0x0400000F RID: 15 RVA: 0x0003CA74 File Offset: 0x0003B874
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@failure@ios_base@std@@8;

	// Token: 0x04000010 RID: 16 RVA: 0x0003CA20 File Offset: 0x0003B820
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@system_error@std@@8;

	// Token: 0x04000011 RID: 17 RVA: 0x0003C9D0 File Offset: 0x0003B7D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@_System_error@std@@8;

	// Token: 0x04000012 RID: 18 RVA: 0x0003C980 File Offset: 0x0003B780
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@range_error@std@@8;

	// Token: 0x04000013 RID: 19 RVA: 0x0003C934 File Offset: 0x0003B734
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@runtime_error@std@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x0003C8E8 File Offset: 0x0003B6E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_weak_ptr@std@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x0003C89C File Offset: 0x0003B69C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_cast@std@@8;

	// Token: 0x04000016 RID: 22 RVA: 0x0003C84C File Offset: 0x0003B64C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x04000017 RID: 23 RVA: 0x0003C800 File Offset: 0x0003B600
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x04000018 RID: 24 RVA: 0x0003C7B8 File Offset: 0x0003B5B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x04000019 RID: 25 RVA: 0x0003CAA8 File Offset: 0x0003B8A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3failure@ios_base@std@@8;

	// Token: 0x0400001A RID: 26 RVA: 0x0003CA50 File Offset: 0x0003B850
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3system_error@std@@8;

	// Token: 0x0400001B RID: 27 RVA: 0x0003C9FC File Offset: 0x0003B7FC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3_System_error@std@@8;

	// Token: 0x0400001C RID: 28 RVA: 0x0003C9AC File Offset: 0x0003B7AC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3range_error@std@@8;

	// Token: 0x0400001D RID: 29 RVA: 0x0003C95C File Offset: 0x0003B75C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3runtime_error@std@@8;

	// Token: 0x0400001E RID: 30 RVA: 0x0003C910 File Offset: 0x0003B710
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_weak_ptr@std@@8;

	// Token: 0x0400001F RID: 31 RVA: 0x0003C8C4 File Offset: 0x0003B6C4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_cast@std@@8;

	// Token: 0x04000020 RID: 32 RVA: 0x0003C878 File Offset: 0x0003B678
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x04000021 RID: 33 RVA: 0x0003C828 File Offset: 0x0003B628
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x04000022 RID: 34 RVA: 0x0003C7DC File Offset: 0x0003B5DC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x04000023 RID: 35 RVA: 0x0003CAB8 File Offset: 0x0003B8B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4failure@ios_base@std@@6B@;

	// Token: 0x04000024 RID: 36 RVA: 0x0003CA60 File Offset: 0x0003B860
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4system_error@std@@6B@;

	// Token: 0x04000025 RID: 37 RVA: 0x0003CA0C File Offset: 0x0003B80C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4_System_error@std@@6B@;

	// Token: 0x04000026 RID: 38 RVA: 0x0003C9BC File Offset: 0x0003B7BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4range_error@std@@6B@;

	// Token: 0x04000027 RID: 39 RVA: 0x0003C96C File Offset: 0x0003B76C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4runtime_error@std@@6B@;

	// Token: 0x04000028 RID: 40 RVA: 0x0003C920 File Offset: 0x0003B720
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4bad_weak_ptr@std@@6B@;

	// Token: 0x04000029 RID: 41 RVA: 0x0003C8D4 File Offset: 0x0003B6D4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4bad_cast@std@@6B@;

	// Token: 0x0400002A RID: 42 RVA: 0x0003C888 File Offset: 0x0003B688
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x0400002B RID: 43 RVA: 0x0003C838 File Offset: 0x0003B638
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4bad_alloc@std@@6B@;

	// Token: 0x0400002C RID: 44 RVA: 0x0003C7EC File Offset: 0x0003B5EC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4exception@std@@6B@;

	// Token: 0x0400002D RID: 45 RVA: 0x00040AE0 File Offset: 0x0003EAE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_27 ??_R0?AVfailure@ios_base@std@@@8;

	// Token: 0x0400002E RID: 46 RVA: 0x000400A4 File Offset: 0x0003E0A4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7failure@ios_base@std@@6B@;

	// Token: 0x0400002F RID: 47 RVA: 0x00040AA0 File Offset: 0x0003EAA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AV_System_error@std@@@8;

	// Token: 0x04000030 RID: 48 RVA: 0x00040AC0 File Offset: 0x0003EAC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVsystem_error@std@@@8;

	// Token: 0x04000031 RID: 49 RVA: 0x00040090 File Offset: 0x0003E090
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7system_error@std@@6B@;

	// Token: 0x04000032 RID: 50 RVA: 0x00040080 File Offset: 0x0003E080
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7_System_error@std@@6B@;

	// Token: 0x04000033 RID: 51 RVA: 0x00040A60 File Offset: 0x0003EA60
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AVruntime_error@std@@@8;

	// Token: 0x04000034 RID: 52 RVA: 0x00040A80 File Offset: 0x0003EA80
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_22 ??_R0?AVrange_error@std@@@8;

	// Token: 0x04000035 RID: 53 RVA: 0x00040070 File Offset: 0x0003E070
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7range_error@std@@6B@;

	// Token: 0x04000036 RID: 54 RVA: 0x00040060 File Offset: 0x0003E060
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7runtime_error@std@@6B@;

	// Token: 0x04000037 RID: 55 RVA: 0x00040A40 File Offset: 0x0003EA40
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVbad_weak_ptr@std@@@8;

	// Token: 0x04000038 RID: 56 RVA: 0x00040050 File Offset: 0x0003E050
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_weak_ptr@std@@6B@;

	// Token: 0x04000039 RID: 57 RVA: 0x00040A24 File Offset: 0x0003EA24
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_19 ??_R0?AVbad_cast@std@@@8;

	// Token: 0x0400003A RID: 58 RVA: 0x00040040 File Offset: 0x0003E040
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_cast@std@@6B@;

	// Token: 0x0400003B RID: 59 RVA: 0x00015318 File Offset: 0x00014118
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Fake_allocator std.?A0x3b496f95._Fake_alloc;

	// Token: 0x0400003C RID: 60 RVA: 0x0003D3F4 File Offset: 0x0003C1F4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_12 _CTA3?AVbad_array_new_length@std@@;

	// Token: 0x0400003D RID: 61 RVA: 0x0003D3D8 File Offset: 0x0003C1D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVexception@std@@@8??0exception@std@@$$FQAE@ABV01@@Z12;

	// Token: 0x0400003E RID: 62 RVA: 0x0003D3BC File Offset: 0x0003C1BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_alloc@std@@@8??0bad_alloc@std@@$$FQAE@ABV01@@Z12;

	// Token: 0x0400003F RID: 63 RVA: 0x0003D3A0 File Offset: 0x0003C1A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_array_new_length@std@@@8??0bad_array_new_length@std@@$$FQAE@ABV01@@Z12;

	// Token: 0x04000040 RID: 64 RVA: 0x000409C4 File Offset: 0x0003E9C4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x04000041 RID: 65 RVA: 0x000409E0 File Offset: 0x0003E9E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x04000042 RID: 66 RVA: 0x000409FC File Offset: 0x0003E9FC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x04000043 RID: 67 RVA: 0x0003D404 File Offset: 0x0003C204
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI3?AVbad_array_new_length@std@@;

	// Token: 0x04000044 RID: 68 RVA: 0x00040020 File Offset: 0x0003E020
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x04000045 RID: 69 RVA: 0x00040014 File Offset: 0x0003E014
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x04000046 RID: 70 RVA: 0x00040004 File Offset: 0x0003E004
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x04000047 RID: 71 RVA: 0x0001554D File Offset: 0x0001434D
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00$$CBD ??_C@_00CNPNBAHC@@;

	// Token: 0x04000048 RID: 72 RVA: 0x00015378 File Offset: 0x00014178
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08$$CBD ??_C@_08EPJLHIJG@bad?5cast@;

	// Token: 0x04000049 RID: 73 RVA: 0x00015368 File Offset: 0x00014168
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@EFJOMLHH@ImageFlash?5?5@;

	// Token: 0x0400004A RID: 74 RVA: 0x000158B0 File Offset: 0x000146B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@MKPNLBEM@readFfu?$CI?$CJ?5Start?4@;

	// Token: 0x0400004B RID: 75 RVA: 0x000154FC File Offset: 0x000142FC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02$$CBD ??_C@_02JDPG@rb@;

	// Token: 0x0400004C RID: 76 RVA: 0x00015730 File Offset: 0x00014530
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CD@$$CBD ??_C@_0CD@GAPHJKMF@Could?5not?5open?5FFU?5file?0?5filena@;

	// Token: 0x0400004D RID: 77 RVA: 0x000158C4 File Offset: 0x000146C4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DA@$$CBD ??_C@_0DA@OLEAJDFH@Corrupted?5FFU?0?5incorrect?5header@;

	// Token: 0x0400004E RID: 78 RVA: 0x000158F4 File Offset: 0x000146F4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DG@$$CBD ??_C@_0DG@LJEFNDMN@Corrupted?5FFU?0?5could?5not?5read?5i@;

	// Token: 0x0400004F RID: 79 RVA: 0x0001592C File Offset: 0x0001472C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DI@$$CBD ??_C@_0DI@OMEKDBC@Corrupted?5FFU?0?5size?5of?5image?5he@;

	// Token: 0x04000050 RID: 80 RVA: 0x00015964 File Offset: 0x00014764
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CP@$$CBD ??_C@_0CP@NOMNLDJO@Corrupted?5FFU?0?5cannot?5skip?5mani@;

	// Token: 0x04000051 RID: 81 RVA: 0x00015994 File Offset: 0x00014794
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CP@$$CBD ??_C@_0CP@JCLBHCCP@Corrupted?5FFU?0?5cannot?5skip?5padd@;

	// Token: 0x04000052 RID: 82 RVA: 0x000159C8 File Offset: 0x000147C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DF@$$CBD ??_C@_0DF@NIFEHAIP@fread?$CI?$CGstoreHeader?0?5sizeof?$CIStor@;

	// Token: 0x04000053 RID: 83 RVA: 0x000159C4 File Offset: 0x000147C4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02$$CBD ??_C@_02KEGNLNML@?0?5@;

	// Token: 0x04000054 RID: 84 RVA: 0x00015AA0 File Offset: 0x000148A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@LPLCCEEL@?0?5filename?3?5@;

	// Token: 0x04000055 RID: 85 RVA: 0x00015A9C File Offset: 0x0001489C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01$$CBD ??_C@_01LFCBOECM@?4@;

	// Token: 0x04000056 RID: 86 RVA: 0x00015A5C File Offset: 0x0001485C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DN@$$CBD ??_C@_0DN@EDLJMDMN@The?5FFU?5has?5wrong?5version?0?5must@;

	// Token: 0x04000057 RID: 87 RVA: 0x00015A00 File Offset: 0x00014800
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0EL@$$CBD ??_C@_0EL@FHOOFPLA@The?5FFU?5is?5not?5a?5full?5flash?5FFU@;

	// Token: 0x04000058 RID: 88 RVA: 0x00015A4C File Offset: 0x0001484C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0P@$$CBD ??_C@_0P@FGKADOIP@readFfu?$CI?$CJ?5End?4@;

	// Token: 0x04000059 RID: 89 RVA: 0x00015B08 File Offset: 0x00014908
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CJ@$$CBD ??_C@_0CJ@EDDAJGLL@Too?5small?5maximum?5block?5size?0?5f@;

	// Token: 0x0400005A RID: 90 RVA: 0x00015B38 File Offset: 0x00014938
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0IB@$$CBD ??_C@_0IB@KAMICEBB@0?5?$CB?$DN?5readDescriptors?$CIfp?0?5payloa@;

	// Token: 0x0400005B RID: 91 RVA: 0x00015C2C File Offset: 0x00014A2C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CN@$$CBD ??_C@_0CN@KKAJNADJ@fread?$CI?$CGblock?0?5sizeof?$CIDataBlock?$CJ@;

	// Token: 0x0400005C RID: 92 RVA: 0x00015BF0 File Offset: 0x000149F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DJ@$$CBD ??_C@_0DJ@NAHGMBCO@block?4blockLocationCount?5?$DO?50?5?$CG?$CG@;

	// Token: 0x0400005D RID: 93 RVA: 0x00015BBC File Offset: 0x000149BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DE@$$CBD ??_C@_0DE@OEBOFPPN@1?5?$DN?$DN?5fread?$CI?$CGlocation?0?5sizeof?$CIBl@;

	// Token: 0x0400005E RID: 94 RVA: 0x00015DB0 File Offset: 0x00014BB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DE@$$CBD ??_C@_0DE@KMDNEMNG@Seek?5to?5the?5GPT?5header?5?$CL?5MBR?5si@;

	// Token: 0x0400005F RID: 95 RVA: 0x00015D8C File Offset: 0x00014B8C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CE@$$CBD ??_C@_0CE@CGHHDHDD@Read?5table?5header?5failed?0?5filen@;

	// Token: 0x04000060 RID: 96 RVA: 0x000156F4 File Offset: 0x000144F4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08$$CBD ??_C@_08BOGKMBPC@EFI?5PART@;

	// Token: 0x04000061 RID: 97 RVA: 0x00015D58 File Offset: 0x00014B58
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DB@$$CBD ??_C@_0DB@NLCGKNEK@Wrong?5number?5of?5GPT?5partition?5e@;

	// Token: 0x04000062 RID: 98 RVA: 0x00015D28 File Offset: 0x00014B28
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CN@$$CBD ??_C@_0CN@BBLDFMCJ@Wrong?5size?5of?5GPT?5partition?5ent@;

	// Token: 0x04000063 RID: 99 RVA: 0x00015CF8 File Offset: 0x00014AF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CN@$$CBD ??_C@_0CN@EGEGKNHO@Cannot?5read?5GPT?5partition?5entri@;

	// Token: 0x04000064 RID: 100 RVA: 0x00015CA4 File Offset: 0x00014AA4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CA@$$CBD ??_C@_0CA@MGBAEBFD@?5Calculated?5CRC32?5of?5header?3?50x@;

	// Token: 0x04000065 RID: 101 RVA: 0x00015CC4 File Offset: 0x00014AC4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DE@$$CBD ??_C@_0DE@NAEAEFBF@CRC32?5mismatch?5of?5GPT?5header?$CB?5C@;

	// Token: 0x04000066 RID: 102 RVA: 0x00015C60 File Offset: 0x00014A60
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0ED@$$CBD ??_C@_0ED@DFFBNCAK@CRC32?5mismatch?5of?5GPT?5partition@;

	// Token: 0x04000067 RID: 103 RVA: 0x00015DE4 File Offset: 0x00014BE4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04$$CBD ??_C@_04MIKOFOFJ@SBL1@;

	// Token: 0x04000068 RID: 104 RVA: 0x00015DEC File Offset: 0x00014BEC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04$$CBD ??_C@_04DAEIOFGI@UEFI@;

	// Token: 0x04000069 RID: 105 RVA: 0x000154E0 File Offset: 0x000142E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BM@$$CBD ??_C@_0BM@GBJGKMED@?5image?5parsed?5successfully?4@;

	// Token: 0x0400006A RID: 106 RVA: 0x00015500 File Offset: 0x00014300
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CA@$$CBD ??_C@_0CA@JFGCLPEA@Could?5not?5open?5file?0?5filename?3?5@;

	// Token: 0x0400006B RID: 107 RVA: 0x00015520 File Offset: 0x00014320
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CN@$$CBD ??_C@_0CN@CDANHBKI@The?5file?5is?5not?5valid?5boot?5imag@;

	// Token: 0x0400006C RID: 108 RVA: 0x00015AE4 File Offset: 0x000148E4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@DFMAPPBF@readGpt?$CI?$CJ?5Start?4@;

	// Token: 0x0400006D RID: 109 RVA: 0x00015418 File Offset: 0x00014218
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08$$CBD ??_C@_08KJPBNJGC@Overflow@;

	// Token: 0x0400006E RID: 110 RVA: 0x00015AF8 File Offset: 0x000148F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0P@$$CBD ??_C@_0P@KKOFAEKD@readGpt?$CI?$CJ?5End?4@;

	// Token: 0x0400006F RID: 111 RVA: 0x00015AB0 File Offset: 0x000148B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BJ@$$CBD ??_C@_0BJ@EAKABFJM@readDescriptors?$CI?$CJ?5Start?4@;

	// Token: 0x04000070 RID: 112 RVA: 0x00015ACC File Offset: 0x000148CC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BH@$$CBD ??_C@_0BH@ODFOOBCN@readDescriptors?$CI?$CJ?5End?4@;

	// Token: 0x04000071 RID: 113 RVA: 0x00015400 File Offset: 0x00014200
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@EFIOPFCC@readRkh?$CI?$CJ?5Start?4@;

	// Token: 0x04000072 RID: 114 RVA: 0x00015414 File Offset: 0x00014214
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY03$$CBD ??_C@_03GKBDPGIH@GPT@;

	// Token: 0x04000073 RID: 115 RVA: 0x00015424 File Offset: 0x00014224
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0P@$$CBD ??_C@_0P@OAPOKHFA@readRkh?$CI?$CJ?5End?4@;

	// Token: 0x04000074 RID: 116 RVA: 0x00015550 File Offset: 0x00014350
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CJ@$$CBD ??_C@_0CJ@KHIPEPBA@readSecurityHdrAndCheckValidity@;

	// Token: 0x04000075 RID: 117 RVA: 0x0001557C File Offset: 0x0001437C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CO@$$CBD ??_C@_0CO@NPKMMIFF@Corrupted?5FFU?0?5could?5not?5read?5s@;

	// Token: 0x04000076 RID: 118 RVA: 0x000155AC File Offset: 0x000143AC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBD ??_C@_0N@LJGGPJIJ@SignedImage?5@;

	// Token: 0x04000077 RID: 119 RVA: 0x00015650 File Offset: 0x00014450
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CP@$$CBD ??_C@_0CP@MDFGNDNN@Corrupted?5FFU?0?5image?5header?5sig@;

	// Token: 0x04000078 RID: 120 RVA: 0x000155D4 File Offset: 0x000143D4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@LJPEICOC@?4?5Expected?5size?3?5@;

	// Token: 0x04000079 RID: 121 RVA: 0x000155BC File Offset: 0x000143BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BG@$$CBD ??_C@_0BG@MMHJALCK@Invalid?5struct?5size?3?5@;

	// Token: 0x0400007A RID: 122 RVA: 0x000155EC File Offset: 0x000143EC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@NPNBLBND@Unsupported?5algorithm?3?5@;

	// Token: 0x0400007B RID: 123 RVA: 0x00015604 File Offset: 0x00014404
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@CHEBECHJ@Invalid?5chunk?5size?3?5@;

	// Token: 0x0400007C RID: 124 RVA: 0x0001561C File Offset: 0x0001441C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BH@$$CBD ??_C@_0BH@EMBEMOOJ@Invalid?5catalog?5size?3?5@;

	// Token: 0x0400007D RID: 125 RVA: 0x00015634 File Offset: 0x00014434
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BK@$$CBD ??_C@_0BK@HOBHKCKM@Invalid?5hash?5table?5size?3?5@;

	// Token: 0x0400007E RID: 126 RVA: 0x00015680 File Offset: 0x00014480
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CH@$$CBD ??_C@_0CH@EGAMGHCG@readSecurityHdrAndCheckValidity@;

	// Token: 0x0400007F RID: 127 RVA: 0x000156A8 File Offset: 0x000144A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@FFBKAKPK@DumpPartitions?$CI?$CJ?5Start?4@;

	// Token: 0x04000080 RID: 128 RVA: 0x000156C0 File Offset: 0x000144C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04$$CBD ??_C@_04GKHLBAIJ@?4bin@;

	// Token: 0x04000081 RID: 129 RVA: 0x000156C8 File Offset: 0x000144C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BG@$$CBD ??_C@_0BG@GBPPGJLC@DumpPartitions?$CI?$CJ?5End?4@;

	// Token: 0x04000082 RID: 130 RVA: 0x000156E0 File Offset: 0x000144E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@HHMKMHJH@DumpGpt?$CI?$CJ?5Start?4@;

	// Token: 0x04000083 RID: 131 RVA: 0x00015700 File Offset: 0x00014500
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06$$CBD ??_C@_06DPNFGDJN@?$CFd?4bin@;

	// Token: 0x04000084 RID: 132 RVA: 0x00015708 File Offset: 0x00014508
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0P@$$CBD ??_C@_0P@LHKOCCJE@DumpGpt?$CI?$CJ?5End?4@;

	// Token: 0x04000085 RID: 133 RVA: 0x00015718 File Offset: 0x00014518
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@MDMCKOFE@integrityCheck?$CI?$CJ?5Start?4@;

	// Token: 0x04000086 RID: 134 RVA: 0x00015758 File Offset: 0x00014558
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0EB@$$CBD ??_C@_0EB@NNMJIEIB@Corrupted?5FFU?0?5cannot?5seek?5to?5b@;

	// Token: 0x04000087 RID: 135 RVA: 0x0001579C File Offset: 0x0001459C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DC@$$CBD ??_C@_0DC@DBIHALHK@Corrupted?5FFU?0?5could?5not?5read?5F@;

	// Token: 0x04000088 RID: 136 RVA: 0x000157D0 File Offset: 0x000145D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DJ@$$CBD ??_C@_0DJ@BLCILKMJ@Corrupted?5FFU?0?5cannot?5seek?5to?5s@;

	// Token: 0x04000089 RID: 137 RVA: 0x0001580C File Offset: 0x0001460C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CI@$$CBD ??_C@_0CI@CGNAALBM@Corrupted?5FFU?0?5hash?5mismatch?0?5f@;

	// Token: 0x0400008A RID: 138 RVA: 0x00015834 File Offset: 0x00014634
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CI@$$CBD ??_C@_0CI@PNDJCJNO@Calculation?5of?5hash?5terminated?5@;

	// Token: 0x0400008B RID: 139 RVA: 0x0001585C File Offset: 0x0001465C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BG@$$CBD ??_C@_0BG@NGLINCBG@integrityCheck?$CI?$CJ?5End?4@;

	// Token: 0x0400008C RID: 140 RVA: 0x00015874 File Offset: 0x00014674
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY03$$CBD ??_C@_03BBDBFBBB@dpp@;

	// Token: 0x0400008D RID: 141 RVA: 0x00015878 File Offset: 0x00014678
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BL@$$CBD ??_C@_0BL@EMPFHFAH@checkDppPartition?$CI?$CJ?5Start?4@;

	// Token: 0x0400008E RID: 142 RVA: 0x00015894 File Offset: 0x00014694
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BJ@$$CBD ??_C@_0BJ@PLPGDIKF@checkDppPartition?$CI?$CJ?5End?4@;

	// Token: 0x0400008F RID: 143 RVA: 0x00015434 File Offset: 0x00014234
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@CMIDDDDM@ImageId?5read?5ok?4@;

	// Token: 0x04000090 RID: 144 RVA: 0x00015448 File Offset: 0x00014248
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@FCPPKGEC@readImageId?$CI?$CJ?5Start?4@;

	// Token: 0x04000091 RID: 145 RVA: 0x00015460 File Offset: 0x00014260
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CI@$$CBD ??_C@_0CI@FEMMPJOB@readImageId?0?5Unable?5to?5read?5fil@;

	// Token: 0x04000092 RID: 146 RVA: 0x00015488 File Offset: 0x00014288
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BO@$$CBD ??_C@_0BO@EIIEAOIB@Encrypted?5image?5not?5supported@;

	// Token: 0x04000093 RID: 147 RVA: 0x000154A8 File Offset: 0x000142A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0CC@$$CBD ??_C@_0CC@DMHIICGH@Encrypted?5image?5is?5not?5supporte@;

	// Token: 0x04000094 RID: 148 RVA: 0x000154CC File Offset: 0x000142CC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BD@$$CBD ??_C@_0BD@BAEILGK@readImageId?$CI?$CJ?5End?4@;

	// Token: 0x04000095 RID: 149 RVA: 0x00015384 File Offset: 0x00014184
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BK@$$CBD ??_C@_0BK@CONNOIJG@isValidBootImage?$CI?$CJ?5Start?4@;

	// Token: 0x04000096 RID: 150 RVA: 0x000153A0 File Offset: 0x000141A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@HEHMDDPC@isValidBootImage?$CI?$CJ?5End?4@;

	// Token: 0x04000097 RID: 151 RVA: 0x000153B8 File Offset: 0x000141B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BJ@$$CBD ??_C@_0BJ@DHFDPMIM@invalid?5vector?5subscript@;

	// Token: 0x04000098 RID: 152 RVA: 0x00015DF4 File Offset: 0x00014BF4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@CFPLBAOH@invalid?5string?5position@;

	// Token: 0x04000099 RID: 153 RVA: 0x000153D4 File Offset: 0x000141D4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@$$CBD ??_C@_0BA@FOIKENOD@vector?5too?5long@;

	// Token: 0x0400009A RID: 154 RVA: 0x0003CD78 File Offset: 0x0003BB78
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_istream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x0400009B RID: 155 RVA: 0x0003CDE8 File Offset: 0x0003BBE8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_iostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x0400009C RID: 156 RVA: 0x00040CD8 File Offset: 0x0003ECD8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_72 ??_R0?AV?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@8;

	// Token: 0x0400009D RID: 157 RVA: 0x00040B28 File Offset: 0x0003EB28
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_50 ??_R0?AV?$basic_filebuf@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x0400009E RID: 158 RVA: 0x00040390 File Offset: 0x0003E390
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04Q6AXXZ ??_7FfuReader@@6B@;

	// Token: 0x0400009F RID: 159 RVA: 0x0003CE64 File Offset: 0x0003BC64
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@IFfuReader@@8;

	// Token: 0x040000A0 RID: 160 RVA: 0x0003CD34 File Offset: 0x0003BB34
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4?$basic_ofstream@DU?$char_traits@D@std@@@std@@6B@;

	// Token: 0x040000A1 RID: 161 RVA: 0x0003CD24 File Offset: 0x0003BB24
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_ofstream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000A2 RID: 162 RVA: 0x0003CD88 File Offset: 0x0003BB88
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_istream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000A3 RID: 163 RVA: 0x00040DBC File Offset: 0x0003EDBC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_16 ??_R0?AVFfuReader@@@8;

	// Token: 0x040000A4 RID: 164 RVA: 0x00040B04 File Offset: 0x0003EB04
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_22 ??_R0?AUFfuReaderResult@@@8;

	// Token: 0x040000A5 RID: 165 RVA: 0x0003CE98 File Offset: 0x0003BC98
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4IFfuReader@@6B@;

	// Token: 0x040000A6 RID: 166 RVA: 0x0003CE88 File Offset: 0x0003BC88
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3IFfuReader@@8;

	// Token: 0x040000A7 RID: 167 RVA: 0x00040DA0 File Offset: 0x0003EDA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_17 ??_R0?AVIFfuReader@@@8;

	// Token: 0x040000A8 RID: 168 RVA: 0x0003CEE4 File Offset: 0x0003BCE4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4FfuReader@@6B@;

	// Token: 0x040000A9 RID: 169 RVA: 0x0003D470 File Offset: 0x0003C270
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI1?AUFfuReaderResult@@;

	// Token: 0x040000AA RID: 170 RVA: 0x00040BA0 File Offset: 0x0003EBA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_69 ??_R0?AV?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@8;

	// Token: 0x040000AB RID: 171 RVA: 0x00040CA0 File Offset: 0x0003ECA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_19 ??_R0?AVios_base@std@@@8;

	// Token: 0x040000AC RID: 172 RVA: 0x0003CB68 File Offset: 0x0003B968
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000AD RID: 173 RVA: 0x00040D28 File Offset: 0x0003ED28
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_51 ??_R0?AV?$basic_iostream@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000AE RID: 174 RVA: 0x0003CCB0 File Offset: 0x0003BAB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R17A@3EA@?$_Iosb@H@std@@8;

	// Token: 0x040000AF RID: 175 RVA: 0x0003CD48 File Offset: 0x0003BB48
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000B0 RID: 176 RVA: 0x0004034C File Offset: 0x0003E34C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04Q6AXXZ ??_7IFfuReader@@6B@;

	// Token: 0x040000B1 RID: 177 RVA: 0x0003CD0C File Offset: 0x0003BB0C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_20 ??_R2?$basic_ofstream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000B2 RID: 178 RVA: 0x0003CEC8 File Offset: 0x0003BCC8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2FfuReader@@8;

	// Token: 0x040000B3 RID: 179 RVA: 0x0003CB00 File Offset: 0x0003B900
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_streambuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000B4 RID: 180 RVA: 0x000411AC File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> ?A0x3d80193d.IMAGE_HEADER_SIGNATURE;

	// Token: 0x040000B5 RID: 181 RVA: 0x000402BC File Offset: 0x0003E2BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@Q6AXXZ ??_7?$basic_filebuf@DU?$char_traits@D@std@@@std@@6B@;

	// Token: 0x040000B6 RID: 182 RVA: 0x000153F8 File Offset: 0x000141F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01$$CBH ??_8?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@7B?$basic_ostream@DU?$char_traits@D@std@@@1@@;

	// Token: 0x040000B7 RID: 183 RVA: 0x0003CD64 File Offset: 0x0003BB64
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$basic_istream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000B8 RID: 184 RVA: 0x0003D414 File Offset: 0x0003C214
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_cast@std@@@8??0bad_cast@std@@$$FQAE@ABV01@@Z12;

	// Token: 0x040000B9 RID: 185 RVA: 0x00040CBC File Offset: 0x0003ECBC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AV?$_Iosb@H@std@@@8;

	// Token: 0x040000BA RID: 186 RVA: 0x0004033C File Offset: 0x0003E33C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7?$basic_ofstream@DU?$char_traits@D@std@@@std@@6B@;

	// Token: 0x040000BB RID: 187 RVA: 0x0003D430 File Offset: 0x0003C230
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_8 _CTA2?AVbad_cast@std@@;

	// Token: 0x040000BC RID: 188 RVA: 0x00040BF0 File Offset: 0x0003EBF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_51 ??_R0?AV?$basic_ofstream@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000BD RID: 189 RVA: 0x00040B64 File Offset: 0x0003EB64
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_52 ??_R0?AV?$basic_streambuf@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000BE RID: 190 RVA: 0x0003CED4 File Offset: 0x0003BCD4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3FfuReader@@8;

	// Token: 0x040000BF RID: 191 RVA: 0x0003CB74 File Offset: 0x0003B974
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000C0 RID: 192 RVA: 0x0003CCCC File Offset: 0x0003BACC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$basic_ostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000C1 RID: 193 RVA: 0x0003CC68 File Offset: 0x0003BA68
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_ios@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000C2 RID: 194 RVA: 0x000153E4 File Offset: 0x000141E4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01$$CBH ??_8?$basic_ofstream@DU?$char_traits@D@std@@@std@@7B@;

	// Token: 0x040000C3 RID: 195 RVA: 0x0003D43C File Offset: 0x0003C23C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI2?AVbad_cast@std@@;

	// Token: 0x040000C4 RID: 196 RVA: 0x000402FC File Offset: 0x0003E2FC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@Q6AXXZ ??_7?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;

	// Token: 0x040000C5 RID: 197 RVA: 0x0003CDA4 File Offset: 0x0003BBA4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1BA@?0A@EA@?$basic_ostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000C6 RID: 198 RVA: 0x00040D64 File Offset: 0x0003ED64
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_50 ??_R0?AV?$basic_istream@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000C7 RID: 199 RVA: 0x00040E00 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '12894362189'.
	internal static ulong ?_OptionsStorage@?1??__local_stdio_printf_options@@YAPA_KXZ@4_KA;

	// Token: 0x040000C8 RID: 200 RVA: 0x0003CC3C File Offset: 0x0003BA3C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@ios_base@std@@8;

	// Token: 0x040000C9 RID: 201 RVA: 0x0003CCF0 File Offset: 0x0003BAF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_ostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000CA RID: 202 RVA: 0x0003CB38 File Offset: 0x0003B938
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4?$basic_filebuf@DU?$char_traits@D@std@@@std@@6B@;

	// Token: 0x040000CB RID: 203 RVA: 0x00040C68 File Offset: 0x0003EC68
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_46 ??_R0?AV?$basic_ios@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000CC RID: 204 RVA: 0x00040344 File Offset: 0x0003E344
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;

	// Token: 0x040000CD RID: 205 RVA: 0x0003CAE8 File Offset: 0x0003B8E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_4 ??_R2?$basic_streambuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000CE RID: 206 RVA: 0x0003CBD0 File Offset: 0x0003B9D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$_Iosb@H@std@@8;

	// Token: 0x040000CF RID: 207 RVA: 0x0003D468 File Offset: 0x0003C268
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_4 _CTA1?AUFfuReaderResult@@;

	// Token: 0x040000D0 RID: 208 RVA: 0x0003CB4C File Offset: 0x0003B94C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000D1 RID: 209 RVA: 0x0003CE80 File Offset: 0x0003BC80
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_4 ??_R2IFfuReader@@8;

	// Token: 0x040000D2 RID: 210 RVA: 0x00015365 File Offset: 0x00014165
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Fake_allocator std.?A0x3d80193d._Fake_alloc;

	// Token: 0x040000D3 RID: 211 RVA: 0x000153EC File Offset: 0x000141EC
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '16'.
	internal static uint ?_BUF_SIZE@?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@0IB;

	// Token: 0x040000D4 RID: 212 RVA: 0x00040DF8 File Offset: 0x0003EDF8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static locale.facet* ?_Psave@?$_Facetptr@V?$codecvt@DDU_Mbstatet@@@std@@@std@@2PBVfacet@locale@2@B;

	// Token: 0x040000D5 RID: 213 RVA: 0x0003CDC0 File Offset: 0x0003BBC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_36 ??_R2?$basic_iostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000D6 RID: 214 RVA: 0x0003CE14 File Offset: 0x0003BC14
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_40 ??_R2?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000D7 RID: 215 RVA: 0x0003CB84 File Offset: 0x0003B984
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4?$basic_stringbuf@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;

	// Token: 0x040000D8 RID: 216 RVA: 0x0003CE50 File Offset: 0x0003BC50
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator ??_R4?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@6B@;

	// Token: 0x040000D9 RID: 217 RVA: 0x0003CB98 File Offset: 0x0003B998
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_ofstream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000DA RID: 218 RVA: 0x0003CACC File Offset: 0x0003B8CC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_filebuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000DB RID: 219 RVA: 0x0003CC78 File Offset: 0x0003BA78
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@A@3FA@?$basic_ios@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000DC RID: 220 RVA: 0x0003CB28 File Offset: 0x0003B928
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_filebuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000DD RID: 221 RVA: 0x00040C2C File Offset: 0x0003EC2C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_50 ??_R0?AV?$basic_ostream@DU?$char_traits@D@std@@@std@@@8;

	// Token: 0x040000DE RID: 222 RVA: 0x0003CC20 File Offset: 0x0003BA20
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2ios_base@std@@8;

	// Token: 0x040000DF RID: 223 RVA: 0x0003CCE0 File Offset: 0x0003BAE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_ostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000E0 RID: 224 RVA: 0x0003D44C File Offset: 0x0003C24C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AUFfuReaderResult@@@8??0FfuReaderResult@@$$FQAE@ABU0@@Z28;

	// Token: 0x040000E1 RID: 225 RVA: 0x0003CDF8 File Offset: 0x0003BBF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_iostream@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000E2 RID: 226 RVA: 0x000153F0 File Offset: 0x000141F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01$$CBH ??_8?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@7B?$basic_istream@DU?$char_traits@D@std@@@1@@;

	// Token: 0x040000E3 RID: 227 RVA: 0x0003CC2C File Offset: 0x0003BA2C
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3ios_base@std@@8;

	// Token: 0x040000E4 RID: 228 RVA: 0x0003CB1C File Offset: 0x0003B91C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2?$basic_filebuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000E5 RID: 229 RVA: 0x0003CAF0 File Offset: 0x0003B8F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_streambuf@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000E6 RID: 230 RVA: 0x0003CE40 File Offset: 0x0003BC40
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$basic_stringstream@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@8;

	// Token: 0x040000E7 RID: 231 RVA: 0x0003CBEC File Offset: 0x0003B9EC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_4 ??_R2?$_Iosb@H@std@@8;

	// Token: 0x040000E8 RID: 232 RVA: 0x0003CC94 File Offset: 0x0003BA94
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@A@3EA@ios_base@std@@8;

	// Token: 0x040000E9 RID: 233 RVA: 0x000152BC File Offset: 0x000140BC
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x3d80193d.IMAGE_HEADER_SIGNATURE$initializer$;

	// Token: 0x040000EA RID: 234 RVA: 0x0003CBB4 File Offset: 0x0003B9B4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$basic_ios@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000EB RID: 235 RVA: 0x0003CBF4 File Offset: 0x0003B9F4
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$_Iosb@H@std@@8;

	// Token: 0x040000EC RID: 236 RVA: 0x0003CC04 File Offset: 0x0003BA04
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R17?0A@EA@?$_Iosb@H@std@@8;

	// Token: 0x040000ED RID: 237 RVA: 0x00040DF0 File Offset: 0x0003EDF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Mbstatet ?_Stinit@?1??_Init@?$basic_filebuf@DU?$char_traits@D@std@@@std@@IAEXPAU_iobuf@@W4_Initfl@23@@Z@4U_Mbstatet@@A;

	// Token: 0x040000EE RID: 238 RVA: 0x0003CEAC File Offset: 0x0003BCAC
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@FfuReader@@8;

	// Token: 0x040000EF RID: 239 RVA: 0x0003CC58 File Offset: 0x0003BA58
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_12 ??_R2?$basic_ios@DU?$char_traits@D@std@@@std@@8;

	// Token: 0x040000F0 RID: 240 RVA: 0x00015C5C File Offset: 0x00014A5C
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?hex@std@@$$FYAAAVios_base@1@AAV21@@Z;

	// Token: 0x040000F1 RID: 241 RVA: 0x000155E8 File Offset: 0x000143E8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@??$endl@DU?$char_traits@D@std@@@std@@$$FYAAAV?$basic_ostream@DU?$char_traits@D@std@@@0@AAV10@@Z;

	// Token: 0x040000F2 RID: 242 RVA: 0x000411C4 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int __@@_PchSym_@00@UzUBUhUhlfixvUuufurovivzwviUivovzhvUhgwzucOlyq@4B2008FD98C1DD4;

	// Token: 0x040000F3 RID: 243 RVA: 0x00015F44 File Offset: 0x00014D44
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000F4 RID: 244
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000F5 RID: 245 RVA: 0x000152A8 File Offset: 0x000140A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x040000F6 RID: 246 RVA: 0x00015F34 File Offset: 0x00014D34
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000F7 RID: 247 RVA: 0x00015F54 File Offset: 0x00014D54
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000F8 RID: 248 RVA: 0x00015F64 File Offset: 0x00014D64
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000F9 RID: 249
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000FA RID: 250
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000FB RID: 251 RVA: 0x00041540 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000FC RID: 252 RVA: 0x00040868 File Offset: 0x0003E868
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x040000FD RID: 253 RVA: 0x00041543 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000FE RID: 254 RVA: 0x0004153C File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x040000FF RID: 255
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000100 RID: 256
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000101 RID: 257 RVA: 0x00041542 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000102 RID: 258
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000103 RID: 259
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000104 RID: 260 RVA: 0x00041541 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000105 RID: 261 RVA: 0x00040864 File Offset: 0x0003E864
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000106 RID: 262 RVA: 0x000152C0 File Offset: 0x000140C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xc_mp_z;

	// Token: 0x04000107 RID: 263 RVA: 0x000152C8 File Offset: 0x000140C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xi_vt_z;

	// Token: 0x04000108 RID: 264 RVA: 0x0001529C File Offset: 0x0001409C
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x04000109 RID: 265 RVA: 0x00015290 File Offset: 0x00014090
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xc_ma_a;

	// Token: 0x0400010A RID: 266 RVA: 0x000152B0 File Offset: 0x000140B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xc_ma_z;

	// Token: 0x0400010B RID: 267 RVA: 0x00015294 File Offset: 0x00014094
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x0400010C RID: 268 RVA: 0x000152AC File Offset: 0x000140AC
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x0400010D RID: 269 RVA: 0x000152C4 File Offset: 0x000140C4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xi_vt_a;

	// Token: 0x0400010E RID: 270 RVA: 0x000152A4 File Offset: 0x000140A4
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x0400010F RID: 271 RVA: 0x000152B4 File Offset: 0x000140B4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPBXXZ __xc_mp_a;

	// Token: 0x04000110 RID: 272 RVA: 0x000152A0 File Offset: 0x000140A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x04000111 RID: 273 RVA: 0x00015298 File Offset: 0x00014098
	// Note: this field is marked with 'hasfieldrva'.
	internal static method ?A0x98f5405c.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZA;

	// Token: 0x04000112 RID: 274 RVA: 0x00015F74 File Offset: 0x00014D74
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCGJPAX@Z;

	// Token: 0x04000113 RID: 275 RVA: 0x00015F78 File Offset: 0x00014D78
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCGJPAX@Z;

	// Token: 0x04000114 RID: 276 RVA: 0x00041658 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Fac_tidy_reg_t std.?A0x3e6dbf51._Fac_tidy_reg;

	// Token: 0x04000115 RID: 277 RVA: 0x000152B8 File Offset: 0x000140B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static method std.?A0x3e6dbf51._Fac_tidy_reg$initializer$;

	// Token: 0x04000116 RID: 278 RVA: 0x00041650 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static _Fac_node* std.?A0x3e6dbf51._Fac_head;

	// Token: 0x04000117 RID: 279 RVA: 0x000416A0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static method* ?A0xac88cc7b.__onexitbegin_m;

	// Token: 0x04000118 RID: 280 RVA: 0x0004169C File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static uint ?A0xac88cc7b.__exit_list_size;

	// Token: 0x04000119 RID: 281
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x0400011A RID: 282
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PAXA;

	// Token: 0x0400011B RID: 283
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400011C RID: 284 RVA: 0x000416A4 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static method* ?A0xac88cc7b.__onexitend_m;

	// Token: 0x0400011D RID: 285
	[FixedAddressValueType]
	internal static uint __exit_list_size_app_domain;

	// Token: 0x0400011E RID: 286
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400011F RID: 287 RVA: 0x00015070 File Offset: 0x00013E70
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static basic_ostream<char,std::char_traits<char>\u0020>* __imp_std.cout;

	// Token: 0x04000120 RID: 288 RVA: 0x00015F14 File Offset: 0x00014D14
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x04000121 RID: 289 RVA: 0x00015068 File Offset: 0x00013E68
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static locale.id* __imp_?id@?$codecvt@DDU_Mbstatet@@@std@@2V0locale@2@A;

	// Token: 0x04000122 RID: 290 RVA: 0x0001527C File Offset: 0x0001407C
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000123 RID: 291 RVA: 0x00041508 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000124 RID: 292 RVA: 0x0004150C File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000125 RID: 293 RVA: 0x00015270 File Offset: 0x00014070
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000126 RID: 294 RVA: 0x00015278 File Offset: 0x00014078
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000127 RID: 295 RVA: 0x0004085C File Offset: 0x0003E85C
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000128 RID: 296 RVA: 0x00015274 File Offset: 0x00014074
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
