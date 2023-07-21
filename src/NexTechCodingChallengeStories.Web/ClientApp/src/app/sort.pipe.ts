import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: "sort" })
export class SortPipe implements PipeTransform {
  transform(array: any, sort: any): any[] {

    if (!Array.isArray(array)) {
      return array;
    }

    let field: string;
    let ascending: boolean = true;
    if (Array.isArray(sort)) {
      field = sort[0];
      ascending = sort[1];
    } else {
      field = sort;
    }

    array.sort((a: any, b: any) => {
      if (a[field] < b[field]) {
        return ascending ? -1 : 1;
      } else if (a[field] > b[field]) {
        return ascending ? 1 : -1;
      } else {
        return 0;
      }
    });
    return array;
  }
}
