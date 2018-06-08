using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        StorageContext db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db = new StorageContext();
            StorageItem stor_item = new StorageItem
            {
                Article = Article.Text,
                Name = Name.Text,
                Cost = Cost.Text
            };
            db.SaveChanges();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            db = new StorageContext();
            StorageItem stor = new StorageItem();
            stor = db.StorageItem.First();
            db.StorageItem.Remove(stor);
            db.SaveChanges();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            db = new StorageContext();
            Random rand = new Random();
            StorageItem item_stor = new StorageItem
            {
                Article = Article.Text,
                Name = Name.Text,
                Cost = Cost.Text,
                Count = rand.Next(1, 100)
            };
            db.StorageItem.Add(item_stor);
            db.SaveChanges();
        }
    }
}
