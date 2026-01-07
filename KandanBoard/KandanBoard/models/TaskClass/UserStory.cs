using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KandanBoard.models.TaskClass
{
    public class UserStory : Task
    {
        public int StoryPoints;
        public string AcceptanceCriteria;

        public int GetStoryPoints()
        {
            return StoryPoints;
        }

        public string GetAcceptanceCriteria()
        {
            return AcceptanceCriteria;
        }

        public void SetStoryPoints(int StoryPoints)
        {
            this.StoryPoints = StoryPoints;
        }

        public void SetAcceptanceCriteria(string AcceptanceCriteria)
        {
            this.AcceptanceCriteria = AcceptanceCriteria;
        }

        public UserStory(int taskId, string title, string desc, TaskPriority priority, TaskStatus status, int points, string criteria) : base(taskId, title, desc, priority, status)
        {
            StoryPoints = points;
            AcceptanceCriteria = criteria; 
        }

        public override string DisplayTask()
        {
            return $"{base.DisplayTask()} Story Points: {StoryPoints}";
        }
    }
}
