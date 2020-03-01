import { Component, Input, Output, AfterViewInit, ViewChild, ElementRef, OnInit, Inject } from "@angular/core"
import { service } from "../services/service"
import { Axiom } from "../models/Axiom"
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material'
  


@Component({
    selector: 'my-axiomList',
    templateUrl: "./src/app/components/axiomList.component.html"
})

export class AxiomList {    
         constructor(private service: service,
          private matDialogRef: MatDialogRef<AxiomList>,
          @Inject(MAT_DIALOG_DATA) public data: any) {
             debugger;        
              console.log("ddd", data)
    }



}
