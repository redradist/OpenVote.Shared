namespace OpenVote.Shared.Organizations
{
    public class Country : Union
    {
        public Country()
        {
            Type = OrganizationType.Country;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
