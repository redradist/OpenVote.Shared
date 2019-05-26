namespace OpenVote.Shared.Organizations
{
    public class City : Union
    {
        public City()
        {
            Type = OrganizationType.City;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
