using System.Collections.Generic;
using Dal.Mapping;

namespace Dal
{
    public class User : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual IList<GroupRegistration> Registrations { get; set; }=new List<GroupRegistration>();
    }
}