using System;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace _12lab
{
    public partial class Add : Page
    {
        public static string count = null;
        public interface IFactoryObject
        {
            string Obj { get; }
            Cube ObjCreate();
            Circle Objsec();
            SingletonBoss MainBoss();
        }
        public Add()
        {
            InitializeComponent();
            Type.Items.Add("Cube");
            Type.Items.Add("Circle");
        }
        public class Circle : IFactoryObject
        {
            public string Obj => "Circle";

            public Circle()
            {
                Objsec();
            }
            Cube IFactoryObject.ObjCreate()
            {
                throw new NotImplementedException();
            }

            public Circle Objsec()
            {
                MessageBox.Show(Obj);
                return null;
            }

            public SingletonBoss MainBoss()
            {
                throw new NotImplementedException();
            }
        }

        public sealed class SingletonBoss : IFactoryObject
        {
            private static readonly SingletonBoss inst = new SingletonBoss();
            public static string Obj => "SingletonBoss";
            string IFactoryObject.Obj => Obj;

            public Cube ObjCreate()
            {
                throw new NotImplementedException();
            }

            public Circle Objsec()
            {
                throw new NotImplementedException();
            }

            public SingletonBoss MainBoss()
            {
                throw new NotImplementedException();
            }

            private SingletonBoss() {}

            static SingletonBoss()
            {
                MessageBox.Show(Obj);
            }

            public static SingletonBoss GetInst()
            {
                return inst;
            }

        }
        public class Cube : IFactoryObject
        {
            public string Obj => "Cube";

            public Cube()
            {
                ObjCreate();
            }
            public Cube ObjCreate()
            {
                MessageBox.Show(Obj);
                return null;
            }
            
            public Circle Objsec()
            {
                throw new NotImplementedException();
            }

            public SingletonBoss MainBoss()
            {
                throw new NotImplementedException();
            }
        }
        public class Object:IFactoryObject
        {
            public string Obj { get; }

            public Cube ObjCreate()
            {
                for (int a = 0; a <int.Parse(count);a++)
                {
                    new Cube();
                }
                return null;
            }

            public Circle Objsec()
            {
                for (int a = 0; a < int.Parse(count); a++)
                {
                    new Circle();
                }

                return null;
            }

            public SingletonBoss MainBoss()
            {
                var s = SingletonBoss.GetInst();
                return s;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Object cube = new Object();
                count = Count.Text;
                    if (Type.SelectionBoxItem.Equals("Cube"))
                    {
                        cube.ObjCreate();
                    }
                    else
                    {
                        cube.Objsec();
                    }
                cube.MainBoss();
            }
            catch
            {
                MessageBox.Show("Не заполнено поле ввода цифрой!");
            }
        }
    }
}
