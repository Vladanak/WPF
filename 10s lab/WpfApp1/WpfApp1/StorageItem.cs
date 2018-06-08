namespace WpfApp1
{
    class StorageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public string Cost { get; set; }
        public int Count { get; set; }

        public StorageItem(int id, string name, string article, string cost, int count)
        {
            Id = id;
            Name = name;
            Article = article;
            Cost = cost;
            Count = count;
        }
        public StorageItem() { }
    }
}
