using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class IdiotSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("IdiotSort是HighPing自创的一种极其低效的排序算法。\n算法逻辑：先备份一次原数组，每轮循环将数组中每个项减一（只支持正整数），直到所有项都减为0。先减为0的数会排在最前面，后减为0的数排在后面。\n时间复杂度约为O(n * max)，空间复杂度O(n)。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            int[] backup = (int[])array.Clone();

            Console.WriteLine("开始 IdiotSort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            int[] sorted = IdiotSortArray(array, backup);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "idiotsort_sorted.txt";
            try
            {
                File.WriteAllText(outputPath, string.Join(" ", sorted));
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
            Console.WriteLine("1. 从txt文件读取（每个数字用空格或换行分隔，仅支持正整数）");
            Console.WriteLine("2. 直接在命令行输入数字（用空格分隔，仅支持正整数）");
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
                Console.Write("请输入要排序的数字（用空格分隔，仅支持正整数）：");
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
                if (int.TryParse(token, out int num) && num > 0)
                    list.Add(num);
            }
            return list.ToArray();
        }

        // IdiotSort 实现
        private static int[] IdiotSortArray(int[] array, int[] backup)
        {
            int n = array.Length;
            int[] zeroOrder = new int[n];
            int[] working = (int[])array.Clone();
            int finished = 0;
            int step = 0;
            bool[] done = new bool[n];

            while (finished < n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (!done[i])
                    {
                        working[i]--;
                        if (working[i] == 0)
                        {
                            zeroOrder[finished++] = i;
                            done[i] = true;
                        }
                    }
                }
                step++;
            }

            int[] result = new int[n];
            for (int i = 0; i < n; i++)
                result[i] = backup[zeroOrder[i]];
            return result;
        }
    }
}