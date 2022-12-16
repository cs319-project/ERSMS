import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPreapprovalFormDialogComponent } from './view-preapproval-form-dialog.component';

describe('ViewPreapprovalFormDialogComponent', () => {
  let component: ViewPreapprovalFormDialogComponent;
  let fixture: ComponentFixture<ViewPreapprovalFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPreapprovalFormDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPreapprovalFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
