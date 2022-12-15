import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CteFormDialogComponent } from './cte-form-dialog.component';

describe('CteFormDialogComponent', () => {
  let component: CteFormDialogComponent;
  let fixture: ComponentFixture<CteFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CteFormDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CteFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
