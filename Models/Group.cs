using Microsoft.Extensions.Logging;

namespace StapleIT.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<UserGroup> UserGroup { get; set; }


    }
}
