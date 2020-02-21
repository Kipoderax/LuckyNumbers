import { Component, OnInit } from '@angular/core';
import { UserService } from '../_service/user.service';
import { Router } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css']
})
export class UserSearchComponent implements OnInit {

  username: string;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  searchPlayer() {
    this.userService.getUser(this.username).subscribe(() => {
      if (this.username != null) {
        this.router.navigate(['/gracz/', this.username]);
      }
    }, error => {
      alertify.error('Nie ma takiego gracza');
    });
  }
}
