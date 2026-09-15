using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.ViewModels
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string BaseTheme = "Темная";
        public static string EngTheme = "Светлая";

        public static void ChangeTheme(ResourceDictionary res, string theme)
        {
            if (theme == "Светлая")
            {
                res["MainBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(240, 240, 245)); //светло-серый
                res["ButtonBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); //белый
                res["ButtonForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black); //черный
                res["ForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black); //черный
                res["OpForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 122, 255)); //синий
                res["EqualBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(100, 200, 255)); //светло-синий
                res["NeonBorderBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent); //прозрачный 
                res["HoverOverlayBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black); //черный для  затемнения
            }
            else if (theme == "Темная")
            {
                res["MainBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(20, 20, 20)); //темно-серый
                res["ButtonBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(40, 40, 40)); //темно-серый
                res["ButtonForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); //белый
                res["ForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); //белый
                res["OpForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 200, 255)); //голубой 
                res["EqualBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 130, 180)); //темно-голубой
                res["NeonBorderBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent); //прозрачный
                res["HoverOverlayBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 200, 255)); //голубой для неона  при наведении
            }
            else if (theme == "Серая")
            {
                res["MainBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(59, 59, 59)); //базовый серый
                res["ButtonBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkSlateGray); //темно-серо-зеленый грифельный
                res["ButtonForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.GhostWhite); //призрачно-белый
                res["ForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.GhostWhite); //призрачно-белый
                res["OpForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkOrange); //темно-оранжевый
                res["EqualBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(210, 105, 30)); //шоколадно-оранжевый
                res["NeonBorderBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent); //прозрачный
                res["HoverOverlayBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black); //черный для затемнения при наведении
            }
            else if (theme == "Неоновая")
            {
                res["MainBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black); //черный
                res["ButtonBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(15, 0, 5)); // темно-красный
                res["ButtonForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); //белый
                res["ForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 70)); //неоново-красный
                res["OpForegroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 70)); //неоново-красный
                res["EqualBackgroundBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 0, 50)); //темно-красный
                res["NeonBorderBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 70)); //неоново-красный, светящаяся рамка вокруг кнопок
                res["HoverOverlayBrush"] = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 70)); //ярко-розовый, светящаяся заливка при наведенииЫ
            }
        }
    }
}


