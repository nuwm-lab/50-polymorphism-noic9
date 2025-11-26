using System;
using System.Collections.Generic;

namespace GeometryApp
{
    /// <summary>
    /// Базовий абстрактний клас для геометричних об'єктів
    /// </summary>
    public abstract class GeometricObject
    {
        protected const double EpsilonValue = 1e-10;

        /// <summary>
        /// Абстрактний метод для виведення інформації про об'єкт
        /// </summary>
        public abstract void PrintInfo();

        /// <summary>
        /// Абстрактний метод для перевірки валідності об'єкта
        /// Всі нащадки повинні реалізувати власну логіку валідації
        /// </summary>
        public abstract bool IsValid();

        /// <summary>
        /// Абстрактний метод для обчислення розмірності простору об'єкта
        /// </summary>
        public abstract int GetDimension();

        /// <summary>
        /// Віртуальний метод для отримання типу об'єкта
        /// </summary>
        public virtual string GetObjectType()
        {
            return GetType().Name;
        }
    }

    /// <summary>
    /// Клас для представлення прямої на площині
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
        /// Властивість для доступу до коефіцієнта a1 (коефіцієнт при x)
        /// </summary>
        public double A1
        {
            get => _a1;
            protected set => _a1 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a2 (коефіцієнт при y)
        /// </summary>
        public double A2
        {
            get => _a2;
            protected set => _a2 = value;
        }

        /// <summary>
        /// Конструктор за замовчуванням
        /// Ініціалізує всі коефіцієнти нулями
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
        /// <param name="a0">Вільний член</param>
        /// <param name="a1">Коефіцієнт при x</param>
        /// <param name="a2">Коефіцієнт при y</param>
        public Pryama(double a0, double a1, double a2)
        {
            _a0 = a0;
            _a1 = a1;
            _a2 = a2;
        }

        /// <summary>
        /// Віртуальний метод для завдання коефіцієнтів
        /// </summary>
        /// <param name="coefficients">Масив коефіцієнтів [a0, a1, a2]</param>
        public virtual void SetCoefficients(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length != 3)
            {
                throw new ArgumentException("Для прямої потрібно рівно 3 коефіцієнти: a0, a1, a2");
            }

            A0 = coefficients[0];
            A1 = coefficients[1];
            A2 = coefficients[2];
        }

