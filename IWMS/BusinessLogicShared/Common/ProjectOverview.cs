using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class ProjectOverview
    {
        public ProjectOverview()
        {
            TotalProjectActive = 0;
            TotalProjectSuspended = 0;
            TotalProjecArchived = 0;
            TotalProjectPause = 0;

            TotalTaskEnchancement = 0;
            TotalTaskBug = 0;
            TotalTaskDesign = 0;
            TotalTaskReview = 0;

            HightTask = 0;
            MediumTask = 0;
            LowTask = 0;
        }

        public int TotalProjectActive { get; set; }
        public int TotalProjectSuspended { get; set; }
        public int TotalProjecArchived { get; set; }
        public int TotalProjectPause { get; set; }

        public int TotalTaskEnchancement { get; set; }
        public int TotalTaskBug { get; set; }
        public int TotalTaskDesign { get; set; }
        public int TotalTaskReview { get; set; }

        public int HightTask { get; set; }
        public int MediumTask { get; set; }
        public int LowTask { get; set; }
    }
}
