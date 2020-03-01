import { Component, Input, Output, NgModule } from "@angular/core"
import { service } from "../services/service"
import { MaterialModule } from "../material.module"
import { Expression } from "../models/Expression"

@Component({
    selector: 'my-booleanIdentity',
    templateUrl: "./src/app/components/booleanIdentity.component.html"
})

export class BooleanIdentity {
    expression: Expression;

    constructor(private service: service) {
        this.expression = new Expression();
        this.expression.expression = "";
    }
    writeToExpression(str) {
        this.expression.expression = this.expression.expression + str;
    }
    sendTobooleanIdentity() {
        this.service.sendExpression(this.expression).subscribe(
            data => {
                if (data) {
                    console.log(data);
                    console.log(typeof (data));
                    this.expression.expression = data.toString();
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