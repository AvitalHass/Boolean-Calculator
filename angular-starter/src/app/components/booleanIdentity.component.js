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
var Expression_1 = require("../models/Expression");
var BooleanIdentity = /** @class */ (function () {
    function BooleanIdentity(service) {
        this.service = service;
        this.expression = new Expression_1.Expression();
        this.expression.expression = "";
    }
    BooleanIdentity.prototype.writeToExpression = function (str) {
        this.expression.expression = this.expression.expression + str;
    };
    BooleanIdentity.prototype.sendTobooleanIdentity = function () {
        var _this = this;
        this.service.sendExpression(this.expression).subscribe(function (data) {
            if (data) {
                console.log(data);
                console.log(typeof (data));
                _this.expression.expression = data.toString();
            }
            else {
                console.log("נכשל :(");
            }
        }, function (errors) {
            console.log("request failed");
        });
    };
    BooleanIdentity = __decorate([
        core_1.Component({
            selector: 'my-booleanIdentity',
            templateUrl: "./src/app/components/booleanIdentity.component.html"
        }),
        __metadata("design:paramtypes", [service_1.service])
    ], BooleanIdentity);
    return BooleanIdentity;
}());
exports.BooleanIdentity = BooleanIdentity;
//# sourceMappingURL=booleanIdentity.component.js.map