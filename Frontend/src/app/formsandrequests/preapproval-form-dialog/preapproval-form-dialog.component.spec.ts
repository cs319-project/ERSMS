import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreapprovalFormDialogComponent } from './preapproval-form-dialog.component';

describe('PreapprovalFormDialogComponent', () => {
  let component: PreapprovalFormDialogComponent;
  let fixture: ComponentFixture<PreapprovalFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreapprovalFormDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreapprovalFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
