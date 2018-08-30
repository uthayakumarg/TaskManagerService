using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerService.Entity
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public int ParentId { get; set; }
        public string TaskName { get; set; }
        public string ParentName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ActiveInd { get; set; }
    }
}
