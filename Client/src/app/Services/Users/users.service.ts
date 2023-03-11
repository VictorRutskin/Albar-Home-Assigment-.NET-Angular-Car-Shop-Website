import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/app/Environments/myEnvironment';
import { Observable } from 'rxjs';
import { Credentials } from 'src/app/Models/Credentials.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient,private jwtHelper: JwtHelperService) { }

  get token(): any {
    return localStorage.getItem('jwt');
  }
  PostLogin(credentials: Credentials): Observable<Credentials> {
    return this.http.post<Credentials>(environment.ServerUrl+"/api/User/Login", credentials, { responseType: 'json' });
  }

  IsUserAuthenticated() {
    const token = localStorage.getItem('jwt');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }
  


}
