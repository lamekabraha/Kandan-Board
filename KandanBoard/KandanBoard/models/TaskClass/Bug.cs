using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KandanBoard.models.TaskClass
{
    public class Bugs : Task
    {
        public string Severity;
        public string ReproductionSteps;

        public string GetSeverity()
        {
            return Severity;
        }

        public string GetReproductionSteps()
        {
            return ReproductionSteps;
        }

        public void SetSeverity(string Severity)
        {
            this.Severity= Severity;
        }

        public void SetReproductionSteps(string ReproductionSteps)
        {
            this.ReproductionSteps = ReproductionSteps;
        }

        public Bugs(int taskId, string title, string desc, TaskPriority priority, TaskStatus status, string severity, string reproductionSteps) : base(taskId, title, desc, priority, status)
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
