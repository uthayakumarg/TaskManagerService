using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerService.Entity;
using TaskManagerService.DataLayer;

namespace TaskManagerService.BusinessLayer
{
    public class TaskManagerBL : ITaskManagerBL
    {
        private ITasksRepository _repo;
        public TaskManagerBL(ITasksRepository repo)
        {
            _repo = repo;
        }

        public List<TaskEntity> GetAllTasks()
        {
            return _repo.GetAllTasks();
        }

        public TaskEntity GetTaskById(int taskId)
        {
            return _repo.GetTaskById(taskId);
        }

        public void AddTask(TaskEntity task)
        {
            _repo.AddTask(task);
        }

        public void UpdateTask(TaskEntity task)
        {
            _repo.UpdateTask(task);
        }

        public void EndTask(int taskId, string userId)
        {
            _repo.EndTask(taskId, userId);
        }
    }
}
