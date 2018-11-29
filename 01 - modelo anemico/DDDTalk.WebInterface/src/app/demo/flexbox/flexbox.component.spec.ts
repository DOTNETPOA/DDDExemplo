import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FlexboxComponent } from './flexbox.component';

describe('FlexboxComponent', () => {
  let component: FlexboxComponent;
  let fixture: ComponentFixture<FlexboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlexboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlexboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
