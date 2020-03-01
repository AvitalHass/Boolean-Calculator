
import { NgModule } from '@angular/core';
import {  MatButtonBase, MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatToolbar, MatCard, MatButtonToggleModule, MatInputModule, MatTableModule, MatDialogModule } from '@angular/material';


@NgModule({
    imports: [MatButtonModule,
       
        MatMenuModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatButtonToggleModule,
        MatInputModule,
        MatDialogModule
    ],
    exports: [
       
        MatButtonModule,
        MatMenuModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatToolbar,
        MatCard,
        MatButtonToggleModule,
        MatTableModule,
        MatInputModule,
        MatDialogModule
    ]
})
export class MaterialModule {

}