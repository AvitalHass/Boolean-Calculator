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
var twoEx_1 = require("../models/twoEx");
var BoolianColculator = /** @class */ (function () {
    function BoolianColculator(service) {
        this.service = service;
        //@ViewChild("xx", { read: ElementRef }) tref: ElementRef;
        this.ex = "";
        this.SVG = "<svg width='190' height='160'><path d='M10 10 h 25 C 45 20,  45 40, 35 50 h -25 ' stroke='black' fill='transparent' /> </svg>";
        this.expression = new Expression_1.Expression();
        this.expression2 = new Expression_1.Expression();
        this.expression.expression = "";
        this.expression2.expression = "";
        this.logicGate = "";
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.booleanIdentutyDegel = false;
        this.isValid = false;
        this.fReduceLevels = false;
        this.reduceExpression = "";
        this.exToReduce = new Expression_1.Expression();
        this.pressDegel = false;
        //nativeElement.inerrhtml
    }
    BoolianColculator.prototype.sendToReduce = function () {
        var _this = this;
        this.fReduceLevels = true;
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.checkValidation(this.expression);
        console.log(this.isValid);
        this.exToReduce = this.expression;
        if (this.isValid) {
            this.isValid = false;
            this.service.sendExpression(this.exToReduce).subscribe(function (data) {
                if (data) {
                    console.log(data);
                    console.log(typeof (data));
                    _this.reduceLevels = data;
                    _this.reduceExpression = _this.reduceLevels[_this.reduceLevels.length - 1];
                }
                else {
                    console.log("נכשך בשליחה לצמצום :(");
                }
            }, function (errors) {
                console.log("request failed");
            });
        }
    };
    BoolianColculator.prototype.pressEx2 = function () {
        this.pressDegel = true;
    };
    BoolianColculator.prototype.pressEx1 = function () {
        this.pressDegel = false;
    };
    BoolianColculator.prototype.writeToExpression = function (str) {
        if (!this.pressDegel)
            this.expression.expression = this.expression.expression + str;
        else
            this.expression2.expression = this.expression2.expression + str;
    };
    BoolianColculator.prototype.deleteAll = function () {
        if (!this.pressDegel)
            this.expression.expression = "";
        else
            this.expression2.expression = "";
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
    };
    BoolianColculator.prototype.deleteone = function () {
        if (!this.pressDegel)
            this.expression.expression = this.expression.expression.substr(0, this.expression.expression.length - 1);
        else
            this.expression2.expression = this.expression2.expression.substr(0, this.expression2.expression.length - 1);
        this.expression.expression = this.expression.expression.substr(0, this.expression.expression.length - 1);
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
    };
    BoolianColculator.prototype.checkValidation = function (ex) {
        var _this = this;
        this.service.validation(ex).subscribe(function (data) {
            if (data) {
                console.log(data);
                console.log(typeof (data));
                _this.isValid = true;
            }
            else {
                new alert("הביטוי לא תקין");
                console.log("לא תקין");
                _this.isValid = data;
            }
        }, function (errors) {
            console.log("request failed");
        });
        ;
    };
    BoolianColculator.prototype.sendToTruthTable = function () {
        var _this = this;
        this.fReduceLevels = false;
        this.logicGateDegel = false;
        this.truthDegel = true;
        this.checkValidation(this.expression);
        if (this.isValid) {
            this.isValid = false;
            this.service.ExpressionToTruthTable(this.expression).subscribe(function (data) {
                if (data) {
                    console.log(data);
                    console.log(typeof (data));
                    _this.truthTable = data;
                }
                else {
                    console.log("נכשל טבלת אמת :(");
                }
            }, function (errors) {
                console.log("request failed");
            });
        }
    };
    BoolianColculator.prototype.sendToLogicGate = function () {
        var _this = this;
        this.fReduceLevels = false;
        this.truthDegel = false;
        this.checkValidation(this.expression);
        this.service.ExpressionToLogicGate(this.expression).subscribe(function (data) {
            if (data) {
                console.log(data);
                console.log(typeof (data));
                _this.logicGate = data;
                _this.logicGateDegel = true;
                debugger;
            }
            else {
                console.log("נכשל בשער לוגי :(");
            }
        }, function (errors) {
            console.log("request failed");
        });
    };
    BoolianColculator.prototype.BooleanIdentity = function () {
        this.fReduceLevels = false;
        this.booleanIdentutyDegel = true;
        this.logicGateDegel = false;
        this.truthDegel = false;
        this.pressDegel = true;
        console.log(this.booleanIdentutyDegel);
    };
    BoolianColculator.prototype.sendToBooleanIdentity = function () {
        var _this = this;
        this.checkValidation(this.expression);
        this.checkValidation(this.expression2);
        this.exToSend = new twoEx_1.twoEx(this.expression, this.expression2);
        this.service.booleanIdentity(this.exToSend).subscribe(function (data) {
            if (data) {
                console.log(data);
                console.log(typeof (data));
                _this.identityResult = data;
            }
            else {
                console.log("נכשל :(");
            }
        }, function (errors) {
            console.log("request failed");
        });
    };
    BoolianColculator.prototype.closeBooleanIdentity = function () {
        this.booleanIdentutyDegel = false;
        this.expression2.expression = "";
    };
    BoolianColculator = __decorate([
        core_1.Component({
            selector: 'my-BoolianColculator',
            templateUrl: "./src/app/components/BoolianColculator.component.html"
        }),
        __metadata("design:paramtypes", [service_1.service])
    ], BoolianColculator);
    return BoolianColculator;
}());
exports.BoolianColculator = BoolianColculator;
//# sourceMappingURL=BoolianColculator.component.js.map