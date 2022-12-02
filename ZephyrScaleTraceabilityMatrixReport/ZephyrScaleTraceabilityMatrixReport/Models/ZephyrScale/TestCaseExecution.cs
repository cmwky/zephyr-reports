namespace ZephyrScaleTraceabilityMatrixReport.Models.ZephyrScale
{
    internal class TestCaseExecution
    {
        internal long Id { get; set; }
        internal string Key { get; set; }
        internal TestCaseExecution_TestCase TestCase { get; set; }
        internal TestCaseExecution_TestExecutionStatus TestExecutionStatus { get; set; }

    }
}
