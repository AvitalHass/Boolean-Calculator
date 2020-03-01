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
var ObjectToTruthTable_1 = require("../models/ObjectToTruthTable");
var Object2ToTruthTable_1 = require("../models/Object2ToTruthTable");
var TruthTable = /** @class */ (function () {
    //lstOperands = this.truthTable.lstOperands
    //valuesOfOpands = this.truthTable.valuesOfOpands
    function TruthTable() {
        this.lstOperands = new Array();
        this.afterInit = false;
        debugger;
    }
    TruthTable.prototype.ngOnInit = function () {
        debugger;
        this.arrOptionsAndExpression = new Array();
        for (var i = 0; i < this.truthTable.arryOptions.length; i++) {
            for (var j = 0; j < this.truthTable.lstOperands.length; j++) {
                this.lstOperands.push(this.truthTable.valuesOfOperands[j][i]);
            }
            debugger;
            this.arrOptionsAndExpression[i] = new Object2ToTruthTable_1.Object2ToTruthTable(this.lstOperands, this.truthTable.arryOptions[i]);
            this.lstOperands = new Array();
        }
        this.afterInit = true;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", ObjectToTruthTable_1.ObjectToTruthTable)
    ], TruthTable.prototype, "truthTable", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], TruthTable.prototype, "expression", void 0);
    TruthTable = __decorate([
        core_1.Component({
            selector: 'my-truthTable',
            templateUrl: "./src/app/components/truthTable.component.html"
        }),
        __metadata("design:paramtypes", [])
    ], TruthTable);
    return TruthTable;
}());
exports.TruthTable = TruthTable;
//# sourceMappingURL=truthTable.component.js.map