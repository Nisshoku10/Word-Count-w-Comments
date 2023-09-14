using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize Dictionary, with key (string) and value (int)
            // Creates a new Dictionary set using "new Dictionary<string, int>()"
            Dictionary<string, int> unsortedWordCount = new Dictionary<string, int>();
            Dictionary<string, int> sortedWordCount = new Dictionary<string, int>();

            // Initialize wordCount var for counting numbers in fileName 
            int wordCount = 0;

            // Creates a string of List of lines 
            List<string> lines = new List<string>();
            string[] words = new string[] { };

            // Declaration leastCount for sorting
            int leastCount = 0;
            
            // Declaration variable for sorting key in Dictionary
            string sortKey = "";

            // Declaration of a array of character letters
            char[] letters = { };

            // Declarion of file to be accessed
            // string fileName = "Jupiter.txt";
            string fileName = "Odin.txt";

            // To be used by streamwriter to print wordcount 
            string outputFile = "";

            // create output file name
            outputFile = fileName.Split('.')[0] + "_WordCount.txt";

            // Informs user that console is alreading reading/accessing the file
            Console.Write("Begin Reading...");

            // Declaration of line var
            string line = "";

            //uses StreamReader class to read string
            using (StreamReader sr = new StreamReader(fileName))
            {
                //uses while-loop to read lines in the file 
                // with condition line != to null or nothing inside 
                while ((line = sr.ReadLine()) != null)
                {
                    //sets condition that line length should be > 0
                    if (line.Length > 0)
                        //then it will proceed to read the file line by line
                        lines.Add(line);
                }
            }

            //Informs user that reading the file is successful
            Console.WriteLine("Done!");

            //Informs user that word counting process
            Console.Write("Begin Counting...");

            //declaration of var l since var line was already used
            //accesses the string l in List lines
            foreach (string l in lines)
            {
                //words get isolated 
                words = l.Split(' ');

                //then for loop will get word length
                for (int x = 0; x < words.Length; x++)
                //foreach(string word in words)
                {
                    //if word length > 0, proceed
                    if (words[x].Length > 0)
                    {
                        //if word length > 1, proceed
                        // special character filter
                        if (words[x].Length > 1)
                        {
                            //string words gets converted to char
                            letters = words[x].ToCharArray();

                            //if-conditional that will remove special characters 
                            if ((int)letters[letters.Length - 1] == 33 // !
                                || (int)letters[letters.Length - 1] == 44 // ,
                                || (int)letters[letters.Length - 1] == 46 // .
                                || (int)letters[letters.Length - 1] == 63 // ?
                                )
                            {
                                //that special character gets removed
                                words[x] = "";

                                //the letter length reduced by 1
                                //letters gets rebuild into the word
                                for (int y = 0; y < letters.Length - 1; y++)
                                {
                                    words[x] += letters[y];
                                }
                            }
                        }

                        // updates unsortedWordCount dictionary for the no. of 
                        // that words' intances
                        if (unsortedWordCount.ContainsKey(words[x].ToLower()))
                            unsortedWordCount[words[x].ToLower()] += 1;
                        else
                            unsortedWordCount[words[x].ToLower()] = 1;

                        wordCount++;
                    }
                }
            }

            Console.WriteLine("Done!");

            Console.Write("Begin Sorting...");

            //Iterates unsortedWordCount dictionary as long as it has elements 
            while (unsortedWordCount.Count > 0)
            {
                //Initialized to keep track of lowest frequency of 
                //a word
                leastCount = 0;

                //Compares the value (frequency) to the leastCount var
                //syntax for accessing dictionary
                foreach (KeyValuePair<string, int> kvp in unsortedWordCount)
                {
                    // if leastCount < kvp value (frequency)
      
                    if (leastCount < kvp.Value)
                    {
                    // it gets rewrited
                    // then assigns corresponding key 
                        leastCount = kvp.Value;
                        sortKey = kvp.Key;
                    }
                }

                //sortedKey is put in sortedWordCount dictionary
                sortedWordCount[sortKey] = leastCount;
                // then gets removed in the unsortedWordCount dictionary
                unsortedWordCount.Remove(sortKey);
            }
            Console.WriteLine("Done!");

            Console.Write("Begin Writing...");

            //Used to print result in the output txt file
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                //writing a line to indicate total word count of txt file
                sw.WriteLine($"Word count of {fileName}.");
                sw.WriteLine($"Total Wordcount is {wordCount}.");

                // foreach loop used to access keyValuePair in sortedWordCount Dictionary
                foreach (KeyValuePair<string, int> kvp in sortedWordCount)
                {
                    //Printing each line with format key (word) and value (frequency)
                    sw.WriteLine($"{kvp.Key} - {kvp.Value}");
                }
            }
            Console.WriteLine("Done!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}