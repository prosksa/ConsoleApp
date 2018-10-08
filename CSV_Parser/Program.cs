using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Parser
{
	class Program
	{
		public static int[] maxSize;
		public static DateTime max, min;
		public static DateTime[,] period;

		static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("task2.csv");
			var sorted = lines.Select(line => new
			{
				SortKey = DateTime.Parse(line.Split(';')[1]),
				Line = line
			})
				  .OrderBy(x => x.SortKey)
				  .Select(x => x.Line);

			Values[] values = sorted.Select(v => Values.FromCsv(v)).ToArray();
			maxSize = new int[sorted.Count()];
			period = new DateTime[sorted.Count(), 2];

			int c = 0;
			int b = 1;
			for (int a = 0; a < values.Length; a++)
			{
				if (b < values.Length)
				{
					max = values[a].end;
					min = values[a].begin;
					Loop(a, b, c, values);
					b++;
					c++;
				}
				period[a, 0] = min;
				period[a, 1] = max;
			}

			int maxS = maxSize.Max();
			int i = Array.IndexOf(maxSize, maxS);

			Console.WriteLine($"В период с {period[i, 0]} по {period[i, 1]} был использован максимальный объем данных: {maxS}");
			Console.ReadKey();
		}

		static void Loop(int a, int b, int c, Values[] values)
		{
			if (b < values.Length)
			{

				if (values[b].begin < values[a].end || values[b].begin < max)
				{
					if (values[a].end > max)
					{
						max = values[a].end;
					}
					maxSize[c] = maxSize[c] + values[a].size;
					Loop(a + 1, b + 1, c, values);
				}
				else maxSize[c] = maxSize[c] + values[a].size;
			}
		}
	}

	class Values
	{
		public int id;
		public DateTime begin;
		public DateTime end;
		public int size;

		public static Values FromCsv(string csvLine)
		{
			string[] read = csvLine.Split(';');
			Values values = new Values
			{
				id = int.Parse(read[0]),
				begin = DateTime.Parse(read[1]),
				end = DateTime.Parse(read[2]),
				size = int.Parse(read[3])
			};
			return values;
		}
	}
}
