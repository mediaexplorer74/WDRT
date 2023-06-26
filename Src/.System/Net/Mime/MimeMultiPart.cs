using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x0200024A RID: 586
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x0007301C File Offset: 0x0007121C
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x170004A5 RID: 1189
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0007302B File Offset: 0x0007122B
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0007304F File Offset: 0x0007124F
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0007308E File Offset: 0x0007128E
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this.parts == null)
				{
					this.parts = new Collection<MimeBasePart>();
				}
				return this.parts;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000730AC File Offset: 0x000712AC
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext.completed)
			{
				throw e;
			}
			try
			{
				mimePartContext.outputStream.Close();
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0007310C File Offset: 0x0007130C
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00073158 File Offset: 0x00071358
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			((MimeWriter)mimePartContext.writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0007318C File Offset: 0x0007138C
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000731D8 File Offset: 0x000713D8
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
			mimeBasePart.EndSend(result);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				mimeBasePart = mimePartContext.partsEnumerator.Current;
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00073274 File Offset: 0x00071474
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000732C0 File Offset: 0x000714C0
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.writer = new MimeWriter(mimePartContext.outputStream, base.ContentType.Boundary);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
				this.mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0007338C File Offset: 0x0007158C
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this.allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext mimePartContext = new MimeMultiPart.MimePartContext(writer, mimePartAsyncResult, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000733F8 File Offset: 0x000715F8
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00073480 File Offset: 0x00071680
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x04001715 RID: 5909
		private Collection<MimeBasePart> parts;

		// Token: 0x04001716 RID: 5910
		private static int boundary;

		// Token: 0x04001717 RID: 5911
		private AsyncCallback mimePartSentCallback;

		// Token: 0x04001718 RID: 5912
		private bool allowUnicode;

		// Token: 0x02000796 RID: 1942
		internal class MimePartContext
		{
			// Token: 0x060042B7 RID: 17079 RVA: 0x00117393 File Offset: 0x00115593
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this.writer = writer;
				this.result = result;
				this.partsEnumerator = partsEnumerator;
			}

			// Token: 0x0400336D RID: 13165
			internal IEnumerator<MimeBasePart> partsEnumerator;

			// Token: 0x0400336E RID: 13166
			internal Stream outputStream;

			// Token: 0x0400336F RID: 13167
			internal LazyAsyncResult result;

			// Token: 0x04003370 RID: 13168
			internal BaseWriter writer;

			// Token: 0x04003371 RID: 13169
			internal bool completed;

			// Token: 0x04003372 RID: 13170
			internal bool completedSynchronously = true;
		}
	}
}
