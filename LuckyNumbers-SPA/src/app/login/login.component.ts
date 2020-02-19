import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';

declare let alertify: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      alertify.success('Zalogowano');
    }, error => {
      alertify.error('Zły login lub hasło');
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    alertify.message('Wylogowano');
  }

}
