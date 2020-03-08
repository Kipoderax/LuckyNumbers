import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_service/user.service';
import { DecimalPipe } from '@angular/common';
import { User } from 'src/app/_model/user';

@Component({
  selector: 'app-right-side',
  templateUrl: './right-side.component.html',
  styleUrls: ['./right-side.component.css']
})
export class RightSideComponent implements OnInit {

  statusServerData: number[];
  registered: number;
  sendBets: number;
  amountOfThrees: number;
  amountOfFours: number;
  amountOfFives: number;
  amountOfSixes: number;
  percentChangeForThree: string;
  percentChangeForFour: string;
  percentChangeForFive: string;
  percentChangeForSix: string;

  users: User[];
  user1: User[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.loadStatus();
    this.load5BestPlayers();
    this.loadBest5LastXp();
  }

  loadStatus() {
    this.userService.getStatus().subscribe((status: number[]) => {
      this.registered = status[0];
      this.sendBets = status[1];
      this.amountOfThrees = status[2];
      this.amountOfFours = status[3];
      this.amountOfFives = status[4];
      this.amountOfSixes = status[5];
      this.percentChangeForThree = ((status[2] / status[1]) * 100).toFixed(2);
      this.percentChangeForFour = (status[3] / status[1] * 100).toFixed(2);
      this.percentChangeForFive = (status[4] / status[1] * 100).toFixed(2);
      this.percentChangeForSix = (status[5] / status[1] * 100).toFixed(3);
    });
  }

  load5BestPlayers() {
    this.userService.get5BestPlayers().subscribe( (users: User[]) => {
      this.users = users;
    });
  }

  loadBest5LastXp() {
    this.userService.getLast5Xp().subscribe( (users: User[]) => {
      this.user1 = users;
    });
  }

}
