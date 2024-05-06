namespace StapleIT.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ListDetail> ListDetail { get; set; }
    }
}
