import { NO_ERRORS_SCHEMA } from '@angular/core';
import { TestBed, ComponentFixture } from '@angular/core/testing';
import { of } from 'rxjs';

import { NewStoriesDataComponent, StoryTile } from './new-stories.component';
import { HttpDataService } from '../services/httpData.service';

class MockHttpDataService implements Partial<HttpDataService> {
  responseMockObservable = [{
    id: '1',
    time: new Date(),
    title: 'Title1',
    url: '/data'
  }];
  getNewStoriesCount$() {
    return of(123);
  }
  loadOnePage$(currentPage: number, pageSize: number) {
    return of(this.responseMockObservable);
  }
  fullSearch$(searchText: string) {
    return of(this.responseMockObservable);
  }
}

describe('NewStoriesDataComponent', () => {
  let responseStories: StoryTile[];

  let component: NewStoriesDataComponent;
  let fixture: ComponentFixture<NewStoriesDataComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewStoriesDataComponent],
      providers: [{ provide: HttpDataService, useClass: MockHttpDataService }],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewStoriesDataComponent);
    component = fixture.componentInstance;
  });

  it('should create NewStoriesDataComponent', () => {
    expect(component).toBeTruthy();
  });

  it('Should get total story count from observable', () => {
    let httpDataService = TestBed.inject<HttpDataService>(HttpDataService);
    spyOn(httpDataService, 'getNewStoriesCount$').and.callThrough();
    expect(component.collectionSize).toBe(123);
  });

  it('Should get 1 story for loadOnePage$ from observable', () => {
    let httpDataService = TestBed.inject<HttpDataService>(HttpDataService);
      spyOn(httpDataService, 'loadOnePage$').withArgs(1, 10).and.callThrough();
      component.stories$?.subscribe(result => {
      responseStories = result;
    });
    expect(responseStories[0].id).toBe('1');

  });
});
