import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { Router } from '@angular/router';

declare let alertify: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      alertify.success('Zalogowano');
    }, error => {
      alertify.error('Zły login lub hasło');
    }, () => {
      this.router.navigate(['/moje-konto']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
