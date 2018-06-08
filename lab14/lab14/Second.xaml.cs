using System.Windows;
using System.Windows.Controls;

namespace lab14
{
    public partial class Second : Page
    {
        public Second()
        {
            InitializeComponent();
        }

        #region Command
        interface ICommand
        {
            void Execute();
        };

        public class JumpCommand : ICommand
        {
            public void Execute()
            {
                Jump();
            }

            private void Jump()
            {
                MessageBox.Show("Прыжок!");
            }
        };

        public class FireCommand : ICommand
        {
            public void Execute()
            {
                Fire();
            }

            private void Fire()
            {
                MessageBox.Show("Огонь!");
            }
        };

        public class ClimbCommand : ICommand
        {
            public void Execute()
            {
                Climb();
            }

            private void Climb()
            {
                MessageBox.Show("Подниматься!");
            }
        };
        #endregion

        #region State
        interface IWaterState
        {
            void Heat(Water water);
            void Frost(Water water);
        }

        class Water
        {
            public IWaterState State { private get; set; }

            public Water(IWaterState ws)
            {
                State = ws;
            }

            public void Heat()
            {
                State.Heat(this);
            }
            public void Frost()
            {
                State.Frost(this);
            }
        }

        class SolidWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Превращаем лед в жидкость");
                water.State = new LiquidWaterState();
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Продолжаем заморозку льда");
            }
        }
        class LiquidWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Превращаем жидкость в пар");
                water.State = new GasWaterState();
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Превращаем жидкость в лед");
                water.State = new SolidWaterState();
            }
        }
        class GasWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Повышаем температуру водяного пара");
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Превращаем водяной пар в жидкость");
                water.State = new LiquidWaterState();
            }
        }
        #endregion

        #region Save
        public class PersonState
        {
            public  string name { get; }
            public  int age { get;  }

            public PersonState(string Name,int Age)
            {
                name = Name;
                age = Age;
            }
        }

        public class Person
        {
            public string _name = "Jokar";
            public int _age = 5;

            public void SetName(string name)
            {
                _name = name;
            }

            public void SetAge(int age)
            {
                _age = age;
            }

            public PersonState GetState()
            {
                var person = new PersonState(_name,_age);
                return person;
            }

            public void SetState(PersonState state)
            {
                _name = state.name;
                _age = state.age;
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new Person();
            p.SetName("Vlad");
            p.SetAge(18);
            MessageBox.Show(p._name + "" + p._age);
            var memento = p.GetState();
            p.SetAge(19);
            MessageBox.Show(p._name + "" + p._age);
            p.SetState(memento);
            MessageBox.Show(p._name + "" + p._age);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Water water = new Water(new LiquidWaterState());
            water.Heat();
            water.Frost();
            water.Frost();
        }
    }
}