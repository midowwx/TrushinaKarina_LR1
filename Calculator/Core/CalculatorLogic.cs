using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;

namespace Calculator.Core
{
    public static class CalculatorLogger
    {
        private static readonly string LogPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        public static void Log(string action)
        {
            try
            {
                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {action}";
                File.AppendAllText(LogPath, line + Environment.NewLine);
            }
            catch { }
        }

        public static void Clear()
        {
            try
            {
                File.WriteAllText(LogPath, string.Empty);
            }
            catch { }
        }
    }

    public static class CalculatorHistory
    {
        public static List<string> Entries = new List<string>();

        public static void Add(string expression, string result)
        {
            Entries.Add($"{expression} = {result}");
        }
    }

    public static class CalculatorLogic
    {
        public static void Num_Click(TextBox display, object sender)
        {
            if (display.Text == "Ошибка" || display.Text.StartsWith("Ошибка:"))
                display.Text = "0";

            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (display.Text == "0" && digit != ",")
            {
                display.Text = digit;
            }
            else
            {
                if (digit == "," && display.Text.EndsWith(",")) return;
                display.Text += digit;
            }

            CalculatorLogger.Log($"Ввод цифры: '{digit}' -> дисплей: '{display.Text}'");
        }

        public static void Op_Click(TextBox display, object sender)
        {
            if (display.Text == "Ошибка" || display.Text.StartsWith("Ошибка:")) return;

            Button btn = (Button)sender;
            string op = btn.Content.ToString();
            
            if (op == "x^n") op = "^";

            if (display.Text.EndsWith(" "))
            {
                display.Text = display.Text.Substring(0, display.Text.Length - 3);
            }

            display.Text += " " + op + " ";
            CalculatorLogger.Log($"Выбрана операция: '{op}' -> дисплей: '{display.Text.Trim()}'");
        }

        public static void Clear_Click(TextBox display)
        {
            display.Text = "0";
            CalculatorLogger.Log("Очистка дисплея (C)");
        }

        public static void Back_Click(TextBox display)
        {
            if (display.Text == "Ошибка" || display.Text.StartsWith("Ошибка:"))
            {
                display.Text = "0";
                CalculatorLogger.Log("Backspace: сброс ошибки");
                return;
            }

            if (display.Text.Length > 1)
            {
                if (display.Text.EndsWith(" "))
                {
                    display.Text = display.Text.Substring(0, display.Text.Length - 3);
                }
                else
                {
                    display.Text = display.Text.Substring(0, display.Text.Length - 1);
                }
            }
            else
            {
                display.Text = "0";
            }
            CalculatorLogger.Log($"Backspace -> дисплей: '{display.Text}'");
        }

        public static void Sign_Click(TextBox display)
        {
            if (display.Text == "Ошибка" || display.Text.StartsWith("Ошибка:") || display.Text == "0") return;

            string[] parts = display.Text.Split(' ');
            string lastPart = parts[parts.Length - 1];

            if (lastPart == "") return;

            if (lastPart.StartsWith("-"))
                parts[parts.Length - 1] = lastPart.Substring(1);
            else
                parts[parts.Length - 1] = "-" + lastPart;

            display.Text = string.Join(" ", parts);
            CalculatorLogger.Log($"Смена знака -> дисплей: '{display.Text}'");
        }

        public static void Eq_Click(TextBox display)
        {
            try
            {
                string expr = display.Text;

                if (expr.EndsWith(" "))
                {
                    expr = expr.Substring(0, expr.Length - 3);
                }

                string originalExpr = expr;

                expr = expr.Replace(",", ".");

                double parsedResult;

                if (expr.Contains("^"))
                {
                    string[] parts = expr.Split(new string[] { " ^ " }, StringSplitOptions.None);
                    parsedResult = Convert.ToDouble(new DataTable().Compute(parts[0], null));
                    for(int i = 1; i < parts.Length; i++)
                    {
                        double powNum = Convert.ToDouble(new DataTable().Compute(parts[i], null));
                        parsedResult = Math.Pow(parsedResult, powNum);
                    }
                }
                else
                {
                    var result = new DataTable().Compute(expr, null);
                    parsedResult = Convert.ToDouble(result);
                }

                if (double.IsInfinity(parsedResult))
                {
                    display.Text = "Ошибка: большое число";
                    return;
                }
                if (double.IsNaN(parsedResult))
                {
                    display.Text = "Ошибка: не определено (NaN)";
                    return;
                }

                display.Text = parsedResult.ToString();
                if (originalExpr.Trim() != "0")
                {
                    CalculatorHistory.Add(originalExpr, display.Text);
                    CalculatorLogger.Log($"Вычисление: {originalExpr} = {display.Text}");
                }
            }
            catch
            {
                display.Text = "Ошибка";
                CalculatorLogger.Log($"Ошибка вычисления выражения");
            }
        }

        public static void Math_Click(TextBox display, object sender)
        {
            Button btn = (Button)sender;
            string func = btn.Content.ToString();

            try
            {
                if (display.Text == "Ошибка" || display.Text.StartsWith("Ошибка:")) return;

                string expr = display.Text;
                if (expr.EndsWith(" ")) return; //не применяем к висящим операторам ("2 + ")

                string[] parts = expr.Split(' ');
                string lastPart = parts[parts.Length - 1];

                double n = Convert.ToDouble(new DataTable().Compute(lastPart.Replace(",", "."), null));

                double res = 0;

                switch (func)
                {
                    case "sqrt":
                        if (n < 0) { display.Text = "Ошибка: корень < 0"; return; }
                        res = Math.Sqrt(n);
                        break;
                    case "%":
                        res = n / 100;
                        break;
                    case "1/x":
                        if (n == 0) { display.Text = "Ошибка: деление на 0"; return; }
                        res = 1 / n;
                        break;
                    case "sin":
                        res = Math.Sin(n * Math.PI / 180.0);
                        if (Math.Abs(res) < 1e-10) res = 0;
                        break;
                    case "cos":
                        res = Math.Cos(n * Math.PI / 180.0);
                        if (Math.Abs(res) < 1e-10) res = 0;
                        break;
                    case "tg":
                        if (Math.Abs(n % 180) == 90) { display.Text = "Ошибка: не определено"; return; }
                        res = Math.Tan(n * Math.PI / 180.0);
                        if (Math.Abs(res) < 1e-10) res = 0;
                        break;
                    case "ctg":
                        if (Math.Abs(n % 180) == 0) { display.Text = "Ошибка: деление на 0"; return; }
                        res = 1 / Math.Tan(n * Math.PI / 180.0);
                        if (Math.Abs(res) < 1e-10) res = 0;
                        break;
                    case "ln":
                        if (n <= 0) { display.Text = "Ошибка: логарифм <= 0"; return; }
                        res = Math.Log(n);
                        break;
                    case "log":
                        if (n <= 0) { display.Text = "Ошибка: логарифм <= 0"; return; }
                        res = Math.Log10(n);
                        break;
                    default:
                        return;
                }

                parts[parts.Length - 1] = res.ToString();
                display.Text = string.Join(" ", parts);

                CalculatorHistory.Add($"{func}({n})", res.ToString());
                CalculatorLogger.Log($"Функция: {func}({n}) = {res}");
            }
            catch
            {
                display.Text = "Ошибка";
                CalculatorLogger.Log($"Ошибка функции '{func}'");
            }
        }
    }
}
