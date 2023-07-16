import { TestBed, async, fakeAsync, tick } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { NewStoriesDataComponent } from './new-stories.component';
import * as Rx from 'rxjs';
import { delay } from "rxjs/operators";


describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      declarations: [
        NewStoriesDataComponent 
      ],
      providers: [
        { provide: 'BASE_URL', useValue: "/data" }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(NewStoriesDataComponent);
    const component = fixture.debugElement.componentInstance;
    expect(component).toBeTruthy();
  });

});
