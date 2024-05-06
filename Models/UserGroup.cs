using Microsoft.Extensions.Logging;

namespace StapleIT.Models
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<List> List { get; set; }

    }
}
