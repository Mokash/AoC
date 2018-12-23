using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;	

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Hello World");
		var url ="https://raw.githubusercontent.com/Mokash/C/master/input17";
	        var textFromFile = (new WebClient()).DownloadString(url).Trim();
			//Console.WriteLine(textFromFile);
			//Console.WriteLine(" part 1 : "+ PartOne(textFromFile));
		    Console.WriteLine(" part 2 : "+ PartTow(textFromFile));
	}
	  public static int PartOne(string input){
			return  Regex.Matches(Fill(input), "[~|]").Count;
		}
	 public static int PartTow(string input){
			return  Regex.Matches(Fill(input), "[~]").Count;
		}
	
	public static string Fill(string input) {
            var width = 2000; 
			var height = 2000;
            var mtx = new char[width, height];

            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    mtx[x, y] = '.';
                }
            }

            foreach (var line in input.Split('\n')) {
                var nums = Regex.Matches(line, @"\d+").Cast<Match>().Select(g => int.Parse(g.Value)).ToArray();
			    for (var i = nums[1]; i <= nums[2]; i++) {
                    if (line.StartsWith("x")) {
                        mtx[nums[0], i] = '#';
                    } else {
                        mtx[i, nums[0]] = '#';
                    }
                }
            }
            FillRecursive(mtx, 500, 0);

            var minY = int.MaxValue;
			var maxY = int.MinValue;
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    if (mtx[x, y] == '#') {
                        minY = Math.Min(minY, y);
                        maxY = Math.Max(maxY, y);
                    }
                }
            }
            var sb = new StringBuilder();
            for (var y = minY; y <= maxY; y++) {
                for (var x = 0; x < width; x++) {
                    sb.Append(mtx[x, y]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

       public static void FillRecursive(char[,] mtx, int x, int y) {
            var width = mtx.GetLength(0);
            var height = mtx.GetLength(1);
            if (mtx[x, y] != '.') {
                return;
            }
            mtx[x, y] = '|';
            if (y == height - 1) {
                return ;
            }
            FillRecursive(mtx, x, y + 1);

            if (mtx[x, y + 1] == '#' || mtx[x, y + 1] == '~') {
                if (x > 0) {
                    FillRecursive(mtx, x - 1, y);
                }
                if (x < width - 1) {
                    FillRecursive(mtx, x + 1, y);
                }
            }

            if (IsStill(mtx, x, y)) {
                foreach (var dx in new[] { -1, 1 }) {
                    for (var xT = x; xT >= 0 && xT < width && mtx[xT, y] == '|'; xT += dx) {
                        mtx[xT, y] = '~';
                    }
                }
            }
        }

       public static bool IsStill(char[,] mtx, int x, int y) {
            var width = mtx.GetLength(0);
            foreach (var dx in new[] { -1, 1 }) {
                for (var xT = x; xT >= 0 && xT < width && mtx[xT, y] != '#'; xT += dx) {
                    if (mtx[xT, y] == '.' || mtx[xT, y + 1] == '|') {
                        return false;
                    }
                }
            }
            return true;
        }
}
