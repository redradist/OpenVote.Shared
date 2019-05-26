using System;
using System.Collections.Generic;
using System.Text;

namespace OpenVote.Shared.Organizations
{
    public class Anonymous : Union
    {
        public Anonymous()
        {
            Type = OrganizationType.Anonymous;
        }

        public override string Name { get; set; }
        public override OrganizationType Type { get; protected set; }
    }
}
