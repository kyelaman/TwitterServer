﻿namespace TwitterServer.Models.Entities
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

    }
}
