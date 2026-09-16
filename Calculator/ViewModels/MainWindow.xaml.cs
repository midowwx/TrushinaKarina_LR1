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

namespace Calculator.ViewModels
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.ChangeTheme(this.Resources, App.BaseTheme);
            this.Loaded += (s, e) => { ThemeSelector.Text = App.BaseTheme; };
        }

        private void Num_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Num_Click(Display, sender);
        }

        private void Op_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Op_Click(Display, sender);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Clear_Click(Display);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Back_Click(Display);
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Sign_Click(Display);
        }

        private void Eq_Click(object sender, RoutedEventArgs e)
        {
            CalculatorLogic.Eq_Click(Display);
        }

        private void Hist_Click(object sender, RoutedEventArgs e)
        {
            if (CalculatorHistory.Entries.Count == 0)
            {
                MessageBox.Show("История пуста", "История");
                return;
            }

            var recent = CalculatorHistory.Entries;
            if (recent.Count > 15)
                recent = recent.Skip(recent.Count - 15).ToList();

            string text = string.Join("\n", recent);
            if (CalculatorHistory.Entries.Count > 15)
                text = "...\n" + text;

            MessageBoxResult res = MessageBox.Show(text + "\n\nЖелаете очистить историю?", "История (последние операции)", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                CalculatorHistory.Entries.Clear();
                CalculatorLogger.Clear();
                CalculatorLogger.Log("История и лог очищены пользователем");
            }
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            Display.CaretIndex = Display.Text.Length;
            Display.ScrollToHorizontalOffset(double.MaxValue);
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsInitialized) return;
            if (ThemeSelector.SelectedItem is ComboBoxItem item)
            {
                App.BaseTheme = item.Content.ToString();
                App.ChangeTheme(this.Resources, App.BaseTheme);
            }
        }

        private void ModeChange(object sender, RoutedEventArgs e)
        {
            if (!this.IsInitialized) return;

            RadioButton rb = (RadioButton)sender;

            if (rb.Content.ToString() == "Инженерный")
            {
                Window1 eng = new Window1();
                eng.Show();
                this.Close();
            }
        }
    }
}
