function FillColor-TestCoverageMatrix()
{
    $file = Open-ExcelPackage -Path "C:\temp\Output.xlsx"
    $testCoverageMatrixWorksheet = $file.TestCoverageMatrix
    $testCoverageMatrixWorksheet.Cells["A1"].Value = "hello world"
    $testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.PatternType = 'Solid'
    $testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor("Green")
    Close-ExcelPackage -ExcelPackage $file
}

FillColor-TestCoverageMatrix

#$testCaseListWorksheet = $file.TestCaseList
#$testCoverageMatrixWorksheet.Cells['A2'].Value | Out-File -FilePath "C:\temp\test.txt"