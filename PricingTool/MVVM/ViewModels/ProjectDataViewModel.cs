using Microsoft.Office.Interop.Excel;
using PricingTool.MVVM.Models;
using PricingTool.MVVM.Views;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using static PricingTool.MVVM.Views.ProjectDataView;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using System.ComponentModel;


namespace PricingTool.MVVM.ViewModels;


public class ProjectDataViewModel : ContentPage
{

    //public ProjectData ProjectData { get; set; }
    ProjectData projectData = new ProjectData();
    //string outputFile = "C:\\Users\\Filip\\Desktop\\testyCsv\\dataInteropProjectData.xlsx";

    public ProjectDataViewModel()
    {


    }



    public void TransformLDC(string inputFileLDC)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLDC = excelApp.Workbooks.Open(inputFileLDC);
        Excel.Worksheet worksheetLDC = (Excel.Worksheet)workbookLDC.Worksheets[1];

        //usuniêcie pierwszych 8 wierszy
        for (int i = 1; i <= 8; i++)
        {
            worksheetLDC.Cells[i].EntireRow.Delete();
        }
        int lastRowLDC = worksheetLDC.Cells.End[XlDirection.xlDown].Row;

        //usuniecie vinylu
        worksheetLDC.Cells[1, 8].EntireColumn.Delete();
        //usuniecie pianki
        worksheetLDC.Cells[1, 6].EntireColumn.Delete();

        //przeniesienie materialu
        worksheetLDC.Cells[1, 3].EntireColumn.Insert();
        worksheetLDC.Range[$"F1:F{lastRowLDC}"].Cut(worksheetLDC.Range[$"C1:C{lastRowLDC}"]);

        //dodanie kolumn
        for (int i = 1; i <= 3; i++)
        {
            worksheetLDC.Cells[1, 3].EntireColumn.Insert();
        }

        //dodanie pack i ilosci
        worksheetLDC.Range[$"D1:D{lastRowLDC}"].Value = "Pack";
        worksheetLDC.Range[$"E1:E{lastRowLDC}"].Value = 1;

        //dodanie Print No
        worksheetLDC.Cells[1, 14].EntireColumn.Insert();
        worksheetLDC.Range[$"N1:N{lastRowLDC}"].Value = "No";
        for (int i = 1; i <= lastRowLDC; i++)
        {
            var cell = worksheetLDC.Cells[i, 15];
            if (cell.Value != null && (cell.Value.ToString().ToLower().Contains("print") || cell.Value.ToString().ToLower().Contains("graf")))
            {
                worksheetLDC.Cells[i, 14].Value = "Yes";
            }
        }

        //zamiana ac z opisem ac
        worksheetLDC.Cells[1, 12].EntireColumn.Insert();
        worksheetLDC.Range[$"N1:N{lastRowLDC}"].Cut(worksheetLDC.Range[$"L1:L{lastRowLDC}"]);
        worksheetLDC.Cells[1, 16].EntireColumn.Insert();
        worksheetLDC.Cells[1, 14].EntireColumn.Delete();

        //pozbycie sie wiersza z myslnikami
        for (int i = 1; i <= lastRowLDC; i++)
        {
            var cell = worksheetLDC.Cells[i, 1];
            if (cell.Value.ToString() == "-")
            {
                worksheetLDC.Cells[i, 1].EntireRow.Delete();
            }
        }


        lastRowLDC = worksheetLDC.Cells.End[XlDirection.xlDown].Row;
        worksheetLDC.Cells.Style = "Normal";
        Excel.Range rangeLDC = worksheetLDC.Range[$"A1:P{lastRowLDC}"];
        projectData.dataLDC = (object[,])rangeLDC.Value;




