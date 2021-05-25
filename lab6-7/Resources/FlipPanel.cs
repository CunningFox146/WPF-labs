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
using System.Windows.Controls.Primitives;

namespace lab6_7
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_4OOP6"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_4OOP6;assembly=_4OOP6"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:FlipPanel/>
    ///
    /// </summary>

    [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton)),
    TemplatePart(Name = "FlipButtonAlternate", Type = typeof(ToggleButton)),
    TemplateVisualState(Name = "Normal", GroupName = "ViewStates"),
    TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
    public class FlipPanel : Control
    {
        static FlipPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel), new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        public static readonly DependencyProperty FrontContentProperty =
        DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);

        public object FrontContent
        {
            get
            {
                return GetValue(FrontContentProperty);
            }
            set
            {
                SetValue(FrontContentProperty, value);
            }
        }


        public static readonly DependencyProperty BackContentProperty =
        DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);

        public object BackContent
        {
            get
            {
                return GetValue(BackContentProperty);
            }
            set
            {
                SetValue(BackContentProperty, value);
            }
        }


        public static readonly DependencyProperty IsFlippedProperty =
        DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), null);

        public bool IsFlipped
        {
            get
            {
                return (bool)GetValue(IsFlippedProperty);
            }
            set
            {
                SetValue(IsFlippedProperty, value);

                ChangeVisualState(true);
            }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), new FrameworkPropertyMetadata(new CornerRadius(20), FrameworkPropertyMetadataOptions.None, null, new CoerceValueCallback(CoerceValue)), new ValidateValueCallback(ValidateValue));
        private static bool ValidateValue(object value)
        {
            int currentValue =(int)((CornerRadius)value).TopLeft;
            if (currentValue >= 0)
                return true;
            return false;
        }
        private static object CoerceValue(DependencyObject d, object value)
        {
            if (((int)((CornerRadius)value).TopLeft) > 25)
            {
                return 25;
            }
            return value;
        }
        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ToggleButton flipButton = base.GetTemplateChild("FlipButton") as ToggleButton;
            if (flipButton != null) flipButton.Click += flipButton_Click;

            ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
            if (flipButtonAlternate != null)
                flipButtonAlternate.Click += flipButton_Click;

            this.ChangeVisualState(false);
        }

        private void flipButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsFlipped = !this.IsFlipped;
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.IsFlipped)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }

            // Disable flipped side to prevent tabbing to invisible buttons.            
            UIElement front = FrontContent as UIElement;
            if (front != null)
            {
                if (IsFlipped)
                {
                    front.Visibility = Visibility.Hidden;
                }
                else
                {
                    front.Visibility = Visibility.Visible;
                }
            }
            UIElement back = BackContent as UIElement;
            if (back != null)
            {
                if (IsFlipped)
                {
                    back.Visibility = Visibility.Visible;
                }
                else
                {
                    back.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}

