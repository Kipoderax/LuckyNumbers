import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_model/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient, private router: Router) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users');
  }

  getUser(username: string): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + username);
  }

  searchPlayer(username: string) {
    if (username.length === 0) {
      return false;
    }
    this.router.navigate(['/gracz/', username]);
  }

}
