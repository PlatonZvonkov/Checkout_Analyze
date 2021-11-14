using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CheckoutAnalyze.Extractors;
using Newtonsoft.Json;

namespace CheckoutAnalyze.Report
{    
    class GenerateReport : RegexPaths
    {
        private readonly string textFromEXCEL;
        public ReportModel report;
        
        public GenerateReport()
        {
           report = new ReportModel();
        }
        public GenerateReport(string textFromEXCEL)
        {
            this.textFromEXCEL = textFromEXCEL;
            report = new ReportModel();
        }
        public void GenerateJSON(List<ReportModel> list)
        {
           string json = JsonConvert.SerializeObject(list, Formatting.Indented); 
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Report{DateTime.Now:yyyy-dd-M--HH-mm-ss}.json");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(json);
            }
        }              
        public string FindDealerINN()
        {
            Regex regex = new Regex(INN_REGEX);
            Match match = Regex.Match(textFromEXCEL, regex.ToString());
            if (match.ToString().Contains("7806248874") == true)
            {
                match.NextMatch();
            }
            string temp = match.ToString().Replace("ИНН", String.Empty).Trim();
            return temp;
        }
        
        #region unused
        public void FindItemName()
        {
            Regex regex = new Regex(NAME_REGEX_OPTION1);
            Match match = Regex.Match(textFromEXCEL, regex.ToString());
            if (match.ToString().Contains("(работы, услуги)") == true)
            {
                report.Товар = match.ToString().Replace("Товары (работы, услуги)", String.Empty).Trim();
            }
            else if (match.ToString().Contains("Наименование") == true)
            {
                report.Товар = match.ToString().Replace("Наименование", String.Empty).Trim();
            }
            else
            {
                report.Товар = match.ToString();
            }
        }
        public void FindItemPrice()
        {
            Regex regex = new Regex(PRICE_BIG_REGEX);
            Match match = Regex.Match(textFromEXCEL, regex.ToString());
            string temp = match.ToString();
            if (temp.ToLower().Contains("(руб)") == true)
            {
                report.Цена = temp.ToLower().Replace("цена (руб)",String.Empty).Trim();
            }
            else if (temp.Contains("Цена") == true)
            {
                report.Цена = temp.Replace("Цена",String.Empty).Trim();
            }
        }
        public void FindItemCount()
        {
            Regex regex = new Regex(COUNT_VALUE_REGEX);
            Match match = Regex.Match(textFromEXCEL, regex.ToString());
            if (match.ToString().Contains("Кол-во") == true)
            {
                report.Количество = match.ToString().Replace("Кол-во", String.Empty).Trim();
            }
            else if (match.ToString().Contains("Количество") == true)
            {
                report.Количество = match.ToString().Replace("Количество", String.Empty).Trim();
            }
            else
            {
                report.Количество = match.ToString();
            }
        }
        public void FindItemMeasure()
        {
            Regex regex = new Regex(COUNT_MEASURE_REGEX);
            Match match = Regex.Match(textFromEXCEL, regex.ToString());
           
            if (match.ToString().Contains("Ед.\nизм.") == true)
            {
                report.Единицы = match.ToString().Replace("Ед.\nизм.", String.Empty).Trim();
            }
            else if (match.ToString().Contains("изм.") == true)
            {
                report.Единицы = match.ToString().Replace("Eд. изм.", String.Empty).Trim();
            }
            else if (match.ToString().Contains("Ед.") == true)
            {
                report.Единицы = match.ToString().Replace("Ед.", String.Empty).Trim();
            }            
            else
            {
                report.Единицы = match.ToString();
            }
        }
        #endregion
        
    }
}