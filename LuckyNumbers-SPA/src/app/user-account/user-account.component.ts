import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { User } from '../_model/user';
import { UserService } from '../_service/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  user: User;

  constructor(public authService: AuthService,
              private userService: UserService,
              private router: ActivatedRoute) { }

  ngOnInit() {
      this.router.data.subscribe(data => {
      this.user = data.user;
    });
  }
}
