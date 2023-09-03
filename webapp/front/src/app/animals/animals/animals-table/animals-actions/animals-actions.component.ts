import {ChangeDetectionStrategy, Component, EventEmitter, Output} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {AnimalAddComponent} from "./animal-add/animal-add.component";

@Component({
  selector: 'app-animals-actions',
  templateUrl: './animals-actions.component.html',
  styleUrls: ['./animals-actions.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AnimalsActionsComponent {
  @Output() onAdd = new EventEmitter<string>();
  constructor(public dialog: MatDialog) {}

  addData() {
    this.dialog.open(AnimalAddComponent, {
      width: '350px'
    }).afterClosed()
      .subscribe(result => {
        if (result) {
          this.onAdd.emit(result);
        }
      })
  }

}
