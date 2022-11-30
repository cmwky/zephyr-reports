## ZephyrScale Traceability Matrix (Test Coverage) Report

---

### The Problem

ZephyrScale doesn't offer the ability to export their Traceability Matrix report. What's more is that if your Jira/ZephyrScale projects contain a significant amount of issues/test cases, ZephyrScale strictly paginates the report and you have to click "Load More" quite a bit each time.

Additionally, the traceability matrix report doesn't indicate which jira issues *aren't* covered by a test case (this feature doesn't exist yet either within this application but it's the next to be developed). 

### Solution

A .NET (desktop) application that integrates Jira and ZephyrScale APIs to collect test case coverage data and export to an `.xlsx` file for further custom reporting.

### How It Works

The first step is to get all ZephyrScale test cases that we want within the report. By default, we grab all test cases that belong to a singular project. 
The reason why we start on the ZephyrScale "side" of things is because Jira issues don't sufficiently contain links to the test cases but the test cases do for the issues.

Second, we take all the "linked" Jira issues and fetch their details. Additionally, we create a distinct list of Jira issue keys. This allows us to more easily populate the report file, however, in the near future more details will be provided in the report and so, a collection of JiraIssues will be passed to `ExcelExport`. 

The third and final step is to parse through the data collected in the first two steps and output that to an Excel file, followed up by a PowerShell script that reads the file and fills out the traceability matrix. The reasoan for going with PowerShell is that it's just easier to work with Excel files than with a C# library (specifically, when formatting the cells with color and other types of styling).

The end result is a `.xlsx` workbook that contains two worksheets, one being a "raw" list of test cases and their linked issues, and the other being the actual traceability test coverage matrix.


### How To Use

0. Install PowerShell module [ImportExcel](https://www.powershellgallery.com/packages/ImportExcel/7.4.1).
1. After cloning the repo, copy & paste `App.config.template` and rename the copied file to `App.config`, then fill out all the Jira and ZephyrScale values specific to your own setup.
2. Launch the program.
3. View the generated report at `\Reports\Output.xlsx`.

### Things To Note

 - As it currently stands, the PowerShell script does not attempt to address any `ExecutionPolicy` settings for the machine it's being ran from. That said, before running this application please verify that your machine can freely run PowerShell scripts. See [about Execution Policies](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies?view=powershell-7.3) for more information about this.

- Test case status / color styling is not yet implemented. Once they are though, only out-of-box test case statuses are supported (Pass, Not Executed, Blocked, In Progress).

- Only supports exporting 100 Zephyr Scale test cases and/or 100 Jira Issues due to API pagination.

- Exported report does not overwrite any previously existing reports (i.e., `\Reports` must not already contain `Output.xlsx` when generating new report).
