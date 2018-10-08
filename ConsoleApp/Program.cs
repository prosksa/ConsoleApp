using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AVG_SD
{
	class Program
	{
		public static int min;
		public static int max;
		public static long ammount;
		static List<long> list = new List<long>();
		static Random random = new Random();

		static void Main(string[] args)
		{
			#region Get info from User
			Console.WriteLine("Please, enter minimum of number range (0-49999): ");
			min = int.Parse(Console.ReadLine());
			while (min < 0 || min >= 50000)
			{
				Console.WriteLine("Sorry, your number is out of range. Please, enter new number: ");
				min = int.Parse(Console.ReadLine());
			}

			Console.WriteLine("Please, enter maximum of number range (1-50000): ");
			max = int.Parse(Console.ReadLine());
			while (max < 1 || max > 50000)
			{
				Console.WriteLine("Sorry, your number is out of range. Please, enter new number: ");
				max = int.Parse(Console.ReadLine());
			}

			Console.WriteLine("Please, enter ammount of numbers to generate: ");
			ammount = long.Parse(Console.ReadLine());
			#endregion

			DateTime start = DateTime.Now;

			#region Generate Collection
			Task task = Task.Factory.StartNew(TaskMethod);
			Task.WaitAll(task);
			#endregion

			#region Avg and SD processing
			Console.WriteLine($"Average value: {AverageValue(list)}");
			Console.WriteLine($"Standard deviation: {StandardDeviation(list)}");
			#endregion

			TimeSpan timeSpent = DateTime.Now - start;
			Console.WriteLine($"Memory used: {GC.GetTotalMemory(true)}");
			Console.WriteLine($"Time spent: {timeSpent}");
			Console.ReadLine();
		}

		private static void TaskMethod()
		{
			for (long a = 0; a < ammount; a++)
			{
				list.Add(random.Next(min, max));
			}
		}

		private static long AverageValue(List<long> list)
		{
			return (long)list.Average();
		}

		private static long StandardDeviation(List<long> list)
		{
			long average = AverageValue(list);
			long sum = 0;
			foreach (long value in list)
			{
				sum += (value) * (value);
			}
			long sumAverage = sum / (list.Count - 1);
			return (long)Math.Sqrt(sumAverage - (average * average));
		}
	}
}
