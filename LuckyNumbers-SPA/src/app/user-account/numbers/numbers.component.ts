import { Component, OnInit } from '@angular/core';
import { UserSendedBets } from 'src/app/_model/userSendedBets';
import { AuthService } from 'src/app/_service/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-numbers',
  templateUrl: './numbers.component.html',
  styleUrls: ['../user-account.component.css']
})
export class NumbersComponent implements OnInit {

  sendedBets: UserSendedBets[];

  constructor(private authService: AuthService, private router: ActivatedRoute) { }

  ngOnInit() {
    this.loadUserSendedBets();
  }

  loadUserSendedBets() {
    this.router.data.subscribe(data => {
      this.sendedBets = data.bets;
    });
  }

}
