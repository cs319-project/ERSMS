import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManualPlacementDialogComponent } from './manual-placement-dialog.component';

describe('ManualPlacementDialogComponent', () => {
  let component: ManualPlacementDialogComponent;
  let fixture: ComponentFixture<ManualPlacementDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManualPlacementDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManualPlacementDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
