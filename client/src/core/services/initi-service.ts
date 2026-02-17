import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitiService {
  
  private accountService = inject(AccountService);

  init()
  {
      const userString = localStorage.getItem('user');
    if(!userString)       return of(null);
      const user = JSON.parse(userString);
      this.accountService.currentuser.set(user);
return of(null)

  }
}
