import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HomeComponent } from "./home.component";
import { TestBed, ComponentFixture } from "@angular/core/testing";
import { RouterLinkWithHref } from '@angular/router';

describe("HomeComponent", () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      schemas: [NO_ERRORS_SCHEMA],
    });

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it(`should have title Walter Liu`, () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.walterclass')?.textContent).toContain('Walter Liu');
  });

  it("should contain Hacker News API link", () => {
    const compiled = fixture.nativeElement as HTMLElement;
    let el = compiled.querySelector('p a[href]')?.attributes.getNamedItem("href");
    expect(el?.textContent).toEqual('https://github.com/HackerNews/API');
  });
});
