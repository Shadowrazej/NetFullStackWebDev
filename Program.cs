using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            string path = args[0];

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.Peek() != -1)
                    list.Add(reader.ReadLine());
                reader.Close();
            }
            string textToLower = list[0].ToLower();
            string[] words = textToLower.Split(new char[] { ' ', '.', ',', ':' }, StringSplitOptions.RemoveEmptyEntries); //can add different symbols

            using (StreamWriter writer = new StreamWriter(args[1]))
            {
                var query = words
                    .Select(t => t)
                    .OrderBy(t => t)
                    .GroupBy(t => t[0])
                    .Select(g => new
                    {
                        Key = g.Key,
                        data = g.GroupBy(p => p).Select(c => new
                        {
                            word = c.Key,
                            count = c.Count()
                        }).OrderByDescending(c => c.count)
                    });

                foreach (var text in query)
                {
                    writer.WriteLine(text.Key);
                    foreach (var word in text.data)
                    {
                        writer.WriteLine("{0} {1}", word.word, word.count);
                    }
                }
            }            
        }
    }
}
