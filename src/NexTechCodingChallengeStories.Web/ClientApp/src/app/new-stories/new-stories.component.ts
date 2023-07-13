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

  constructor(  private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getNewStoriesCount();
  }
  
  // get stories count
  public getNewStoriesCount() {
    this.http.get<number>(this.baseUrl + `codechallenge/storiesCount`)
      .subscribe(result => {
        this.collectionSize = result;
        this.calculateTotalPages = this.collectionSize / this.perPage;
        this.loadOnePage(1);  
      }, error => console.error(error));

  }
  // setup 1st page
  public loadOnePage(currentPage: number) {
    this.http.get<StroyTitle[]>(`${this.baseUrl}codechallenge/onePage?p=${currentPage}&ps=${this.perPage}`)
      .subscribe(result => {
        this.stories = result;
      }, error => console.error(error));

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
