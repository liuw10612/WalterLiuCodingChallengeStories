import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: "filter" })
export class FilterPipe implements PipeTransform {
  transform(array: any, searchText: string, properties: string[]): any[] {
    if (!searchText || !Array.isArray(array)) {
      return array;
    }
    return array.filter(item => {
      for (let property of properties) {
        if (!!item[property] && item[property].toString().toLowerCase().includes(searchText.toLowerCase())) {
          console.log('Filtered');
          return true;
        }
      }
      console.log('Not Filtered');
      return false;
    });
  }
}
