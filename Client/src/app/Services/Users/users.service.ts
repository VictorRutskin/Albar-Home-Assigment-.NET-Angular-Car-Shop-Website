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
  LogStatus:boolean = false;

  constructor(private http: HttpClient,private jwtHelper: JwtHelperService) { }

  get token(): any {
    return localStorage.getItem('jwt');
  }
  PostLogin(credentials: Credentials): Observable<Credentials> {
    return this.http.post<Credentials>(environment.ServerUrl+"/api/User/Login", credentials, { responseType: 'json' });
  }

  LogOut(){
    localStorage.removeItem('jwt'); // Remove the JWT token from storage
    this.LogStatus = false; // Update the LoggedIn flag
  }

  IsUserAuthenticated() {
    const token = localStorage.getItem('jwt');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.LogStatus=true
      return this.LogStatus;
    }
    this.LogStatus = false;
    return this.LogStatus;
  }
  


}
