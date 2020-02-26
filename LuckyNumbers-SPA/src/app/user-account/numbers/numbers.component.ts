import { Component, OnInit } from '@angular/core';
import { UserSendedBets } from 'src/app/_model/userSendedBets';
import { AuthService } from 'src/app/_service/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/_service/user.service';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-numbers',
  templateUrl: './numbers.component.html',
  styleUrls: ['../user-account.component.css']
})
export class NumbersComponent implements OnInit {

  sendedBets: UserSendedBets[];
  leftSendToBets: number;
  lottoNumbers: UserSendedBets;

  private updateSubscription: Subscription;


  constructor(private authService: AuthService, private userService: UserService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit() {
    this.loadUserSendedBets();
    this.loadNumberBetsToSend();
  }

  loadUserSendedBets() {
    this.router.data.subscribe(data => {
      this.sendedBets = data.bets;
    });
  }

  loadNumberBetsToSend() {
     this.userService.getUser(this.authService.decodedToken.unique_name).subscribe(data =>
       this.leftSendToBets = data.maxBetsToSend);
  }

  sendGenerateNumbers() {
    this.userService.sendLottoNumber(this.authService.decodedToken.nameid, {}).subscribe(() => {
      // this.loadUserSendedBets();
      window.location.reload();
    }, error => {
      console.log(error);
    });
  }
}
