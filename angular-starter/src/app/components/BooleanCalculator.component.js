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
var material_1 = require("@angular/material");
var DialogValidation_component_1 = require("./DialogValidation.component");
require("rxjs/add/operator/map");
var ObjectToTruthTable_1 = require("../models/ObjectToTruthTable");
var axiomList_component_1 = require("../components/axiomList.component");
var BooleanCalculator = /** @class */ (function () {
    function BooleanCalculator(service, dialog) {
        this.service = service;
        this.dialog = dialog;
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
        this.axiomListDegel = false;
        this.truthTable = new ObjectToTruthTable_1.ObjectToTruthTable();
        this.reduceExpression = "";
        this.exToReduce = new Expression_1.Expression();
        this.pressDegel = false;
        this.validationMessage = "הביטוי שהוכנס אינו תקין";
        this.identityMessage = "הביטויים זהים";
        this.notIdentityMessage = "הביטויים אינם זהים";
        this.nullExpressionMessage = "לא הוכנס ביטוי";
        this.lstEnableOpands = new Array();
        this.aFlag = true;
        this.bFlag = true;
        this.cFlag = true;
        this.xFlag = true;
        this.yFlag = true;
        this.zFlag = true;
        this.wFlag = true;
    }
    BooleanCalculator.prototype.openValidationDialog = function () {
        var dialogRef = this.dialog.open(DialogValidation_component_1.DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.validationMessage }
        });
    };
    BooleanCalculator.prototype.openIdentityDialog = function () {
        var dialogRef = this.dialog.open(DialogValidation_component_1.DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.identityMessage }
        });
    };
    BooleanCalculator.prototype.openNullExpressionDialog = function () {
        var dialogRef = this.dialog.open(DialogValidation_component_1.DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.nullExpressionMessage }
        });
    };
    BooleanCalculator.prototype.openNotIdentityDialog = function () {
        var dialogRef = this.dialog.open(DialogValidation_component_1.DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.notIdentityMessage }
        });
    };
    BooleanCalculator.prototype.sendToReduce = function () {
        var _this = this;
        if (this.booleanIdentutyDegel == false) {
            if (this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(function (d) {
                    _this.truthDegel = false;
                    _this.logicGateDegel = false;
                    _this.axiomListDegel = false;
                    console.log(_this.isValid);
                    _this.exToReduce = _this.expression;
                    if (_this.isValid) {
                        _this.isValid = false;
                        _this.service.sendExpression(_this.exToReduce).subscribe(function (data) {
                            if (data) {
                                _this.fReduceLevels = true;
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
                });
            }
        }
    };
    BooleanCalculator.prototype.pressEx2 = function () {
        this.pressDegel = true;
    };
    BooleanCalculator.prototype.pressEx1 = function () {
        this.pressDegel = false;
    };
    BooleanCalculator.prototype.writeToExpression = function (str) {
        if (this.truthDegel || this.logicGateDegel || this.fReduceLevels)
            this.deleteAll();
        if (!this.pressDegel) {
            this.expression.expression = this.expression.expression + str;
        }
        else
            this.expression2.expression = this.expression2.expression + str;
    };
    BooleanCalculator.prototype.deleteAll = function () {
        if (!this.pressDegel)
            this.expression.expression = "";
        else
            this.expression2.expression = "";
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
    };
    BooleanCalculator.prototype.deleteone = function () {
        if (!this.pressDegel)
            this.expression.expression = this.expression.expression.substr(0, this.expression.expression.length - 1);
        else
            this.expression2.expression = this.expression2.expression.substr(0, this.expression2.expression.length - 1);
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
        this.axiomListDegel = false;
    };
    BooleanCalculator.prototype.checkValidation = function (ex) {
        var _this = this;
        return this.service.validation(ex).map(function (data) {
            if (data) {
                console.log(data);
                console.log(typeof (data));
                _this.isValid = true;
                return data;
            }
            else {
                _this.openValidationDialog();
                console.log("לא תקין");
                _this.isValid = data;
                return data;
            }
        }, function (errors) {
            console.log("request failed");
        });
    };
    BooleanCalculator.prototype.sendToTruthTable = function () {
        var _this = this;
        if (this.booleanIdentutyDegel == false) {
            if (this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(function (d) {
                    _this.fReduceLevels = false;
                    _this.logicGateDegel = false;
                    _this.axiomListDegel = false;
                    if (_this.isValid) {
                        _this.isValid = false;
                        _this.service.ExpressionToTruthTable(_this.expression).subscribe(function (data) {
                            if (data) {
                                console.log(data);
                                debugger;
                                console.log(typeof (data));
                                _this.truthTable = data;
                                _this.truthDegel = true;
                            }
                            else {
                                console.log("נכשל טבלת אמת :(");
                            }
                        }, function (errors) {
                            console.log("request failed");
                        });
                    }
                });
            }
        }
    };
    BooleanCalculator.prototype.sendToLogicGate = function () {
        var _this = this;
        if (this.booleanIdentutyDegel == false) {
            if (this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(function (d) {
                    _this.fReduceLevels = false;
                    _this.truthDegel = false;
                    _this.axiomListDegel = false;
                    if (_this.isValid) {
                        _this.isValid = false;
                        _this.service.ExpressionToLogicGate(_this.expression).subscribe(function (data) {
                            if (data) {
                                console.log(data);
                                console.log(typeof (data));
                                _this.logicGate = data;
                                _this.logicGateDegel = true;
                            }
                            else {
                                console.log("נכשל בשער לוגי :(");
                            }
                        }, function (errors) {
                            console.log("request failed");
                        });
                    }
                });
            }
        }
    };
    BooleanCalculator.prototype.BooleanIdentity = function () {
        var _this = this;
        if (this.expression.expression == "")
            this.openNullExpressionDialog();
        else {
            this.checkValidation(this.expression).subscribe(function (d) {
                if (_this.isValid) {
                    _this.isValid = false;
                    for (var i = 0; i < _this.expression.expression.length; i++) {
                        switch (_this.expression.expression[i]) {
                            case "a":
                                _this.aFlag = false;
                                break;
                            case "b":
                                _this.bFlag = false;
                                break;
                            case "c":
                                _this.cFlag = false;
                                break;
                            case "x":
                                _this.xFlag = false;
                                break;
                            case "y":
                                _this.yFlag = false;
                                break;
                            case "z":
                                _this.zFlag = false;
                                break;
                            case "w":
                                _this.wFlag = false;
                                break;
                        }
                    }
                    _this.axiomListDegel = false;
                    _this.fReduceLevels = false;
                    _this.booleanIdentutyDegel = true;
                    _this.logicGateDegel = false;
                    _this.truthDegel = false;
                    _this.pressDegel = true;
                    console.log(_this.booleanIdentutyDegel);
                }
            });
        }
        ;
    };
    BooleanCalculator.prototype.sendToBooleanIdentity = function () {
        var _this = this;
        if (this.booleanIdentutyDegel == true) {
            if (this.expression2.expression == "" || this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(function (d) {
                    if (_this.isValid) {
                        _this.isValid = false;
                        _this.checkValidation(_this.expression2).subscribe(function (d) {
                            if (_this.isValid) {
                                _this.isValid = false;
                                _this.exToSend = new twoEx_1.twoEx(_this.expression, _this.expression2);
                                _this.service.booleanIdentity(_this.exToSend).subscribe(function (data) {
                                    if (data) {
                                        console.log(data);
                                        console.log(typeof (data));
                                        _this.identityResult = data;
                                        _this.openIdentityDialog();
                                    }
                                    else {
                                        _this.openNotIdentityDialog();
                                        console.log("נכשל :(");
                                    }
                                }, function (errors) {
                                    console.log("request failed");
                                });
                            }
                        });
                    }
                });
            }
        }
    };
    BooleanCalculator.prototype.closeBooleanIdentity = function () {
        this.booleanIdentutyDegel = false;
        // this.expression.expression = "";
        this.expression2.expression = "";
        this.aFlag = true;
        this.bFlag = true;
        this.cFlag = true;
        this.xFlag = true;
        this.yFlag = true;
        this.zFlag = true;
        this.wFlag = true;
        this.pressDegel = false;
    };
    BooleanCalculator.prototype.openAxiomList = function () {
        console.log("44444", this.axiomList);
        var dialogRef = this.dialog.open(axiomList_component_1.AxiomList, {
            height: '600px', width: '600px',
            disableClose: false,
            data: { flag: this.axiomList }
        });
    };
    BooleanCalculator.prototype.axiomsList = function () {
        var _this = this;
        if (this.booleanIdentutyDegel == false) {
            this.axiomListDegel = true;
            this.service.axiomList().subscribe(function (data) {
                debugger;
                if (data) {
                    console.log(data);
                    console.log(typeof (data));
                    _this.axiomList = data;
                    _this.openAxiomList();
                }
                else {
                    console.log("נכשל :(");
                }
            }, function (errors) {
                console.log("request failed");
            });
        }
    };
    BooleanCalculator = __decorate([
        core_1.Component({
            selector: 'my-BoolianColculator',
            templateUrl: "./src/app/components/BooleanCalculator.component.html"
        }),
        __metadata("design:paramtypes", [service_1.service, material_1.MatDialog])
    ], BooleanCalculator);
    return BooleanCalculator;
}());
exports.BooleanCalculator = BooleanCalculator;
//# sourceMappingURL=BooleanCalculator.component.js.map