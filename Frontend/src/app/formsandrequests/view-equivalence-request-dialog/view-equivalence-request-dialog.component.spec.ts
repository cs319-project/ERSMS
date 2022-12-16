import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEquivalenceRequestDialogComponent } from './view-equivalence-request-dialog.component';

describe('ViewEquivalanceRequestDialogComponent', () => {
  let component: ViewEquivalenceRequestDialogComponent;
  let fixture: ComponentFixture<ViewEquivalenceRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewEquivalenceRequestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEquivalenceRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
