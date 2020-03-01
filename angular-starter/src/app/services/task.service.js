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
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/toPromise");
var TASKS = [{ id: 1, name: "task1" }, { id: 2, name: "task2" }, { id: 3, name: "task3" }];
var TaskService = /** @class */ (function () {
    function TaskService(http) {
        this.http = http;
    }
    TaskService.prototype.getAllTasks = function () {
        return TASKS;
    };
    TaskService.prototype.getAllTasksPromise = function () {
        return Promise.resolve(TASKS);
    };
    TaskService.prototype.getAllTasksPromiseSlowly = function () {
        return new Promise(function (resolve) {
            setTimeout(function () { return resolve(TASKS); }, 5000);
        });
    };
    TaskService.prototype.getAllTasksFromServer = function () {
        return this.http.get("/api/Task/Get").map(function (data) { return data.json(); });
    };
    TaskService.prototype.getAllTasksFromServerToPromise = function () {
        return this.http.get("/api/Task/Get").toPromise()
            .then(function (data) { return data.json(); })
            .catch(function (err) { return []; });
    };
    TaskService.prototype.saveAllTasksToServer = function (tasksToSave) {
        return this.http.post("/api/Task/Post", tasksToSave)
            .map(function (data) { return data.json(); })
            .catch(function (err) {
            var errMsg = "post faild";
            if (err instanceof Response) {
                errMsg = err.status + "==" + err.statusText;
            }
            ;
            return Observable_1.Observable.throw(false);
        });
    };
    TaskService.prototype.saveAllTasksToServerToPromise = function (tasksToSave) {
        return this.http.post("/api/Task/Post", tasksToSave).toPromise()
            .then(function (data) { return true; })
            .catch(function (err) { return false; });
    };
    TaskService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], TaskService);
    return TaskService;
}());
exports.TaskService = TaskService;
//# sourceMappingURL=task.service.js.map