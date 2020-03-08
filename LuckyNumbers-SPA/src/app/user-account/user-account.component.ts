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
  percentChanceForThree: any;
  percentChanceForFour: any;
  percentChanceForFive: any;
  percentChanceForSix: any;

  expToNextLevel: string;
  expForAllLevel: string;

  constructor(public authService: AuthService,
              private router: ActivatedRoute) { }

  ngOnInit() {
      this.loadUser();
  }

  loadUser() {
    this.router.data.subscribe(data => {
      this.user = data.user;

      this.percentChanceForThree = (this.user.amountOfThree / this.user.betsSended * 100).toFixed(2);
      this.percentChanceForFour = (this.user.amountOfFour / this.user.betsSended * 100).toFixed(2);
      this.percentChanceForFive = (this.user.amountOfFive / this.user.betsSended * 100).toFixed(3);
      this.percentChanceForSix = (this.user.amountOfSix / this.user.betsSended * 100).toFixed(4);

      this.expToNextLevel = this.needExpToNextLevel(this.user.level, this.user.experience).toString();
      this.expForAllLevel = this.needExpForAllLevel(this.user.level);
    });
  }

  needExpToNextLevel(level: number, experience: number) {
    if (experience === 0) {
      return 1;
    } else {
      return ((0.176777 * Math.pow(level + 1, 2.5)) - experience).toFixed();
    }
  }

  needExpForAllLevel(level: number) {
    return ((0.176777 * Math.pow(level + 1, 2.5)) -
           (0.176777 * Math.pow(level, 2.5))).toFixed();
  }

}
