using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephyrScaleTraceabilityMatrixReport.Models
{
    internal class TestCaseExecution
    {
        public long id { get; set; }
        public string key { get; set; }
        public TestCaseExecutionStatus status { get; set; }
    }
}
