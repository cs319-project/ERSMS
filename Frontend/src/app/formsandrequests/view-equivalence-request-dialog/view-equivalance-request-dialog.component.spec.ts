import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEquivalanceRequestDialogComponent } from './view-equivalance-request-dialog.component';

describe('ViewEquivalanceRequestDialogComponent', () => {
  let component: ViewEquivalanceRequestDialogComponent;
  let fixture: ComponentFixture<ViewEquivalanceRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewEquivalanceRequestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEquivalanceRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
