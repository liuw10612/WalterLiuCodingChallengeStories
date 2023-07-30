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
  public cacheInfoText: string = '';
  public cacheInfo!: iCacheInfo;

  stories$: Observable<any> | undefined;
  storyErrors$ = new BehaviorSubject<string>("");
  logEntries: iLogEntry[] = [];

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
    this.getCacheInfo();
  }

  // get cache Info for tooltip display
  public getCacheInfo() {
    this.loading = true;
    this.httpDataService.getCacheInfo()
      .subscribe(
        {
          next: (result) => {
            this.loading = false;
            this.cacheInfo = result;
            this.cacheInfoText = `Pages to cache 1-${this.cacheInfo.cachePages} for page size=${this.cacheInfo.cachePageSize}, cache expires after ${this.cacheInfo.cacheExpireHours} hours`;
          },
          error: (err: any) => {
            this.logMessage(`no cache info data returned`);
          },
          complete: () => {
            this.logMessage(`Get cache info completed successfully : ${this.cacheInfoText}`);
          }
        });
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
    
  // load one page of stories
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

  // load a new page
  public onPageChange(event: Event) {
    const currentPage = event;
    if (+currentPage != this.page) {
      this.loadOnePage(+currentPage);
    }
  }

  // page size changed, load the 1st page for the new page size
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

  // display logs to show customer what is going on
  public logMessage(message: string) {
    this.logEntries.push({
      message: message,
      time: new Date(),
      isError: false
    });
  }

 }
export interface iStoryTile {
  id: string;
  time: Date;
  title: string;
  url: string;
}

interface iLogEntry {
  message: string;
  time: Date;
  isError: Boolean;
}
export interface iCacheInfo {
  cachePages: number;
  cachePageSize: number;
  cacheExpireHours: number;
}
