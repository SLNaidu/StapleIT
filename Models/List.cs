namespace StapleIT.Models
{
    public class List
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public DateTime? CreatedDateTime { get; set; }
   
        public int? UserGroupId { get; set; }
        public virtual ICollection<ListDetail> ListDetail { get; set; }
    }
}
