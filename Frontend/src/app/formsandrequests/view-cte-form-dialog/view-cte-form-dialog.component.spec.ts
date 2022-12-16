import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCteFormDialogComponent } from './view-cte-form-dialog.component';

describe('ViewCteFormDialogComponent', () => {
  let component: ViewCteFormDialogComponent;
  let fixture: ComponentFixture<ViewCteFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewCteFormDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewCteFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
