namespace Core.Framework.Contracts
{
    public interface ISecurityService
    {
        UserProfile GetUserProfile(string domainUserId);
    }
}
