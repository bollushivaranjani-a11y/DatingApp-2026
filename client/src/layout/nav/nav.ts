import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {

  protected accountservice  = inject(AccountService);
 protected router = inject(Router);
 private toast = inject(ToastService);

protected creds : any = {}

LoginSubmit()
{
    this.accountservice.LoginSubmit(this.creds).subscribe({
    next: result =>
    {
    //console.log(result);
 this.router.navigateByUrl('/members');
    this.creds = {};
    this.toast.success('Logged In Successfully',5000);
    },
    error : error => {
      this.toast.error(error.error,5000);
    }
    })
    }

logout()
{
  this.accountservice.logout();
  this.router.navigateByUrl('/');
}
}
