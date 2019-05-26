namespace OpenVote.Shared.Organizations
{
    public class Company : Union
    {
        public Company()
        {
            Type = OrganizationType.Company;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
