using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ManySorts.Sorts
{
    internal class BucketSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("桶排序（Bucket Sort）是一种基于分布的排序算法，适用于数据均匀分布的场景。\n它将元素分到若干个桶中，每个桶内再单独排序，最后合并所有桶得到有序序列。\n时间复杂度平均为O(n+k)，空间复杂度O(n+k)。适合大量、分布均匀的浮点数排序。\n");

            double[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Bucket Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            BucketSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "bucketsort_sorted.txt";
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

        private static double[] GetArrayFromUser()
        {
            Console.WriteLine("请选择输入方式：");
            Console.WriteLine("1. 从txt文件读取（每个数字用空格或换行分隔，支持小数）");
            Console.WriteLine("2. 直接在命令行输入数字（用空格分隔，支持小数）");
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
                Console.Write("请输入要排序的数字（用空格分隔，支持小数）：");
                string input = Console.ReadLine();
                return ParseArray(input);
            }
            else
            {
                Console.WriteLine("无效选项。");
                return null;
            }
        }

        private static double[] ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<double>();
            var tokens = input.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<double>();
            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double num))
                    list.Add(num);
            }
            return list.ToArray();
        }

        // Bucket Sort 实现
        private static void BucketSortArray(double[] array)
        {
            if (array.Length == 0) return;

            double minValue = array[0], maxValue = array[0];
            foreach (var num in array)
            {
                if (num < minValue) minValue = num;
                if (num > maxValue) maxValue = num;
            }

            int bucketCount = Math.Max(1, array.Length / 2);
            var buckets = new List<double>[bucketCount];
            for (int i = 0; i < bucketCount; i++)
                buckets[i] = new List<double>();

            foreach (var num in array)
            {
                int bucketIndex = (int)((num - minValue) / (maxValue - minValue + 1e-9) * (bucketCount - 1));
                buckets[bucketIndex].Add(num);
            }

            int idx = 0;
            foreach (var bucket in buckets)
            {
                bucket.Sort();
                foreach (var num in bucket)
                    array[idx++] = num;
            }
        }
    }
}