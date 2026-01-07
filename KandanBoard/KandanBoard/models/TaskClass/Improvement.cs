using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KandanBoard.models.TaskClass
{
    public class Improvement : Task
    {
        public string AffectedComponent;

        public string GetAffectedComponent()
        {
            return AffectedComponent;
        }

        public void SetAffectedComponent(string AffectedComponent)
        {
            this.AffectedComponent = AffectedComponent;
        }

        public Improvement(int taskId, string title, string desc, TaskPriority priority, TaskStatus status, string affectedComponent) : base(taskId, title, desc, priority, status)
        {
            AffectedComponent = affectedComponent;
        }

        public override string DisplayTask()
        {
            return $"{base.DisplayTask()} Improveing Component {AffectedComponent}";
        }
    }
}
