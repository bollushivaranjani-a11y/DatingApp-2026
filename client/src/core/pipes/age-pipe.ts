import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'age'
})
export class AgePipe implements PipeTransform {

  transform(value: string ): number {
     
    const today = new Date();
    const DOB = new Date(value);

    let age = today.getFullYear() - DOB.getFullYear()

    const monthdiff = today.getMonth() - DOB.getMonth()

    if(monthdiff < 0 || (monthdiff === 0  && today.getDate() < DOB.getDate() ) )
    {
      age--;
    }
 return age;
  }

}
