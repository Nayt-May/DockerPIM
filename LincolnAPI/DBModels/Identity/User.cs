﻿namespace LincolnAPI.DBModels.Identity
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual List<Role> Roles { get; set; }
    }

}
