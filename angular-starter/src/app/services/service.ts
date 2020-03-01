import { Injectable } from "@angular/core"
import { Http } from "@angular/http"
import { Observable } from "rxjs/Observable"
import "rxjs/add/operator/map"
import { Expression } from "../models/Expression"
import { twoEx } from "../models/twoEx"
import { ObjectToTruthTable } from "../models/ObjectToTruthTable"
import { Axiom } from "../models/Axiom"

@Injectable()
export class service {
    constructor(private http: Http) { }

    sendExpression(ex: Expression): Observable<string[]> {
        return this.http.post("api/reduce/", ex).map(data => { console.log(data); return data.json() });
    }
    ExpressionToTruthTable(ex: Expression): Observable<ObjectToTruthTable> {
        return this.http.post("api/truthTable/", ex).map(data => { console.log(data); return data.json() });
    }
    ExpressionToLogicGate(ex: Expression): Observable<string> {
        return this.http.post("api/logicGate/", ex).map(data => { console.log(data); return data.json() as string });
    }
    booleanIdentity(ex: twoEx): Observable<boolean> {
        return this.http.post("/api/booleanIdentity/", ex).map(data => { console.log(data); return data.json() as boolean });
    }
    validation(ex: Expression): Observable<boolean> {
        return this.http.post("api/validation/", ex).map(data => {
            console.log(data); return data.json() as boolean
        });
    }
    svg(): Observable<string> {
        return this.http.get("api/svg/").map(data => { console.log(data); return data.json() });
    }
    axiomList(): Observable<Axiom[]> {
        return this.http.post("/api/axiomList/",null).map(data => { console.log(data); return data.json()});
    }
}