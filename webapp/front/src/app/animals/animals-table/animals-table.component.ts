import {ChangeDetectionStrategy, Component} from '@angular/core';
import {AnimalsDataSource} from "./animals.data-source";
import {ApiClient, IAnimalViewModel} from "../../core/api.client";
import {nameof, NgOnDestroy} from "../../core/functions";

@Component({
  selector: 'app-animals-table',
  templateUrl: './animals-table.component.html',
  styleUrls: ['./animals-table.component.scss'],
  providers: [NgOnDestroy],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AnimalsTableComponent {

  displayedColumns: string[] = [
    'position',
    nameof<IAnimalViewModel>('name'),
    'actions'
  ];
  dataSource: AnimalsDataSource;

  constructor(apiClient: ApiClient) {
    this.dataSource = new AnimalsDataSource(apiClient);
  }
}
