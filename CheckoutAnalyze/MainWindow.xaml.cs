using System.Collections.Generic;
using System.Windows;
using Window = System.Windows.Window;
using CheckoutAnalyze.Extractors;
using CheckoutAnalyze.Report;


namespace CheckoutAnalyze
{
    /** <summary>
    ** Interaction logic for MainWindow.xaml 
    */
    public partial class MainWindow : Window
    {
        string[] filePaths;
        List<ReportModel> reportList;
        GenerateReport generate;

        public MainWindow() {
            InitializeComponent();
            reportList = new List<ReportModel>();
            generate = new GenerateReport();
        }

        private void FileDropStackPanel_OnDrop(object sender, DragEventArgs e)
        {  
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            { 
                filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var path in filePaths)
                {
                    HandleFileOpen(path);
                }
            }
        }
                
        private void HandleFileOpen(string path)
        {  
            if (System.IO.Path.GetExtension(path).ToLower() == ".xls"||System.IO.Path.GetExtension(path).ToLower() == ".xlsx")
            {                
                reportList.AddRange(ExcelExtractor.ExtractXLStext(path));                
            }
            else if (System.IO.Path.GetExtension(path).ToLower() == ".pdf")
            {
                reportList.AddRange(PdfExtractor.PdfToExcel(path));
            }
            else
            {
                MessageBox.Show($"Эта программа поддерживает только .xls .xlsx и .pdf форматы!\n Убедитесь что все файлы в должном виде!");
                Application.Current.Shutdown();
            }
            label1.Content = path.ToString();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            generate.GenerateJSON(reportList);
            label1.Content = "Файл 'Report' находиться в папке программы";
        }
    }
}