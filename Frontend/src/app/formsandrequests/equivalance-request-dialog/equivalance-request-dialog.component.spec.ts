import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquivalanceRequestDialogComponent } from './equivalance-request-dialog.component';

describe('EquivalanceRequestDialogComponent', () => {
  let component: EquivalanceRequestDialogComponent;
  let fixture: ComponentFixture<EquivalanceRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EquivalanceRequestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EquivalanceRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
