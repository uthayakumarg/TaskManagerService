using System.Collections.Generic;
using TaskManagerService.Entity;

namespace TaskManagerService.DataLayer
{
    public interface ITasksRepository
    {
        void AddTask(TaskEntity task);
        void EndTask(int taskId, string userId);
        List<TaskEntity> GetAllTasks();
        TaskEntity GetTaskById(int taskId);
        void UpdateTask(TaskEntity task);
    }
}