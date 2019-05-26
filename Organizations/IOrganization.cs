namespace OpenVote.Shared
{
    public interface IOrganization
    {
        string Name { get; }
        OrganizationType Type { get; }
    }
}
