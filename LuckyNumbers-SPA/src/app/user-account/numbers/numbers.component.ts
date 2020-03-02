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

  sendGenerateNumbers(numberBetsToGenerate: number) {
    if (numberBetsToGenerate > this.leftSendToBets) {
      alertify.error('Możesz wysłać co najwyżej jeszcze ' + this.leftSendToBets + ' zakładów.');
      return false;
    } else {
      for (let i = 0; i < numberBetsToGenerate; i++) {
        this.userService.sendGenerateLottoNumbers(this.authService.decodedToken.nameid, {}, numberBetsToGenerate).subscribe(() => {
          this.reloadPage(i, numberBetsToGenerate);
        }, error => {
          console.log(error);
        });
      }
    }
  }

  reloadPage(iteration: number, isSended: number) {
    if (iteration === isSended - 1) {
      // this.loadUserSendedBets();
      // this.loadNumberBetsToSend();
      window.location.reload();
    }
  }
}
