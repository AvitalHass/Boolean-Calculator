"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var task_1 = require("../models/task");
var task_service_1 = require("../services/task.service");
var TaskListComponent = /** @class */ (function () {
    function TaskListComponent(taskService) {
        this.taskService = taskService;
        this.title = "hello angular title";
    }
    TaskListComponent.prototype.ngOnInit = function () {
        var _this = this;
        //this.taskService.getAllTasksPromiseSlowly().then(data => {
        //    this.taskList = data;
        //});
        //this.taskService.getAllTasksFromServer().subscribe(
        //    success => {
        //        this.taskList = success;
        //    },
        //    errors => {
        //        console.log("request failed");
        //    }
        //)
        this.taskService.getAllTasksFromServerToPromise().then(function (data) {
            _this.taskList = data;
        });
    };
    TaskListComponent.prototype.removeTask = function (task) {
        var index = this.taskList.findIndex(function (e) { return e == task; });
        this.taskList.splice(index, 1);
    };
    TaskListComponent.prototype.selectTask = function (task) {
        this.currentTask = task;
    };
    TaskListComponent.prototype.addNewTask = function () {
        this.currentTask = new task_1.Task();
        //this.taskList.push(this.currentTask);
    };
    TaskListComponent.prototype.saveTaskToList = function (taskToSave) {
        taskToSave.id = this.taskList.length + 1;
        this.taskList.push(taskToSave);
    };
    TaskListComponent.prototype.saveAllTaskToServer = function () {
        //this.taskService.saveAllTasksToServer(this.taskList).subscribe(
        //    success => {
        //        if (success)
        //            alert("save success!");
        //        else
        //            alert("save faild from success");
        //    },
        //    error => {
        //        console.log(error);
        //        alert("save faild!");
        //    }
        //)
        this.taskService.saveAllTasksToServerToPromise(this.taskList).then(function (data) {
            if (data)
                alert("save success!");
            else
                alert("save faild from success");
        });
    };
    TaskListComponent = __decorate([
        core_1.Component({
            templateUrl: "./src/app/components/task-list.component.html",
            selector: "task-list"
        }),
        __metadata("design:paramtypes", [task_service_1.TaskService])
    ], TaskListComponent);
    return TaskListComponent;
}());
exports.TaskListComponent = TaskListComponent;
//# sourceMappingURL=task-list.component.js.map