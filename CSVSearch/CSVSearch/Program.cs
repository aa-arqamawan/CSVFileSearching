using System;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CSVSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            string path;
            int index;
            string value;
            #endregion
            Console.Write("Enter csv file path, index, name: ");
            string[] input = Console.ReadLine().Split(' ');

            path = input[0].ToString();
            index = int.Parse(input[1]);
            value = input[2].ToString();

            searchRecord(path, index,value);
            Console.ReadKey();
        }


        public static void searchRecord(string filePath, int index, string searchValue)
        {
            try
            {
                using (var streamReader = new StreamReader(@filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<AssessmentMap>();
                        var records = csvReader.GetRecords<Assessment>().ToList();
                        string[] data = new string[records.Count];
                        for (int i = 0; i < records.Count; i++)
                        {
                            data[i] = records[i].id + "," + records[i].firstname + "," + records[i].lastname + "," + records[i].date;
                        }

                        string findByIndex = data[index-1];
                        if (findByIndex != "" || findByIndex.Trim() != string.Empty.Trim())
                        {
                            if (findByIndex.Contains(searchValue))
                            {
                                Console.WriteLine("Output: {0}", findByIndex);
                            }
                            else
                            {
                                Console.WriteLine("Error: Value cannot find..");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Index out of range..");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            

        }
    }

   
    public class AssessmentMap : ClassMap<Assessment>
    {
        public AssessmentMap()
        {
            Map(m => m.id).Name("id");
            Map(m => m.firstname).Name("firstname");
            Map(m => m.lastname).Name("lastname");
            Map(m => m.date).Name("date");
        }
    }
    public class Assessment
    {
        [Name("id")]
        public string id { get; set; }
        [Name("firstname")]
        public string firstname { get; set; }
        [Name("lastname")]
        public string lastname { get; set; }
        [Name("date")]
        public string date { get; set; }
    }
}

