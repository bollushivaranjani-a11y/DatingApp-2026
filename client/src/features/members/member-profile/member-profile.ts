import { Component, HostListener, inject, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';
 
import { EditableMember, Member } from '../../../Types/member';
import { DatePipe } from '@angular/common';
import { MemberService } from '../../../core/services/member-service';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastService } from '../../../core/services/toast-service';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-member-profile',
  imports: [DatePipe, FormsModule],
  templateUrl: './member-profile.html',
  styleUrl: './member-profile.css',
})
export class MemberProfile implements OnInit, OnDestroy {

@ViewChild('editForm') editForm ? : NgForm;
@HostListener('window:beforeunload',['$event']) notify($event:BeforeUnloadEvent)
{
  if(this.editForm?.dirty)
  {
    $event.preventDefault();
  }
}
private accountservice = inject(AccountService)
  protected memberservice = inject(MemberService)
  private toast = inject(ToastService) 
  protected editablemember  : EditableMember ={
      displayName : '',
      description :'',
      city : '',
      country : ''
  }

  
ngOnInit(): void {


     this.editablemember = {
      displayName : this.memberservice.member()?.displayName || '' ,
      description : this.memberservice.member()?.description || '' ,
      city : this.memberservice.member()?.city || '' ,
      country : this.memberservice.member()?.country || '' ,
    }

  }

updateProfile()
{
  if(!this.memberservice.member()) return;

  const updatedMember = {...this.memberservice.member(), ...this.editablemember }
  this.memberservice.Updatemember(this.editablemember).subscribe({
    next : () => {
      const currentuser = this.accountservice.currentuser();
      if(currentuser && updatedMember.displayName !== currentuser?.displayName )
      {
        currentuser.displayName = updatedMember.displayName;
        this.accountservice.setCurrentUser(currentuser);
      }
 console.log("==== updateProfile ========" + updatedMember);
 this.toast.success('Profile updated successfully');
 this.memberservice.editMode.set(false);
 this.memberservice.member.set(updatedMember as Member);
 this.editForm?.reset(updatedMember);
    }
  })

}

  ngOnDestroy(): void {
 if(this.memberservice.editMode())
 {
  this.memberservice.editMode.set(false)
 }
  }
 

}
