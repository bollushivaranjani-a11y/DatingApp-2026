import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { User } from '../../Types/users';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  //@Input({required:true}) membersfromApp : User[] =[];
 protected registermode = signal(false);

ShowRegister(value:boolean)
{
 this.registermode.set(value);
}
}
