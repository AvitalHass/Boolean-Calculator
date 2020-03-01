import { Component, Input, Output } from "@angular/core"
import { service } from "../services/service"
import { Expression } from "../models/Expression"


@Component({
    selector: 'my-reduceLevels',
    templateUrl: "./src/app/components/ReduceLevels.component.html"
})

export class ReduceLevels {
    @Input()
    reduceLevels: string[];
    @Input()
    reduceExpression: string;
    debugger;
    constructor(private service: service) {
    }



}

