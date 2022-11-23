using FastExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Exporters
{
    internal static class ExcelExport
    {
        public static void ExportToExcel(List<TestCase> testCases, List<string> issueIds)
        {
            FileInfo templateFile = new FileInfo("C:\\Temp\\Template.xlsx");
            FileInfo outputFile = new FileInfo("C:\\Temp\\Output.xlsx");

            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(templateFile, outputFile))
            {
                //
                // TEST COVERAGE MATRIX WORKSHEET //
                //
                Worksheet worksheet = new Worksheet();
                List<Row> rows = new List<Row>();
                List<Cell> cells = new List<Cell>();

                //jira issues down the y-axis, column A
                for(int rowNumber = 2; rowNumber < issueIds.Count + 2; rowNumber++)
                {
                    cells.Add(new Cell(1, issueIds[rowNumber - 2]));
                    rows.Add(new Row(rowNumber, cells));
                    cells = new List<Cell>();
                }

                //test cases along the x-axis, row 1
                for(int columnNumber = 0; columnNumber < testCases.Count; columnNumber++)
                {
                    //starts at  column 2 due to jiraIssues being in column 1
                    cells.Add(new Cell(columnNumber + 2, testCases[columnNumber].key));
                }

                rows.Insert(0, new Row(1, cells));
                worksheet.Rows = rows;
                fastExcel.Write(worksheet, "TestCoverageMatrix");


                //
                // TESTCASE LIST WORKSHEET //
                //
                worksheet = new Worksheet();
                rows = new List<Row>();
                cells = new List<Cell>();

                for (int rowNumber = 0; rowNumber < testCases.Count; rowNumber++)
                {
                    cells.Add(new Cell(1, testCases[rowNumber].key));

                    if(testCases[rowNumber].jiraIssues.Count > 0)
                    {
                        for (int columnNumber = 0; columnNumber < testCases[rowNumber].jiraIssues.Count; columnNumber++)
                        {
                            cells.Add(new Cell(columnNumber + 2, testCases[rowNumber].jiraIssues[columnNumber].key));
                        }
                    }
                    
                    rows.Add(new Row(rowNumber + 1, cells));
                    cells = new List<Cell>();
                }

                worksheet.Rows = rows;
                fastExcel.Write(worksheet, "TestCaseList");
            }
        }
    }
}
