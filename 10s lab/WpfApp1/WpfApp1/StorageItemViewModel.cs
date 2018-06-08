using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SampleMVVM.Commands;

namespace WpfApp1
{
    class StorageItemViewModel : INotifyPropertyChanged
    {
        public StorageItem StorageItem;
        StorageContext db;
        public StorageItemViewModel(StorageItem StorItem)
        {
            StorageItem = StorItem;
        }
        public int Id
        {
            get => StorageItem.Id;
            set
            {
                StorageItem.Id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get => StorageItem.Name;
            set
            {
                StorageItem.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Article
        {
            get => StorageItem.Article;
            set
            {
                StorageItem.Article = value;
                OnPropertyChanged("Article");
            }
        }

        public string Cost
        {
            get => StorageItem.Cost;
            set
            {
                StorageItem.Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public int Count
        {
            get => StorageItem.Count;
            set
            {
                StorageItem.Count = value;
                OnPropertyChanged("Count");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Commands

        #region Удаление

        private DelegateCommand getItemCommand;

        public ICommand GetItemCommand => getItemCommand ?? (getItemCommand = new DelegateCommand(GetItem));

        private void GetItem()
        {
            db = new StorageContext();
            if (db.StorageItem != null)
            {
                db.StorageItem.Load();
                db.StorageItem.Remove(db.StorageItem.First());
                db.SaveChanges();
                db.Dispose();
            }
            else
                MessageBox.Show("ПУсто!");
        }

        #endregion

        #region Обновление

        private DelegateCommand giveItemCommand;

        public ICommand GiveItemCommand => giveItemCommand ?? (giveItemCommand = new DelegateCommand(GiveItem));

        private void GiveItem()
        {
            db = new StorageContext();
            db.StorageItem.Load();
            var stor_item = db.StorageItem.Find(Id);
            if (stor_item != null) stor_item.Count = Count;
            db.SaveChanges();
            db.Dispose();
        }


        #endregion

        #region Добавление 
        private DelegateCommand addItemCommand;

        public ICommand AddItemCommand => addItemCommand ?? (addItemCommand = new DelegateCommand(AddItem));

        private void AddItem()
        {
            db = new StorageContext();
            db.StorageItem.Load();
            Random rand = new Random();
            StorageItem item_stor = new StorageItem
            {
                Name = Name,
                Count = rand.Next(1, 100),
                Article = Article,
                Cost = Cost
            };
            db.StorageItem.Add(item_stor);
            db.SaveChanges();
            db.Dispose();
            Console.WriteLine("Сhenit" + item_stor.Name);
        }
        #endregion
        #endregion
    }
}
