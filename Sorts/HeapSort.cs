using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class HeapSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("堆排序（Heap Sort）是一种基于堆数据结构的比较排序算法。\n它通过构建最大堆（或最小堆），不断取出堆顶元素实现排序。\n时间复杂度O(n log n)，空间复杂度O(1)。适合大多数整数排序场景。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Heap Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            HeapSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "heapsort_sorted.txt";
            try
            {
                File.WriteAllText(outputPath, string.Join(" ", array));
                Console.WriteLine($"排序结果已保存到 {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存文件时出错: {ex.Message}");
            }

            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        private static int[] GetArrayFromUser()
        {
            Console.WriteLine("请选择输入方式：");
            Console.WriteLine("1. 从txt文件读取（每个数字用空格或换行分隔，仅支持整数）");
            Console.WriteLine("2. 直接在命令行输入数字（用空格分隔，仅支持整数）");
            Console.Write("请输入选项（1或2）：");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("请输入txt文件路径（如：array.txt）：");
                string path = Console.ReadLine();
                try
                {
                    string content = File.ReadAllText(path);
                    return ParseArray(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"读取文件失败: {ex.Message}");
                    return null;
                }
            }
            else if (option == "2")
            {
                Console.Write("请输入要排序的数字（用空格分隔，仅支持整数）：");
                string input = Console.ReadLine();
                return ParseArray(input);
            }
            else
            {
                Console.WriteLine("无效选项。");
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

        // Heap Sort 实现
        private static void HeapSortArray(int[] array)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] arr, int heapSize, int root)
        {
            int largest = root;
            int left = 2 * root + 1;
            int right = 2 * root + 2;

            if (left < heapSize && arr[left] > arr[largest])
                largest = left;
            if (right < heapSize && arr[right] > arr[largest])
                largest = right;

            if (largest != root)
            {
                (arr[root], arr[largest]) = (arr[largest], arr[root]);
                Heapify(arr, heapSize, largest);
            }
        }
    }
}