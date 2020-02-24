import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_service/auth.service';
import { ActivatedRoute } from '@angular/router';
import { HistoryGame } from 'src/app/_model/historyGame';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {

  historyGame: HistoryGame[];

  constructor(public authService: AuthService,
              private router: ActivatedRoute) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.router.data.subscribe(data => {
      this.historyGame = data.history;
    });
  }

}
