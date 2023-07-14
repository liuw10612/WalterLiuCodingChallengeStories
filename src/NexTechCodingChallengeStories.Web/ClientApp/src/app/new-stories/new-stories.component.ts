import { Component, Inject  } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-new-stories',
  templateUrl: './new-stories.component.html',
  styleUrls: ['./new-stories.component.css']
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
  // load one page
  public loadOnePage(currentPage: number) {
    this.searchText = "";   // reset filter string
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
    if (+currentPage != this.page) {
      console.log(currentPage + "- real page =" + this.page);
      this.loadOnePage(+currentPage);
    }
  }

  public onPageSizeChange(pageSize: number) {
    if (this.perPage != pageSize) {
      this.perPage = pageSize;
      this.loadOnePage(this.page);
    };
  }
  // search all available stories by searchText
  public fullSearch() {
    if (this.searchText.trim()=="") {
      alert("Please write someing on the search box");
      return;
    }
    if (confirm('Are you sure to do a full search, it may take a while?')) {
      this.perPage = 100; // make size big enough for holding all the full searchs
      this.page = 1;
      this.loading = true;
      this.http.get<StroyTitle[]>(`${this.baseUrl}codechallenge/onePageFullSearch?s=${this.searchText}`)
        .subscribe(result => {
          this.loading = false;
          this.stories = result;
        }, error => {
          this.loading = false;
          console.error(error);
        });
    }
  }
 }
interface StroyTitle {
  id: string;
  time: Date;
  title: string;
  url: string;
}
