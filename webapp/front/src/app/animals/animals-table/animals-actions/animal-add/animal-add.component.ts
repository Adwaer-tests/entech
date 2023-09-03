import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-animal-add',
  templateUrl: './animal-add.component.html',
  styleUrls: ['./animal-add.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AnimalAddComponent {
  formGroup: FormGroup;
  constructor(fb: FormBuilder, private dialogRef: MatDialogRef<any>,) {
    this.formGroup = fb.group({
      name: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)]
      ]
    })
  }

  save() {
    if (this.formGroup.invalid) {
      return;
    }

    this.dialogRef.close(this.formGroup.value.name);
  }
}
