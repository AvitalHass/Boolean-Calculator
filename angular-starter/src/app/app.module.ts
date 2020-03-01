import { NgModule}from "@angular/core"
import { AppComponent}from "./app.component"
import { FormsModule} from "@angular/forms"
import { BrowserModule } from '@angular/platform-browser'
import { service } from "../app/services/service"
import { MaterialModule } from "./material.module"
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpModule } from '@angular/http'
import { DialogValidation } from "../app/components/DialogValidation.component"
import { BooleanCalculator } from "../app/components/BooleanCalculator.component"
import { LogicGate } from "../app/components/logicGate.component"
import { ReduceLevels } from "../app/components/ReduceLevels.component"
import { TruthTable } from "../app/components/truthTable.component"
import { BooleanIdentity } from "../app/components/booleanIdentity.component"
import { AxiomList } from "../app/components/axiomList.component"
import { Routes, RouterModule } from "@angular/router"
import { MatDialogModule, MatDialogRef } from '@angular/material'


import 'node_modules/hammerjs/hammer.js';
//import { ElementRef, TemplateRef, ViewRef, ComponentRef, ViewContainerRef } from "@angular/"

const ROUTES: Routes = [
    { path: "homePage", component: DialogValidation },
    { path: "BooleanCalculator", component: BooleanCalculator},
    { path: "logicGate", component: LogicGate },
    { path: "truthTable", component: TruthTable },
    { path: "BooleanIdentity", component: BooleanIdentity }
];



@NgModule({
    entryComponents: [AxiomList],
    imports: [MatDialogModule, BrowserModule, FormsModule, MaterialModule, BrowserAnimationsModule, HttpModule, RouterModule.forRoot(ROUTES, { useHash: true })],//all modul that need
    declarations: [AppComponent, BooleanCalculator, TruthTable, LogicGate, BooleanIdentity, DialogValidation, ReduceLevels, AxiomList],//all the component that need
    bootstrap: [AppComponent],//the first component
    providers: [service]
    
})
    

export class AppModule
{
    constructor() { }
}