        workbookLDC.Saved = true;
        workbookLDC.Close(false);


    }

    public void TransformLPA(string inputFileLPA)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLPA = excelApp.Workbooks.Open(inputFileLPA);
        Excel.Worksheet worksheetLPA = (Excel.Worksheet)workbookLPA.Worksheets[1];

        //usuniêcie pierwszych 8 wierszy
        for (int i = 1; i <= 8; i++)
        {
            worksheetLPA.Cells[i].EntireRow.Delete();
        }
        //zliczenie wierszy
        int lastRowLPA = worksheetLPA.Cells.End[XlDirection.xlDown].Row;

        //przeniesienie ac
        worksheetLPA.Cells[1, 7].EntireColumn.Insert(1);
        worksheetLPA.Range[$"I1:I{lastRowLPA}"].Cut(worksheetLPA.Range[$"G1:G{lastRowLPA}"]);


        //dodanie kolumn
        for (int i = 1; i <= 2; i++)
        {
            worksheetLPA.Cells[1, 4].EntireColumn.Insert();
        }

        //dodanie pack i ilosci
        worksheetLPA.Range[$"D1:D{lastRowLPA}"].Value = "Pack";
        worksheetLPA.Range[$"E1:E{lastRowLPA}"].Value = 1;

        //double Panel
        for (int i = 1; i < lastRowLPA && i < 30; i++)
        {
            var cellDouble = worksheetLPA.Cells[i, 12];

            if (cellDouble.Value != null && cellDouble.Value.ToString().ToLower().Contains("doubl"))
            {
                worksheetLPA.Cells[i + 1, 1].EntireRow.Insert();
                worksheetLPA.Range[$"A{i}:N{i}"].Copy(worksheetLPA.Range[$"A{i + 1}:N{i + 1}"]);
                worksheetLPA.Range[$"I{i + 1}:M{i + 1}"].Clear();
                worksheetLPA.Cells[i + 1, 1].Value = $"{worksheetLPA.Cells[i, 1].Value}a";
                worksheetLPA.Cells[i + 1, 4].Value = "Issue";
                worksheetLPA.Cells[i + 1, 10].Value = $"Montowac z {worksheetLPA.Cells[i, 1].Value}";
                lastRowLPA++;
            }


        }


        //dodanie kolumn
        for (int i = 1; i <= 3; i++)
        {
            worksheetLPA.Cells[1, 9].EntireColumn.Insert();
        }

        lastRowLPA = worksheetLPA.Cells.End[XlDirection.xlDown].Row;
        //dodanie print
        worksheetLPA.Range[$"N1:N{lastRowLPA}"].Value = "No";
        for (int i = 1; i <= lastRowLPA; i++)
        {
            var cell = worksheetLPA.Cells[i, 16];
            if (cell.Value != null && (cell.Value.ToString().ToLower().Contains("print") || cell.Value.ToString().ToLower().Contains("graf")))
            {
                worksheetLPA.Cells[i, 14].Value = "Yes";
            }
        }
        worksheetLPA.Range[$"O1:O{lastRowLPA}"].Clear();


        //pozbycie sie wiersza z myslnikami
        for (int i = 1; i <= lastRowLPA; i++)
        {
            var cell = worksheetLPA.Cells[i, 1];
            if (cell.Value.ToString() == "-")
            {
                worksheetLPA.Cells[i, 1].EntireRow.Delete();
            }
        }



        worksheetLPA.Cells.Style = "Normal";
        lastRowLPA = worksheetLPA.Cells.End[XlDirection.xlDown].Row;
        Excel.Range rangeLPA = worksheetLPA.Range[$"A1:P{lastRowLPA}"];
        projectData.dataLPA = (object[,])rangeLPA.Value; // Pobieramy dane jako dwuwymiarowa tablica obiektów


        workbookLPA.Saved = true;
        workbookLPA.Close(false);
        excelApp.Quit();
    }

    public void TransformLPL(string inputFileLPL)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLPL = excelApp.Workbooks.Open(inputFileLPL);
        Excel.Worksheet worksheetLPL = (Excel.Worksheet)workbookLPL.Worksheets[1];
        //int pos = 0;
        projectData.dataLPL = new List<List<object>>();

        for (int i = 4; i <= 50; i++)
        {
            var cell = worksheetLPL.Cells[i, 5];
            if (cell.Value != null)
            {
                projectData.dataLPL.Add(new List<object> { cell.Value, cell.Offset[0, 1].Value, cell.Offset[0, -1].Value });
            }
        }

        for (int i = 4; i <= 37; i++)
        {
            var cell = worksheetLPL.Cells[i, 12];
            if (cell.Value != null)
            {
                projectData.dataLPL.Add(new List<object> { cell.Value, cell.Offset[0, 1].Value, cell.Offset[0, -2].Value });
            }
        }

        projectData.regLPL = worksheetLPL.Cells[40, 11].Value;
        projectData.plateLPL = worksheetLPL.Cells[42, 11].Value;
        projectData.plateKidsLPL = worksheetLPL.Cells[43, 11].Value.ToString();



        workbookLPL.Saved = true;
        workbookLPL.Close(false);
        excelApp.Quit();
    }

    public void TransformLAC(string inputFileLAC)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLAC = excelApp.Workbooks.Open(inputFileLAC);
        Excel.Worksheet worksheetLAC = (Excel.Worksheet)workbookLAC.Worksheets[1];

        int num = (int)worksheetLAC.Cells[11, 3].End[XlDirection.xlDown].Value;
        projectData.dataLAC = new List<List<object>>();

        for (int i = 11; i <= num + 11; i++)
        {
            var cell = worksheetLAC.Cells[i, 6];
            if (cell.Value != null && cell.Value != "-")
            {
                projectData.dataLAC.Add(new List<object> { cell.Value, cell.Offset[0, -1].Value, cell.Offset[0, -2].Value });
            }
        }


        workbookLAC.Saved = true;
        workbookLAC.Close(false);
        excelApp.Quit();
    }

    public void TransformTrave(string inputFileTrave)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookTrave = excelApp.Workbooks.Open(inputFileTrave);
        Excel.Worksheet worksheetTrave = (Excel.Worksheet)workbookTrave.Worksheets[1];

        int lastRowLTrave = worksheetTrave.Cells.End[XlDirection.xlDown].Row;
        projectData.dataTrave = new List<List<object>>();

        for (int i = 2; i <= lastRowLTrave; i++)
        {
            var cell = worksheetTrave.Cells[i, 1];
            if (cell.Value != null)
            {
                string[] cellString = worksheetTrave.Cells[i, 1].Value2.ToString().Split(';');


                projectData.dataTrave.Add(new List<object> { cellString[0], cellString[5] });
            }
        }

        workbookTrave.Saved = true;
        workbookTrave.Close(false);
        excelApp.Quit();
    }

    public void TransformLTU(string inputFileLTU)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLTU = excelApp.Workbooks.Open(inputFileLTU);
        Excel.Worksheet worksheetLTU = (Excel.Worksheet)workbookLTU.Worksheets[1];

        projectData.pospadValue = worksheetLTU.Cells[8, 10].Value.ToString();

        workbookLTU.Saved = true;
        workbookLTU.Close(false);
        excelApp.Quit();

    }

    public void TransformLKK(string inputFileLKK)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbookLKK = excelApp.Workbooks.Open(inputFileLKK);
        Excel.Worksheet worksheetLKK = (Excel.Worksheet)workbookLKK.Worksheets[1];

        int lastRowLKK = worksheetLKK.Cells[10, 3].End[XlDirection.xlDown].Row;
        projectData.dataLKK = new List<List<object>>();

        for (int i = 10; i <= lastRowLKK; i++)
        {
            var cell = worksheetLKK.Cells[i, 3];
            if (cell.Value != null)
            {
                projectData.dataLKK.Add(new List<object> { cell.Value, cell.Offset[0, 1].Value, cell.Offset[0, -1].Value });
            }
        }

        workbookLKK.Saved = true;
        workbookLKK.Close(false);
        excelApp.Quit();
    }

    public void NewFileExcel(string outputFile)
    {
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook newWorkbook = excelApp.Workbooks.Add();
        Excel.Worksheet newWorksheet = newWorkbook.Sheets[1];

        newWorksheet.Cells[1, 1].Value = "PC-CUSTOM";
        int lastRowCSV = newWorksheet.UsedRange.Rows.Count;


        #region LDC
        if (projectData.dataLDC != null)
        {
            int rowsLDC = projectData.dataLDC.GetLength(0);
            int colsLPA = projectData.dataLDC.GetLength(1);

            for (int i = 1; i <= rowsLDC; i++)
            {
                for (int j = 1; j <= colsLPA; j++)
                {
                    newWorksheet.Cells[i + lastRowCSV, j].Value = projectData.dataLDC[i, j];

                }
            }
            lastRowCSV += rowsLDC + 1;
        }
        #endregion

        #region LPA
        if (projectData.dataLPA != null)
        {
            lastRowCSV -= 1;
            int rowsLPA = projectData.dataLPA.GetLength(0);
            int colsLPA = projectData.dataLPA.GetLength(1);

            for (int i = 1; i <= rowsLPA; i++)
            {
                for (int j = 1; j <= colsLPA; j++)
                {
                    newWorksheet.Cells[i + lastRowCSV, j].Value = projectData.dataLPA[i, j];

                }
            }
            lastRowCSV += rowsLPA + 1;
        }
        #endregion

        #region LPL
        if (projectData.dataLPL != null)
        {
            for (int i = 0; i < projectData.dataLPL.Count; i++)
            {
                if (projectData.dataLPL[i][2].ToString().ToLower().Contains("winylu"))
                {
                    newWorksheet.Cells[i + lastRowCSV, 1].Value = projectData.dataLPL[i][2]; //opis
                    newWorksheet.Cells[i + lastRowCSV, 6].Value = projectData.dataLPL[i][0]; //kod
                    newWorksheet.Cells[i + lastRowCSV, 4].Value = "Pack";
                    newWorksheet.Cells[i + lastRowCSV, 5].Value = 1; //ilosc
                    newWorksheet.Cells[i + lastRowCSV, 7].Value = 1800;
                    newWorksheet.Cells[i + lastRowCSV, 8].Value = (double)projectData.dataLPL[i][1] * 1000;
                    newWorksheet.Cells[i + lastRowCSV, 14].Value = "No";
                }
                else if (projectData.dataLPL.Count < 34)
                {
                    newWorksheet.Cells[i + lastRowCSV, 1].Value = projectData.dataLPL[i][0]; //kod
                    newWorksheet.Cells[i + lastRowCSV, 2].Value = projectData.dataLPL[i][2]; //opis
                    newWorksheet.Cells[i + lastRowCSV, 5].Value = projectData.dataLPL[i][1]; //ilosc
                    newWorksheet.Cells[i + lastRowCSV, 4].Value = "Pack";
                    newWorksheet.Cells[i + lastRowCSV, 14].Value = "No";
                }
                else
                {
                    newWorksheet.Cells[i + lastRowCSV, 1].Value = projectData.dataLPL[i][0];
                    newWorksheet.Cells[i + lastRowCSV, 2].Value = projectData.dataLPL[i][2]; //opis
                    newWorksheet.Cells[i + lastRowCSV, 5].Value = projectData.dataLPL[i][1]; //ilosc
                    newWorksheet.Cells[i + lastRowCSV, 4].Value = "Pack";
                    newWorksheet.Cells[i + lastRowCSV, 14].Value = "No";
                }

            }

            lastRowCSV += projectData.dataLPL.Count;

            if (projectData.regLPL != null && projectData.plateLPL != null)
            {
                newWorksheet.Cells[lastRowCSV, 1].Value = projectData.regLPL.Substring(projectData.plateLPL.Length - 11).Replace(" ", "");
                newWorksheet.Cells[lastRowCSV, 13].Value = projectData.regLPL.Substring(0, projectData.plateLPL.Length - 13);
                newWorksheet.Cells[lastRowCSV, 4].Value = "Pack";
                newWorksheet.Cells[lastRowCSV, 5].Value = 1;
                newWorksheet.Cells[lastRowCSV, 6].Value = "07-00062-020";
                newWorksheet.Cells[lastRowCSV, 7].Value = 297;
                newWorksheet.Cells[lastRowCSV, 8].Value = 420;
                newWorksheet.Cells[lastRowCSV, 14].Value = "Yes";
                newWorksheet.Cells[lastRowCSV, 16].Value = $"ilosc dzieci: {projectData.plateKidsLPL}";


                newWorksheet.Cells[lastRowCSV + 1, 1].Value = projectData.plateLPL.Substring(projectData.plateLPL.Length - 12).Replace(" ", "");
                newWorksheet.Cells[lastRowCSV + 1, 13].Value = projectData.plateLPL.Substring(0, projectData.plateLPL.Length - 14);
                newWorksheet.Cells[lastRowCSV + 1, 4].Value = "Pack";
                newWorksheet.Cells[lastRowCSV + 1, 5].Value = 1;
                newWorksheet.Cells[lastRowCSV + 1, 6].Value = "01-00395-000";
                newWorksheet.Cells[lastRowCSV + 1, 14].Value = "Yes";
                newWorksheet.Cells[lastRowCSV + 1, 16].Value = $"ilosc dzieci: {projectData.plateKidsLPL}";
            }
            lastRowCSV += 2;
        }
        #endregion

        #region LAC
        if (projectData.dataLAC != null)
        {
            for (int i = 0; i < projectData.dataLAC.Count; i++)
            {
                newWorksheet.Cells[lastRowCSV + i, 1].Value = projectData.dataLAC[i][0];
                newWorksheet.Cells[lastRowCSV + i, 2].Value = projectData.dataLAC[i][2];//opis
                newWorksheet.Cells[lastRowCSV + i, 4].Value = "Pack";
                newWorksheet.Cells[lastRowCSV + i, 5].Value = projectData.dataLAC[i][1];//ilosc
                newWorksheet.Cells[lastRowCSV + i, 14].Value = "No";
            }
            lastRowCSV += projectData.dataLAC.Count;
        }
        #endregion

        #region Trave
        if (projectData.dataTrave != null)
        {
            for (int i = 0; i < projectData.dataTrave.Count; i++)
            {
                newWorksheet.Cells[lastRowCSV + i, 1].Value = projectData.dataTrave[i][0];
                newWorksheet.Cells[lastRowCSV + i, 4].Value = "Pack";
                newWorksheet.Cells[lastRowCSV + i, 5].Value = 1;
                newWorksheet.Cells[lastRowCSV + i, 6].Value = projectData.dataTrave[i][1];//material
                newWorksheet.Cells[lastRowCSV + i, 9].Value = projectData.dataTrave[i][1].ToString().Substring(projectData.dataTrave[i][1].ToString().Length - 4);//material
                newWorksheet.Cells[lastRowCSV + i, 14].Value = "No";
            }
            lastRowCSV += projectData.dataTrave.Count;
        }
        #endregion

        #region LTU
        if (projectData.pospadValue != null)
        {
            newWorksheet.Cells[lastRowCSV, 1].Value = "11-00043-040";
            newWorksheet.Cells[lastRowCSV, 4].Value = "Issue";
            newWorksheet.Cells[lastRowCSV, 5].Value = projectData.pospadValue;
            newWorksheet.Cells[lastRowCSV, 14].Value = "No";
            lastRowCSV += 1;
        }
        #endregion

        #region LKK
        if (projectData.dataLKK != null)
        {
            for (int i = 0; i < projectData.dataLKK.Count; i++)
            {

                newWorksheet.Cells[i + lastRowCSV, 1].Value = projectData.dataLKK[i][1]; //kod
                newWorksheet.Cells[i + lastRowCSV, 2].Value = projectData.dataLKK[i][2]; //opis
                newWorksheet.Cells[i + lastRowCSV, 5].Value = projectData.dataLKK[i][0]; //ilosc
                newWorksheet.Cells[i + lastRowCSV, 4].Value = "Issue";
                newWorksheet.Cells[i + lastRowCSV, 14].Value = "No";


            }
            lastRowCSV += projectData.dataLKK.Count;
        }
        
        #endregion
        if (File.Exists(outputFile))
        {
            //try
            //{
                File.Delete(outputFile);
                newWorkbook.SaveAs(outputFile);
            //}
            //catch (IOException ex)
            //{
            //    //lblLicznik.Text = $"zamknij plik ";
            //}

        }
        else
        {
            newWorkbook.SaveAs(outputFile);
        }



        newWorkbook.Close();
        excelApp.Quit();
    }


    public void KillExcel()
    {
        Process[] excelProcesses = Process.GetProcessesByName("EXCEL");

        // Zakoñcz ka¿dy proces Excel
        foreach (Process process in excelProcesses)
        {
            process.Kill();
        }

    }


}
