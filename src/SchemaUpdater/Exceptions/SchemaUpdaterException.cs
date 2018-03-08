using System;
using System.Runtime.Serialization;

namespace SchemaUpdater.Exceptions
{
    [Serializable]
    public class SchemaUpdaterException : Exception
    {
        public SchemaUpdaterException() : base()
        {
        }

        public SchemaUpdaterException(string message) : base(message)
        {
        }

        public SchemaUpdaterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SchemaUpdaterException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }

        public SchemaUpdaterException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public SchemaUpdaterException(ErrorCodes errorCode, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        protected SchemaUpdaterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ErrorCodes ErrorCode { get; set; }

        public string GetErrorMessageFromErrorCode()
        {
            switch (ErrorCode)
            {
                case ErrorCodes.UpdateIsAlreadyLocked: return "The given update is already locked. You can't add additional changes.";
                case ErrorCodes.UpdateIsAlreadyAdded: return "The given update is already added.";
                case ErrorCodes.ProviderNotSupported: return "The given provider is not supported.";
                case ErrorCodes.NotColumnsAddedToUpdate: return "No columns are added to update.";
                case ErrorCodes.PrimaryKeyIsMissing: return "Primary Key is missing.";
            }

            return string.Empty;
        }
    }
}