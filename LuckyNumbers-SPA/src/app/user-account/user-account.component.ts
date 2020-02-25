import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { User } from '../_model/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  user: User;
  percentChanceForThree: string;
  percentChanceForFour: any;
  percentChanceForFive: any;
  percentChanceForSix: any;

  constructor(public authService: AuthService,
              private router: ActivatedRoute) { }

  ngOnInit() {
      this.loadUser();
  }

  loadUser() {
    this.router.data.subscribe(data => {
      this.user = data.user;
      this.percentChanceForThree = (this.user.amountOfThree / this.user.betsSended * 100).toPrecision(3);
      this.percentChanceForFour = (this.user.amountOfFour / this.user.betsSended * 100).toPrecision(3);
      this.percentChanceForFive = (this.user.amountOfFive / this.user.betsSended * 100).toPrecision(1);
      this.percentChanceForSix = (this.user.amountOfSix / this.user.betsSended * 100).toPrecision(1);
    });
  }

}
