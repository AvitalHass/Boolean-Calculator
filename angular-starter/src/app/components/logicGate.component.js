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
var LogicGate = /** @class */ (function () {
    function LogicGate(service) {
        this.service = service;
        this.svg = "";
    }
    LogicGate.prototype.ngOnInit = function () {
        this.tref.nativeElement.innerHTML = this.svg;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], LogicGate.prototype, "svg", void 0);
    __decorate([
        core_1.ViewChild("SVG", { read: core_1.ElementRef }),
        __metadata("design:type", core_1.ElementRef)
    ], LogicGate.prototype, "tref", void 0);
    LogicGate = __decorate([
        core_1.Component({
            selector: 'my-logicGate',
            templateUrl: "./src/app/components/logicGate.component.html"
        }),
        __metadata("design:paramtypes", [service_1.service])
    ], LogicGate);
    return LogicGate;
}());
exports.LogicGate = LogicGate;
//# sourceMappingURL=logicGate.component.js.map