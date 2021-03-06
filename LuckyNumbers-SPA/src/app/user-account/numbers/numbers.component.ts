import { Component, OnInit } from '@angular/core';
import { UserSendedBets } from 'src/app/_model/userSendedBets';
import { AuthService } from 'src/app/_service/auth.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_service/user.service';

declare let alertify: any;

@Component({
  selector: 'app-numbers',
  templateUrl: './numbers.component.html',
  styleUrls: ['../user-account.component.css']
})
export class NumbersComponent implements OnInit {

  sendedBets: UserSendedBets[];
  lottoNumbers: UserSendedBets;
  leftSendToBets: number;

  constructor(private authService: AuthService, private userService: UserService, private router: ActivatedRoute) { }

  ngOnInit() {
    this.loadUserSendedBets();
    this.loadNumberBetsToSend();
  }

  loadUserSendedBets() {
    this.router.data.subscribe(data => {
      this.sendedBets = data.bets;
    });
  }

  loadNumberBetsToSend(): number {
    this.userService.getUser(this.authService.decodedToken.unique_name).subscribe(data =>
      this.leftSendToBets = data.maxBetsToSend - this.sendedBets.length);

    return this.leftSendToBets;
  }

  sendGenerateNumbers(amountBetsToSend: number) {
    if (amountBetsToSend > this.leftSendToBets) {
      alertify.error('Możesz wysłać co najwyżej jeszcze ' + this.leftSendToBets + ' zakładów.');
      return false;
    } else {
      this.userService.sendGenerateLottoNumbers(this.authService.decodedToken.nameid, {}, amountBetsToSend)
        .subscribe(() => {
          window.location.reload();
        }, error => {
          console.log(error);
        });
    }
  }

}
