## Welcome!

This repo is a C#/.NET solution for building out custom ZephyrScale-Jira reports.

---

### The Problem

ZephyrScale offers some out-of-box reporting, however, in many cases these reports are lazy loaded, which makes it difficult to actually use them. Additionally, ZephyrScale doesn't provide the ability to export these reports.

### Solution

Create a desktop application that consumes both ZephyrScale and Jira APIs to reconstruct these reports and then export to Excel.
