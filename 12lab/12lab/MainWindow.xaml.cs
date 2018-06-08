using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace _12lab
{
    public partial class MainWindow : Window
    {
        public interface IBuilder
        {
            void BuildHead(string head);
            void BuildMenu(string menuItems);
            void BuildPost(string post);
            void BuildFooter(string footer);
            string GetResult();
        }
        [Serializable]
        public abstract class Prototype<T>
        {
            public virtual T Clone()
            {
                return (T) MemberwiseClone();
            }
            public virtual T DeepCopy()
            {
                using (var stream = new MemoryStream())
                {
                    var context = new StreamingContext(StreamingContextStates.Clone);
                    var formatter = new BinaryFormatter {Context = context};
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return (T) formatter.Deserialize(stream);
                }
            }
        }
        [Serializable]
        public class Obj : Prototype<Obj>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Builder : IBuilder
        {
            private string _page = string.Empty;
            public void BuildHead(string head)
            {
                _page += head + Environment.NewLine;
            }

            public void BuildMenu(string menuItems)
            {
                _page += menuItems + Environment.NewLine;
            }

            public void BuildPost(string post)
            {
                _page += post + Environment.NewLine;
            }

            public void BuildFooter(string footer)
            {
                _page += footer + Environment.NewLine;
            }

            public string GetResult()
            {
                return _page;
            }
        }

        public class PrintBuilder : IBuilder
        {
            private string _page = string.Empty;
            public void BuildHead(string head)
            { }

            public void BuildMenu(string menuItems)
            { }

            public void BuildPost(string post)
            {
                _page += post + Environment.NewLine;
            }

            public void BuildFooter(string footer)
            { }

            public string GetResult()
            {
                return _page;
            }
        }
        public class Director
        {
            private readonly IBuilder _build;

            public Director(IBuilder build)
            {
                _build = build;
            }

            public string BuildPage(int number)
            {
                _build.BuildHead(GetHeade(number));
                _build.BuildMenu(GetMenuItems(number));
                foreach (var post in GetPosts(number))
                {
                    _build.BuildPost(post);
                }

                _build.BuildFooter(GetFooter(number));
                return _build.GetResult();
            }

            private string GetHeade(int number)
            {
                return "Header of page "+number;
            }

            private string GetMenuItems(int number)
            {
                return "Menu" + number;
            }

            private IEnumerable<string> GetPosts(int number)
            {
                return new List<string> {"Post 1", "Post 2"};
            }

            private string GetFooter(int number)
            {
                return "Footer of build " + number;
;            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Second.Source = new Uri("Add.xaml", UriKind.Relative);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var obj = new Obj{Id = 1, Name = "Vlad"};
            MessageBox.Show("First: " + obj.Id + "" + obj.Name);
            var clone = obj.DeepCopy();
            clone.Id = 2;
            clone.Name = "DED";
            MessageBox.Show("New obj: " + clone.Id + "" + clone.Name);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var Build = new Builder();
            var Direc = new Director(Build);
            var page = Direc.BuildPage(123);
            MessageBox.Show(page);
        }
    }
}
