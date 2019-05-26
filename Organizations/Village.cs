namespace OpenVote.Shared.Organizations
{
    public class Village : Union
    {
        public Village()
        {
            Type = OrganizationType.Village;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
