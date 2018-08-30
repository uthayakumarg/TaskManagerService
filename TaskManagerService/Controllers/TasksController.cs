using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagerService.Entity;
using TaskManagerService.BusinessLayer;

namespace TaskManagerService.Controllers
{
    public class TasksController : ApiController
    {
        private ITaskManagerBL _tasksService;
        public TasksController(ITaskManagerBL service)
        {
            _tasksService = service;
        }

        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            var result = _tasksService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetTaskById(int taskId)
        {
            var result = _tasksService.GetTaskById(taskId);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody]TaskEntity taskModel)
        {
            _tasksService.AddTask(taskModel);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]TaskEntity taskModel)
        {
            _tasksService.UpdateTask(taskModel);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EndTask(int taskId, string userId)
        {
            _tasksService.EndTask(taskId, userId);
            return Ok();
        }
    }
}
