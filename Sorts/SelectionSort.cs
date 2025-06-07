using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class SelectionSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("ѡ������Selection Sort����һ�ּ�ֱ�۵ıȽ������㷨��ÿ�δ�δ���򲿷�ѡ����С�������Ԫ�أ��ŵ�����������ĩβ��\nʱ�临�Ӷ�O(n^2)���ռ临�Ӷ�O(1)���ʺ�С��ģ��������\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("δ��ȡ����Ч���飬���򼴽��˳���");
                return;
            }

            Console.WriteLine("��ʼ Selection Sort ����...");
            Stopwatch sw = Stopwatch.StartNew();
            SelectionSortArray(array);
            sw.Stop();

            Console.WriteLine("������ɣ���ʱ: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "selectionsort_sorted.txt";
            try
            {
                File.WriteAllText(outputPath, string.Join(" ", array));
                Console.WriteLine($"�������ѱ��浽 {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"�����ļ�ʱ����: {ex.Message}");
            }

            Console.WriteLine("��������˳�...");
            Console.ReadKey();
        }

        private static int[] GetArrayFromUser()
        {
            Console.WriteLine("��ѡ�����뷽ʽ��");
            Console.WriteLine("1. ��txt�ļ���ȡ��ÿ�������ÿո���зָ�����֧��������");
            Console.WriteLine("2. ֱ�����������������֣��ÿո�ָ�����֧��������");
            Console.Write("������ѡ�1��2����");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("������txt�ļ�·�����磺array.txt����");
                string path = Console.ReadLine();
                try
                {
                    string content = File.ReadAllText(path);
                    return ParseArray(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"��ȡ�ļ�ʧ��: {ex.Message}");
                    return null;
                }
            }
            else if (option == "2")
            {
                Console.Write("������Ҫ��������֣��ÿո�ָ�����֧����������");
                string input = Console.ReadLine();
                return ParseArray(input);
            }
            else
            {
                Console.WriteLine("��Чѡ�");
                return null;
            }
        }

        private static int[] ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<int>();
            var tokens = input.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<int>();
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out int num))
                    list.Add(num);
            }
            return list.ToArray();
        }

        // Selection Sort ʵ��
        private static void SelectionSortArray(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIdx])
                        minIdx = j;
                }
                if (minIdx != i)
                    (array[i], array[minIdx]) = (array[minIdx], array[i]);
            }
        }
    }
}