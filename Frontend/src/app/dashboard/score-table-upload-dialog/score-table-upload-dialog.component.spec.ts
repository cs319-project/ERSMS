import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScoreTableUploadDialogComponent } from './score-table-upload-dialog.component';

describe('ScoreTableUploadDialogComponent', () => {
  let component: ScoreTableUploadDialogComponent;
  let fixture: ComponentFixture<ScoreTableUploadDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScoreTableUploadDialogComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScoreTableUploadDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
