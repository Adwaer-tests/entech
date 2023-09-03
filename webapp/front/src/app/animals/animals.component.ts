import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-animals',
  templateUrl: './animals.component.html',
  styleUrls: ['./animals.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AnimalsComponent {

}
