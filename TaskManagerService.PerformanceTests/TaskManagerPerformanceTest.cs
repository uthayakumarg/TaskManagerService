using NBench;
using System.Collections.Generic;
using TaskManagerService.BusinessLayer;
using TaskManagerService.Entity;
using TaskManagerService.DataLayer;
using System;

namespace TaskManagerService.PerformanceTests
{
    public class TaskManagerPerformanceTest
    {
        private ITaskManagerBL _taskService;
        private TaskEntity _newTask;
        private TaskEntity _taskToBeUpdated;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            ITasksRepository _repo = new TaskManagerDataAccess();
            _taskService = new TaskManagerBL(_repo);
            _newTask = new TaskEntity { TaskId = 0, TaskName = "Coding", ParentId = 1, ParentName = "Project Task", Priority = 2, ActiveInd = "Y", StartDate = new DateTime(2018, 04, 21), EndDate = new DateTime(2018, 08, 26), CreatedBy = "PerfTest" };
            _taskToBeUpdated = new TaskEntity { TaskId = 1, TaskName = "Analysis", ParentId = 1, ParentName = "Project Task", Priority = 3, ActiveInd = "Y", StartDate = new DateTime(2018, 04, 18), EndDate = new DateTime(2018, 08, 20) };
        }

        [PerfBenchmark(NumberOfIterations =1, RunMode =RunMode.Throughput, TestMode =TestMode.Test, SkipWarmups =true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds =2000)]
        public void Get_All_Tasks_Elapsed_Time()
        {
            var result = _taskService.GetAllTasks();
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, RunTimeMilliseconds = 5000, SkipWarmups = true)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, ByteConstants.SixteenKb)]
        public void Get_All_Tasks_Memory_Consumed()
        {
            var result = _taskService.GetAllTasks();
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Add_Task_Elapsed_Time()
        {
            _taskService.AddTask(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, RunTimeMilliseconds = 5000, SkipWarmups = true)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, ByteConstants.SixteenKb)]
        public void Add_Task_Memory_Consumed()
        {
            _taskService.AddTask(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void Update_Task_Elapsed_Time()
        {
            _taskService.UpdateTask(_taskToBeUpdated);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, RunTimeMilliseconds = 5000, SkipWarmups = true)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, ByteConstants.SixteenKb)]
        public void Update_Task_Memory_Consumed()
        {
            _taskService.UpdateTask(_taskToBeUpdated);
        }


    }
}
