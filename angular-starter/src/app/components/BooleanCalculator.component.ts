import { Component, Input, Output, AfterViewInit, ViewChild, ElementRef, OnInit } from "@angular/core"
import { service } from "../services/service"
import { Expression } from "../models/Expression"
import { twoEx } from "../models/twoEx"
import { MatTableDataSource } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogValidation } from './DialogValidation.component'
import "rxjs/add/operator/map"
import { Observable } from "rxjs/Observable"
import { ObjectToTruthTable } from "../models/ObjectToTruthTable"
import { Axiom } from "../models/Axiom"
import { AxiomList } from "../components/axiomList.component"
@Component({
    selector: 'my-BoolianColculator',
    templateUrl: "./src/app/components/BooleanCalculator.component.html"
})

export class BooleanCalculator   /*AfterViewInit*/ {
    //@ViewChild("xx", { read: ElementRef }) tref: ElementRef;

    ex: string = "";
    expression: Expression;
    expression2: Expression;
    identityResult: boolean;
    logicGateDegel: boolean;
    truthDegel: boolean;
    pressDegel: boolean;
    booleanIdentutyDegel: boolean;
    axiomListDegel: boolean;
    truthTable: ObjectToTruthTable ;
    reduceLevels: string[];
    axiomList: Axiom[];
    logicGate: string;
    isValid: boolean;
    fReduceLevels: boolean;
    x: number;
    reduceExpression: string;
    exToReduce: Expression;
    exToSend: twoEx;
    arrOpnds: boolean[];
    validationMessage: string
    identityMessage: string
    notIdentityMessage: string
    nullExpressionMessage: string
    lstEnableOpands: string[];
    aFlag: boolean;
    bFlag: boolean;
    cFlag: boolean;
    xFlag: boolean;
    yFlag: boolean;
    zFlag: boolean;
    wFlag: boolean;

    SVG: string = "<svg width='190' height='160'><path d='M10 10 h 25 C 45 20,  45 40, 35 50 h -25 ' stroke='black' fill='transparent' /> </svg>";

    constructor(private service: service, public dialog: MatDialog) {
        this.expression = new Expression();
        this.expression2 = new Expression();
        this.expression.expression = "";
        this.expression2.expression = "";
        this.logicGate = "";
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.booleanIdentutyDegel = false;
        this.isValid = false;
        this.fReduceLevels = false;
        this.axiomListDegel = false;
        this.truthTable = new ObjectToTruthTable();
        this.reduceExpression = "";
        this.exToReduce = new Expression();
        this.pressDegel = false;
        this.validationMessage = "הביטוי שהוכנס אינו תקין";
        this.identityMessage = "הביטויים זהים";
        this.notIdentityMessage = "הביטויים אינם זהים";
        this.nullExpressionMessage = "לא הוכנס ביטוי";
        this.lstEnableOpands = new Array<string>();
        this.aFlag = true;
        this.bFlag = true;
        this.cFlag = true;
        this.xFlag = true;
        this.yFlag = true;
        this.zFlag = true;
        this.wFlag = true;
    }


