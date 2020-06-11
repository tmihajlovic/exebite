import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterViewByComponent } from './filter-view-by.component';

describe('FilterViewByComponent', () => {
  let component: FilterViewByComponent;
  let fixture: ComponentFixture<FilterViewByComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilterViewByComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterViewByComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
