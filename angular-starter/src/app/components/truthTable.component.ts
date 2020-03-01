import { Component, Input, Output, OnInit, AfterViewInit } from "@angular/core"
import { service } from "../services/service"
import { Expression } from "../models/Expression"
import { ObjectToTruthTable } from "../models/ObjectToTruthTable"
import { Object2ToTruthTable } from "../models/Object2ToTruthTable";
@Component({
    selector: 'my-truthTable',
    templateUrl: "./src/app/components/truthTable.component.html"
})

export class TruthTable implements OnInit {
    @Input()
    truthTable: ObjectToTruthTable;
    @Input()
    expression: string;
    arrOptionsAndExpression: Array<Object2ToTruthTable>;
    lstOperands: Array<any> = new Array<CharacterData>();
    afterInit: boolean = false;
    //lstOperands = this.truthTable.lstOperands
    //valuesOfOpands = this.truthTable.valuesOfOpands
    constructor() {        
        debugger;
    }
    ngOnInit() {
        debugger;
        this.arrOptionsAndExpression = new Array<Object2ToTruthTable>();
        for (var i = 0; i < this.truthTable.arryOptions.length; i++) {
            for (var j = 0; j < this.truthTable.lstOperands.length; j++) {
                this.lstOperands.push(this.truthTable.valuesOfOperands[j][i]);
            }
            debugger;
            this.arrOptionsAndExpression[i] = new Object2ToTruthTable(this.lstOperands, this.truthTable.arryOptions[i]);
            this.lstOperands = new Array<CharacterData>();
        }
        this.afterInit = true;
    }
}

