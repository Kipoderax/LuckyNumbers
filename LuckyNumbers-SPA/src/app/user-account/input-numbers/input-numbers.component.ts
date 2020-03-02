import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_service/user.service';
import { AuthService } from 'src/app/_service/auth.service';
import { UserSendedBets } from 'src/app/_model/userSendedBets';

declare let alertify: any;

@Component({
  selector: 'app-input-numbers',
  templateUrl: './input-numbers.component.html',
  styleUrls: ['./input-numbers.component.css']
})
export class InputNumbersComponent implements OnInit {

  model: any = {};
  sendedBets: UserSendedBets[];
  leftSendToBets: number;

  constructor(private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
  }

  saveInputNumbers() {
    return this.userService.sendInputLottoNumbers(this.authService.decodedToken.nameid, this.model).subscribe(
      () => alertify.success('Liczby wysłano'),
      error => alertify.error('Sprawdz czy liczby są poprawnie podane lub stan salda na koncie')
    );
  }
}
