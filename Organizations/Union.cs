using System;
using System.Collections.Generic;
using System.Text;

namespace OpenVote.Shared.Organizations
{
    public class Union : IOrganization
    {
        public Union()
        {
            Type = OrganizationType.Union;
        }

        public virtual string Name { get; set; }
        public virtual OrganizationType Type { get; protected set; }

        public override string ToString()
        {
            return Type.ToString();
        }

        public string toString()
        {
            return Name;
        }
    }
}
