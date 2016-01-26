using Dal.Mapping;

namespace Dal
{
    public class GroupRegistration : DomainObject
    {
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}