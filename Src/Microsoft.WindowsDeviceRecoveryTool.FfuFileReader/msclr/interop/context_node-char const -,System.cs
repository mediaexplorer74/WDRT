using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using msclr.interop.details;

namespace msclr.interop
{
	// Token: 0x02000004 RID: 4
	internal class context_node<char\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00001FB8 File Offset: 0x000013B8
		public unsafe context_node<char\u0020const\u0020*,System::String\u0020^>(sbyte** _to_object, string _from_object)
		{
			this._ptr = null;
			char_buffer<char> char_buffer<char>;
			if (_from_object == null)
			{
				*_to_object = 0;
			}
			else
			{
				uint num = <Module>.msclr.interop.details.GetAnsiStringSize(_from_object);
				sbyte* ptr = <Module>.new[](num);
				char_buffer<char> = ptr;
				try
				{
					if (ptr == null)
					{
						throw new InsufficientMemoryException();
					}
					<Module>.msclr.interop.details.WriteAnsiString(ptr, num, _from_object);
					char_buffer<char> = 0;
					this._ptr = ptr;
					*_to_object = ptr;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&char_buffer<char>));
					throw;
				}
				<Module>.delete[](null);
				GC.KeepAlive(this);
			}
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(msclr.interop.details.char_buffer<char>.{dtor}), (void*)(&char_buffer<char>));
				throw;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00002074 File Offset: 0x00001474
		private unsafe void ~context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000017A0 File Offset: 0x00000BA0
		private unsafe void !context_node<char\u0020const\u0020*,System::String\u0020^>()
		{
			<Module>.delete[]((void*)this._ptr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00002094 File Offset: 0x00001494
		[HandleProcessCorruptedStateExceptions]
		protected unsafe virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				<Module>.delete[]((void*)this._ptr);
				GC.KeepAlive(this);
			}
			else
			{
				try
				{
					this.!context_node<char\u0020const\u0020*,System::String\u0020^>();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00002378 File Offset: 0x00001778
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000020E4 File Offset: 0x000014E4
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400012A RID: 298
		private unsafe sbyte* _ptr;
	}
}