    openValidationDialog(): void {
        let dialogRef = this.dialog.open(DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.validationMessage }
        });
    }

    openIdentityDialog(): void {
        let dialogRef = this.dialog.open(DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.identityMessage }
        });
    }
    openNullExpressionDialog(): void {
        let dialogRef = this.dialog.open(DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.nullExpressionMessage }
        });
    }
    openNotIdentityDialog(): void {
        let dialogRef = this.dialog.open(DialogValidation, {
            width: '250px',
            data: { dialogMessage: this.notIdentityMessage }
        });
    }

    sendToReduce()
    {
        if (this.booleanIdentutyDegel == false) {
        if (this.expression.expression == "")
            this.openNullExpressionDialog();
        else {
            this.checkValidation(this.expression).subscribe(d => {             
                this.truthDegel = false;
                this.logicGateDegel = false;
                this.axiomListDegel = false;
                console.log(this.isValid);
                this.exToReduce = this.expression;
                if (this.isValid) {
                    this.isValid = false;
                    this.service.sendExpression(this.exToReduce).subscribe(
                        data => {                            
                            if (data) {
                                this.fReduceLevels = true;
                                console.log(data);
                                console.log(typeof (data));
                                this.reduceLevels = data;
                                this.reduceExpression = this.reduceLevels[this.reduceLevels.length - 1];
                            }
                            else {
                                console.log("נכשך בשליחה לצמצום :(");
                            }
                        },
                        errors => {
                            console.log("request failed");
                        });
                }                
            });
        }}
    }

    pressEx2() {
        this.pressDegel = true;
    }
    pressEx1() {
        this.pressDegel = false
    }

    writeToExpression(str) {
        if (this.truthDegel || this.logicGateDegel || this.fReduceLevels)
            this.deleteAll();
        if (!this.pressDegel) {
            this.expression.expression = this.expression.expression + str;     
        }
        else
            this.expression2.expression = this.expression2.expression + str;
    }
    deleteAll() {
        if (!this.pressDegel)
            this.expression.expression = "";
        else
            this.expression2.expression = "";
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
    }
    deleteone() {
        if (!this.pressDegel)
            this.expression.expression = this.expression.expression.substr(0, this.expression.expression.length - 1);
        else
            this.expression2.expression = this.expression2.expression.substr(0, this.expression2.expression.length - 1);
        this.truthDegel = false;
        this.logicGateDegel = false;
        this.fReduceLevels = false;
        this.axiomListDegel = false;
    }

    checkValidation(ex: Expression): Observable<boolean> {
        return this.service.validation(ex).map(
            data => {
                if (data) {
                    console.log(data);
                    console.log(typeof (data));
                    this.isValid = true;
                    return data;

                }
                else {
                    this.openValidationDialog();
                    console.log("לא תקין");
                    this.isValid = data;
                    return data;
                }
            },
            errors => {
                console.log("request failed");
            });
    }

    sendToTruthTable() {
        if (this.booleanIdentutyDegel == false) {
            if (this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(d => {
                    this.fReduceLevels = false;
                    this.logicGateDegel = false;
                    this.axiomListDegel = false;
                    
                    if (this.isValid) {
                        this.isValid = false;
                        this.service.ExpressionToTruthTable(this.expression).subscribe(
                            data => {                              
                                if (data) {                                   
                                    console.log(data);
                                    debugger;
                                    console.log(typeof (data));
                                    this.truthTable = data;
                                    this.truthDegel = true;
                                }
                                else {
                                    console.log("נכשל טבלת אמת :(");
                                }
                            },
                            errors => {
                                console.log("request failed");
                            });
                    }
                });
            }
        }
    }
    sendToLogicGate() {
        if (this.booleanIdentutyDegel == false) {
            if (this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(d => {
                    this.fReduceLevels = false;
                    this.truthDegel = false;
                    this.axiomListDegel = false;
                    if (this.isValid) {
                        this.isValid = false;
                        this.service.ExpressionToLogicGate(this.expression).subscribe(
                            data => {
                                if (data) {
                                    console.log(data);
                                    console.log(typeof (data));
                                    this.logicGate = data;
                                    this.logicGateDegel = true;
                                }
                                else {
                                    console.log("נכשל בשער לוגי :(");
                                }
                            },
                            errors => {
                                console.log("request failed");
                            });
                    }
                });
            }
        }
    }
    BooleanIdentity() {
        if (this.expression.expression == "")
            this.openNullExpressionDialog();
        else {
            this.checkValidation(this.expression).subscribe(d => {
                if (this.isValid) {
                    this.isValid = false;
                    for (var i = 0; i < this.expression.expression.length; i++) {
                        switch (this.expression.expression[i]) {
                            case "a":
                                this.aFlag = false;
                                break;
                            case "b":
                                this.bFlag = false;
                                break;
                            case "c":
                                this.cFlag = false;
                                break;
                            case "x":
                                this.xFlag = false;
                                break;
                            case "y":
                                this.yFlag = false;
                                break;
                            case "z":
                                this.zFlag = false;
                                break;
                            case "w":
                                this.wFlag = false;
                                break;
                        }
                    }
                    this.axiomListDegel = false;
                    this.fReduceLevels = false;
                    this.booleanIdentutyDegel = true;
                    this.logicGateDegel = false;
                    this.truthDegel = false;
                    this.pressDegel = true;
                    console.log(this.booleanIdentutyDegel);
                }
            }
            
       ) };
    }

    sendToBooleanIdentity() {
        if (this.booleanIdentutyDegel == true) {
            if (this.expression2.expression == "" || this.expression.expression == "")
                this.openNullExpressionDialog();
            else {
                this.checkValidation(this.expression).subscribe(d => {
                    if (this.isValid) {
                        this.isValid = false;
                        this.checkValidation(this.expression2).subscribe(d => {
                            if (this.isValid) {
                                this.isValid = false;
                                this.exToSend = new twoEx(this.expression, this.expression2)
                                this.service.booleanIdentity(this.exToSend).subscribe(
                                    data => {
                                        if (data) {
                                            console.log(data);
                                            console.log(typeof (data));
                                            this.identityResult = data;
                                            this.openIdentityDialog();
                                        }
                                        else {
                                            this.openNotIdentityDialog();
                                            console.log("נכשל :(");
                                        }
                                    },
                                    errors => {
                                        console.log("request failed");
                                    });
                            }
                        });
                    }
                });
            }
        }
    }
    closeBooleanIdentity() {
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
        this.pressDegel = false
    }

    openAxiomList(): void{
        console.log("44444", this.axiomList)
        let dialogRef = this.dialog.open(AxiomList,
            {
                height: '600px', width: '600px',
                disableClose: false,
                data: { flag: this.axiomList }
            });     
    }

    axiomsList() {
        if (this.booleanIdentutyDegel == false) {
            this.axiomListDegel = true;
            this.service.axiomList().subscribe(
                data => {
                    debugger;
                    if (data) {
                        console.log(data);
                        console.log(typeof (data));
                        this.axiomList = data;
                        this.openAxiomList();
                    }
                    else {
                        console.log("נכשל :(");
                    }
                },
                errors => {
                    console.log("request failed");
                });
        }
    }
}