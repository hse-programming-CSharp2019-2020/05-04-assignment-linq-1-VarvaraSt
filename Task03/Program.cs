﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке возрастания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    enum Manufacturer
    {
        Dell,
        Asus,
        Apple,
        Microsoft
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            int N = 0;
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();
            try
            {
                N = int.Parse(Console.ReadLine());
                char[] chr = { ' ' };
                for (int i = 0; i < N; i++)
                {
                    string[] str = Console.ReadLine().Split(chr, StringSplitOptions.RemoveEmptyEntries);
                    computerInfoList.Add(new ComputerInfo { Owner = str[0], Year = int.Parse(str[1]), ComputerManufacturer = (Manufacturer)int.Parse(str[2]) });
                }
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


            // выполните сортировку одним выражением
            var computerInfoQuery = from x in computerInfoList
                                    orderby x.Year descending
                                    orderby x.ComputerManufacturer.ToString()
                                    orderby x.Owner descending
                                    select x;

            PrintCollectionInOneLine(computerInfoQuery);

            Console.WriteLine();

            // выполните сортировку одним выражением
            var computerInfoMethods = computerInfoList.OrderByDescending(x => x.Owner).ThenBy(x => x.ComputerManufacturer.ToString()).ThenByDescending(x => x.Year);

            PrintCollectionInOneLine(computerInfoMethods);

            Console.ReadLine();
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection)
        {
            collection.ToList().ForEach(x => Console.WriteLine($"{x.Owner}: {x.ComputerManufacturer} [{x.Year}]"));
        }
    }


    class ComputerInfo
    {
        int year;
        Manufacturer manufacturer;
        public int Year
        {
            get { return year; }
            set { if (value < 1970 || value > 2020) throw new ArgumentException(); year = value; }
        }
        public string Owner { get; set; }
        public Manufacturer ComputerManufacturer
        {
            get => manufacturer;
            set { if (value < Manufacturer.Dell || value > Manufacturer.Microsoft) throw new ArgumentException(); manufacturer = value; }
        }

    }
}
