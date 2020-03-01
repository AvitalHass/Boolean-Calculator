import { Component, Output, Input } from "@angular/core"
import { service } from "./services/service"
@Component(
    {
        selector: "my-app",
        templateUrl:"./src/app/app.component.html"      
        })
export class AppComponent 
{
    
    svgg: string;    
    logicGateDegel: boolean;
    constructor(private service: service) {
        this.svgg = " ";
    }
    
  
   
}
