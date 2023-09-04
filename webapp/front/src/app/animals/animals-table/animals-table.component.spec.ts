import {ComponentFixture, TestBed} from '@angular/core/testing';

import {AnimalsTableComponent} from './animals-table.component';
import {ApiClient, IAnimalViewModel} from "../../core/api.client";
import {AnimalsActionsComponent} from "./animals-actions/animals-actions.component";
import {CommonModule} from "@angular/common";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import {ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatIconModule} from "@angular/material/icon";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {HarnessLoader} from "@angular/cdk/testing";
import {TestbedHarnessEnvironment} from "@angular/cdk/testing/testbed";
import {nameof} from "../../core/functions";
import {MatTableHarness} from "@angular/material/table/testing";
import {Observable, of} from "rxjs";

const materialModules = [
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatIconModule,
    MatProgressBarModule
];

const animals: IAnimalViewModel[] = [
    {id: 1, name: 'Cat'},
    {id: 2, name: 'Dog'},
    {id: 3, name: 'Pig'},
];

const createSpyApiClient = (): any => {
    const closureAnimals = [...animals];
    return {
        [nameof<ApiClient>('animalsGet')]: jasmine.createSpy().and.returnValue(of(closureAnimals)),
        [nameof<ApiClient>('animalsDelete')]: jasmine.createSpy().and.returnValue(of(null)),
        [nameof<ApiClient>('animalsPost')]: jasmine.createSpy().and.callFake((name: string): Observable<IAnimalViewModel> => {
            const maxId = closureAnimals
                .reduce((previousValue, currentValue) => {
                    return (currentValue?.id || 1) > (previousValue?.id || 1) ? currentValue : previousValue
                })?.id || 1;

            return of({id: maxId + 1, name});
        }),
    }
}

describe('AnimalsTableComponent', () => {
    let component: AnimalsTableComponent;
    let fixture: ComponentFixture<AnimalsTableComponent>;
    let loader: HarnessLoader;
    let table: MatTableHarness;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [AnimalsTableComponent, AnimalsActionsComponent],
            imports: [
                CommonModule,
                ReactiveFormsModule,
                ...materialModules,
            ],
            providers: [{
                provide: ApiClient,
                useValue: createSpyApiClient()
            }]
        }).compileComponents();

        fixture = TestBed.createComponent(AnimalsTableComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        loader = TestbedHarnessEnvironment.loader(fixture);
        table = await loader.getHarness(MatTableHarness);
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be visible 3 records in the table', async () => {
        const rows = await table.getRows();
        expect(rows.length).toBe(3);
    });

    it('should be visible 4 records in the table (after add)', async () => {
        component.dataSource.add('Sheep');

        const rows = await table.getRows();
        expect(rows.length).toBe(4);
    });

    it('should be visible 2 records in the table (after delete)', async () => {
        component.dataSource.remove(2);

        const rows = await table.getRows();
        expect(rows.length).toBe(2);
    });
});
