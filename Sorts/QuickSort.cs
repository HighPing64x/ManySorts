using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class QuickSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("快速排序（Quick Sort）是一种分治法的高效排序算法。通过选取基准元素，将数组分为两部分递归排序。\n平均时间复杂度O(n log n)，空间复杂度O(log n)。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Quick Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            QuickSortArray(array, 0, array.Length - 1);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "quicksort_sorted.txt";
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

        // Quick Sort 实现
        private static void QuickSortArray(int[] array, int left, int right)
        {
            if (left >= right) return;
            int pivot = array[(left + right) / 2];
            int i = left, j = right;
            while (i <= j)
            {
                while (array[i] < pivot) i++;
                while (array[j] > pivot) j--;
                if (i <= j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    i++; j--;
                }
            }
            if (left < j) QuickSortArray(array, left, j);
            if (i < right) QuickSortArray(array, i, right);
        }
    }
}