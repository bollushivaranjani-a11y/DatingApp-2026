import { Component, inject, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds, User } from '../../../Types/users';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
protected creds =   {} as RegisterCreds;
 //membersfromHome = input.required<User[]>();
 CancelRegister = output<boolean>();
private accountservice = inject(AccountService);
RegisterSubmit()
{
 // console.log(this.creds);
 this.accountservice.register(this.creds).subscribe({
  next : response => {
    console.log(response);
    this.CancelRegisterbtn();
 
  },
  error : error => console.log(error)
 })
}

CancelRegisterbtn()
{
 // console.log('Cancelled Register');
this.CancelRegister.emit(false);
}
}
