import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_model/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_service/user.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: User;
  percentChanceForThree: string;
  percentChanceForFour: string;
  percentChanceForFive: string;
  percentChanceForSix: string;

  constructor(public router: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getUser(this.router.snapshot.params.username)
      .subscribe( (user: User) => {
        this.user = user;
        this.percentChanceForThree = (this.user.amountOfThree / this.user.betsSended).toPrecision(2);
        this.percentChanceForFour = (this.user.amountOfFour / this.user.betsSended).toPrecision(2);
        this.percentChanceForFive = (this.user.amountOfFive / this.user.betsSended).toPrecision(1);
        this.percentChanceForSix = (this.user.amountOfSix / this.user.betsSended).toPrecision(1);
      });
  }
}
