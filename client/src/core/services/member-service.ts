import { HttpClient  } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
 
import { Member, Photo } from '../../Types/member'; 
import { environment } from '../../environments/environment';
 

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient); 
  private baseurl = environment.apiURL;


  getMembers()
  {
    alert(this.baseurl + 'members');
    return this.http.get<Member[]>(this.baseurl + 'members' );
  }

  getMember(id : string)
  {
    return this.http.get<Member>(this.baseurl + 'members/' + id );
  }

 
getMemberPhotos(id : string)
{
return this.http.get<Photo[]>(this.baseurl + 'members/' + id + '/photos')
}







}
