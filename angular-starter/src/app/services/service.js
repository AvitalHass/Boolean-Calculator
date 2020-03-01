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
require("rxjs/add/operator/map");
var service = /** @class */ (function () {
    function service(http) {
        this.http = http;
    }
    service.prototype.sendExpression = function (ex) {
        return this.http.post("api/reduce/", ex).map(function (data) { console.log(data); return data.json(); });
    };
    service.prototype.ExpressionToTruthTable = function (ex) {
        return this.http.post("api/truthTable/", ex).map(function (data) { console.log(data); return data.json(); });
    };
    service.prototype.ExpressionToLogicGate = function (ex) {
        return this.http.post("api/logicGate/", ex).map(function (data) { console.log(data); return data.json(); });
    };
    service.prototype.booleanIdentity = function (ex) {
        return this.http.post("/api/booleanIdentity/", ex).map(function (data) { console.log(data); return data.json(); });
    };
    service.prototype.validation = function (ex) {
        return this.http.post("api/validation/", ex).map(function (data) {
            console.log(data);
            return data.json();
        });
    };
    service.prototype.svg = function () {
        return this.http.get("api/svg/").map(function (data) { console.log(data); return data.json(); });
    };
    service.prototype.axiomList = function () {
        return this.http.post("/api/axiomList/", null).map(function (data) { console.log(data); return data.json(); });
    };
    service = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], service);
    return service;
}());
exports.service = service;
//# sourceMappingURL=service.js.map