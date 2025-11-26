using System;
using System.Collections.Generic;

namespace GeometryApp
{
    /// <summary>
    /// Базовий абстрактний клас для геометричних об'єктів
    /// </summary>
    public abstract class GeometricObject
    {
        protected const double Epsilon = 1e-10;

        /// <summary>
        /// Віртуальний метод для виведення інформації про об'єкт
        /// </summary>
        public abstract void PrintInfo();

        /// <summary>
        /// Абстрактний метод для перевірки валідності об'єкта
        /// </summary>
        public abstract bool IsValid();

        /// <summary>
        /// Віртуальний метод для обчислення "розмірності" об'єкта
        /// </summary>
        public abstract int GetDimension();
    }

    /// <summary>
    /// Базовий клас для представлення прямої на площині
    /// Рівняння: a1*x + a2*y + a0 = 0
    /// </summary>
    public class Pryama : GeometricObject
    {
        // Приватні поля з префіксом _
        private double _a0;
        private double _a1;
        private double _a2;

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

        /// <summary>
        /// Конструктор за замовчуванням
        /// </summary>
        public Pryama()
        {
            _a0 = 0;
            _a1 = 0;
            _a2 = 0;
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        public Pryama(double a0, double a1, double a2)
        {
            _a0 = a0;
            _a1 = a1;
            _a2 = a2;
        }

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
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console. WriteLine("║                         ПРЯМА                             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
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

            if (point.Length != 2)
            {
                throw new ArgumentException("Для прямої потрібно 2 координати (x, y)");
            }

            double x = point[0];
            double y = point[1];
            double result = A1 * x + A2 * y + A0;

            return Math.Abs(result) < Epsilon;
        }

        /// <summary>
        /// Віртуальний метод обчислення відстані від точки до прямої
        /// </summary>
        public virtual double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != 2)
            {
                throw new ArgumentException("Потрібно 2 координати");
            }

            double x = point[0];
            double y = point[1];

            // Формула відстані від точки до прямої: |a1*x + a2*y + a0| / sqrt(a1² + a2²)
            double numerator = Math.Abs(A1 * x + A2 * y + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2);

            if (denominator < Epsilon)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти прямої");
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Віртуальний метод для отримання типу об'єкта
        /// </summary>
        public virtual string GetObjectType()
        {
            return "Пряма";
        }

        /// <summary>
        /// Перевизначення абстрактного методу PrintInfo
        /// </summary>
        public override void PrintInfo()
        {
            Console.WriteLine($"┌─ Тип: {GetObjectType()}");
            Console.WriteLine($"│  Рівняння: ({A1})*x + ({A2})*y + ({A0}) = 0");
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}");
            Console.WriteLine($"└─ Статус: {(IsValid() ? "✓ Валідний" : "✗ Невалідний")}");
        }

