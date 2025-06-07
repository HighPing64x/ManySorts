using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class ShellSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("希尔排序（Shell Sort）是插入排序的一种更高效的改进版本。通过将整个待排序的记录序列分割成为若干子序列分别进行直接插入排序。\n时间复杂度O(n^1.3)~O(n^2)，空间复杂度O(1)。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Shell Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            ShellSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "shellsort_sorted.txt";
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

        // Shell Sort 实现
        private static void ShellSortArray(int[] array)
        {
            int n = array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;
                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
            }
        }
    }
}