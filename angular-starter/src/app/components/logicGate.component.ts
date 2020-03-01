import { Component, Input, Output, AfterViewInit, ViewChild, ElementRef,OnInit } from "@angular/core"
import { service } from "../services/service"
import { Expression } from "../models/Expression"


@Component({
    selector: 'my-logicGate',
    templateUrl: "./src/app/components/logicGate.component.html"
})

export class LogicGate implements OnInit{
    @Input()
    svg: string;

    constructor(private service: service) {
        this.svg = "";
    }

    @ViewChild("SVG", { read: ElementRef }) tref: ElementRef;

    ngOnInit() {
        this.tref.nativeElement.innerHTML = this.svg;
    }

}
