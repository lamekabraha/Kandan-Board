using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KandanBoard.models.TaskClass
{
    public class Bug : Task
    {
        public enum BugSeverity
        {
            Trivial,
            Minor,
            Moderate,
            Major,
            Critical
        }

        public BugSeverity Severity;
        public string ReproductionSteps;

        public BugSeverity GetSeverity()
        {
            return Severity;
        }

        public string GetReproductionSteps()
        {
            return ReproductionSteps;
        }

        public void SetSeverity(BugSeverity Severity)
        {
            this.Severity= Severity;
        }

        public void SetReproductionSteps(string ReproductionSteps)
        {
            this.ReproductionSteps = ReproductionSteps;
        }

        public Bug(int taskId, string title, string desc, TaskPriority priority, TaskStatus status, BugSeverity severity, string reproductionSteps) : base(taskId, title, desc, priority, status)
        {
            Severity = severity;
            ReproductionSteps = reproductionSteps;
        }

        public override string DisplayTask()
        {
            return $"{base.DisplayTask()} Bug Severity: {Severity}" ;
        }
    }
}
