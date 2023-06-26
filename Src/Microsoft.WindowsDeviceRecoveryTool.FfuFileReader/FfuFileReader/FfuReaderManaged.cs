using System;
using System.Runtime.InteropServices;
using std;

namespace FfuFileReader
{
	// Token: 0x02000007 RID: 7
	public class FfuReaderManaged
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00002B20 File Offset: 0x00001F20
		public unsafe int Read(string path)
		{
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*(int*)<Module>.__imp_std.cout + 4) + <Module>.__imp_std.cout, 4, false);
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			<Module>.msclr.interop.marshal_as<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>,class\u0020System::String\u0020^>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref path);
			int num;
			try
			{
				FfuReader ffuReader;
				<Module>.FfuReader.{ctor}(ref ffuReader);
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
					<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					FfuReaderResult ffuReaderResult;
					<Module>.FfuReader.readFfu(ref ffuReader, &ffuReaderResult, (basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2), 131072U, true, false, false);
					try
					{
						if (ffuReaderResult != null)
						{
							GC.KeepAlive(this);
							num = ffuReaderResult;
						}
						else
						{
							this.rootKeyHash = <Module>.msclr.interop.marshal_as<class\u0020System::String\u0020^,class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>\u0020>((ref ffuReader) + 28);
							IntPtr intPtr = new IntPtr((ref ffuReader) + 17040);
							this.platformId = Marshal.PtrToStringAnsi(intPtr);
							GC.KeepAlive(this);
							num = 0;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = (ref ffuReaderResult) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReader.{dtor}), (void*)(&ffuReader));
					throw;
				}
				<Module>.FfuReader.{dtor}(ref ffuReader);
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
			return num;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00002CD8 File Offset: 0x000020D8
		public unsafe int ReadPlatformId(string path)
		{
			<Module>.std.basic_ios<char,std::char_traits<char>\u0020>.setstate(*(*(int*)<Module>.__imp_std.cout + 4) + <Module>.__imp_std.cout, 4, false);
			basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>;
			<Module>.msclr.interop.marshal_as<class\u0020std::basic_string<char,struct\u0020std::char_traits<char>,class\u0020std::allocator<char>\u0020>,class\u0020System::String\u0020^>(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>, ref path);
			int num;
			try
			{
				FfuReader ffuReader;
				<Module>.FfuReader.{ctor}(ref ffuReader);
				try
				{
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020> basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2;
					<Module>.std._String_val<std::_Simple_types<char>\u0020>.{ctor}(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2);
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Construct_lv_contents(ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2, ref basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), (void*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
						throw;
					}
					FfuReaderResult ffuReaderResult;
					<Module>.FfuReader.readFfuPlatformId(ref ffuReader, &ffuReaderResult, (basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>*)(&basic_string<char,std::char_traits<char>,std::allocator<char>_u0020>2));
					try
					{
						if (ffuReaderResult != null)
						{
							GC.KeepAlive(this);
							num = ffuReaderResult;
						}
						else
						{
							IntPtr intPtr = new IntPtr((ref ffuReader) + 17040);
							this.platformId = Marshal.PtrToStringAnsi(intPtr);
							GC.KeepAlive(this);
							num = 0;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(FfuReaderResult.{dtor}), (void*)(&ffuReaderResult));
						throw;
					}
					basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* ptr = (ref ffuReaderResult) + 4;
					try
					{
						<Module>.std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>._Tidy_deallocate((ref ffuReaderResult) + 4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(std._Compressed_pair<std::allocator<char>,std::_String_val<std::_Simple_types<char>\u0020>,1>.{dtor}), ptr);
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(FfuReader.{dtor}), (void*)(&ffuReader));
					throw;
				}
				<Module>.FfuReader.{dtor}(ref ffuReader);
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
			return num;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00001B88 File Offset: 0x00000F88
		public string PlatformId
		{
			get
			{
				return this.platformId;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00001B9C File Offset: 0x00000F9C
		public string RootKeyHash
		{
			get
			{
				return this.rootKeyHash;
			}
		}

		// Token: 0x0400012D RID: 301
		private string rootKeyHash;

		// Token: 0x0400012E RID: 302
		private string platformId;
	}
}
