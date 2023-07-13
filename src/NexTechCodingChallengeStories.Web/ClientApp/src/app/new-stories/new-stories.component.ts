import { Component, Inject  } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
//import { Router, ActivatedRoute, ParamMap } from '@angular/router';
//import { switchMap } from 'rxjs/operators'

@Component({
  selector: 'app-new-stories',
  templateUrl: './new-stories.component.html'
})
export class NewStoriesDataComponent  {
  searchForm = new UntypedFormGroup({
    page: new UntypedFormControl(1),
    pageSize: new UntypedFormControl(10),
  });

  public forecasts: WeatherForecast[] = [];

  //public page: number = 1;
  //public pageSize: number = 10;
  //public pages: number[] = [];
  //public totalPages: number = 0;
  //public total: number = 1;
  page = 1;
  maxSize = 5;
  collectionSize = 450;
  perPage = 10;
  calculateTotalPages = this.collectionSize / this.perPage;

  constructor(  private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //http.get<WeatherForecast[]>(baseUrl + 'weatherforecast/allStories').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
    this.getNewStoriesCount();
   
  }
  //ngOnInit() {
  //  this.activateRouter.paramMap.pipe(
  //    switchMap((params: ParamMap) => {
  //      //this.page = params!.get('id');
  //      var tmp = params!.get('id');
  //      this.page = +tmp!;
  //      return "123";
  //    })).subscribe(res => {
  //      //this.data = res;
  //    })
  //}
  //public loadNewStories() {
  //  var formValue = this.searchForm.value;
  //  let page = formValue.page;
  //  let pageSize = formValue.pageSize;

  //  this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast/allStories')
  //    .subscribe(result => {
  //      this.forecasts = result;
  //      // TEST initial pages
  //      //this.total = 445;
  //      //var range = [];
  //      //let offset = page > 3 ? page - 3 : 0;
  //      //for (var i = offset * pageSize; i < this.total && range.length < 5; i += pageSize) {
  //      //  range.push(range.length + 1 + offset);
  //      //}
  //      //this.pages = range;
  //      //this.totalPages = this.total / pageSize + 1;
  //       // TEST initial pages
  //  }, error => console.error(error));

  //}

  public getNewStoriesCount() {

    this.http.get<number>(this.baseUrl + `codechallenge/storiesCount`)
      .subscribe(result => {
        this.collectionSize = result;
        this.calculateTotalPages = this.collectionSize / this.perPage;
        this.loadOnePage(1);
      }, error => console.error(error));

  }
  public loadOnePage(currentPage: number) {
    let headers = new HttpHeaders(
      {
        "X-Requested-Width": "XMLHttpRequest"
      }
    )
    const a = 12;
    console.log(`baseUrl = ${this.baseUrl}  current page = ${currentPage}`  );
    console.log(`${this.perPage}` + this.perPage);

    //this.http.get<WeatherForecast[]>(`${this.baseUrl}weatherforecast/onePage?p=${currentPage}&ps=${this.perPage}`)
    this.http.get<WeatherForecast[]>(`${this.baseUrl}codechallenge/onePage?p=${currentPage}&ps=${this.perPage}`)
      .subscribe(result => {
        this.forecasts = result;
        // TEST initial pages
        //this.total = 445;
        //var range = [];
        //let offset = page > 3 ? page - 3 : 0;
        //for (var i = offset * pageSize; i < this.total && range.length < 5; i += pageSize) {
        //  range.push(range.length + 1 + offset);
        //}
        //this.pages = range;
        //this.totalPages = this.total / pageSize + 1;
        // TEST initial pages
      }, error => console.error(error));

  }
  ////public setPage(page: number): void {
  ////  this.searchForm.controls.page.setValue(page);
  ////}
  //public onPageChange(page: number) {
  //  var page1 = page;
  //  alert(page1+"- real page ="+this.page);
  //}
  public onPageChange(event: Event) {
    const currentPage = event;
    console.log(currentPage + "- real page =" + this.page);
    //this.loadOnePageNewStories(+currentPage);
    this.loadOnePage(+currentPage);
  }
  //public onPaginationClick(page: number) {
  //  alert( "- real page =" + this.page);
  //}
  }

  
 

interface WeatherForecast {
  id: string;
  time: Date;
  title: string;
  url: string;

}
