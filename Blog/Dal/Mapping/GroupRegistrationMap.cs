namespace Dal.Mapping
{
    public class GroupRegistrationMap : DomainObjectMap<GroupRegistration>
    {
        public GroupRegistrationMap()
        {
            Table("Registrations");

            ManyToOne(x => x.Group);//, map=>map.NotNullable(true));
            ManyToOne(x => x.User); //, map => map.NotNullable(true));
        }
    }
}