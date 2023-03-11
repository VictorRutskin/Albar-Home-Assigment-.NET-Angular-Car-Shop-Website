import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './../../Models/User.model';
import { UsersService } from './../../Services/Users/users.service';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent {
  invalidLogin: boolean = false;

  constructor(
    private router: Router,
    private usersService: UsersService,
    private jwtHelper: JwtHelperService
  ) {}

  isUserAuthenticated() {
    const token = localStorage.getItem('jwt');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  myUser: User = {
    name: '',
    id: 0,
    password: '',
    lastLogin: '',
  };

  login(form: NgForm) {
    const credentials = {
      name: form.value.name,
      password: form.value.password,
    };

    this.myUser.name = credentials.name;
    this.myUser.password = credentials.password;

    this.usersService.PostLogin(credentials).subscribe(
      (response) => {
        const token = (<any>response).token;
        localStorage.setItem('jwt', token);
        this.invalidLogin = false;
        this.router.navigate(['/']);
      },
      (err) => {
        this.invalidLogin = true;
      }
    );
  }
}