        /// <summary>
        /// Перевизначення методу валідності
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
    }

    /// <summary>
    /// Похідний клас для гіперплощини у 4-вимірному просторі
    /// Рівняння: a4*x4 + a3*x3 + a2*x2 + a1*x1 + a0 = 0
    /// </summary>
    public class Giperploschyna : Pryama
    {
        private double _a3;
        private double _a4;

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

        public Giperploschyna() : base()
        {
            _a3 = 0;
            _a4 = 0;
        }

        public Giperploschyna(double a0, double a1, double a2, double a3, double a4)
            : base(a0, a1, a2)
        {
            _a3 = a3;
            _a4 = a4;
        }

        /// <summary>
        /// Перевизначений віртуальний метод SetCoefficients
        /// </summary>
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

        /// <summary>
        /// Перевизначений віртуальний метод PrintCoefficients
        /// </summary>
        public override void PrintCoefficients()
        {
            Console. WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     ГІПЕРПЛОЩИНА                          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Рівняння: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0");
            Console.WriteLine($"Коефіцієнти: a0={A0}, a1={A1}, a2={A2}, a3={A3}, a4={A4}");
        }

        /// <summary>
        /// Перевизначений віртуальний метод ContainsPoint
        /// </summary>
        public override bool ContainsPoint(params double[] point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (point.Length != 4)
            {
                throw new ArgumentException("Для гіперплощини потрібно 4 координати (x1, x2, x3, x4)");
            }

            double result = A4 * point[3] + A3 * point[2] + A2 * point[1] + A1 * point[0] + A0;
            return Math.Abs(result) < Epsilon;
        }

        /// <summary>
        /// Перевизначений віртуальний метод DistanceToPoint
        /// </summary>
        public override double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != 4)
            {
                throw new ArgumentException("Потрібно 4 координати");
            }

            // Формула відстані в 4D: |a1*x1 + a2*x2 + a3*x3 + a4*x4 + a0| / sqrt(a1² + a2² + a3² + a4²)
            double numerator = Math. Abs(A1 * point[0] + A2 * point[1] + A3 * point[2] + A4 * point[3] + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2 + A3 * A3 + A4 * A4);

            if (denominator < Epsilon)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти гіперплощини");
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Перевизначений віртуальний метод GetObjectType
        /// </summary>
        public override string GetObjectType()
        {
            return "Гіперплощина";
        }

        /// <summary>
        /// Перевизначення методу PrintInfo
        /// </summary>
        public override void PrintInfo()
        {
            Console.WriteLine($"┌─ Тип: {GetObjectType()}");
            Console.WriteLine($"│  Рівняння: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0");
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}");
            Console.WriteLine($"└─ Статус: {(IsValid() ? "✓ Валідний" : "✗ Невалідний")}");
        }

        /// <summary>
        /// Перевизначення методу валідності
        /// </summary>
        public override bool IsValid()
        {
            return Math.Abs(A1) > Epsilon || Math. Abs(A2) > Epsilon ||
                   Math.Abs(A3) > Epsilon || Math.Abs(A4) > Epsilon;
        }

        /// <summary>
        /// Розмірність простору
        /// </summary>
        public override int GetDimension()
        {
            return 4;
        }

        public override string ToString()
        {
            return $"Гіперплощина: ({A4})*x4 + ({A3})*x3 + ({A2})*x2 + ({A1})*x1 + ({A0}) = 0";
        }
    }

    /// <summary>
    /// Клас для демонстрації поліморфізму та роботи з динамічними об'єктами
    /// </summary>
    public class GeometryManager
    {
        private List<GeometricObject> _objects;

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
                Console.ForegroundColor = ConsoleColor. Green;
                Console.WriteLine($"✓ Додано об'єкт: {obj.GetType().Name}");
                Console. ResetColor();
            }
        }

        /// <summary>
        /// Виведення інформації про всі об'єкти (демонстрація поліморфізму)
        /// </summary>
        public void PrintAllObjects()
        {
            Console.WriteLine($"\n{UIConstants.BoxTop}");
            Console.WriteLine("║          СПИСОК ВСІХ ОБ'ЄКТІВ (Поліморфізм)              ║");
            Console.WriteLine($"{UIConstants.BoxBottom}\n");

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
        /// Демонстрація виклику віртуальних методів
        /// </summary>
        public void DemonstrateVirtualMethods()
        {
            Console.WriteLine($"\n{UIConstants.BoxTop}");
            Console.WriteLine("║       ДЕМОНСТРАЦІЯ ВІРТУАЛЬНИХ МЕТОДІВ                    ║");
            Console.WriteLine($"{UIConstants.BoxBottom}\n");

            foreach (var obj in _objects)
            {
                Console.WriteLine($"\n{UIConstants. Separator}");
                obj.PrintInfo();
                
                // Перевірка чи об'єкт підтримує PrintCoefficients
                if (obj is Pryama pryama)
                {
                    pryama.PrintCoefficients();
                }
            }
        }

        /// <summary>
        /// Перевірка точки для всіх об'єктів
        /// </summary>
        public void CheckPointForAll(double[] point)
        {
            Console.WriteLine($"\n{UIConstants.BoxTop}");
            Console.WriteLine($"║  ПЕРЕВІРКА ТОЧКИ ({string.Join(", ", point)})");
            Console.WriteLine($"{UIConstants.BoxBottom}\n");

            foreach (var obj in _objects)
            {
                // Перевірка розмірності перед викликом методів
                int requiredDimension = obj.GetDimension();
                if (point.Length != requiredDimension)
                {
                    Console.ForegroundColor = ConsoleColor. Red;
                    Console.WriteLine($"{obj.GetType().Name}: Невідповідна розмірність точки (потрібно {requiredDimension}D)");
                    Console. ResetColor();
                    continue;
                }

                try
                {
                    if (obj is Pryama pryama)
                    {
                        bool belongs = pryama.ContainsPoint(point);
                        double distance = pryama.DistanceToPoint(point);

                        Console.ForegroundColor = belongs ?  ConsoleColor.Green : ConsoleColor.Yellow;
                        Console.WriteLine($"{pryama.GetObjectType()}: {(belongs ? "✓ НАЛЕЖИТЬ" : "✗ НЕ НАЛЕЖИТЬ")}");
                        Console.WriteLine($"  Відстань: {distance:F6}");
                        Console.ResetColor();
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{obj. GetType().Name}: Помилка - {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Отримання кількості об'єктів
        /// </summary>
        public int GetObjectCount()
        {
            return _objects.Count;
        }
    }

    /// <summary>
    /// Константи для UI
    /// </summary>
    public static class UIConstants
    {
        public const string BoxTop = "╔═══════════════════════════════════════════════════════════╗";
        public const string BoxBottom = "╚═══════════════════════════════════════════════════════════╝";
        public const string Separator = "────────────────────────────────────────────────────────────";
        public const string SectionTop = "┌─────────────────────────────────────────────────────────┐";
        public const string SectionBottom = "└─────────────────────────────────────────────────────────┘";
    }

    /// <summary>
    /// Допоміжний клас для введення даних
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

                Console.ForegroundColor = ConsoleColor. Red;
                Console.WriteLine("❌ Помилка!  Введіть коректне число.");
                Console. ResetColor();
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
                Console.WriteLine($"❌ Помилка! Введіть коректне число (мінімум {minValue}).");
                Console.ResetColor();
            }
        }

        public static double[] ReadCoefficients(int count, string typeName)
        {
            double[] coefficients = new double[count];
            Console.WriteLine($"\n📝 Введіть {count} коефіцієнтів для {typeName} (a0, a1, ...):");

            for (int i = 0; i < count; i++)
            {
                coefficients[i] = ReadDouble($"   a{i} = ");
            }

            return coefficients;
        }

        public static double[] ReadPoint(int dimension)
        {
            double[] point = new double[dimension];
            Console.WriteLine($"\n📍 Введіть координати точки ({dimension}D):");

            for (int i = 0; i < dimension; i++)
            {
                point[i] = ReadDouble($"   {(dimension == 2 ? (i == 0 ? "x" : "y") : $"x{i + 1}")} = ");
            }

            return point;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PrintHeader();

            try
            {
                GeometryManager manager = new GeometryManager();

                // Розбиття на методи для кращої читабельності
                CreateObjects(manager);
                DemonstratePolymorphism(manager);
                manager.DemonstrateVirtualMethods();
                manager.PrintAllObjects();
                CheckPointsLoop(manager);
                DemonstrateArrayPolymorphism(manager);
                ShowStatistics(manager);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor. Red;
                Console.WriteLine($"\n❌ Критична помилка: {ex. Message}");
                Console.WriteLine($"Деталі: {ex.StackTrace}");
                Console.ResetColor();
            }

            PrintFooter();
            Console.ReadKey();
        }

        /// <summary>
        /// Виведення заголовка програми
        /// </summary>
        private static void PrintHeader()
        {
            Console.WriteLine(UIConstants.BoxTop);
            Console.WriteLine("║  Лабораторна робота: Віртуальні методи та поліморфізм    ║");
            Console.WriteLine("║  Виконав: noic9                                           ║");
            Console.WriteLine("║  Дата: 2025-11-13                                         ║");
            Console.WriteLine($"{UIConstants.BoxBottom}\n");
        }

        /// <summary>
        /// Виведення футера програми
        /// </summary>
        private static void PrintFooter()
        {
            Console.WriteLine($"\n{UIConstants.BoxTop}");
            Console.WriteLine("║  Програма завершена.  Натисніть будь-яку клавішу...       ║");
            Console.WriteLine(UIConstants.BoxBottom);
        }

        /// <summary>
        /// Створення об'єктів
        /// </summary>
        private static void CreateObjects(GeometryManager manager)
        {
            Console.WriteLine(UIConstants.SectionTop);
            Console.WriteLine("│ ЕТАП 1: Динамічне створення об'єктів                    │");
            Console.WriteLine($"{UIConstants.SectionBottom}\n");

            // Створення прямої (динамічно)
            Console.WriteLine("🔹 Створення об'єкта 'Пряма':");
            Pryama pryama = new Pryama();
            double[] coeffPryama = InputHelper.ReadCoefficients(3, "прямої");
            pryama.SetCoefficients(coeffPryama);
            manager.AddObject(pryama);

            // Створення гіперплощини (динамічно)
            Console.WriteLine("\n🔹 Створення об'єкта 'Гіперплощина':");
            Giperploschyna giper = new Giperploschyna();
            double[] coeffGiper = InputHelper.ReadCoefficients(5, "гіперплощини");
            giper.SetCoefficients(coeffGiper);
            manager.AddObject(giper);
        }

        /// <summary>
        /// Демонстрація поліморфізму через посилання
        /// </summary>
        private static void DemonstratePolymorphism(GeometryManager manager)
        {
            Console.WriteLine($"\n{UIConstants.SectionTop}");
            Console.WriteLine("│ ЕТАП 2: Демонстрація поліморфізму через посилання       │");
            Console. WriteLine($"{UIConstants.SectionBottom}\n");

            // Створюємо гіперплощину для демонстрації
            Giperploschyna giper = new Giperploschyna(1, 2, 3, 4, 5);
            
            // Покажчик базового класу на об'єкт похідного класу
            Pryama baseRef = giper; // Поліморфізм! 

            Console.WriteLine("📌 Посилання базового класу (Pryama) вказує на об'єкт Giperploschyna:");
            Console.WriteLine($"   GetObjectType() повертає: {baseRef.GetObjectType()}");
            Console. WriteLine($"   ToString() повертає: {baseRef}");
            Console.WriteLine($"   GetDimension() повертає: {baseRef.GetDimension()}");
        }

        /// <summary>
        /// Цикл перевірки точок
        /// </summary>
        private static void CheckPointsLoop(GeometryManager manager)
        {
            Console.WriteLine($"\n{UIConstants.SectionTop}");
            Console.WriteLine("│ ЕТАП 3: Перевірка належності точок                      │");
            Console.WriteLine(UIConstants.SectionBottom);

            int pointCount = InputHelper.ReadInt("\nВведіть кількість точок для перевірки: ", 0);

            for (int i = 0; i < pointCount; i++)
            {
                Console.WriteLine($"\n{new string('─', 60)}");
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
                manager.CheckPointForAll(point);
            }
        }

        /// <summary>
        /// Демонстрація поліморфізму через масив
        /// </summary>
        private static void DemonstrateArrayPolymorphism(GeometryManager manager)
        {
            Console.WriteLine($"\n{UIConstants.SectionTop}");
            Console.WriteLine("│ ЕТАП 4: Додаткова демонстрація віртуальних методів      │");
            Console.WriteLine($"{UIConstants.SectionBottom}\n");

            // Створюємо масив для демонстрації
            GeometricObject[] geometryArray = new GeometricObject[] 
            { 
                new Pryama(1, 2, 3), 
                new Giperploschyna(1, 1, 1, 1, 1) 
            };

            Console.WriteLine("📊 Використання масиву посилань базового класу:\n");

            for (int i = 0; i < geometryArray.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] Об'єкт:");
                geometryArray[i].PrintInfo();

                Console.WriteLine($"    IsValid(): {geometryArray[i].IsValid()}");
                Console.WriteLine($"    GetDimension(): {geometryArray[i].GetDimension()}D");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Виведення статистики
        /// </summary>
        private static void ShowStatistics(GeometryManager manager)
        {
            Console.WriteLine($"\n{UIConstants.BoxTop}");
            Console.WriteLine("║                      СТАТИСТИКА                           ║");
            Console.WriteLine(UIConstants.BoxBottom);
            Console.WriteLine($"Всього створено об'єктів: {manager.GetObjectCount()}");
            Console.WriteLine($"Використано віртуальних методів: 6+");
            Console.WriteLine($"Продемонстровано поліморфізм: ✓");
            Console.WriteLine($"Динамічне створення об'єктів: ✓");
        }
    }
}
