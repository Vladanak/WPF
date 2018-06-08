using System.Windows;
using System.Linq;
using System;

namespace WpfApp1
{
    public partial class ChannelWindow : Window
    {
        private readonly EfGenericRep<Item> _itemRep = new EfGenericRep<Item>(new ChannelContext());
        private readonly EfGenericRep<Channel> _chRep = new EfGenericRep<Channel>(new ChannelContext());
        public ChannelWindow()
        {
            InitializeComponent();
            DataGrid1.ItemsSource = _chRep.Get();
        }

        private void UpdateDb()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = _chRep.Get();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var cWin = new ChannelWin();
            if (cWin.ShowDialog() == true)
            {
                var channel = new Channel
                {
                    Title = cWin.textBox1.Text,
                    Description = cWin.textBox2.Text,
                    Link = cWin.textBox3.Text,
                    Copyright = cWin.textBox4.Text
                };
                _chRep.Create(channel);
                UpdateDb();
                MessageBox.Show("Новый канал добавлен");
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
                if (!(t is Channel channel)) continue;
                var cWin = new ChannelWin
                {
                    textBox1 = {Text = channel.Title},
                    textBox2 = {Text = channel.Description},
                    textBox3 = {Text = channel.Link},
                    textBox4 = {Text = channel.Copyright}
                };
                if (cWin.ShowDialog() == true)
                {
                    channel.Title = cWin.textBox1.Text;
                    channel.Description = cWin.textBox2.Text;
                    channel.Link = cWin.textBox3.Text;
                    channel.Copyright = cWin.textBox4.Text;
                    _chRep.Update(channel);
                    UpdateDb();
                    MessageBox.Show("Статья редактирована");
                }
                else
                {
                    return;
                }
            }
        }

        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItems.Count > 0)
            {
                foreach (var t in DataGrid1.SelectedItems)
                {
                    if (!(t is Channel channel)) continue;
                    for (var j = 0; j <_itemRep.Get().Count(); j++)
                    {
                        var items = _itemRep.Get().Where(p => p.ChannelId == channel.Id);
                        foreach (var it in items)
                        {
                            it.ChannelId = null;
                        }
                    }
                    _chRep.Remove(channel);
                    UpdateDb();
                }
            }
            MessageBox.Show("Объект удален");
        }

        private void Items_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItems.Count <= 0) return;
            foreach (var t in DataGrid1.SelectedItems)
            {
                if (t is Channel channel)
                {
                    Label1.Content = "Кол.во статей: " + _itemRep.FindById(channel.Id);
                }
            }
        }
    }
}