using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using TaskManagerService.DataLayer;
using TaskManagerService.BusinessLayer;
using TaskManagerService.Entity;


namespace TaskManagerService.Tests.BusinessLayer
{
    [TestFixture]
    public class TaskManagerBLTests
    {
        private ITasksRepository _mockRepository;
        private List<TaskEntity> _tasks;

        [SetUp]
        public void Initialize()
        {
            var repository = new Mock<ITasksRepository>();
            _tasks = new List<TaskEntity>()
                        {
                            new TaskEntity { TaskId = 1, TaskName = "Project Task", ParentId = 0, ParentName = "", Priority = 1, ActiveInd = "Y", StartDate = new DateTime(2018, 04, 18), EndDate = new DateTime(2018, 08, 24) },
                            new TaskEntity { TaskId = 2, TaskName = "Coding", ParentId = 1, ParentName = "Project Task", Priority = 2, ActiveInd = "Y", StartDate = new DateTime(2018, 04, 21), EndDate = new DateTime(2018, 08, 26) },
                            new TaskEntity { TaskId = 3, TaskName = "Analysis", ParentId = 1, ParentName = "Project Task", Priority = 3, ActiveInd = "N", StartDate = new DateTime(2018, 04, 18), EndDate = new DateTime(2018, 08, 20) }
                        };

            // Get All
            repository.Setup(r => r.GetAllTasks()).Returns(_tasks);

            // Get Task by Id
            repository.Setup(r => r.GetTaskById(It.IsAny<int>()))
                .Returns((int i) => _tasks.Where(t => t.TaskId == i).SingleOrDefault());

            // Insert task
            repository.Setup(r => r.AddTask(It.IsAny<TaskEntity>()))
                .Callback((TaskEntity t) => _tasks.Add(t));

            // Update Task
            repository.Setup(r => r.UpdateTask(It.IsAny<TaskEntity>())).Callback(
                (TaskEntity target) =>
                {
                    DateTime now = DateTime.Now;
                    var original = _tasks.Where(
                        q => q.TaskId == target.TaskId).Single();

                    original.TaskName = target.TaskName;
                    original.ParentId = target.ParentId;
                    original.Priority = target.Priority;
                    original.StartDate = target.StartDate;
                    original.EndDate = target.EndDate;
                });

            // End Task
            repository.Setup(r => r.EndTask(It.IsAny<int>(), It.IsAny<string>())).Callback(
                (int taskId, string userId) =>
                {
                    DateTime now = DateTime.Now;
                    var original = _tasks.Where(
                        q => q.TaskId == taskId).Single();

                    original.EndDate = DateTime.Now;
                    original.UpdatedBy = userId;
                    original.ActiveInd = "N";
                });

            _mockRepository = repository.Object;
        }

        [Test]
        public void Get_All_Tasks()
        {
            List<TaskEntity> tasks = _mockRepository.GetAllTasks();

            Assert.IsTrue(tasks.Count() == 3);
            Assert.IsTrue(tasks.ElementAt(1).TaskName == "Coding");
            Assert.IsTrue(tasks.ElementAt(0).StartDate == new DateTime(2018, 04, 18));
            Assert.IsTrue(tasks.ElementAt(2).ActiveInd == "N");
            Assert.IsTrue(tasks.ElementAt(0).ParentId == 0);
        }

        [Test]
        public void Get_Task_By_Id()
        {
            var taskId = 3;
            
            TaskEntity task = _mockRepository.GetTaskById(taskId);

            Assert.IsNotNull(task);
            Assert.IsTrue(task.TaskName == "Analysis");
            Assert.IsTrue(task.EndDate == new DateTime(2018, 08, 20));
            Assert.IsTrue(task.ActiveInd == "N");
            Assert.IsTrue(task.ParentId == 1);
        }

        [Test]
        public void Add_Task()
        {
            var taskId = _tasks.Count() + 1;
            var task = new TaskEntity
            {
                TaskId = taskId,
                TaskName = "Coding",
                ParentId = 1,
                ParentName = "Project Task",
                Priority = 2,
                ActiveInd = "Y",
                StartDate = new DateTime(2018, 04, 21),
                EndDate = new DateTime(2018, 08, 26)
            };
            
            _mockRepository.AddTask(task);
            Assert.IsTrue(_tasks.Count() == 4);
            TaskEntity testTask = _mockRepository.GetTaskById(taskId);
            Assert.IsNotNull(testTask);
            Assert.AreSame(testTask.GetType(), typeof(TaskEntity));
            Assert.AreEqual(taskId, testTask.TaskId);
        }

        [Test]
        public void Update_Task()
        {
            var taskId = 2;
            var task = new TaskEntity
            {
                TaskId = taskId,
                TaskName = "Test Task",
                ParentId = 1,
                Priority = 8,
                StartDate = new DateTime(2018, 04, 21),
                EndDate = new DateTime(2018, 08, 26)
            };

            _mockRepository.UpdateTask(task);

            var updatedTask = _mockRepository.GetTaskById(taskId);
            Assert.IsTrue(updatedTask.TaskName == "Test Task");
            Assert.IsTrue(task.Priority == 8);
            Assert.IsTrue(task.EndDate == new DateTime(2018, 08, 26));
        }

        [Test]
        public void End_Task()
        {
            var taskId = 1;
            
            _mockRepository.EndTask(taskId, "TestUser");

            var updatedTask = _mockRepository.GetTaskById(taskId);
            Assert.IsTrue(updatedTask.ActiveInd == "N");
            Assert.IsTrue(updatedTask.UpdatedBy == "TestUser");
        }

        [TearDown]
        public void CleanUp()
        {
            _tasks.Clear();
        }
    }
}
