import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';

declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      alertify.success('Konto zostało utworzone');
    }, error => {
      alertify.error(error);
    });
  }

}
