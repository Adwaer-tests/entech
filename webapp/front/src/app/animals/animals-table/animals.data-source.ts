import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import {AnimalCreateRequest, AnimalViewModel, ApiClient, IAnimalViewModel} from "../../core/api.client";
import {BehaviorSubject, map, mergeMap, Observable, shareReplay, Subject, takeUntil, tap} from "rxjs";


interface localChange {
  add?: AnimalViewModel,
  remove?: number,
}

export class AnimalsDataSource extends DataSource<IAnimalViewModel> {
  private disconnected$ = new Subject<void>();
  private localChanges$ = new BehaviorSubject<localChange>({})

  noData$ = new BehaviorSubject<boolean>(false);

  constructor(private apiClient: ApiClient) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<IAnimalViewModel[]> {
    return this.apiClient.animalsGet().pipe(
      mergeMap(animals => this.localChanges$.pipe(
        map(change => {
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
      takeUntil(this.disconnected$),
      shareReplay(1)
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
