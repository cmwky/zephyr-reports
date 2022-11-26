#param([String]$reportPath)

#$file = Open-ExcelPackage -Path $reportPath

$file = Open-ExcelPackage -Path "C:\src\zephyr-reports\ZephyrScaleTraceabilityMatrixReport\ZephyrScaleTraceabilityMatrixReport\Reports\Output.xlsx"

#worksheets
$testCoverageMatrixWorksheet = $file.TestCoverageMatrix
$testCaseListWorksheet = $file.TestCaseList

#test cases and issues
$testCases = $testCaseListWorksheet.Cells | ? { $_.Address -like "A*"}
$jiraIssues = $testCaseListWorksheet.Cells | ? { $_.Address -notlike "A*"}

#create hash table where testcases are key and jira issues are value
$hash = @{}
foreach($testcase in $testCases) {

    $issuesToAdd = $jiraIssues | ?{$_.Address -like "*$($testcase.Address.Substring(1))"} | Select-Object -Property Value
    $hash.Add($testcase.Value, $issuesToAdd)    
}

#use hash table to fill out traceability matrix worksheet
$matrixTestCases = $testCoverageMatrixWorksheet.Cells | ? { $_.Address -like "*1"}
$matrixJiraIssues = $testCoverageMatrixWorksheet.Cells | ? { $_.Address -like "A*"}

#along the y-axis
foreach($issue in $matrixJiraIssues) {
    
    #along the x-axis
    foreach($testcase in $matrixTestCases) {
        
        if($hash[$testcase].Contains($issue)) {
            $testCoverageMatrixWorksheet.Cells["$($testcase.Address.Substring(0,1))$($issue.Address.Substring(1))"].Value = "X"
        }
    }
}

#$testCoverageMatrixWorksheet.Cells["A1"].Value = "hello world"
#$testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.PatternType = 'Solid'
#$testCoverageMatrixWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor("Green")

Close-ExcelPackage -ExcelPackage $file