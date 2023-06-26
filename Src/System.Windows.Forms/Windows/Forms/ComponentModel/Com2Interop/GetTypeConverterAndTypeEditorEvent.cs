using System;
using System.ComponentModel;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AC RID: 1196
	internal class GetTypeConverterAndTypeEditorEvent : EventArgs
	{
		// Token: 0x06004F3F RID: 20287 RVA: 0x0014609E File Offset: 0x0014429E
		public GetTypeConverterAndTypeEditorEvent(TypeConverter typeConverter, object typeEditor)
		{
			this.typeEditor = typeEditor;
			this.typeConverter = typeConverter;
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x001460B4 File Offset: 0x001442B4
		// (set) Token: 0x06004F41 RID: 20289 RVA: 0x001460BC File Offset: 0x001442BC
		public TypeConverter TypeConverter
		{
			get
			{
				return this.typeConverter;
			}
			set
			{
				this.typeConverter = value;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x001460C5 File Offset: 0x001442C5
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x001460CD File Offset: 0x001442CD
		public object TypeEditor
		{
			get
			{
				return this.typeEditor;
			}
			set
			{
				this.typeEditor = value;
			}
		}

		// Token: 0x04003441 RID: 13377
		private TypeConverter typeConverter;

		// Token: 0x04003442 RID: 13378
		private object typeEditor;
	}
}
