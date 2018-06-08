using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;

namespace lab14
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown; ;
        }

        #region Map and Component
        public interface IComponent
        {
            string Title { get; set; }
            void Draw();
            IComponent Find(string title);
        }
        public class MapComponent : IComponent
        {
            public string Title { get; set; }
            public void Draw()
            {
                MessageBox.Show(Title);
            }
            public IComponent Find(string title)
            {
                return Title == title ? this : null;
            }
        }
        public class Map : IComponent
        {
            private readonly List<IComponent> _map = new List<IComponent>();
            public string Title { get; set; }
            public void AddComponent(IComponent component)
            {
                _map.Add(component);
            }
            public void Draw()
            {
                MessageBox.Show(Title);
                foreach (var component in _map)
                {
                    component.Draw();
                }
            }
            public IComponent Find(string title)
            {
                if (Title.Equals(title))
                {
                    return this;
                }
                foreach (var component in _map)
                {
                    var found = component.Find(title);
                    if (found != null)
                    {
                        return found;
                    }
                }
                return null;
            }
        }
        #endregion

        #region Decorator
        public interface IElement
        {
            string Text { get; set; }
            void Draw();
        }
        public class Element : IElement
        {
            public string Text { get; set; }
            public void Draw()
            {
                MessageBox.Show($"Element text = {Text}");
            }
        }
        public class ElementDecorator : IElement
        {
            protected readonly IElement Component;
            public ElementDecorator(IElement component)
            {
                Component = component;
            }
            public virtual string Text
            {
                get => Component.Text;
                set => Component.Text = value;
            }
            public virtual void Draw()
            {
                Component.Draw();
            }
        }
        public class ElementWithBrackets : ElementDecorator
        {
            public ElementWithBrackets(IElement component) : base(component)
            {
            }
            public override void Draw()
            {
                MessageBox.Show("[");
                MessageBox.Show("(");
                base.Draw();
                MessageBox.Show(")");
                MessageBox.Show("]");
            }
        }
        #endregion

        #region Builder
        public interface IBuilder
        {
            void BuildHead(string head);
            void BuildMenu(string menuItems);
            void BuildPost(string post);
            void BuildFooter(string footer);
            string GetResult();
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
                return "Header of page " + number;
            }

            private string GetMenuItems(int number)
            {
                return "Menu" + number;
            }

            private IEnumerable<string> GetPosts(int number)
            {
                return new List<string> { "Post 1", "Post 2" };
            }

            private string GetFooter(int number)
            {
                return "Footer of build " + number;
                ;
            }
        }
        #endregion

        #region Prototype

        [Serializable]
        public abstract class Prototype<T>
        {
            public virtual T Clone()
            {
                return (T)MemberwiseClone();
            }
            public virtual T DeepCopy()
            {
                using (var stream = new MemoryStream())
                {
                    var context = new StreamingContext(StreamingContextStates.Clone);
                    var formatter = new BinaryFormatter { Context = context };
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return (T)formatter.Deserialize(stream);
                }
            }
        }
        [Serializable]
        public class Obj : Prototype<Obj>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Second.JumpCommand jump = new Second.JumpCommand();
                jump.Execute();
            }

            if (e.Key == Key.W)
            {
                Second.ClimbCommand climb = new Second.ClimbCommand();
                climb.Execute();
            }
            if (e.Key == Key.F)
            {
                Second.FireCommand fire = new Second.FireCommand();
                fire.Execute();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            First.Source = new Uri("Second.xaml", UriKind.Relative);
            Second.Source = new Uri("Add.xaml", UriKind.Relative);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var obj = new Obj { Id = 1, Name = "Vlad" };
            MessageBox.Show("First:" + obj.Id + "" + obj.Name);
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IElement element = new Element { Text = "Test" };
            element.Draw();
            IElement eb = new ElementWithBrackets(element);
            eb.Draw();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var district = new Map { Title = "District" };
            district.AddComponent(new MapComponent { Title = "Cube 1" });
            district.AddComponent(new MapComponent { Title = "Cube 2" });
            var map = new Map { Title = "New Map of Objects" };
            map.AddComponent(district);
            map.Draw();
            var find = map.Find("Cube 1");
            find.Draw();
        }
    }
}
