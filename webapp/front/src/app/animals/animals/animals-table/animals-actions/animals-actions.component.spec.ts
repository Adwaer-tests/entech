import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimalsActionsComponent } from './animals-actions.component';

describe('AnimalsActionsComponent', () => {
  let component: AnimalsActionsComponent;
  let fixture: ComponentFixture<AnimalsActionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnimalsActionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnimalsActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