        /// <summary>
        /// Віртуальний метод для виведення коефіцієнтів
        /// </summary>
        public virtual void PrintCoefficients()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         ПРЯМА                             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Рівняння: ({A1})*x + ({A2})*y + ({A0}) = 0");
            Console.WriteLine($"Коефіцієнти: a0={A0}, a1={A1}, a2={A2}");
        }

        /// <summary>
        /// Віртуальний метод для визначення належності точки до прямої
        /// </summary>
        /// <param name="point">Координати точки [x, y]</param>
        /// <returns>true, якщо точка належить прямій</returns>
        public virtual bool ContainsPoint(params double[] point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (point.Length != 2)
            {
                throw new ArgumentException("Для прямої потрібно рівно 2 координати: x, y");
            }

            double x = point[0];
            double y = point[1];
            double result = A1 * x + A2 * y + A0;

            return Math.Abs(result) < EpsilonValue;
        }

        /// <summary>
        /// Віртуальний метод для обчислення відстані від точки до прямої
        /// Використовує формулу: d = |a1*x + a2*y + a0| / sqrt(a1² + a2²)
        /// </summary>
        /// <param name="point">Координати точки [x, y]</param>
        /// <returns>Відстань від точки до прямої</returns>
        public virtual double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != 2)
            {
                throw new ArgumentException("Потрібно рівно 2 координати: x, y");
            }

            double x = point[0];
            double y = point[1];

            double numerator = Math.Abs(A1 * x + A2 * y + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2);

            if (denominator < EpsilonValue)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти прямої: a1 та a2 не можуть бути одночасно нульовими");
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Перевизначення методу для отримання типу об'єкта
        /// </summary>
        public override string GetObjectType()
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
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}D");
            Console.WriteLine($"└─ Статус: {(IsValid() ? "✓ Валідний" : "✗ Невалідний")}");
        }

        /// <summary>
        /// Перевизначення методу валідності
        /// Пряма валідна, якщо хоча б один з коефіцієнтів a1 або a2 ненульовий
        /// </summary>
        public override bool IsValid()
        {
            return Math.Abs(A1) > EpsilonValue || Math. Abs(A2) > EpsilonValue;
        }

        /// <summary>
        /// Перевизначення методу для отримання розмірності простору
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
    /// Клас для представлення гіперплощини у 4-вимірному просторі
    /// Рівняння: a1*x1 + a2*x2 + a3*x3 + a4*x4 + a0 = 0
    /// Наслідує безпосередньо GeometricObject для правильної ієрархії
    /// </summary>
    public class Giperploschyna : GeometricObject
    {
        private double _a0;
        private double _a1;
        private double _a2;
        private double _a3;
        private double _a4;

        /// <summary>
        /// Властивість для доступу до коефіцієнта a0 (вільний член)
        /// </summary>
        public double A0
        {
            get => _a0;
            protected set => _a0 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a1 (коефіцієнт при x1)
        /// </summary>
        public double A1
        {
            get => _a1;
            protected set => _a1 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a2 (коефіцієнт при x2)
        /// </summary>
        public double A2
        {
            get => _a2;
            protected set => _a2 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a3 (коефіцієнт при x3)
        /// </summary>
        public double A3
        {
            get => _a3;
            protected set => _a3 = value;
        }

        /// <summary>
        /// Властивість для доступу до коефіцієнта a4 (коефіцієнт при x4)
        /// </summary>
        public double A4
        {
            get => _a4;
            protected set => _a4 = value;
        }

        /// <summary>
        /// Конструктор за замовчуванням
        /// Ініціалізує всі коефіцієнти нулями
        /// </summary>
        public Giperploschyna()
        {
            _a0 = 0;
            _a1 = 0;
            _a2 = 0;
            _a3 = 0;
            _a4 = 0;
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        /// <param name="a0">Вільний член</param>
        /// <param name="a1">Коефіцієнт при x1</param>
        /// <param name="a2">Коефіцієнт при x2</param>
        /// <param name="a3">Коефіцієнт при x3</param>
        /// <param name="a4">Коефіцієнт при x4</param>
        public Giperploschyna(double a0, double a1, double a2, double a3, double a4)
        {
            _a0 = a0;
            _a1 = a1;
            _a2 = a2;
            _a3 = a3;
            _a4 = a4;
        }

        /// <summary>
        /// Метод для завдання коефіцієнтів
        /// </summary>
        /// <param name="coefficients">Масив коефіцієнтів [a0, a1, a2, a3, a4]</param>
        public void SetCoefficients(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length != 5)
            {
                throw new ArgumentException("Для гіперплощини потрібно рівно 5 коефіцієнтів: a0, a1, a2, a3, a4");
            }

            A0 = coefficients[0];
            A1 = coefficients[1];
            A2 = coefficients[2];
            A3 = coefficients[3];
            A4 = coefficients[4];
        }

        /// <summary>
        /// Метод для виведення коефіцієнтів
        /// </summary>
        public void PrintCoefficients()
        {
            Console. WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     ГІПЕРПЛОЩИНА                          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Рівняння: ({A1})*x1 + ({A2})*x2 + ({A3})*x3 + ({A4})*x4 + ({A0}) = 0");
            Console.WriteLine($"Коефіцієнти: a0={A0}, a1={A1}, a2={A2}, a3={A3}, a4={A4}");
        }

        /// <summary>
        /// Метод для визначення належності точки до гіперплощини
        /// </summary>
        /// <param name="point">Координати точки [x1, x2, x3, x4]</param>
        /// <returns>true, якщо точка належить гіперплощині</returns>
        public bool ContainsPoint(params double[] point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (point.Length != 4)
            {
                throw new ArgumentException("Для гіперплощини потрібно рівно 4 координати: x1, x2, x3, x4");
            }

            double result = A1 * point[0] + A2 * point[1] + A3 * point[2] + A4 * point[3] + A0;
            return Math.Abs(result) < EpsilonValue;
        }

        /// <summary>
        /// Метод для обчислення відстані від точки до гіперплощини
        /// Використовує формулу: d = |a1*x1 + a2*x2 + a3*x3 + a4*x4 + a0| / sqrt(a1² + a2² + a3² + a4²)
        /// </summary>
        /// <param name="point">Координати точки [x1, x2, x3, x4]</param>
        /// <returns>Відстань від точки до гіперплощини</returns>
        public double DistanceToPoint(params double[] point)
        {
            if (point == null || point.Length != 4)
            {
                throw new ArgumentException("Потрібно рівно 4 координати: x1, x2, x3, x4");
            }

            double numerator = Math.Abs(A1 * point[0] + A2 * point[1] + A3 * point[2] + A4 * point[3] + A0);
            double denominator = Math.Sqrt(A1 * A1 + A2 * A2 + A3 * A3 + A4 * A4);

            if (denominator < EpsilonValue)
            {
                throw new InvalidOperationException("Некоректні коефіцієнти гіперплощини: всі коефіцієнти a1, a2, a3, a4 не можуть бути одночасно нульовими");
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Перевизначення методу для отримання типу об'єкта
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
            Console.WriteLine($"│  Рівняння: ({A1})*x1 + ({A2})*x2 + ({A3})*x3 + ({A4})*x4 + ({A0}) = 0");
            Console.WriteLine($"│  Розмірність простору: {GetDimension()}D");
            Console.WriteLine($"└─ Статус: {(IsValid() ? "✓ Валідний" : "✗ Невалідний")}");
        }

        /// <summary>
        /// Перевизначення методу валідності
        /// Гіперплощина валідна, якщо хоча б один з коефіцієнтів a1, a2, a3 або a4 ненульовий
        /// </summary>
        public override bool IsValid()
        {
            return Math.Abs(A1) > EpsilonValue || Math.Abs(A2) > EpsilonValue ||
                   Math.Abs(A3) > EpsilonValue || Math.Abs(A4) > EpsilonValue;
        }

        /// <summary>
        /// Перевизначення методу для отримання розмірності простору
        /// </summary>
        public override int GetDimension()
        {
            return 4;
        }

        public override string ToString()
        {
            return $"Гіперплощина: ({A1})*x1 + ({A2})*x2 + ({A3})*x3 + ({A4})*x4 + ({A0}) = 0";
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
        /// <param name="obj">Геометричний об'єкт для додавання</param>
        public void AddObject(GeometricObject obj)
        {
            if (obj != null)
            {
                _objects.Add(obj);
                Console.ForegroundColor = ConsoleColor. Green;
                Console.WriteLine($"✓ Додано об'єкт: {obj.GetObjectType()}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Виведення інформації про всі об'єкти (демонстрація поліморфізму)
        /// </summary>
        public void PrintAllObjects()
        {
            Console.WriteLine($"\n{UiConstants.BoxTop}");
            Console.WriteLine("║          СПИСОК ВСІХ ОБ'ЄКТІВ (Поліморфізм)              ║");
            Console.WriteLine($"{UiConstants.BoxBottom}\n");

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
            Console.WriteLine($"\n{UiConstants.BoxTop}");
            Console.WriteLine("║       ДЕМОНСТРАЦІЯ ВІРТУАЛЬНИХ МЕТОДІВ                    ║");
            Console. WriteLine($"{UiConstants.BoxBottom}\n");

            foreach (var obj in _objects)
            {
                Console.WriteLine($"\n{UiConstants. Separator}");
                obj.PrintInfo();

                // Виклик методу PrintCoefficients залежно від типу
                if (obj is Pryama pryama)
                {
                    pryama.PrintCoefficients();
                }
                else if (obj is Giperploschyna giper)
                {
                    giper.PrintCoefficients();
                }
            }
        }

        /// <summary>
        /// Перевірка точки для всіх об'єктів з попередньою валідацією розмірності
        /// </summary>
        /// <param name="point">Координати точки</param>
        public void CheckPointForAll(double[] point)
        {
            Console.WriteLine($"\n{UiConstants.BoxTop}");
            Console.WriteLine($"║  ПЕРЕВІРКА ТОЧКИ ({string.Join(", ", point)})");
            Console.WriteLine($"{UiConstants.BoxBottom}\n");

            foreach (var obj in _objects)
            {
                // Перевірка розмірності перед викликом методів
                int requiredDimension = obj.GetDimension();
                if (point.Length != requiredDimension)
                {
                    Console.ForegroundColor = ConsoleColor. Red;
                    Console.WriteLine($"{obj.GetObjectType()}: Невідповідна розмірність точки (потрібно {requiredDimension}D, надано {point.Length}D)");
                    Console.ResetColor();
                    continue;
                }

                try
                {
                    bool belongs = false;
                    double distance = 0;

                    if (obj is Pryama pryama)
                    {
                        belongs = pryama. ContainsPoint(point);
                        distance = pryama. DistanceToPoint(point);
                    }
                    else if (obj is Giperploschyna giper)
                    {
                        belongs = giper.ContainsPoint(point);
                        distance = giper. DistanceToPoint(point);
                    }

                    Console.ForegroundColor = belongs ?  ConsoleColor.Green : ConsoleColor.Yellow;
                    Console.WriteLine($"{obj.GetObjectType()}: {(belongs ? "✓ НАЛЕЖИТЬ" : "✗ НЕ НАЛЕЖИТЬ")}");
                    Console.WriteLine($"  Відстань: {distance:F6}");
                    Console.ResetColor();
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
        /// Отримання кількості об'єктів у колекції
        /// </summary>
        public int GetObjectCount()
        {
            return _objects.Count;
        }
    }

    /// <summary>
    /// Константи для елементів користувацького інтерфейсу
    /// </summary>
    public static class UiConstants
    {
        public const string BoxTop = "╔═══════════════════════════════════════════════════════════╗";
        public const string BoxBottom = "╚═══════════════════════════════════════════════════════════╝";
        public const string Separator = "────────────────────────────────────────────────────────────";
        public const string SectionTop = "┌─────────────────────────────────────────────────────────┐";
        public const string SectionBottom = "└─────────────────────────────────────────────────────────┘";
    }

    /// <summary>
    /// Допоміжний клас для введення даних від користувача
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Зчитування дійсного числа з консолі
        /// </summary>
        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console. Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;

                Console.ForegroundColor = ConsoleColor. Red;
                Console.WriteLine("❌ Помилка!  Введіть коректне число.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Зчитування цілого числа з консолі з перевіркою мінімального значення
        /// </summary>
        public static int ReadInt(string prompt, int minValue = int.MinValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= minValue)
                    return result;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Помилка! Введіть коректне число (мінімум {minValue}).");
                Console. ResetColor();
            }
        }

        /// <summary>
        /// Зчитування розмірності простору (тільки 2D або 4D)
        /// </summary>
        public static int ReadDimension(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result == 2 || result == 4)
                        return result;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Помилка! Підтримуються тільки розмірності 2 або 4.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Зчитування масиву коефіцієнтів
        /// </summary>
        public static double[] ReadCoefficients(int count, string typeName)
        {
            double[] coefficients = new double[count];
            Console.WriteLine($"\n📝 Введіть {count} коефіцієнтів для {typeName} в порядку: a0, a1, a2{(count > 3 ? ", a3, a4" : "")}");

            for (int i = 0; i < count; i++)
            {
                coefficients[i] = ReadDouble($"   a{i} = ");
            }

            return coefficients;
        }

        /// <summary>
        /// Зчитування координат точки
        /// </summary>
        public static double[] ReadPoint(int dimension)
        {
            double[] point = new double[dimension];
            Console.WriteLine($"\n📍 Введіть координати точки ({dimension}D):");

            if (dimension == 2)
            {
                point[0] = ReadDouble("   x = ");
                point[1] = ReadDouble("   y = ");
            }
            else
            {
                for (int i = 0; i < dimension; i++)
                {
                    point[i] = ReadDouble($"   x{i + 1} = ");
                }
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
            Console.WriteLine(UiConstants.BoxTop);
            Console.WriteLine("║  Лабораторна робота: Віртуальні методи та поліморфізм    ║");
            Console.WriteLine("║  Виконав: noic9                                           ║");
            Console.WriteLine("║  Дата: 2025-11-13                                         ║");
            Console.WriteLine($"{UiConstants.BoxBottom}\n");
        }

        /// <summary>
        /// Виведення футера програми
        /// </summary>
        private static void PrintFooter()
        {
            Console.WriteLine($"\n{UiConstants.BoxTop}");
            Console.WriteLine("║  Програма завершена.  Натисніть будь-яку клавішу...        ║");
            Console.WriteLine(UiConstants.BoxBottom);
        }

        /// <summary>
        /// Створення об'єктів (пряма та гіперплощина)
        /// </summary>
        private static void CreateObjects(GeometryManager manager)
        {
            Console.WriteLine(UiConstants.SectionTop);
            Console.WriteLine("│ ЕТАП 1: Динамічне створення об'єктів                    │");
            Console.WriteLine($"{UiConstants.SectionBottom}\n");

            // Створення прямої
            Console.WriteLine("🔹 Створення об'єкта 'Пряма' (2D):");
            Pryama pryama = new Pryama();
            double[] coeffPryama = InputHelper.ReadCoefficients(3, "прямої");
            pryama.SetCoefficients(coeffPryama);
            manager.AddObject(pryama);

            // Створення гіперплощини
            Console.WriteLine("\n🔹 Створення об'єкта 'Гіперплощина' (4D):");
            Giperploschyna giper = new Giperploschyna();
            double[] coeffGiper = InputHelper.ReadCoefficients(5, "гіперплощини");
            giper.SetCoefficients(coeffGiper);
            manager.AddObject(giper);
        }

        /// <summary>
        /// Демонстрація поліморфізму через посилання базового класу
        /// </summary>
        private static void DemonstratePolymorphism(GeometryManager manager)
        {
            Console.WriteLine($"\n{UiConstants.SectionTop}");
            Console. WriteLine("│ ЕТАП 2: Демонстрація поліморфізму через посилання       │");
            Console.WriteLine($"{UiConstants.SectionBottom}\n");

            // Створюємо гіперплощину для демонстрації
            Giperploschyna giper = new Giperploschyna(1, 2, 3, 4, 5);

            // Посилання базового класу на об'єкт похідного класу
            GeometricObject baseRef = giper; // Поліморфізм! 

            Console.WriteLine("📌 Посилання базового класу (GeometricObject) вказує на об'єкт Giperploschyna:");
            Console.WriteLine($"   GetObjectType() повертає: {baseRef.GetObjectType()}");
            Console. WriteLine($"   ToString() повертає: {baseRef}");
            Console.WriteLine($"   GetDimension() повертає: {baseRef.GetDimension()}D");
            Console.WriteLine($"   IsValid() повертає: {baseRef.IsValid()}");
        }

        /// <summary>
        /// Цикл перевірки точок з валідацією розмірності
        /// </summary>
        private static void CheckPointsLoop(GeometryManager manager)
        {
            Console.WriteLine($"\n{UiConstants.SectionTop}");
            Console.WriteLine("│ ЕТАП 3: Перевірка належності точок                      │");
            Console.WriteLine(UiConstants.SectionBottom);

            int pointCount = InputHelper.ReadInt("\nВведіть кількість точок для перевірки: ", 0);

            for (int i = 0; i < pointCount; i++)
            {
                Console.WriteLine($"\n{new string('─', 60)}");
                Console.WriteLine($"Точка #{i + 1}:");

                int dimension = InputHelper.ReadDimension("Розмірність точки (2 або 4): ");

                double[] point = InputHelper.ReadPoint(dimension);
                manager.CheckPointForAll(point);
            }
        }

        /// <summary>
        /// Демонстрація поліморфізму через масив базового типу
        /// </summary>
        private static void DemonstrateArrayPolymorphism(GeometryManager manager)
        {
            Console.WriteLine($"\n{UiConstants.SectionTop}");
            Console.WriteLine("│ ЕТАП 4: Додаткова демонстрація віртуальних методів      │");
            Console.WriteLine($"{UiConstants.SectionBottom}\n");

            // Масив посилань базового класу
            GeometricObject[] geometryArray = new GeometricObject[]
            {
                new Pryama(1, 2, 3),
                new Giperploschyna(1, 1, 1, 1, 1)
            };

            Console.WriteLine("📊 Використання масиву посилань базового класу:\n");

            for (int i = 0; i < geometryArray.Length; i++)
            {
                Console. WriteLine($"[{i + 1}] Об'єкт:");
                geometryArray[i].PrintInfo();

                Console.WriteLine($"    IsValid(): {geometryArray[i].IsValid()}");
                Console.WriteLine($"    GetDimension(): {geometryArray[i].GetDimension()}D");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Виведення статистики роботи програми
        /// </summary>
        private static void ShowStatistics(GeometryManager manager)
        {
            Console. WriteLine($"\n{UiConstants.BoxTop}");
            Console.WriteLine("║                      СТАТИСТИКА                           ║");
            Console.WriteLine(UiConstants.BoxBottom);
            Console.WriteLine($"Всього створено об'єктів: {manager.GetObjectCount()}");
            Console.WriteLine($"Використано віртуальних методів: 6+");
            Console.WriteLine($"Продемонстровано поліморфізм: ✓");
            Console.WriteLine($"Динамічне створення об'єктів: ✓");
        }
    }
}
