## ZephyrScale Traceability Matrix Report

---

### The Problem

ZephyrScale doesn't offer the ability to export their Traceability Matrix report.

### Solution

Create a .NET desktop app that integrates with Jira API and ZephyrScale API to collect test case coverage data and export to `.xlsx`.

### Current Constriants

- Only supports exporting 100 Zephyr Scale test cases and/or 100 Jira Issues due to API pagination
- Exported report does not overwrite any previously existing reports (i.e., `Reports` folder must not contain `Output.xlsx` when generating new report)
- Only out-of-box test case statuses are supported (Pass, Not Executed, Blocked, In Progress)
