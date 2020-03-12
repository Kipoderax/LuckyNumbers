import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_service/user.service';
import { LottoResult } from 'src/app/_model/lottoResult';

@Component({
  selector: 'app-lotto-result',
  templateUrl: './lotto-result.component.html',
  styleUrls: ['./lotto-result.component.css']
})
export class LottoResultComponent implements OnInit {

  lottoResult: LottoResult;

  constructor(private router: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.router.data.subscribe( data => {
      this.lottoResult = data.result;

      console.log(this.lottoResult.failGoal);
    });
  }

}
