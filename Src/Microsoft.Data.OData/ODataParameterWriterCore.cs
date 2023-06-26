using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000198 RID: 408
	internal abstract class ODataParameterWriterCore : ODataParameterWriter, IODataReaderWriterListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x0002A4A0 File Offset: 0x000286A0
		protected ODataParameterWriterCore(ODataOutputContext outputContext, IEdmFunctionImport functionImport)
		{
			this.outputContext = outputContext;
			this.functionImport = functionImport;
			this.scopes.Push(ODataParameterWriterCore.ParameterWriterState.Start);
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002A4E0 File Offset: 0x000286E0
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
				if ((duplicatePropertyNamesChecker = this.duplicatePropertyNamesChecker) == null)
				{
					duplicatePropertyNamesChecker = (this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(false, false));
				}
				return duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002A507 File Offset: 0x00028707
		private ODataParameterWriterCore.ParameterWriterState State
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002A514 File Offset: 0x00028714
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			this.InterceptException(new Action(this.FlushSynchronously));
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002A538 File Offset: 0x00028738
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.EnterErrorScope();
			});
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002A560 File Offset: 0x00028760
		public sealed override void WriteStart()
		{
			this.VerifyCanWriteStart(true);
			this.InterceptException(delegate
			{
				this.WriteStartImplementation();
			});
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002A597 File Offset: 0x00028797
		public sealed override Task WriteStartAsync()
		{
			this.VerifyCanWriteStart(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.InterceptException(delegate
				{
					this.WriteStartImplementation();
				});
			});
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002A5D8 File Offset: 0x000287D8
		public sealed override void WriteValue(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(true, parameterName, parameterValue);
			this.InterceptException(delegate
			{
				this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002A67C File Offset: 0x0002887C
		public sealed override Task WriteValueAsync(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(false, parameterName, parameterValue);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.InterceptException(delegate
				{
					this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
				});
			});
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002A700 File Offset: 0x00028900
		public sealed override ODataCollectionWriter CreateCollectionWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(true, parameterName);
			return this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002A790 File Offset: 0x00028990
		public sealed override Task<ODataCollectionWriter> CreateCollectionWriterAsync(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(false, parameterName);
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(() => this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference)));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002A7ED File Offset: 0x000289ED
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.InterceptException(delegate
			{
				this.WriteEndImplementation();
			});
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002A84A File Offset: 0x00028A4A
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.InterceptException(delegate
				{
					this.WriteEndImplementation();
				});
			}).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002A875 File Offset: 0x00028A75
		void IODataReaderWriterListener.OnException()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Error);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002A87E File Offset: 0x00028A7E
		void IODataReaderWriterListener.OnCompleted()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002A887 File Offset: 0x00028A87
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			throw new ODataException(Strings.ODataParameterWriter_InStreamErrorNotSupported);
		}

		// Token: 0x06000C40 RID: 3136
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000C41 RID: 3137
		protected abstract void FlushSynchronously();

		// Token: 0x06000C42 RID: 3138
		protected abstract Task FlushAsynchronously();

		// Token: 0x06000C43 RID: 3139
		protected abstract void StartPayload();

		// Token: 0x06000C44 RID: 3140
		protected abstract void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000C45 RID: 3141
		protected abstract ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000C46 RID: 3142
		protected abstract void EndPayload();

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002A893 File Offset: 0x00028A93
		private void VerifyCanWriteStart(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Start)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteStart);
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002A8B5 File Offset: 0x00028AB5
		private void WriteStartImplementation()
		{
			this.InterceptException(new Action(this.StartPayload));
			this.EnterScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		private IEdmTypeReference VerifyCanWriteParameterAndGetTypeReference(bool synchronousCall, string parameterName)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.VerifyNotInErrorOrCompletedState();
			if (this.State != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteParameter);
			}
			if (this.parameterNamesWritten.Contains(parameterName))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(parameterName));
			}
			this.parameterNamesWritten.Add(parameterName);
			return this.GetParameterTypeReference(parameterName);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002A938 File Offset: 0x00028B38
		private IEdmTypeReference VerifyCanWriteValueParameter(bool synchronousCall, string parameterName, object parameterValue)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsODataPrimitiveTypeKind() && !edmTypeReference.IsODataComplexTypeKind())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (parameterValue != null && (!EdmLibraryExtensions.IsPrimitiveType(parameterValue.GetType()) || parameterValue is Stream) && !(parameterValue is ODataComplexValue))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(parameterName, parameterValue.GetType()));
			}
			return edmTypeReference;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002A9AC File Offset: 0x00028BAC
		private IEdmTypeReference VerifyCanCreateCollectionWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (edmTypeReference != null)
			{
				return edmTypeReference.GetCollectionItemType();
			}
			return null;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002A9F0 File Offset: 0x00028BF0
		private IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			if (this.functionImport == null)
			{
				return null;
			}
			IEdmFunctionParameter edmFunctionParameter = this.functionImport.FindParameter(parameterName);
			if (edmFunctionParameter == null)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_ParameterNameNotFoundInFunctionImport(parameterName, this.functionImport.Name));
			}
			return this.outputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002AA68 File Offset: 0x00028C68
		private void WriteValueImplementation(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.InterceptException(delegate
			{
				this.WriteValueParameter(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002AAAC File Offset: 0x00028CAC
		private ODataCollectionWriter CreateCollectionWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataCollectionWriter odataCollectionWriter = this.CreateFormatCollectionWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataCollectionWriter;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002AACA File Offset: 0x00028CCA
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.VerifyNotInErrorOrCompletedState();
			if (this.State != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteEnd);
			}
			this.VerifyAllParametersWritten();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002AB5C File Offset: 0x00028D5C
		private void VerifyAllParametersWritten()
		{
			if (this.functionImport != null && this.functionImport.Parameters != null)
			{
				IEnumerable<IEdmFunctionParameter> enumerable;
				if (this.functionImport.IsBindable)
				{
					enumerable = this.functionImport.Parameters.Skip(1);
				}
				else
				{
					enumerable = this.functionImport.Parameters;
				}
				IEnumerable<string> enumerable2 = from p in enumerable
					where !this.parameterNamesWritten.Contains(p.Name) && !this.outputContext.EdmTypeResolver.GetParameterType(p).IsNullable
					select p.Name;
				if (enumerable2.Any<string>())
				{
					enumerable2 = enumerable2.Select((string name) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { name }));
					throw new ODataException(Strings.ODataParameterWriterCore_MissingParameterInParameterPayload(string.Join(", ", enumerable2.ToArray<string>()), this.functionImport.Name));
				}
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002AC49 File Offset: 0x00028E49
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				this.EndPayload();
			});
			this.LeaveScope();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002AC63 File Offset: 0x00028E63
		private void VerifyNotInErrorOrCompletedState()
		{
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Error || this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteInErrorOrCompletedState);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002AC82 File Offset: 0x00028E82
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002AC91 File Offset: 0x00028E91
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataParameterWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002ACF8 File Offset: 0x00028EF8
		private T InterceptException<T>(Func<T> function)
		{
			T t;
			try
			{
				t = function();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
			return t;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002AD28 File Offset: 0x00028F28
		private void EnterErrorScope()
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error)
			{
				this.EnterScope(ODataParameterWriterCore.ParameterWriterState.Error);
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002AD3A File Offset: 0x00028F3A
		private void EnterScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Push(newState);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002AD4F File Offset: 0x00028F4F
		private void LeaveScope()
		{
			this.ValidateTransition(ODataParameterWriterCore.ParameterWriterState.Completed);
			if (this.State == ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				this.scopes.Pop();
			}
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Completed);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002AD74 File Offset: 0x00028F74
		private void ReplaceScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(newState);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002AD98 File Offset: 0x00028F98
		private void ValidateTransition(ODataParameterWriterCore.ParameterWriterState newState)
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error && newState == ODataParameterWriterCore.ParameterWriterState.Error)
			{
				return;
			}
			switch (this.State)
			{
			case ODataParameterWriterCore.ParameterWriterState.Start:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter && newState != ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromStart));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.CanWriteParameter:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter && newState != ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter && newState != ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCanWriteParameter));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromActiveSubWriter));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCompleted));
			case ODataParameterWriterCore.ParameterWriterState.Error:
				if (newState != ODataParameterWriterCore.ParameterWriterState.Error)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromError));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x04000432 RID: 1074
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000433 RID: 1075
		private readonly IEdmFunctionImport functionImport;

		// Token: 0x04000434 RID: 1076
		private Stack<ODataParameterWriterCore.ParameterWriterState> scopes = new Stack<ODataParameterWriterCore.ParameterWriterState>();

		// Token: 0x04000435 RID: 1077
		private HashSet<string> parameterNamesWritten = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000436 RID: 1078
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x02000199 RID: 409
		private enum ParameterWriterState
		{
			// Token: 0x0400043A RID: 1082
			Start,
			// Token: 0x0400043B RID: 1083
			CanWriteParameter,
			// Token: 0x0400043C RID: 1084
			ActiveSubWriter,
			// Token: 0x0400043D RID: 1085
			Completed,
			// Token: 0x0400043E RID: 1086
			Error
		}
	}
}
