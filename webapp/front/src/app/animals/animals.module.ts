import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AnimalsComponent} from './animals/animals.component';
import {AnimalsTableComponent} from './animals/animals-table/animals-table.component';
import {MatTableModule} from "@angular/material/table";
import { AnimalsActionsComponent } from './animals/animals-table/animals-actions/animals-actions.component';
import {MatButtonModule} from "@angular/material/button";
import { AnimalAddComponent } from './animals/animals-table/animals-actions/animal-add/animal-add.component';
import {MatDialogModule} from "@angular/material/dialog";
import {ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatIconModule} from "@angular/material/icon";


@NgModule({
  declarations: [
    AnimalsComponent,
    AnimalsTableComponent,
    AnimalsActionsComponent,
    AnimalAddComponent,
  ],
  exports: [
    AnimalsComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule
  ]
})
export class AnimalsModule {
}
