using System;

class MainClass
{
    public static int[] sortAsc(int[] arr)
    {
        int temp = 0;

        for (int write = 0; write < arr.Length; write++)
        {
            for (int sort = 0; sort < arr.Length - 1; sort++)
            {
                if (arr[sort] > arr[sort + 1])
                {
                    temp = arr[sort + 1];
                    arr[sort + 1] = arr[sort];
                    arr[sort] = temp;
                }
            }
        }

        return arr;
    }

    public static int[] ArrayChallenge(int[] arr)
    {
        int[] group0, group1;
        if (arr == null)
            throw new ArgumentNullException("arr");        

        if (arr.Length <= 1)
            return new int[1] { -1 };

        int totalSum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            int n = arr[i];
            if (n <= 0)
                throw new ArgumentException("arr is not allowed to contain values less than one.");
            totalSum += n;
        }
        if (totalSum % 2 != 0)
            return new int[1] { -1 };
        int desiredSum = totalSum / 2;

        //use the true and false value to identify the value group that satisfy the sum.
        bool[] groupPerNumber = new bool[arr.Length];

        if (!AnalyzeCombination(arr, groupPerNumber, 0, 0, desiredSum))
            return new int[1] { -1 };

        int groupSize0 = 0;
        for (int i = 0; i < groupPerNumber.Length; i++)
            if (!groupPerNumber[i])
                groupSize0++;
        group0 = new int[groupSize0];
        group1 = new int[arr.Length - groupSize0];

        int groupIndex0 = 0;
        int groupIndex1 = 0;
        for (int i = 0; i < arr.Length; i++)
            if (!groupPerNumber[i])
                group0[groupIndex0++] = arr[i];
            else
                group1[groupIndex1++] = arr[i];

        group0 = sortAsc(group0);
        group1 = sortAsc(group1);

        int[] result = new int[arr.Length];
        int c = 0;
        for (int k = 0; k < 2; k++)
        {            
            for (int i = 0; i < group0.Length; i++)
            {
                if (k == 0)
                {
                    result[c] = group0[i];
                    c++;
                }
                else
                {
                    result[c] = group1[i];
                    c++;
                }
            }
        }
        return result;
    }

    static public bool AnalyzeCombination(int[] numbers, bool[] groupPerNumber, int position, int runningSum, int desiredSum)
    {
        //use this recursive function to add subsequent numbers in the array to get the desired result.
        if (position == numbers.Length)
            return false;

        int currentNumber = numbers[position];
        runningSum += currentNumber;
        if (runningSum < desiredSum)
        {
            groupPerNumber[position] = true;
            if (AnalyzeCombination(numbers, groupPerNumber, position + 1, runningSum, desiredSum))
                return true;
            runningSum -= currentNumber;
            groupPerNumber[position] = false;
        }
        else if (runningSum == desiredSum)
        {
            groupPerNumber[position] = true;
            return true;
        }
        if (AnalyzeCombination(numbers, groupPerNumber, position + 1, runningSum, desiredSum))
            return true;

        return false;
    }

    public static string SecondArray(string[] strArr)
    {
        if (strArr == null || strArr.Length < 2) throw new ArgumentException("invalid input param 'straArr'.");

        string first = strArr[0];
        string dict = strArr[1];

        //split the dictionary into respective words.
        string[] splitedDict = new string[100];
        string word = "";
        int c = 0;
        for (int i = 0; i < dict.Length; i++)
        {
            if (dict[i] != ',')
            {
                word += dict[i];
            }
            else
            {
                splitedDict[c] = word;
                c++;
                word = "";
            }
            //check for last word.
            if (i == dict.Length - 1)
                splitedDict[c] = word;
        }

        //check the first index value with the dictionary.
        string[] foundWords = new string[100];
        c = 0;
        string r = "";
        for (int a = 0; a < first.Length; a++)
        {
            word = "";
            for (int i = a; i < first.Length; i++)
            {
                word += first[i];
                for (var j = 0; j < splitedDict.Length; j++)
                {
                    if (splitedDict[j] == word)
                    {
                        foundWords[c] = word;
                        r += word + ",";
                        c++;
                        break;
                    }
                }
            }
        }

        //select the 2 words that form the original string, with same length.
        string result = "";
        for (int i = 0; i < foundWords.Length; i++)
        {
            for (int j = i + 1; j < foundWords.Length; j++)
            {
                string temp = foundWords[i] + foundWords[j];
                if (temp == first)
                {
                    result = foundWords[i] + "," + foundWords[j];
                    break;
                }
            }

            if (result != "") break;
        }

        if (result == "") return "-1";

        return result;
    }

    static void Main()
    {        
        //Question 1
        var a1 = ArrayChallenge(new int[] { 1, 1, 1, 1, 1, 1 });
        Console.WriteLine("{0}", string.Join(",", a1));                
        var a2 = ArrayChallenge(new int[] { 1, 2, 3, 4 });
        Console.WriteLine("{0}", string.Join(",", a2));                
        var a3 = ArrayChallenge(new int[] { 4, 3, 2, 1 });
        Console.WriteLine("{0}", string.Join(",", a3));
        var a4 = ArrayChallenge(new int[] { 10, 15, 20, 5 });
        Console.WriteLine("{0}", string.Join(",", a4));
        var a5 = ArrayChallenge(new int[] { 5, 6 });
        Console.WriteLine("{0}", string.Join(",", a5));
        var a6 = ArrayChallenge(new int[] { 2, 1, 2, 1, 2, 1, 2, 1 });
        Console.WriteLine("{0}", string.Join(",", a6));
        var a7 = ArrayChallenge(new int[] { 2, 1, 2, 1, 2, 1, 2 });
        Console.WriteLine("{0}", string.Join(",", a7));


        //Question 2
        var r1 = SecondArray(new string[] { "baseball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z,zebra" });
        Console.WriteLine(r1);
        var r2 = SecondArray(new string[] { "ballbase", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" });
        Console.WriteLine(r2);
        var r3 = SecondArray(new string[] { "catball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" });
        Console.WriteLine(r3);
        var r4 = SecondArray(new string[] { "bb", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" });
        Console.WriteLine(r4);
        var r5 = SecondArray(new string[] { "ballball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" });
        Console.WriteLine(r5);
        var r6 = SecondArray(new string[] { "zall", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" });
        Console.WriteLine(r6);

        Console.ReadLine();
    }
}