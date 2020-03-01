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
var service_1 = require("../services/service");
var ReduceLevels = /** @class */ (function () {
    function ReduceLevels(service) {
        this.service = service;
    }
    __decorate([
        core_1.Input(),
        __metadata("design:type", Array)
    ], ReduceLevels.prototype, "reduceLevels", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], ReduceLevels.prototype, "reduceExpression", void 0);
    ReduceLevels = __decorate([
        core_1.Component({
            selector: 'my-reduceLevels',
            templateUrl: "./src/app/components/ReduceLevels.component.html"
        }),
        __metadata("design:paramtypes", [service_1.service])
    ], ReduceLevels);
    return ReduceLevels;
}());
exports.ReduceLevels = ReduceLevels;
//# sourceMappingURL=ReduceLevels.component.js.map