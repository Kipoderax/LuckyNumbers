import { Component, OnInit } from '@angular/core';
import { ServerService } from '../_service/server.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  numbers: number[];

  constructor(private serverService: ServerService) { }

  ngOnInit() {
    this.getLatestLottoNumbers();
  }

  getLatestLottoNumbers() {
    return this.serverService.getLatestLottoNumbers().subscribe(
      (numbers: number[]) => { this.numbers = numbers; }
    );
  }

}
