import { Component, Input, Output, NgModule,Inject } from "@angular/core"
import { MaterialModule } from "../material.module"
import { FormsModule, ReactiveFormsModule } from "@angular/forms"
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'my-dialogValidation',
    templateUrl: "./src/app/components/DialogValidation.component.html"
})
export class DialogValidation {

    constructor(
        public dialogRef: MatDialogRef<DialogValidation>,
        @Inject(MAT_DIALOG_DATA) public data: any) { }

    onNoClick(): void {
        this.dialogRef.close();
    }

}
