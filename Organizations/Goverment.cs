namespace OpenVote.Shared.Organizations
{
    public class Goverment : Union
    {
        public Goverment()
        {
            Type = OrganizationType.Government;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
