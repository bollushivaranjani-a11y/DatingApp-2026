import { HttpClient  } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
 
import { EditableMember, Member, Photo } from '../../Types/member'; 
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
 

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient); 
  private baseurl = environment.apiUrl;
  editMode = signal(false);
  member = signal<Member | null>(null);

  getMembers()
  {
    alert(this.baseurl + 'members');
    return this.http.get<Member[]>(this.baseurl + 'members' );
  }

  getMember(id : string)
  {
    return this.http.get<Member>(this.baseurl + 'members/' + id ).pipe(
      tap(member => { this.member.set(member) })
    )
  }

 
getMemberPhotos(id : string)
{
return this.http.get<Photo[]>(this.baseurl + 'members/' + id + '/photos')
}


Updatemember(member : EditableMember)
{
 return this.http.put(this.baseurl + 'members', member);
}




}
