namespace Authentication.Models.User
{
    public class CreateUser
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool sex { get; set; }
    }
}
