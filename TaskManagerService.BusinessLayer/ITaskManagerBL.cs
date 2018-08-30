using System.Collections.Generic;
using TaskManagerService.Entity;

namespace TaskManagerService.BusinessLayer
{
    public interface ITaskManagerBL
    {
        void AddTask(TaskEntity task);
        void EndTask(int taskId, string userId);
        List<TaskEntity> GetAllTasks();
        TaskEntity GetTaskById(int taskId);
        void UpdateTask(TaskEntity task);
    }
}