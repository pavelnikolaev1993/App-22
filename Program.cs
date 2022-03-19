//Сформировать массив случайных целых чисел (размер  задается пользователем). 
//Вычислить сумму чисел массива и максимальное число в массиве.  Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.
using System.Linq;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите длину массива");
        int n = Convert.ToInt32(Console.ReadLine());
        Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
        Task<int[]> task1 = new Task<int[]>(func1, n);

        Action<Task<int[]>> action1 = new Action<Task<int[]>>(GetSumm);
        Task task2 = task1.ContinueWith(action1);

        Action<Task<int[]>> action2 = new Action<Task<int[]>>(GetMax);
        Task task3 = task1.ContinueWith(action2);

        task1.Start();

        Console.ReadKey();


    }
    static int[] GetArray(object a)
    {
        int n = (int)a;
        int[] array = new int[n];
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            array[i] = random.Next(1, 10);
            Console.Write(array[i] + " ");
        }
        return array;

    }
    static void GetSumm(Task<int[]> task)
    {
        int[] array = task.Result;
        int summ = 0;
        for (int i = 0; i < array.Length; i++)
        {
            summ += array[i];

        }
        Console.WriteLine("\nСумма элементов массива: {0}", summ);
        return;

    }
    static void GetMax(Task<int[]> task)
    {
        int[] array = task.Result;
        int m = array.Max();
        Console.WriteLine("\nМаксимальный элемент массива: {0}", m);
        return;

    }
}