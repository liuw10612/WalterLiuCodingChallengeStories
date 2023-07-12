import { Component, Inject } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FetchdataService } from './fetchdata.service';

@Component({
  selector: 'app-page-test2',
  templateUrl: './page-test2.component.html'
})
export class PageTest2Component {

  page = 1;
  maxSize = 3;
  collectionSize = 1000
  perPage = 50;
  calculateTotalPages = this.collectionSize / this.perPage;

  //showingResultsText = "Showing results";
  //displayingClaimsText = "Displaying claims";
  //rowText = "Rows";
  //showingRowsText = "Showing rows";

  public onPageChange(page: number) {
    var page1 = page;
    console.log(page1 + "- real page =" + this.page);
  }
}
