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
  LoggedIn: boolean = false;
  myUser: User = {
    name: '',
    id: 0,
    password: '',
    lastLogin: '',
  };

  // For Local Creds Saving
  constructor(private router: Router, private usersService: UsersService) {}

  ngOnInit(): void {
    this.usersService.IsUserAuthenticated();
    this.LoggedIn = this.usersService.LogStatus;
  }
  public UserAuthenticated() {
    this.usersService.IsUserAuthenticated();
  }

  async login(form: NgForm) {
    const credentials = {
      name: form.value.name,
      password: form.value.password,
    };

    this.myUser.name = credentials.name;
    this.myUser.password = credentials.password;

    await this.usersService.PostLogin(credentials).subscribe(
      (response) => {
        const token = (<any>response).token;
        localStorage.setItem('jwt', token);
        this.invalidLogin = false;
        if (this.usersService.IsUserAuthenticated()) {
          this.usersService.LogStatus = true;
        }
        location.reload();
      },
      (err) => {
        this.invalidLogin = true;
      }
    );
  }
}
