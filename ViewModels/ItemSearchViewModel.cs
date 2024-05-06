using StapleIT.Models;


namespace StapleIT.ViewModels
{
    public class ItemSearchViewModel
    {
        public Item Items { get; set; }
        public List Lists { get; set; }
        public string SearchError { get; set; }
        public List<Item> ResultList { get; set; }
        public List<List> rList { get; set; }
        public ItemSearchViewModel()
        {
            ResultList = new List<Item>();
            rList = new List<List>();
        }
    }
}
