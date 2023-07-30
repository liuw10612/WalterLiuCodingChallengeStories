import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { iStoryTile, iCacheInfo } from '../new-stories/new-stories.component';

/**
 * A DI HTTP Serivce for UNIT TEST purpose
 * This is a HTTP subscribe service to call Restful api provided by CodingChallengeStories.Web.Services
 * The service make new-stories component unit test much easier.
 */

@Injectable({
  providedIn: 'root',
})
export class HttpDataService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }
  // get cache info to display
  getCacheInfo() {
    let url = this.baseUrl + `codechallenge/cacheInfo`;
    return this.http.get<iCacheInfo>(url, {});
  }

  // get total stories count
  getNewStoriesCount$() {
    return this.http.get<number>(this.baseUrl + `codechallenge/storiesCount`);
  }

  // load one page of stories
  loadOnePage$(currentPage: number, pageSize: number) {
    let url = `${this.baseUrl}codechallenge/onePage?p=${currentPage}&ps=${pageSize}`;
    return this.http.get<iStoryTile[]>(url, {});
  }

  // search all available stories by searchText
  fullSearch$(searchText: string) {
    let url = `${this.baseUrl}codechallenge/onePageFullSearch?s=${searchText}`;
    return this.http.get<iStoryTile[]>(url, {});
  }
}
