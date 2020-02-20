import { Component, OnInit } from '@angular/core';
import { UserService } from '../_service/user.service';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css']
})
export class UserSearchComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  searchPlayer(username: string) {
    this.userService.searchPlayer(username);
  }
}
