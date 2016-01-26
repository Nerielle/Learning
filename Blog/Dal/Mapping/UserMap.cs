namespace Dal.Mapping
{
    public class UserMap : DomainObjectMap<User>
    {
        public UserMap()
        {
            Table("dbo.[User]");
            Property(x => x.Name);
            Bag(x => x.Registrations,
                colmap => colmap.Key(x => x.Column("UserId")),
                map => map.OneToMany());
        }
    }
}