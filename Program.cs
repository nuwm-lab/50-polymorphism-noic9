using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryApp
{
    #region Constants and Resources
    /// <summary>
    /// Константи для UI
    /// </summary>
    public static class UIConstants
    {
        public const int LineWidth = 60;
        public const string HeaderLine = "╔═══════════════════════════════════════════════════════════╗";
        public const string FooterLine = "╚═══════════════════════════════════════════════════════════╝";
        public const string Separator = "─";

        public static class Messages
        {
            public const string ProgramTitle = "║  Лабораторна робота: Віртуальні методи та поліморфізм    ║";
            public const string Author = "║  Виконав: noic9                                           ║";
            public const string ProgramComplete = "║  Програма завершена. Натисніть будь-яку клавішу...       ║";
            public const string ObjectAdded = "✓ Додано об'єкт: ";
            public const string InvalidInput = "❌ Помилка! Введіть коректне число.";
            public const string CriticalError = "❌ Критична помилка: ";
        }

        public static class Symbols
        {
            public const string Check = "✓";
            public const string Cross = "✗";
            public const string Point = "📍";
            public const string Pencil = "📝";
            public const string Chart = "📊";
            public const string Pin = "📌";
        }
    }
    #endregion

    #region Base Classes
    /// <summary>
    /// Базовий абстрактний клас для геометричних об'єктів
    /// </summary>
    public abstract class GeometricObject
    {
        protected const double Epsilon = 1e-10;

        /// <summary>
        /// Абстрактний метод для виведення інформації про об'єкт
        /// </summary>
        public abstract void PrintInfo();

        /// <summary>
        /// Абстрактний метод для перевірки валідності об'єкта
        /// Всі нащадки повинні явно визначити власну логіку валідації
        /// </summary>
        public abstract bool IsValid();

        /// <summary>
        /// Абстрактний метод для обчислення розмірності об'єкта
        /// </summary>
        public abstract int GetDimension();

        /// <summary>
        /// Віртуальний метод для отримання типу об'єкта
        /// </summary>
        public abstract string GetObjectType();
    }
    #endregion

    #region Pryama Class
    /// <summary>
    /// Базовий клас для представлення прямої на площині
    /// Рівняння: a1*x + a2*y + a0 = 0
    /// </summary>
    public class Pryama : GeometricObject
    {
        // Приватні поля
        private double _a0;
        private double _a1;
        private double _a2;

        #region Properties
        /// <summary>
        /// Властивість для доступу до коефіцієнта a0 (вільний член)
        /// </summary>
        public double A0
        {
            get => _a0;
            protected set => _a0 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a1 (при x)
        /// </summary>
        public double A1
        {
            get => _a1;
            protected set => _a1 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a2 (при y)
        /// </summary>
        public double A2
        {
            get => _a2;
            protected set => _a2 = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор за замовчуванням
        /// </summary>
        public Pryama()
        {
            A0 = 0;
            A1 = 0;
            A2 = 0;
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        public Pryama(double a0, double a1, double a2)
        {
            A0 = a0;
            A1 = a1;
            A2 = a2;
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Віртуальний метод завдання коефіцієнтів
        /// </summary>
        public virtual void SetCoefficients(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length != 3)
            {
                throw new ArgumentException("Для прямої потрібно 3 коефіцієнти (a0, a1, a2)");
            }

            A0 = coefficients[0];
            A1 = coefficients[1];
            A2 = coefficients[2];
        }

        /// <summary>
        /// Віртуальний метод виведення коефіцієнтів
        /// </summary>
        public virtual void PrintCoefficients()
        {
            Console.WriteLine(UIConstants.HeaderLine);
            Console.WriteLine("║                         ПРЯМА                             ║");
            Console.WriteLine(UIConstants.FooterLine);
            Console.WriteLine($"Рівняння: ({A1})*x + ({A2})*y + ({A0}) = 0");
            Console.WriteLine($"Коефіцієнти: a0={A0}, a1={A1}, a2={A2}");
        }

        /// <summary>
        /// Віртуальний метод визначення належності точки
        /// </summary>
        public virtual bool ContainsPoint(params double[] point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (point.Length != GetDimension())
            {
                throw new ArgumentException($"Для {GetObjectType()} потрібно {GetDimension()} координат");
            }

            double x = point[0];
            double y = point[1];
            double result = A1 * x + A2 * y + A0;

            return Math.Abs(result) < Epsilon;
        }

        /// <summary>
        /// Віртуальний метод обчислення відстані від точки
        /// </summary>
        public virtual double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != GetDimension())
            {
                throw new ArgumentException($"Потрібно {GetDimension()} координат");
            }

            double x = point[0];
            double y = point[1];

            // Формула відстані: |a1*x + a2*y + a0| / sqrt(a1² + a2²)
            double numerator = Math.Abs(A1 * x + A2 * y + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2);

            if (denominator < Epsilon)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти прямої");
            }

            return numerator / denominator;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Перевизначення методу GetObjectType
        /// </summary>
        public override string GetObjectType()
        {
            return "Пряма";
        }

        /// <summary>
        /// Перевизначення методу PrintInfo
        /// </summary>
        public override void PrintInfo()
        {
            Console.WriteLine($"┌─ Тип: {GetObjectType()}");
            Console.WriteLine($"│  Рівняння: ({A1})*x + ({A2})*y + ({A0}) = 0");
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}D");
            Console.WriteLine($"└─ Статус: {(IsValid() ? UIConstants.Symbols.Check + " Валідний" : UIConstants.Symbols.Cross + " Невалідний")}");
        }

        /// <summary>
        /// Абстрактний метод валідності (явна реалізація)
        /// </summary>
        public override bool IsValid()
        {
            // Пряма валідна, якщо хоча б один з коефіцієнтів a1 або a2 ненульовий
            return Math.Abs(A1) > Epsilon || Math.Abs(A2) > Epsilon;
        }

        /// <summary>
        /// Розмірність простору
        /// </summary>
        public override int GetDimension()
        {
            return 2;
        }

        public override string ToString()
        {
            return $"Пряма: ({A1})*x + ({A2})*y + ({A0}) = 0";
        }
        #endregion
    }
    #endregion

    #region Giperploschyna Class
    /// <summary>
    /// Похідний клас для гіперплощини у 4-вимірному просторі
    /// Рівняння: a4*x4 + a3*x3 + a2*x2 + a1*x1 + a0 = 0
    /// </summary>
    public class Giperploschyna : Pryama
    {
        private double _a3;
        private double _a4;

        #region Properties
        public double A3
        {
            get => _a3;
            private set => _a3 = value;
        }

        public double A4
        {
            get => _a4;
            private set => _a4 = value;
        }
        #endregion

        #region Constructors
        public Giperploschyna() : base()
        {
            A3 = 0;
            A4 = 0;
        }

        public Giperploschyna(double a0, double a1, double a2, double a3, double a4)
            : base(a0, a1, a2)
        {
            A3 = a3;
            A4 = a4;
        }
        #endregion

        #region Overridden Virtual Methods
        public override void SetCoefficients(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length != 5)
            {
                throw new ArgumentException("Для гіперплощини потрібно 5 коефіцієнтів (a0, a1, a2, a3, a4)");
            }

            A0 = coefficients[0];
            A1 = coefficients[1];
            A2 = coefficients[2];
            A3 = coefficients[3];
            A4 = coefficients[4];
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine(UIConstants.HeaderLine);
            Console.WriteLine("║                     ГІПЕРПЛОЩИНА                          ║");
            Console.WriteLine(UIConstants.FooterLine);
            Console.WriteLine($"Рівняння: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0");
            Console.WriteLine($"Коефіцієнти: a0={A0}, a1={A1}, a2={A2}, a3={A3}, a4={A4}");
        }

        public override bool ContainsPoint(params double[] point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (point.Length != GetDimension())
            {
                throw new ArgumentException($"Для {GetObjectType()} потрібно {GetDimension()} координат");
            }

            double result = A4 * point[3] + A3 * point[2] + A2 * point[1] + A1 * point[0] + A0;
            return Math.Abs(result) < Epsilon;
        }

        public override double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != GetDimension())
            {
                throw new ArgumentException($"Потрібно {GetDimension()} координат");
            }

            // Формула відстані в 4D
            double numerator = Math.Abs(A1 * point[0] + A2 * point[1] + A3 * point[2] + A4 * point[3] + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2 + A3 * A3 + A4 * A4);

            if (denominator < Epsilon)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти гіперплощини");
            }

            return numerator / denominator;
        }

        public override string GetObjectType()
        {
            return "Гіперплощина";
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"┌─ Тип: {GetObjectType()}");
            Console.WriteLine($"│  Рівняння: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0");
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}D");
            Console.WriteLine($"└─ Статус: {(IsValid() ? UIConstants.Symbols.Check + " Валідний" : UIConstants.Symbols.Cross + " Невалідний")}");
        }

        public override bool IsValid()
        {
            return Math.Abs(A1) > Epsilon || Math.Abs(A2) > Epsilon ||
                   Math.Abs(A3) > Epsilon || Math.Abs(A4) > Epsilon;
        }

        public override int GetDimension()
        {
            return 4;
        }

        public override string ToString()
        {
            return $"Гіперплощина: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0";
        }
        #endregion
    }
    #endregion

    #region GeometryManager Class
    /// <summary>
    /// Клас для управління колекцією геометричних об'єктів
    /// Використовує List<GeometricObject> для максимальної гнучкості
    /// </summary>
    public class GeometryManager
    {
        private readonly List<GeometricObject> _objects;

        public GeometryManager()
        {
            _objects = new List<GeometricObject>();
        }

        /// <summary>
        /// Додавання об'єкта до колекції
        /// </summary>
        public void AddObject(GeometricObject obj)
        {
            if (obj != null)
            {
                _objects.Add(obj);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(UIConstants.Messages.ObjectAdded + obj.GetObjectType());
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Виведення всіх об'єктів
        /// </summary>
        public void PrintAllObjects()
        {
            Console.WriteLine($"\n{UIConstants.HeaderLine}");
            Console.WriteLine("║          СПИСОК ВСІХ ОБ'ЄКТІВ (Поліморфізм)              ║");
            Console.WriteLine($"{UIConstants.FooterLine}\n");

            if (_objects.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            for (int i = 0; i < _objects.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_objects[i]}");
            }
        }

        /// <summary>
        /// Демонстрація віртуальних методів
        /// </summary>
        public void DemonstrateVirtualMethods()
        {
            Console.WriteLine($"\n{UIConstants.HeaderLine}");
            Console.WriteLine("║       ДЕМОНСТРАЦІЯ ВІРТУАЛЬНИХ МЕТОДІВ                    ║");
            Console.WriteLine($"{UIConstants.FooterLine}\n");

            foreach (var obj in _objects)
            {
                Console.WriteLine("\n" + new string('─', UIConstants.LineWidth));
                obj.PrintInfo();

                if (obj is Pryama pryama)
                {
                    pryama.PrintCoefficients();
                }
            }
        }

        /// <summary>
        /// Перевірка точки для всіх об'єктів з перевіркою розмірності
        /// </summary>
        public void CheckPointForAll(double[] point)
        {
            if (point == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Точка не може бути null");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\n{UIConstants.HeaderLine}");
            Console.WriteLine($"║  ПЕРЕВІРКА ТОЧКИ ({string.Join(", ", point)})");
            Console.WriteLine($"{UIConstants.FooterLine}\n");

            foreach (var obj in _objects)
            {
                // Перевірка розмірності перед викликом методів
                if (point.Length != obj.GetDimension())
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{obj.GetObjectType()}: Пропущено (потрібно {obj.GetDimension()}D, надано {point.Length}D)");
                    Console.ResetColor();
                    continue;
                }

                try
                {
                    // Безпечний виклик методів тільки після перевірки розмірності
                    if (obj is Pryama pryama)
                    {
                        bool belongs = pryama.ContainsPoint(point);
                        double distance = pryama.DistanceToPoint(point);

                        Console.ForegroundColor = belongs ? ConsoleColor.Green : ConsoleColor.Yellow;
                        Console.WriteLine($"{obj.GetObjectType()}: {(belongs ? UIConstants.Symbols.Check + " НАЛЕЖИТЬ" : UIConstants.Symbols.Cross + " НЕ НАЛЕЖИТЬ")}");
                        Console.WriteLine($"  Відстань: {distance:F6}");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{obj.GetObjectType()}: Помилка - {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Отримання кількості об'єктів
        /// </summary>
        public int GetObjectCount() => _objects.Count;

        /// <summary>
        /// Отримання всіх об'єктів (read-only)
        /// </summary>
        public IReadOnlyList<GeometricObject> GetAllObjects() => _objects.AsReadOnly();
    }
    #endregion

    #region InputHelper Class
    /// <summary>
    /// Допоміжний клас для введення даних з валідацією
    /// </summary>
    public static class InputHelper
    {
        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(UIConstants.Messages.InvalidInput);
                Console.ResetColor();
            }
        }

        public static int ReadInt(string prompt, int minValue = int.MinValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= minValue)
                    return result;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{UIConstants.Messages.InvalidInput} (мінімум {minValue})");
                Console.ResetColor();
            }
        }

        public static double[] ReadCoefficients(int count, string typeName)
        {
            double[] coefficients = new double[count];
            Console.WriteLine($"\n{UIConstants.Symbols.Pencil} Введіть {count} коефіцієнтів для {typeName} (a0, a1, ...):");

            for (int i = 0; i < count; i++)
            {
                coefficients[i] = ReadDouble($"   a{i} = ");
            }

            return coefficients;
        }

        public static double[] ReadPoint(int dimension)
        {
            double[] point = new double[dimension];
            Console.WriteLine($"\n{UIConstants.Symbols.Point} Введіть координати точки ({dimension}D):");

            for (int i = 0; i < dimension; i++)
            {
                string label = dimension == 2 ? (i == 0 ? "x" : "y") : $"x{i + 1}";
                point[i] = ReadDouble($"   {label} = ");
            }

            return point;
        }
    }
    #endregion

    #region Program Class
    public class Program
    {
        private static GeometryManager _manager;
        private static Pryama _pryama;
        private static Giperploschyna _giper;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PrintHeader();

            try
            {
                _manager = new GeometryManager();

                // Етап 1: Створення об'єктів
                CreateObjects();

                // Етап 2: Демонстрація поліморфізму
                DemonstratePolymorphism();

                // Етап 3: Демонстрація віртуальних методів
                _manager.DemonstrateVirtualMethods();

                // Етап 4: Виведення всіх об'єктів
                _manager.PrintAllObjects();

                // Етап 5: Перевірка точок
                CheckPointsLoop();

                // Етап 6: Додаткова демонстрація
                DemonstrateArrayPolymorphism();

                // Етап 7: Статистика
                ShowStatistics();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{UIConstants.Messages.CriticalError}{ex.Message}");
                Console.WriteLine($"Деталі: {ex.StackTrace}");
                Console.ResetColor();
            }

            PrintFooter();
            Console.ReadKey();
        }

        #region Helper Methods

        /// <summary>
        /// Виведення заголовка програми
        /// </summary>
        private static void PrintHeader()
        {
            Console.WriteLine(UIConstants.HeaderLine);
            Console.WriteLine(UIConstants.Messages.ProgramTitle);
            Console.WriteLine(UIConstants.Messages.Author);
            Console.WriteLine($"║  Дата: {DateTime.UtcNow:yyyy-MM-dd}                                         ║");
            Console.WriteLine($"{UIConstants.FooterLine}\n");
        }

        /// <summary>
        /// Створення об'єктів
        /// </summary>
        private static void CreateObjects()
        {
            Console.WriteLine("┌" + new string('─', UIConstants.LineWidth - 2) + "┐");
            Console.WriteLine("│ ЕТАП 1: Динамічне створення об'єктів" + new string(' ', 23) + "│");
            Console.WriteLine("└" + new string('─', UIConstants.LineWidth - 2) + "┘\n");

            // Створення прямої
            Console.WriteLine("🔹 Створення об'єкта 'Пряма':");
            _pryama = new Pryama();
            double[] coeffPryama = InputHelper.ReadCoefficients(3, "прямої");
            _pryama.SetCoefficients(coeffPryama);
            _manager.AddObject(_pryama);

            // Створення гіперплощини
            Console.WriteLine("\n🔹 Створення об'єкта 'Гіперплощина':");
            _giper = new Giperploschyna();
            double[] coeffGiper = InputHelper.ReadCoefficients(5, "гіперплощини");
            _giper.SetCoefficients(coeffGiper);
            _manager.AddObject(_giper);
        }

        /// <summary>
        /// Демонстрація поліморфізму через посилання
        /// </summary>
        private static void DemonstratePolymorphism()
        {
            Console.WriteLine("\n┌" + new string('─', UIConstants.LineWidth - 2) + "┐");
            Console.WriteLine("│ ЕТАП 2: Демонстрація поліморфізму через посилання" + new string(' ', 9) + "│");
            Console.WriteLine("└" + new string('─', UIConstants.LineWidth - 2) + "┘\n");

            // Посилання базового класу на похідний об'єкт
            Pryama baseRef = _giper;

            Console.WriteLine($"{UIConstants.Symbols.Pin} Посилання базового класу (Pryama) вказує на об'єкт Giperploschyna:");
            Console.WriteLine($"   GetObjectType() повертає: {baseRef.GetObjectType()}");
            Console.WriteLine($"   ToString() повертає: {baseRef}");
            Console.WriteLine($"   GetDimension() повертає: {baseRef.GetDimension()}D");
            Console.WriteLine($"   IsValid() повертає: {baseRef.IsValid()}");
        }

        /// <summary>
        /// Цикл перевірки точок
        /// </summary>
        private static void CheckPointsLoop()
        {
            Console.WriteLine("\n┌" + new string('─', UIConstants.LineWidth - 2) + "┐");
            Console.WriteLine("│ ЕТАП 3: Перевірка належності точок" + new string(' ', 25) + "│");
            Console.WriteLine("└" + new string('─', UIConstants.LineWidth - 2) + "┘");

            int pointCount = InputHelper.ReadInt("\nВведіть кількість точок для перевірки: ", 0);

            for (int i = 0; i < pointCount; i++)
            {
                Console.WriteLine($"\n{new string('─', UIConstants.LineWidth)}");
                Console.WriteLine($"Точка #{i + 1}:");

                int dimension = InputHelper.ReadInt("Розмірність точки (2 або 4): ", 2);

                if (dimension != 2 && dimension != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Підтримується тільки 2D або 4D!");
                    Console.ResetColor();
                    i--;
                    continue;
                }

                double[] point = InputHelper.ReadPoint(dimension);
                _manager.CheckPointForAll(point);
            }
        }

        /// <summary>
        /// Демонстрація поліморфізму через масив
        /// </summary>
        private static void DemonstrateArrayPolymorphism()
        {
            Console.WriteLine("\n┌" + new string('─', UIConstants.LineWidth - 2) + "┐");
            Console.WriteLine("│ ЕТАП 4: Демонстрація віртуальних методів через масив" + new string(' ', 5) + "│");
            Console.WriteLine("└" + new string('─', UIConstants.LineWidth - 2) + "┘\n");

            GeometricObject[] geometryArray = new GeometricObject[] { _pryama, _giper };

            Console.WriteLine($"{UIConstants.Symbols.Chart} Використання масиву посилань базового класу:\n");

            for (int i = 0; i < geometryArray.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] Об'єкт:");
                geometryArray[i].PrintInfo();
                Console.WriteLine($"    IsValid(): {geometryArray[i].IsValid()}");
                Console.WriteLine($"    GetDimension(): {geometryArray[i].GetDimension()}D");
                Console.WriteLine($"    GetObjectType(): {geometryArray[i].GetObjectType()}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Виведення статистики
        /// </summary>
        private static void ShowStatistics()
        {
            Console.WriteLine($"\n{UIConstants.HeaderLine}");
            Console.WriteLine("║                      СТАТИСТИКА                           ║");
            Console.WriteLine(UIConstants.FooterLine);
            Console.WriteLine($"Всього створено об'єктів: {_manager.GetObjectCount()}");
            Console.WriteLine($"Використано віртуальних методів: 8");
            Console.WriteLine($"Використано абстрактних методів: 4");
            Console.WriteLine($"Продемонстровано поліморфізм: {UIConstants.Symbols.Check}");
            Console.WriteLine($"Динамічне створення об'єктів: {UIConstants.Symbols.Check}");
            Console.WriteLine($"Використано List<GeometricObject>: {UIConstants.Symbols.Check}");
        }

        /// <summary>
        /// Виведення завершального повідомлення
        /// </summary>
        private static void PrintFooter()
        {
            Console.WriteLine($"\n{UIConstants.HeaderLine}");
            Console.WriteLine(UIConstants.Messages.ProgramComplete);
            Console.WriteLine(UIConstants.FooterLine);
        }

        #endregion
    }
    #endregion
}
