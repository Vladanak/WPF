using System.Windows;
using System;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly EfGenericRep<Item> _itemRep = new EfGenericRep<Item>(new ChannelContext());
        private readonly EfGenericRep<Channel> _chRep = new EfGenericRep<Channel>(new ChannelContext());
        public MainWindow()
        {
            InitializeComponent();
            DataGrid1.ItemsSource = _itemRep.Get();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var iWin = new ItemWindow
            {
                comboBox1 =
                {
                    ItemsSource = _chRep.Get(),
                    DisplayMemberPath = "Title"
                }
            };
            if (iWin.ShowDialog() == true)
            {
                var item = new Item
                {
                    Title = iWin.textBox1.Text,
                    Description = iWin.textBox2.Text,
                    Link = iWin.textBox3.Text,
                    PubDate = iWin.textBox4.Text,
                    Channel = (Channel) iWin.comboBox1.SelectedItem
                };
                _itemRep.Create(item);
                UpdateDb();
                MessageBox.Show("Новая статья добавлена");
            }
            else
            {
                return;
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (DataGrid1.SelectedItems.Count <= 0) return;
            foreach (var t in DataGrid1.SelectedItems)
            {
                if (!(t is Item item)) continue;
                var iWin = new ItemWindow
                {
                    textBox1 = {Text = item.Title},
                    textBox2 = {Text = item.Description},
                    textBox3 = {Text = item.Link},
                    textBox4 = {Text = item.PubDate},
                    comboBox1 =
                    {
                        ItemsSource = _chRep.Get(),
                        DisplayMemberPath = "Title"
                    }
                };
                if (item.Channel != null)
                    iWin.comboBox1.SelectedValue = item.Channel.Id;
                if (iWin.ShowDialog() == true)
                {
                    item.Title = iWin.textBox1.Text;
                    item.Description = iWin.textBox2.Text;
                    item.Link = iWin.textBox3.Text;
                    item.PubDate = iWin.textBox4.Text;
                    item.Channel = (Channel)iWin.comboBox1.SelectedItem;
                    _itemRep.Update(item);
                    UpdateDb();
                    MessageBox.Show("Статья редактирована");
                }
                else
                {
                    return;
                }
            }
        }
        //обновление данных грида
        private void UpdateDb()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = _itemRep.Get();
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItems.Count > 0)
            {
                foreach (var t in DataGrid1.SelectedItems)
                {
                    if (!(t is Item item)) continue;
                    _itemRep.Remove(item);
                    UpdateDb();
                }
            }
            MessageBox.Show("Объект удален");
        }

        private void Channels_Click(object sender, RoutedEventArgs e)
        {
            var cWin = new ChannelWindow();
            cWin.Show();
        }
    }
}