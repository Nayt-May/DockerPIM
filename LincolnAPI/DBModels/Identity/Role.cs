namespace LincolnAPI.DBModels.Identity
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool System { get; set; } = false;

    }
}
