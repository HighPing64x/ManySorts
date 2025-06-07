using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class CountingSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("计数排序（Counting Sort）是一种非比较型整数排序算法，适用于数据范围不大且为非负整数的场景。\n其核心思想是统计每个数出现的次数，然后按顺序输出。\n时间复杂度O(n+k)，空间复杂度O(k)。仅适合整数排序。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Counting Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            CountingSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "countingsort_sorted.txt";
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

        // Counting Sort 实现
        private static void CountingSortArray(int[] array)
        {
            if (array.Length == 0) return;

            int min = array[0], max = array[0];
            foreach (var num in array)
            {
                if (num < min) min = num;
                if (num > max) max = num;
            }

            int range = max - min + 1;
            int[] count = new int[range];

            foreach (var num in array)
                count[num - min]++;

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            int[] output = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            for (int i = 0; i < array.Length; i++)
                array[i] = output[i];
        }
    }
}