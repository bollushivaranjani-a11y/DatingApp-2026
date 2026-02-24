import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { LoginCreds, RegisterCreds, User } from '../../Types/users';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';
 
 

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  
  
  private http = inject(HttpClient);
 currentuser  = signal<User | null>(null);
  baseurl =   environment.apiURL;

register(creds : RegisterCreds)
{
 return this.http.post<User>(this.baseurl + 'account/register' , creds).pipe(
      tap( user => {
        if(user)
        {
  this.setCurrentUser(user)
        }
      })
    ) 
}

 

  LoginSubmit(creds : LoginCreds)
  {
    return this.http.post<User>(this.baseurl + 'account/login' , creds).pipe(
      tap( user => {
        if(user)
        {
  this.setCurrentUser(user)
        }
      })
    )
  }

  setCurrentUser(user : User)
  {
     localStorage.setItem('user',JSON.stringify(user));
     alert( localStorage.setItem('user',JSON.stringify(user)));
          this.currentuser.set(user)
  }

  logout()
  {
    this.currentuser.set(null);
    localStorage.removeItem('user');
  }
}
