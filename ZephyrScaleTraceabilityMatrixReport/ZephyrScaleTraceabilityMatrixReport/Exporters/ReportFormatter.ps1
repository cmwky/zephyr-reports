param([String]$reportPath)

#$file = Open-ExcelPackage -Path $reportPath

$file = Open-ExcelPackage -Path "C:\src\zephyr-reports\ZephyrScaleTraceabilityMatrixReport\ZephyrScaleTraceabilityMatrixReport\Reports\Output.xlsx"

#worksheets
$testCoverageMatrixWorksheet = $file.TestCoverageMatrix
$testCaseListWorksheet = $file.TestCaseList

#test cases and issues
$testCases = $testCaseListWorksheet.Cells | ? { $_.Address -like "A*"}
$jiraIssues = $testCaseListWorksheet.Cells | ? { $_.Address -notlike "A*"}

#create hash table from test cases and issues
$hash = @{}
foreach($testcase in $testCases) {

    $issuesToAdd = $jiraIssues | ?{$_.Address -like "*$($testcase.Address.Substring(1))"} | Select-Object -Property Value
    $hash.Add($testcase.Value, $issuesToAdd)    
}

#use hash table to fill out traceability matrix worksheet
$matrixTestCases = $testCoverageMatrixWorksheet.Cells | ? { $_.Address -like "*1"}
$matrixJiraIssues = $testCoverageMatrixWorksheet.Cells | ? { $_.Address -like "A*"}

##TODO
##GET ALL THE ISSUES ON THE Y AXIS, THEN NESTED FOREACH AND TAKE THE LETTER OF TEST CASE + ROW OF ISSUE AND FILL

foreach(


$testCoverageMatrixWorksheet.Cells["A1"].Value = "hello world"
$testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.PatternType = 'Solid'
$testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor("Green")

Close-ExcelPackage -ExcelPackage $file