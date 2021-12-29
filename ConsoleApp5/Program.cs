using System.Collections.Generic;
using System.IO;
using System;

class Queue
{
    private List<int> queue;
    public int N_op = 0;


    public Queue()
    {
        queue = new List<int>();
    }

    public int Count()
    {
        return queue.Count;
    }

    // Добавляем в конец
    public void Push(int newValue)
    {
        N_op++;
        queue.Add(newValue);
    }

    // Извлекаем из начала
    public int Pop()
    {
        int result = queue[0]; N_op += 2;
        queue.RemoveAt(0); N_op++;
        N_op++;
        return result;
    }

    // Показываем значение первого элемента
    public int Show()
    {
        N_op++;
        return queue[0];
    }

    // Получение элемента очереди по i-ой позиции
    public int Get(int pos)
    {
        N_op += 2;
        for (int i = 0; i < pos; ++i)
        {
            N_op++;
            Push(Pop());
            N_op += 4;
        }
        N_op++;
        int result = Show(); N_op++;
        N_op += 2;
        for (int i = pos; i < queue.Count; ++i)
        {
            N_op++;
            Push(Pop());
            N_op += 4;
        }

        N_op++;
        return result;
    }

    // Установка нового элемента на i-ую позицию
    public void Set(int pos, int newValue)
    {
        N_op += 2;
        for (int i = 0; i < pos; ++i)
        {
            N_op++;
            Push(Pop());
            N_op += 4;
        }


        queue[0] = newValue; N_op++;
        N_op += 2;
        for (int i = pos; i < queue.Count; ++i)
        {
            N_op++;
            Push(Pop());
            N_op += 4;
        }

    }

    // Отображение очереди в консоли
    public void Print()
    {
        N_op += 2;
        foreach (int item in queue)
        {
            N_op++;
            Console.WriteLine(item);
        }
        N_op++;
        Console.WriteLine("---------");
    }

    public static Queue Sort(Queue tmp)
    {
        tmp.N_op += 6;
        // int[] counter = new int[tmp.Count()+1];
        Queue counter = new Queue();
        for (int i = 0; i < tmp.Count() + 1; i++)
        {
            counter.Push(0);
            tmp.N_op += 5;
        }
        for (int i = 0; i < tmp.Count(); i++)
        {
            counter.Set(tmp.Get(i), counter.Get(tmp.Get(i)) + 1);
            tmp.N_op += 15;


        }
        int x = 0;

        for (int i = 0; i < tmp.Count() + 1; i++)
        {

            if (counter.Get(i) != 0)
            {
                tmp.N_op += 1;
                for (int j = 0; j < counter.Get(i); j++)
                {
                    tmp.Set(x, i);
                    x++; tmp.N_op++;//1
                    tmp.N_op += 15;
                }
            }
        }
        return tmp;

    }


}


class Program
{
    static void Main()
    {
        Queue tmp = new Queue();
        long t_s; long t_f;
        int[] p = new int[3000];
        int k = 300;

        var random = new Random();


        //Заполнение массива случайными числами                                         
        for (int i = 0; i < 3000; i++)
        {
            p[i] = random.Next(1, 300);
        }


        for (int i = 0; i < 10; i++)
        {

            for (int z = k - 300; z < k; z++)
            {
                tmp.Push(p[z]);

            }
            tmp.N_op = 0;
            t_s = DateTime.Now.Ticks;
            tmp = Queue.Sort(tmp);
            t_f = DateTime.Now.Ticks;

            Console.WriteLine("Номер сортировки: {0}  Колличество отсортированных элементов: {1} Время сортировки(ms): {2} Количество операций: {3}", i + 1, k, t_f - t_s, tmp.N_op);
            //tmp.Print();
            k += 300;
            Console.ReadLine();
        }
    }
}