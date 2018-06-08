using System;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace lab13
{
    public partial class Add : Page
    {
        public static string count = null;
        public Add()
        {
            InitializeComponent();
            Type.Items.Add("Cube");
            Type.Items.Add("Circle");
            List.Items.Add("MoveX");
            List.Items.Add("MoveY");
            List.Items.Add("MoveXY");
        }
        public interface IMoveCube
        {
            void MoveX(string x);
            void MoveY(string y);
            void MoveX_Y(string x,string y);
        }
        public interface IFactoryObject
        {
            string Obj { get; }
            Cube ObjCreate();
            Circle Objsec();
            SingletonBoss MainBoss();
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
        public class CubeAdapter : IMoveCube
        {
            private readonly Cube _cube = new Cube();

            public void MoveX(string x)
            {
                MessageBox.Show(_cube.Obj + " перемещён на " + x);
            }

            public void MoveY(string y)
            {
                MessageBox.Show(_cube.Obj + " перемещён на " + y);
            }

            public void MoveX_Y(string x, string y)
            {
                MessageBox.Show(_cube.Obj + " перемещён на " + x + " и " + y);
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

            private SingletonBoss() { }

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
        public class Object : IFactoryObject
        {
            public string Obj { get; }

            public Cube ObjCreate()
            {
                for (int a = 0; a < Int32.Parse(count); a++)
                {
                    new Cube();
                }
                return null;
            }

            public Circle Objsec()
            {
                for (int a = 0; a < Int32.Parse(count); a++)
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
                if (!Count.Equals(""))
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
            }
            catch
            {
                MessageBox.Show("Пустой бокс");
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (List.SelectionBoxItem.Equals("MoveX") && MoveY.Text.Equals(""))
            {
                CubeAdapter adapter = new CubeAdapter();
                adapter.MoveX(MoveX.Text);
            }

            if (List.SelectionBoxItem.Equals("MoveY") && MoveX.Text.Equals("") )
            {
                CubeAdapter adapter = new CubeAdapter();
                adapter.MoveY(MoveY.Text);
            }

            if (List.SelectionBoxItem.Equals("MoveXY") && !MoveY.Text.Equals("") && !MoveX.Text.Equals(""))
            {
                CubeAdapter adapter = new CubeAdapter();
                adapter.MoveX_Y(MoveX.Text, MoveY.Text);
            }
        }
    }
}
