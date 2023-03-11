import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/app/Environments/myEnvironment';
import { Observable } from 'rxjs';
import { Credentials } from 'src/app/Models/Credentials.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  PostLogin(credentials: Credentials): Observable<Credentials> {
    return this.http.post<Credentials>(environment.ServerUrl+"/api/User/Login", credentials, { responseType: 'json' });
  }
  


}
