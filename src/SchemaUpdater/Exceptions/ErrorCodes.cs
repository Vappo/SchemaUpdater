namespace SchemaUpdater.Exceptions
{
    public enum ErrorCodes
    {
        UpdateIsAlreadyLocked,
        UpdateIsAlreadyAdded,
        ProviderNotSupported,
        NotColumnsAddedToUpdate,
        PrimaryKeyIsMissing,
    }
}