using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        StorageContext db;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            db = new StorageContext();

            db.StorageItem.Load();

            List<StorageItem> ItemStorr = db.StorageItem.ToList();
            MainWindow view = new MainWindow();
            MainViewModel viewModel = new MainViewModel(ItemStorr);
            view.DataContext = viewModel;
            view.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            db.Dispose();
        }
    }
}
