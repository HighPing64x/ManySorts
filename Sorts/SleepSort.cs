using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ManySorts.Sorts
{
    internal class SleepSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("睡眠排序（Sleep Sort）是一种极其低效且不实用的排序算法，仅用于娱乐和演示。\n它通过为每个数字创建一个线程，线程休眠对应时间后输出该数字。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Sleep Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            var sorted = SleepSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "sleepsort_sorted.txt";
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
            Console.WriteLine("1. 从txt文件读取（每个数字用空格或换行分隔，仅支持非负整数）");
            Console.WriteLine("2. 直接在命令行输入数字（用空格分隔，仅支持非负整数）");
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
                Console.Write("请输入要排序的数字（用空格分隔，仅支持非负整数）：");
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
                if (int.TryParse(token, out int num) && num >= 0)
                    list.Add(num);
            }
            return list.ToArray();
        }

        // Sleep Sort 实现
        private static int[] SleepSortArray(int[] array)
        {
            var result = new List<int>();
            var threads = new List<Thread>();
            var locker = new object();

            foreach (var num in array)
            {
                var n = num;
                var t = new Thread(() =>
                {
                    Thread.Sleep(n * 5); // 5ms/单位
                    lock (locker)
                    {
                        result.Add(n);
                    }
                });
                t.Start();
                threads.Add(t);
            }
            foreach (var t in threads)
                t.Join();
            result.Sort();
            return result.ToArray();
        }
    }
}