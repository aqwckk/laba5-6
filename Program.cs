using System;
using System.Text.RegularExpressions;
class Program
{
    #region Вспомогательные функции
    static void PressToContinue()
    {
        Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
        Console.ReadKey();
    }
    static Random rnd = new Random(); // Генератор случайных чисел
    static int IsCorrectNum(string text, int min = int.MinValue, int max = int.MaxValue) // Функция для проверки правильности ввода
    {
        int num = 0;
        bool isConvert = false;
        do
        {
            Console.Write(text);
            isConvert = int.TryParse(Console.ReadLine(), out num);
            if (!isConvert)
            {
                Console.WriteLine("\nОшибка ввода! Введите целое число\n");
            }
            else if (num < min || num > max)
            {
                Console.WriteLine($"\nВаше число выходит за рамки диапазона! Введите число в диапозоне от {min} до {max}\n");
                isConvert = false;
            }
        } while (!isConvert);
        return num;
    }
    static void PrintArray(int[][] ragArray) // Функция для печати двумерного массива
    {
        Console.Clear();
        if (IsRagArrayEmpty(ragArray))// Сообщение, если массив пуст 
        {
            Console.WriteLine("Рваный массив пустой!");
        }
        else
        {
            Console.WriteLine("Рваный массив: ");
            for (int i = 0; i < ragArray.Length; i++)
            {
                for (int j = 0; j < ragArray[i].Length; j++)
                {
                    Console.Write($"{ragArray[i][j],-5}");
                }
                Console.WriteLine();
            }
        }
        PressToContinue();
    }
    static void PrintArray(int[,] matrix)// Функция для печати рваного массива
    {
        Console.Clear();
        if (IsMatrixEmpty(matrix))// Сообщение, если массив пуст
        {
            Console.WriteLine("Двумерный массив пустой!");
        }
        else
        {
            Console.WriteLine("Двумерный массив:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        PressToContinue();
    }
    static bool IsMatrixEmpty(int[,] matrix) => matrix is null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0; // Проверка на пустоту
    static bool IsRagArrayEmpty(int[][] array) => array is null || array.Length == 0; // Проверка на пустоту
    #endregion

    #region Главное меню
    static void Main(string[] args)
    {
        bool exit = true;

        do
        {
            Console.Clear();
            Console.WriteLine("1. Работа с двумерными массивами");
            Console.WriteLine("2. Работа с рваными массивами");
            Console.WriteLine("3. Работа со строками");
            Console.WriteLine("Нажмите ESC для выхода\n");
            Console.Write("Выберите пункт меню: ");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    IsMenuMatrix();
                    break;
                case ConsoleKey.D2:
                    IsMenuRagArray();
                    break;
                case ConsoleKey.D3:
                    IsMenuString();
                    break;
                case ConsoleKey.Escape:
                    exit = false;
                    break;
                default:
                    Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                    PressToContinue();
                    break;
            }
        } while (exit);
    }
    #endregion

    #region Двумерные массивы
    static void IsMenuMatrix() // Меню для двумерных массивов
    {
        int[,] matrix = null;
        bool exit = true;

        do
        {
            Console.Clear();
            Console.WriteLine("Меню двумерного массива\n");
            Console.WriteLine("1. Создать двумерный массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Добавить столбец в начало");
            Console.WriteLine("Нажмите ESC для выхода в главное меню\n");
            Console.Write("Выберите пункт меню: ");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    matrix = ChooseWayCreateMatrix();
                    break;
                case ConsoleKey.D2:
                    PrintArray(matrix);
                    break;
                case ConsoleKey.D3:
                    matrix = AddColumnToMatrix(matrix);
                    break;
                case ConsoleKey.Escape:
                    exit = false;
                    break;
                default:
                    Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                    PressToContinue();
                    break;
            }
        } while (exit);
    }
    static int[,] ChooseWayCreateMatrix() // Способ заполнения двумерного массива
    {
        Console.Clear();
        Console.WriteLine("Создание двумерного массива:\n");

        int rows = IsCorrectNum("Введите количество строк: ", 1, 15);
        int cols = IsCorrectNum("Введите количество столбцов: ", 1, 15);
        int[,] matrix = null;

        Console.WriteLine("\nВыберите метод заполнения: ");
        Console.WriteLine("1. Заполнить массив ДСЧ");
        Console.WriteLine("2. Заполнить массив вручную");
        switch (Console.ReadKey(intercept: true).Key)
        {
            case ConsoleKey.D1:
                matrix = CreateRandomMatrix(rows, cols);
                break;
            case ConsoleKey.D2:
                matrix = CreateMatrix(rows, cols);
                break;
            default:
                Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                PressToContinue();
                break;
        }
        return matrix;
    }
    static int[,] CreateMatrix(int rows, int cols) // Создания двумерного массива вручную
    {
        Console.Clear();
        Console.WriteLine("Создание двумерного массива вручную: \n");

        int[,] matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = IsCorrectNum($"Введите элемент [{i + 1}, {j + 1}]: ", -100, 100);
            }
        }

        Console.WriteLine("Двумерный массив создан!");
        PrintArray(matrix);
        return matrix;
    }
    static int[,] CreateRandomMatrix(int rows, int cols) // Создание двумерного массива ДСЧ
    {
        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rnd.Next(-100, 100);
            }
        }

        Console.WriteLine("Двумерный массив создан!");
        PrintArray(matrix);
        return matrix;
    }
    static int[,] AddColumnToMatrix(int[,] matrix) // Добавление столбца в начало двумерного массива
    {
        int[,] newMatrix;
        Console.Clear();       

        if (IsMatrixEmpty(matrix))
        {
            Console.WriteLine("Ваш массив пуст. Создадим массив из одного столбца.\n");
            int rows = IsCorrectNum("Введите количество строк: ", 1, 15);

            newMatrix = new int[rows, 1];

            Console.WriteLine("\nВыберите метод заполнения:");
            Console.WriteLine("1. Заполнить столбец вручную");
            Console.WriteLine("2. Заполнить столбец случайными числами\n");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    for (int i = 0; i < rows; i++)
                    {
                        newMatrix[i, 0] = IsCorrectNum($"Введите элемент для строки {i + 1}: ", -100, 100);
                    }
                    break;
                case ConsoleKey.D2:
                    Random random = new Random();
                    for (int i = 0; i < rows; i++)
                    {
                        newMatrix[i, 0] = random.Next(-100, 100);
                    }
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Массив не изменен.");
                    return matrix;
            }
        }
        else
        {
            if (matrix.GetLength(1) == 15)
            {
                Console.WriteLine("Матрица имеет максимально возможное количество столбцов. Добавление невозможно!");
                PressToContinue();
                return matrix;
            }

            newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) + 1];

            Console.WriteLine("Выберите метод заполнения нового столбца:\n");
            Console.WriteLine("1. Заполнить столбец вручную");
            Console.WriteLine("2. Заполнить столбец случайными числами\n");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    for (int i = 0; i < newMatrix.GetLength(0); i++)
                    {
                        newMatrix[i, 0] = IsCorrectNum($"Введите элемент для строки {i + 1}: ", -100, 100);
                    }
                    break;
                case ConsoleKey.D2:
                    Random random = new Random();
                    for (int i = 0; i < newMatrix.GetLength(0); i++)
                    {
                        newMatrix[i, 0] = random.Next(-100, 100);
                    }
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Массив не изменен.");
                    return matrix;
            }

            for (int i = 0; i < matrix.GetLength(0); i++) // Переносим старые значения массива
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j + 1] = matrix[i, j];
                }
            }
        }

        Console.WriteLine("Столбец успешно добавлен.");
        PrintArray(newMatrix);
        return newMatrix;
    }
    #endregion

    #region Рваные массивы
    static void IsMenuRagArray() // Меню для рваных массивов
    {
        Console.Clear();

        int[][] ragArray = null;
        bool exit = true;

        do
        {
            Console.Clear();
            Console.WriteLine("Меню рваного массива\n");
            Console.WriteLine("1. Создать рваный массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Выполнить задание");
            Console.WriteLine("Нажмите ESC для выхода\n");
            Console.Write("Выберите пункт меню: ");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    ragArray = ChooseWayCreateRagArray();
                    break;
                case ConsoleKey.D2:
                    PrintArray(ragArray);
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    if (IsRagArrayEmpty(ragArray))
                    {
                        Console.WriteLine("Массив пуст. Удаление невозможно!");
                        PressToContinue();
                    }
                    else
                    {
                        Console.WriteLine("Введите диапозон строк для удаления.\n");
                        int k1 = IsCorrectNum("Введите начало диапазона K1: ", 1, ragArray.Length) - 1;
                        int k2 = IsCorrectNum("Введите начало диапазона K2: ", 1, ragArray.Length) - 1;
                        ragArray = DeleteRows(ragArray, k1, k2);
                    }
                    break;
                case ConsoleKey.Escape:
                    exit = false;
                    break;
                default:
                    Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                    PressToContinue();
                    break;
            }
        } while (exit);
    }
    static int[][] ChooseWayCreateRagArray() // Способ заполнения рваного массива
    {
        Console.Clear();
        Console.WriteLine("Создание рваного массива\n");

        int rows = IsCorrectNum("Введите количество строк: ", 1, 15);

        int[][] ragArray = new int[rows][];

        Console.WriteLine("\nВыберите метод заполнения: \n");
        Console.WriteLine("1. Заполнить массив ДСЧ");
        Console.WriteLine("2. Заполнить массив с клавиатуры");
        Console.Write("Выберите способ заполнения: \n");

        switch (Console.ReadKey(intercept: true).Key)
        {
            case ConsoleKey.D1:
                ragArray = CreateRandomRagArray(rows);
                break;
            case ConsoleKey.D2:
                ragArray = CreateRagArray(rows);
                break;
            default:
                Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                PressToContinue();
                break;
        }
        return ragArray;
    }
    static int[][] CreateRagArray(int rows) // Создание рваного массива вручную
    {
        Console.Clear();
        Console.WriteLine("Создание рваного массива вручную\n");

        int[][] ragArray = new int[rows][];

        for (int i = 0; i < ragArray.Length; i++)
            ragArray[i] = new int[IsCorrectNum($"Введите количество элементов в {i + 1} строке: ", 1, 15)];

        for (int i = 0; i < ragArray.Length; i++)
            for (int j = 0; j < ragArray[i].Length; j++)
                ragArray[i][j] = IsCorrectNum($"Введите элемент [{i + 1}, {j + 1}]: ", -100, 100);

        Console.WriteLine("Рваный массив создан!");
        Console.WriteLine("Созданный массив:");
        PrintArray(ragArray);
        return ragArray;
    }
    static int[][] CreateRandomRagArray(int rows) // Создание рваного массива ДСЧ
    {
        int[][] ragArray = new int[rows][];
        for (int i = 0; i < ragArray.Length; i++)
            ragArray[i] = new int[rnd.Next(1, 10)];
        for (int i = 0; i < ragArray.Length; i++)
            for (int j = 0; j < ragArray[i].Length; j++)
                ragArray[i][j] = rnd.Next(-100, 100);
        Console.WriteLine("Рваный массив создан!");
        Console.WriteLine("Созданный массив:");
        PrintArray(ragArray);
        return ragArray;
    }
    static int[][] DeleteRows(int[][] ragArray, int k1, int k2) // Удаление строк из рваного массива
    {
        if (k1 < 0 || k1 > k2 || k2 >= ragArray.Length)
        {
            Console.WriteLine($"Некорректный диапазон: K1 = {k1 + 1}, K2 = {k2 + 1}. Удаление невозможно.");
            PressToContinue();
            return ragArray;
        }

        int countRows = ragArray.Length - (k2 - k1 + 1);
        int[][] newRagArray = new int[countRows][];
        int index = 0;

        for (int i = 0; i < ragArray.Length; i++)
        {
            if (i < k1 || i > k2)
            {
                newRagArray[index] = ragArray[i];
                index++;
            }
        }

        Console.WriteLine("Удаление завершено. Результирующий массив:");
        PrintArray(newRagArray);
        return newRagArray;
    }
    #endregion

    #region Строки
    static void IsMenuString() // Меню строк
    {
        Console.Clear();

        string line = "";
        bool exit = true;

        do
        {
            Console.Clear();
            Console.WriteLine("Меню работы со строками:\n");
            Console.WriteLine("1. Ввести строку");
            Console.WriteLine("2. Напечатать строку");
            Console.WriteLine("3. Поиск ключевых слов");
            Console.WriteLine("Нажмите ESC для выхода\n");
            Console.Write("Выберите пункт меню: ");

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    line = ChooseWayCreateString();
                    break;
                case ConsoleKey.D2:
                    PrintLine(line);
                    break;
                case ConsoleKey.D3:
                    SearchKeyWords(line);
                    break;
                case ConsoleKey.Escape:
                    exit = false;
                    break;
                default:
                    Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                    PressToContinue();
                    break;
            }
        } while (exit);
    }
    static string ChooseWayCreateString() // Способ ввода строк
    {
        string str = "";
        Console.Clear();
        Console.WriteLine("Выбор ввода строки:\n");
        Console.WriteLine("1. Ввести строку вручную");
        Console.WriteLine("2. Выбрать строку из предложенных");
        Console.WriteLine("Нажмите ESC для выхода\n");
        Console.Write("Выберите пункт меню: ");

        switch (Console.ReadKey(intercept: true).Key)
        {
            case ConsoleKey.D1:
                str = CreateString("Введите строку: ");
                break;
            case ConsoleKey.D2:
                str = ChooseString("Выберите строку: ");
                break;
            case ConsoleKey.Escape:
                break;
            default:
                Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                PressToContinue();
                break;
        }
        return str;
    }
    static string CreateString(string text) // Создание строк вручную
    {
        Console.Clear();

        int maxLengthStr = 150;
        string line = "";
        bool isValid = false;

        string Repeat = @"[\.\,\;\:%!?<>]{2,}";
        string validCharsPattern = @"^[а-яА-Яa-zA-Z0-9=\.\-\+\*\,\;\:%!?<> ]*$";
        string startsWithPuncPattern = @"^[\s\.\,\+\*\;\:%!?<>:]+";
        

        while (!isValid)
        {
            Console.Write(text);

            line = Console.ReadLine();
            line = line.Trim(); // Убираем лишние пробелы с начала и конца

            bool hasValidChars = Regex.IsMatch(line, validCharsPattern);
            bool endsCorrectly = line.EndsWith('.') || line.EndsWith('!') || line.EndsWith('?');
            bool NotStartPunc = !Regex.IsMatch(line, startsWithPuncPattern);
            bool isSecondLastCharLetter = line.Length > 1 && Regex.IsMatch(line.Substring(line.Length - 2, 1), @"[a-zA-Zа-яА-Я0-9]");
            bool isRepeat = !Regex.IsMatch(line, Repeat);

            isValid = endsCorrectly && hasValidChars && NotStartPunc && isSecondLastCharLetter && isRepeat;

            if (!isValid)
            {
                Console.WriteLine($"\nСтрока не соответствует требованиям!\n");
            }
            if (line.Length > maxLengthStr)
            { 
                Console.WriteLine($"\nМаксимальная длина строки - 150 символов\n");
                isValid = false;
            }
        }
         
        Console.WriteLine("\nСтрока успешно введена!");
        PressToContinue();
        return line;
    }
    static string ChooseString(string text) // Выбор строки
    {
        Console.Clear();

        string line = "";
        string[] strVar = ["static void PrintUpper string info12346: WriteLine ToUpper info, 1234info. if x>0 then sign=1; else if x<0 sign=-1; else sign=0.",
            "for int i = 0; i < 10.",
            "public class MyClass private int value; public MyClass value = 10."];

        Console.WriteLine("Предложенные строки: \n");

        for (int i = 0; i < strVar.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {strVar[i]}");
        }

        Console.WriteLine();

        int choice = IsCorrectNum(text, 1, 3);

        if (choice >= 1 && choice <= strVar.Length)
        {
            line = strVar[choice - 1];
            Console.WriteLine("\nСтрока выбрана!");
            PressToContinue();
        }
        return line;
    }
    static void PrintLine(string line) // Печать строки 
    {
        Console.Clear();

        if (string.IsNullOrEmpty(line))
        {
            Console.WriteLine("Строка пустая!");
            PressToContinue();
        }
        else
        {
            Console.Write("Строка выглядит так: ");
            Console.WriteLine(line);
            PressToContinue();
        }
    }
    static void SearchKeyWords(string line) // Поиск ключевых строк
    {
        Console.Clear();

        bool found = false;

        if (string.IsNullOrWhiteSpace(line))
        {
            Console.WriteLine("Строка пуста. Введите строку.");
            PressToContinue();
            return;
        }

        string[] keywords = {"abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue",
    "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally",
    "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long",
    "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly",
    "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "struct", "switch", "this", "throw",
    "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"}; //Массив ключевых слов

        foreach (string keyword in keywords)
        {
            string pattern = $@"\b{keyword}\b"; // \b - граница слова, просто решил использовать регулярные выражения тут
            MatchCollection matches = Regex.Matches(line, pattern); // Regex.Matches ищет все вхождения этого ключевого слова                                                                                            
            if (matches.Count > 0)
            {
                Console.WriteLine($"{keyword} – {matches.Count}"); // Вывод
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Ключевых слов в строке нет.");
        }

        Console.WriteLine("\nОбработка завершена.");
        PressToContinue();
    }
    #endregion
}

