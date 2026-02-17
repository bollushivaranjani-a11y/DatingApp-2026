import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../core/services/account-service';
import { ToastService } from '../core/services/toast-service';

export const authGuard: CanActivateFn = () => {
 const accountservice = inject(AccountService);
 const toast = inject(ToastService);

 if(accountservice.currentuser()) return true;
 else
 {
  toast.error('You shall not pass',5000);
  return false;
 }
  
};
