using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace lab7
{
    public partial class MainWindow : Window
    {
        private readonly DataTable _studTable=new DataTable();
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        public MainWindow()
        {
            InitializeComponent();
            _newcon.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Firstly", _newcon);
            adapter.Fill(_studTable);
            User_Grid.DataContext = _studTable.DefaultView;
            _newcon.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _studTable.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter=new SqlDataAdapter("Select * from Firstly",_newcon);
            adapter.Fill(_studTable);
            User_Grid.DataContext = _studTable.DefaultView;
            _newcon.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _newcon.OpenAsync();
            if (Id.Text.Equals("") ||
                name.Text.Equals("") ||
                email.Text.Equals("") ||
                password.Password.Equals(""))
            { MessageBox.Show("Чтобы добавить элемент нужно заполнить все поля!"); }
            else
            {
                string Value =
                    $"Insert into Firstly(Id,Username,Email,Passwoord) values({Id.Text},'{name.Text}','{email.Text}','{password.Password}')";
                SqlCommand com = new SqlCommand(Value, _newcon);
                await com.ExecuteNonQueryAsync();
                _studTable.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Firstly", _newcon);
                adapter.Fill(_studTable);
                User_Grid.DataContext = _studTable.DefaultView;
            }
            _newcon.Close();
        }

        private async void Try(object sender, RoutedEventArgs e)
        {
            await _newcon.OpenAsync();
            string Value =
                $"Delete From Firstly where (Id = '{Id.Text}' or Username= '{name.Text}' or Email= '{email.Text}' or Passwoord = '{password.Password}')";
            SqlCommand com = new SqlCommand(Value, _newcon);
            await com.ExecuteNonQueryAsync();
            _studTable.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Firstly", _newcon);
            adapter.Fill(_studTable);
            User_Grid.DataContext = _studTable.DefaultView;
            _newcon.Close();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            await _newcon.OpenAsync();
            if (Id.Text.Equals("") && name.Text.Equals("") )
            { MessageBox.Show("Чтобы изменить элемент нужно заполнить поля Id и Username!"); }
            else
            {
                string Value = $"Update Firstly Set Username='{name.Text}' where Id='{Id.Text}'";
                SqlCommand com = new SqlCommand(Value, _newcon);
                com.ExecuteNonQuery();
            }
            _newcon.Close();
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            await _newcon.OpenAsync();
            SqlTransaction sql = _newcon.BeginTransaction();
            SqlCommand com = _newcon.CreateCommand();
            com.CommandText = "Delete from Firstly where Username='vlad'";
            com.Transaction = sql;
            await com.ExecuteNonQueryAsync();
            sql.Commit();
            MessageBox.Show("Транзакция выполнена успешно!");
            _newcon.Close();
        }
    }
}
