import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, BehaviorSubject, of } from 'rxjs';
import { catchError, map, finalize } from 'rxjs/operators';
import { HttpDataService } from '../services/httpData.service';

@Component({
  selector: 'app-new-stories',
  templateUrl: './new-stories.component.html',
  styleUrls: ['./new-stories.component.css']
})
export class NewStoriesDataComponent  {
  page = 1;
  maxSize = 5;
  collectionSize = 450;
  perPage = 10;
  calculateTotalPages = this.collectionSize / this.perPage;

  public sortColumn: string = 'name';
  public ascending: boolean = true;

  public displayLog: boolean = false;
  public loading: boolean = false;
  public searchText: string = '';

  stories$: Observable<any> | undefined;
  storyErrors$ = new BehaviorSubject<string>("");
  logEntries: LogEntry[] = [];

  // sort column
  public setSort(column: string) {
    if (this.sortColumn === column) {
      this.sortColumn = column;
      this.ascending = !this.ascending;
    } else {
      this.sortColumn = column;
      this.ascending = true;
    }
  }

  constructor(private httpDataService: HttpDataService) { 
    this.getNewStoriesCount();
  }

  // get total stories count
  public getNewStoriesCount() {
    this.loading = true;
    this.httpDataService.getNewStoriesCount$()
      .subscribe(
        {
          next: (result) => {
            this.loading = false;
            this.collectionSize = result;
            this.calculateTotalPages = this.collectionSize / this.perPage; // Issue : since bad url ones ignored, it may be less
            this.loadOnePage(1);
          },
          error: (err: any) => {
            this.collectionSize = 0;
            this.calculateTotalPages = 0;
            this.logMessage(`no story count data returned`);
          },
          complete: () => {
            this.logMessage(`Get stories count completed successfully : ${this.collectionSize}`);
          }
        });
  }
    
  // load one page
  public loadOnePage(currentPage: number) {
    this.searchText = "";   // reset filter string
    this.loading = true;
    this.storyErrors$.next(``);
    this.stories$ = this.httpDataService.loadOnePage$(currentPage, this.perPage)
      .pipe(
        map(v => {
          if (v) {
            this.logMessage(`Get one page stories completed successfully : ${currentPage}`);
            return v;
          } else {
            this.logMessage(`no story data returned`);
            return { noData: true };
          }
        }),
        catchError((error) => {
          console.error(`Error loading story data`, error);
          this.logMessage(`Error loading story data` + error.error);
          this.storyErrors$.next(error.error);
          return of()
        }),
        finalize(() => this.loading = false)
      );
  }
  public onPageChange(event: Event) {
    const currentPage = event;
    if (+currentPage != this.page) {
      //console.log(currentPage + "- real page =" + this.page);
      this.loadOnePage(+currentPage);
    }
  }

  public onPageSizeChange(pageSize: number) {
    if (this.perPage != pageSize) {
      this.perPage = pageSize;
      this.page = 1;  
      this.loadOnePage(this.page);
    };
  }

  // search all available stories by searchText
  public fullSearch() {
    if (this.searchText.trim()=="") {
      alert("Please write someing on the search box!");
      return;
    }
    if (confirm('Are you sure to do a full search, it may take a while?')) {
      this.perPage = 100; // make size big enough for holding all the full searchs result
      this.page = 1;
      this.loading = true;
      this.logMessage(`Full search stories is working hard ...`);
      this.storyErrors$.next(`Full search stories is working hard ...`);
      this.stories$ = this.httpDataService.fullSearch$(this.searchText)
        .pipe(
          map(v => {
            if (v) {
              this.logMessage(`Full search stories completed successfully`);
              return v;
            } else {
              this.logMessage(`no story returned : full search`);
              return { noData: true };
            }
          }),
          catchError((error) => {
            console.error(`Error doing full search`, error);
            this.logMessage(`Error doing full search` + error.error);
            this.storyErrors$.next(error.error);
            return of()
          }),
          finalize(() => this.loading = false)
        );
    }
  }

  public logMessage(message: string) {
    this.logEntries.push({
      message: message,
      time: new Date(),
      isError: false
    });
  }

 }
export interface StoryTile {
  id: string;
  time: Date;
  title: string;
  url: string;
}

interface LogEntry {
  message: string;
  time: Date;
  isError: Boolean;
}
