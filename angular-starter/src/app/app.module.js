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
var app_component_1 = require("./app.component");
var forms_1 = require("@angular/forms");
var platform_browser_1 = require("@angular/platform-browser");
var service_1 = require("../app/services/service");
var material_module_1 = require("./material.module");
var animations_1 = require("@angular/platform-browser/animations");
var http_1 = require("@angular/http");
var DialogValidation_component_1 = require("../app/components/DialogValidation.component");
var BooleanCalculator_component_1 = require("../app/components/BooleanCalculator.component");
var logicGate_component_1 = require("../app/components/logicGate.component");
var ReduceLevels_component_1 = require("../app/components/ReduceLevels.component");
var truthTable_component_1 = require("../app/components/truthTable.component");
var booleanIdentity_component_1 = require("../app/components/booleanIdentity.component");
var axiomList_component_1 = require("../app/components/axiomList.component");
var router_1 = require("@angular/router");
var material_1 = require("@angular/material");
require("node_modules/hammerjs/hammer.js");
//import { ElementRef, TemplateRef, ViewRef, ComponentRef, ViewContainerRef } from "@angular/"
var ROUTES = [
    { path: "homePage", component: DialogValidation_component_1.DialogValidation },
    { path: "BooleanCalculator", component: BooleanCalculator_component_1.BooleanCalculator },
    { path: "logicGate", component: logicGate_component_1.LogicGate },
    { path: "truthTable", component: truthTable_component_1.TruthTable },
    { path: "BooleanIdentity", component: booleanIdentity_component_1.BooleanIdentity }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            entryComponents: [axiomList_component_1.AxiomList],
            imports: [material_1.MatDialogModule, platform_browser_1.BrowserModule, forms_1.FormsModule, material_module_1.MaterialModule, animations_1.BrowserAnimationsModule, http_1.HttpModule, router_1.RouterModule.forRoot(ROUTES, { useHash: true })],
            declarations: [app_component_1.AppComponent, BooleanCalculator_component_1.BooleanCalculator, truthTable_component_1.TruthTable, logicGate_component_1.LogicGate, booleanIdentity_component_1.BooleanIdentity, DialogValidation_component_1.DialogValidation, ReduceLevels_component_1.ReduceLevels, axiomList_component_1.AxiomList],
            bootstrap: [app_component_1.AppComponent],
            providers: [service_1.service]
        }),
        __metadata("design:paramtypes", [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map