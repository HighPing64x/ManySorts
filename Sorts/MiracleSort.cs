using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class MiracleSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("MiracleSort（奇迹排序）是一种“理想化”的排序算法，假设只要你相信它，数组就会瞬间变为有序。其核心思想是被动等待硬件或环境中的随机事件（如宇宙射线、硬件故障）意外将无序序列变为有序状态。\n本算法是算法设计中的极端虚无主义体现，仅为娱乐用途，无实际排序过程。\n💡冷知识: 若将 Miracle Sort 应用于包含10个元素的序列，其成功所需时间的数学期望远超宇宙热寂时间（10^100年），排序成功概率低于连续中1000次彩票头奖。\n时间复杂度O(1)，空间复杂度O(n)。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Miracle Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            int[] sorted = MiracleSortArray(array);
            sw.Stop();

            Console.WriteLine("排序居然成功了！你一定是世界上最幸运的人！用时: {0} ms！", sw.ElapsedMilliseconds);

            string outputPath = "miraclesort_sorted.txt";
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

        // MiracleSort 实现
        private static int[] MiracleSortArray(int[] array)
        {
            // 理论上永远不会排序成功
            while (true)
            {
                // 检查是否已排序
                bool sorted = true;
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        sorted = false;
                        break;
                    }
                }
                if (sorted)
                    return (int[])array.Clone(); // 奇迹发生，返回
                                                 // 继续等待奇迹！
            }
        }
    }
}