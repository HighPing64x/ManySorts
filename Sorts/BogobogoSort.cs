using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManySorts.Sorts
{
    internal class BogobogoSort
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("BogobogoSort是一种故意设计得极其低效且荒谬的随机化排序算法。它属于“Bogo排序”（BogoSort）算法家族，该家族以最坏情况下的时间复杂度极高而闻名。\nBogobogoSort 由 David Morgan-Mar 创造，其名称和设计旨在讽刺低效算法和某些理论概念，常被用作计算机科学中关于算法效率、递归滥用和概率边界的教学示例或笑话。\n");

            int[] array = GetArrayFromUser();
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("未获取到有效数组，程序即将退出。");
                return;
            }

            Console.WriteLine("开始 Bogobogo Sort 排序...");
            Stopwatch sw = Stopwatch.StartNew();
            BogobogoSortArray(array);
            sw.Stop();

            Console.WriteLine("排序完成，用时: {0} ms", sw.ElapsedMilliseconds);

            string outputPath = "bogobogo_sorted.txt";
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
            Console.WriteLine("1. 从txt文件读取（每个数字用空格或换行分隔）");
            Console.WriteLine("2. 直接在命令行输入数字（用空格分隔）");
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
                Console.Write("请输入要排序的数字（用空格分隔）：");
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

        // 排序算法实现
        private static void BogobogoSortArray(int[] array)
        {
            BogobogoSortRecursive(array, array.Length);
        }

        private static bool IsSorted(int[] arr, int n)
        {
            for (int i = 1; i < n; i++)
                if (arr[i - 1] > arr[i])
                    return false;
            return true;
        }

        private static void BogobogoSortRecursive(int[] arr, int n)
        {
            if (n <= 1) return;

            while (true)
            {
                BogobogoSortRecursive(arr, n - 1);
                if (arr[n - 2] > arr[n - 1])
                {
                    Shuffle(arr, n);
                }
                else if (IsSorted(arr, n))
                {
                    break;
                }
                else
                {
                    Shuffle(arr, n);
                }
            }
        }

        private static void Shuffle(int[] arr, int n)
        {
            Random rand = new Random();
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
    }
}

