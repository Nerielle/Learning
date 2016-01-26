namespace Dal.Mapping
{
    public class GroupRegistrationMap : DomainObjectMap<GroupRegistration>
    {
        public GroupRegistrationMap()
        {
            Table("Registrations");

            ManyToOne(x => x.Group, map=> map.Column("GroupId"));
            ManyToOne(x => x.User, map => map.Column("UserId"));
        }
    }
}