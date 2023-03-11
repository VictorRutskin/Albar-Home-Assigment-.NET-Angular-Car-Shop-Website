import { User } from './../../Models/User.model';
import { UsersService } from './../../Services/Users/users.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { environment } from 'src/app/Environments/myEnvironment';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent {
  invalidLogin:boolean = false;

  constructor(private router: Router, private http: HttpClient, private usersService:UsersService) {}
  myUser :User ={
    name: "",
    id: 0,
    password: '',
    lastLogin: ''
  }

  login(form: NgForm) {
    const credentials = {
      'name': form.value.name,
      'password': form.value.password,
    };

    this.myUser.name=credentials.name;
    this.myUser.password=credentials.password;

    this.usersService.PostLogin(credentials).subscribe((response) => {
      const token = (<any>response).token;
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
      this.router.navigate(['/']);
    }, err =>{
      this.invalidLogin = true;
    }
    );
  }
}
