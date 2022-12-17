import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquivalenceRequestDialogComponent } from './equivalence-request-dialog.component';

describe('EquivalanceRequestDialogComponent', () => {
  let component: EquivalenceRequestDialogComponent;
  let fixture: ComponentFixture<EquivalenceRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EquivalenceRequestDialogComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EquivalenceRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
