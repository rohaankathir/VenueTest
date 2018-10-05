namespace Core.Framework.Contracts
{
    public interface IAppContext
    {
        string ActiveDirectoryDomainName { get; }

        string ApplicationName { get; }

        string LoadingListSize { get; }

        int DefaultCountryId { get; }

        string DefaultCountryName { get; }

        int SecurityService_AppFunctionGroups_CacheDuration { get; }

        int MaxRecordCount { get; }
    }
}
