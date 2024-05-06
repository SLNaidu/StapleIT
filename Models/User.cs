namespace StapleIT.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmailAdd { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public virtual ICollection<UserGroup> UserGroup { get; set; }
    }
}
