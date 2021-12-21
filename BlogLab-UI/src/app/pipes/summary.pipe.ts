import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'summary'
})
export class SummaryPipe implements PipeTransform {

  transform(content: string, characterCountLimit: number): string {
    if (content.length <= characterCountLimit){
      return content;
    }

    return `${content.substring(0, characterCountLimit)}...`;
  }

}
