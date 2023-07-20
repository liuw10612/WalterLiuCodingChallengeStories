import { TestBed,  ComponentFixture, } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { NewStoriesDataComponent } from './new-stories.component';
import * as Rx from 'rxjs';
import { delay } from "rxjs/operators";


describe('NewStoriesDataComponent', () => {
  let fixture: ComponentFixture<NewStoriesDataComponent>;

  beforeEach(() => {
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
    fixture = TestBed.createComponent(NewStoriesDataComponent);
    fixture.detectChanges();
  });
  
  it('should create the app', () => {
    const fixture = TestBed.createComponent(NewStoriesDataComponent);
    const component = fixture.debugElement.componentInstance;
    expect(component).toBeTruthy();
  });

  // test page size
  it('should start with page size 10, then 20 when clicked(20)', () => {
    const countElement = fixture.nativeElement.querySelector('b');
    expect(countElement.textContent).toEqual('10');

    const page20Button = fixture.nativeElement.querySelector('button[name="page20"]');
    page20Button.click();
    fixture.detectChanges();
    console.log(countElement.textContent);
    expect(countElement.textContent).toEqual('20');
  });

  
  // test observables
  //https://ng-mocks.sudo.eu/extra/mock-observables/#:~:text=A%20mock%20observable%20in%20Angular,defaultMock%20.
  
});
