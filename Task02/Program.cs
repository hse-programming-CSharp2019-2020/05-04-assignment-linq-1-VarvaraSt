using System;
using System.Collections.Generic;
using System.Linq;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 */
namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTesk02();
            Console.ReadLine();
        }

        public static void RunTesk02()
        {
            int[] arr = { };
            try
            {
                char[] chrs = { ' ' };
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = Console.ReadLine().Split(chrs, StringSplitOptions.RemoveEmptyEntries).Select(str => int.Parse(str)).ToArray();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
            }


            var filteredCollection = arr.Where(x => (Array.IndexOf(arr, 0) == -1 || Array.IndexOf(arr, x) < Array.IndexOf(arr, 0)));

            try
            {
                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = Enumerable.Average<int>(filteredCollection, x => x * x);
                Console.WriteLine($"{averageUsingStaticForm:F3}");
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = filteredCollection.Aggregate(0, (total, next) => total + next*next)/(double)filteredCollection.ToList().Count();
                Console.WriteLine($"{averageUsingInstanceForm:F3}");

                // вывести элементы коллекции в одну строку
                filteredCollection.ToList().ForEach(x => Console.Write(x + " "));
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }

        }
        
    }
}
