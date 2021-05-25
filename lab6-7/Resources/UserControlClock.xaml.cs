using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab6_7
{
    /// <summary>
    /// Interaction logic for UserControlClock.xaml
    /// </summary>
    public partial class UserControlClock : UserControl
    {
        public static readonly DependencyProperty TimeOnlyProperty;
        DispatcherTimer timer = new DispatcherTimer();
        static UserControlClock()
        {
            TimeOnlyProperty = DependencyProperty.Register // свойство зависимости
                (
                    "TimeOnly",
                    typeof(bool),
                    typeof(UserControlClock),
                    new FrameworkPropertyMetadata(
                        false,
                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                        FrameworkPropertyMetadataOptions.AffectsRender,
                        new PropertyChangedCallback(OnTextChanged),
                        new CoerceValueCallback(CourseValue)
                        ),
                    new ValidateValueCallback(IsValidReading)
                );
        }

        private static bool IsValidReading(object value)
        {
            bool a = (bool)value;
            if ((a == true) | (a == false))
                return true;
            else return false;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object CourseValue(DependencyObject d, object baseValue)
        {
            UserControlClock clock = (UserControlClock)d;
            bool a = (bool)baseValue;
            if (a) return true;
            else return false;

        }

        public UserControlClock()
        {
            InitializeComponent();
            startclock();
            MouseDown += startclock;
            MouseDown += DaysOfWeek;

        }

        private void DaysOfWeek(object sender, MouseEventArgs e)
        {
            timer.Stop();
            clocks.Text = DateTime.Now.DayOfWeek.ToString();
        }

        public bool TimeOnly
        {
            get { return (bool)GetValue(TimeOnlyProperty); }
            set { SetValue(TimeOnlyProperty, value); }
        }
        private void startclock()
        {

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickivent;
            timer.Start();
        }
        private void startclock(object sender, MouseEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickivent;
            timer.Start();
        }
        private void tickivent(object sender, EventArgs e)
        {
            if (!TimeOnly)
                clocks.Text = DateTime.Now.ToString();
            else
                clocks.Text = DateTime.Now.ToString(@"hh\:mm\:ss");

        }
    }
}
