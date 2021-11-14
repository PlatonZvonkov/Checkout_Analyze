using System.IO;
using SautinSoft;
using System;
using CheckoutAnalyze.Report;
using System.Collections.Generic;

namespace CheckoutAnalyze.Extractors
{
    public class PdfExtractor
    {
        /**
         * Converting pdf to excel for further table's extraction
         */
        public static List<ReportModel> PdfToExcel(string path)
        {
            PdfFocus f = new PdfFocus();
            string temp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp.xls");
            f.OpenPdf(path);
            f.ToExcel(temp);

            return ExcelExtractor.ExtractXLStext(temp);
        }

        #region unused
        /**
         * method for pdf text extractor, unused at the moment
         */
        //public static void ExtractPdfText(string path)
        //{

        //    using (PdfDocument document = PdfDocument.Open(path))
        //    {
        //        string rawText = null;
        //        foreach (Page page in document.GetPages())
        //        {
        //            string pageText = page.Text;

        //            foreach (Word word in page.GetWords())
        //            {
        //                rawText += word.Text + " ";
        //            }
        //        }

        //        Regex regex = new Regex(NAME_REGEX);
        //        Match match = Regex.Match(rawText, regex.ToString());     
        //    }
        //}
        #endregion
    }
}
