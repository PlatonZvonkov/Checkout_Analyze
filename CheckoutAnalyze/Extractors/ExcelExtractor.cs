using System.IO;
using System.Collections.Generic;
using ExcelDataReader;
using System;
using CheckoutAnalyze.Report;

namespace CheckoutAnalyze.Extractors
{
    public class ExcelExtractor
    {
        /**
        * reading text by column from top to bottom
        */
        static List<ReportModel> reports;
        public static List<ReportModel> ExtractXLStext(string path)
        {
            string resultText = null;
            int numberOfGoods = 1;
            reports = new List<ReportModel>();
            //add text to 2d "array".
            List<List<string>> columns = new List<List<string>>();
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(File.OpenRead(path)))
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (i <= columns.Count)
                            columns.Add(new List<string>());
                        if (reader.GetValue(i) != null)
                            columns[i].Add(reader.GetValue(i).ToString());
                    }
                }
            }

            // Read by columns.
            foreach (List<string> column in columns)
            {
                foreach (var cell in column)
                {
                    if (cell != null)
                    {
                        resultText += cell.ToString() + " ";
                    }
                    if (cell == "№")
                    {
                        numberOfGoods = NumberOfTableRows(column);
                       
                        int i = 0;
                        while (i < numberOfGoods)
                        {
                            reports.Add(new ReportModel());
                            i++;
                        }
                       break;
                    }
                    if (cell.Trim() == "Товары (работы, услуги)" || cell.Trim() == "Наименование"|| cell.Trim() == "Товар")
                    {
                        try
                        {                            
                            List<string> temp = column.GetRange(column.IndexOf(cell)+1, numberOfGoods);
                            SetItemNames(temp);
                            break;
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException(e.Message);
                        }
                    }
                    if (cell.Trim() == "Кол-во"|| cell.Trim() == "Количество")
                    {
                        try
                        {
                            List<string> temp = column.GetRange(column.IndexOf(cell) + 1, numberOfGoods);
                            SetItemCount(temp);
                            break;
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException(e.Message);
                        }
                    }
                    if (cell.Trim() == "Цена"||cell.Trim().ToLower() == "цена (руб)")
                    {
                        try
                        {
                            List<string> temp = column.GetRange(column.IndexOf(cell) + 1, numberOfGoods);
                            SetItemPrices(temp);
                            break;
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException(e.Message);
                        }
                    }
                    if (cell.Trim() == "Ед."|| cell.Contains("Ед.\nизм.")|| cell.Trim() == "Eд. изм.")
                    {
                        try
                        {
                            List<string> temp = column.GetRange(column.IndexOf(cell) + 1, numberOfGoods);
                            SetItemMeasureUnit(temp);
                            break;
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException(e.Message);
                        }                        
                    }
                }
            }
            FillINN(resultText);
            return reports;
        }

        /**
        * Find how many rows of data inside 
        */
        public static int NumberOfTableRows(List<string> column)
        {
            int result = 0;

            for (int i = column.IndexOf("№") + 1; i < column.Count; i++)
            {
                if (Int32.TryParse(column[i], out _) == true)
                {
                    result++;
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        /**
        * Fill Names
        */
        private static void SetItemNames(List<string> column)
        {
            int i = 0;
            foreach (var item in reports)
            {
                item.Товар = column[i];
                i++;
            }
        }
        /**
        * Fill Prices
        */
        private static void SetItemPrices(List<string> column)
        {
            int i = 0;
            foreach (var item in reports)
            {
                item.Цена = column[i];
                i++;
            }
        }
        /**
        * Fill Count
        */
        private static void SetItemCount(List<string> column)
        {
            int i = 0;
            foreach (var item in reports)
            {
                item.Количество = column[i];
                i++;
            }
        }
        /**
        * Fill unit of measure
        */
        private static void SetItemMeasureUnit(List<string> column)
        {
            int i = 0;
            foreach (var item in reports)
            {
                item.Единицы = column[i];
                i++;
            }
        }
        /**
        * Fill INN
        */
        private static void FillINN(string resultText)
        {
            GenerateReport g = new GenerateReport(resultText);
            string INN = g.FindDealerINN();
            foreach (var item in reports)
            {
                item.ИНН = INN;
            }
        }
    }
}

