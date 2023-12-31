import {DataSource} from "@angular/cdk/collections";
import {AnimalCreateRequest, AnimalViewModel, ApiClient, IAnimalViewModel} from "../../core/api.client";
import {
    BehaviorSubject,
    map,
    mergeMap,
    Observable,
    of,
    shareReplay,
    Subject,
    takeUntil,
    tap
} from "rxjs";


interface localChange {
    add?: AnimalViewModel,
    remove?: number,
}

export class AnimalsDataSource extends DataSource<IAnimalViewModel> {
    private disconnected$ = new Subject<void>();
    private localChanges$ = new BehaviorSubject<localChange>({})

    noData$ = new BehaviorSubject<boolean>(true);
    isLoading$ = new BehaviorSubject<boolean>(true);

    constructor(private apiClient: ApiClient) {
        super();
    }

    connect(): Observable<IAnimalViewModel[]> {
        return of(null).pipe(
            tap(() => this.isLoading$.next(true)),
            mergeMap(() =>
                this.apiClient.animalsGet().pipe(
                    mergeMap(animals => this.localChanges$.pipe(
                        map(change => {
                            console.log('change', change.remove, change.add);
                            if (change.remove) {
                                animals = animals.filter(a => a.id !== change.remove);
                            }

                            if (change.add) {
                                animals.push(change.add);
                            }

                            return animals;
                        })
                    )),
                    tap(items => this.noData$.next(!items.length)),
                    tap(() => this.isLoading$.next(false)),
                    takeUntil(this.disconnected$),
                    shareReplay(1)
                ))
        );
    }

    disconnect(): void {
        this.disconnected$.next();
    }

    add(name: string) {
        this.apiClient.animalsPost(new AnimalCreateRequest({name}))
            .subscribe({
                next: newItem => this.localChanges$.next({add: newItem}),
                error: err => alert(err.title)
            })
    }

    remove(id: number) {
        this.apiClient.animalsDelete(id)
            .subscribe({
                next: () => this.localChanges$.next({remove: id}),
                error: err => alert(err.title)
            })
    }
}
