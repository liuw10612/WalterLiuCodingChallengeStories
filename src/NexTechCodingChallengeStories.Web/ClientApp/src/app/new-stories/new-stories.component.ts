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
  public stories: StroyTitle[] = [];
  page = 1;
  maxSize = 5;
  collectionSize = 450;
  perPage = 10;
  calculateTotalPages = this.collectionSize / this.perPage;

  public loading: boolean = false;
  public searchText: string = '';

  constructor(  private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getNewStoriesCount();
  }
  
  // get stories count
  public getNewStoriesCount() {
    this.loading = true;
    this.http.get<number>(this.baseUrl + `codechallenge/storiesCount`)
      .subscribe(result => {
        this.loading = false;
        this.collectionSize = result;
        this.calculateTotalPages = this.collectionSize / this.perPage;
        this.loadOnePage(1);  
      }, error => {
        this.loading = false;
        console.error(error);
      });

  }
  // setup 1st page
  public loadOnePage(currentPage: number) {
    this.loading = true;
    this.http.get<StroyTitle[]>(`${this.baseUrl}codechallenge/onePage?p=${currentPage}&ps=${this.perPage}`)
      .subscribe(result => {
        this.loading = false;
        this.stories = result;
      }, error => {
        this.loading = false;
        console.error(error);
      });

  }
  public onPageChange(event: Event) {
    const currentPage = event;
    console.log(currentPage + "- real page =" + this.page);
    //this.loadOnePageNewStories(+currentPage);
    this.loadOnePage(+currentPage);
  }

  }

interface StroyTitle {
  id: string;
  time: Date;
  title: string;
  url: string;
}
