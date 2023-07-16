/* tslint:disable:no-unused-variable */
import { Location } from "@angular/common";
import { TestBed, fakeAsync, tick } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { Router, RouterModule, Route } from "@angular/router";

import { AppComponent } from '../app.component';
import { HomeComponent } from "../home/home.component";
import { NewStoriesDataComponent } from "../new-stories/new-stories.component";

describe("Router: App", () => {
  let location: Location;
  let router: Router;
  let fixture;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule.withRoutes([
        { path: '', component: HomeComponent },
        { path: 'new-stories', component: NewStoriesDataComponent },
      ])
      ],
      declarations: [HomeComponent, NewStoriesDataComponent]
    }).compileComponents();

    router = TestBed.get(Router);
    location = TestBed.get(Location);
    fixture = TestBed.createComponent(AppComponent);
    
    router.initialNavigation();
  });

  it("fakeAsync works", fakeAsync(() => {
    let promise = new Promise(resolve => {
      setTimeout(resolve, 10);
    });
    let done = false;
    promise.then(() => (done = true));
    tick(50);
    expect(done).toBeTruthy();
  }));

  it('navigate to "" redirects you to /home', fakeAsync(() => {
    router.navigate([""]).then(() => {
      console.log('before home');
      console.log(location.path());
      expect(location.path()).toBe("/");
    });
  }));

  it('navigate to "new-stories" takes you to /new-stories', fakeAsync(() => {
    console.log(router);
    router.navigate(["/new-stories"]).then(() => {
      console.log('before new story');
      console.log(location.path());
      expect(location.path()).toBe("/new-stories");
    });
  }));
});
