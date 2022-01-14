namespace TwitterServer.Responses.User
{
    public class GetUserResponse : BaseResponse
    {   
        public Guid UserId { get; set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

    }
}
