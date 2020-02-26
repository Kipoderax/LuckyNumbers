import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_model/user';
import { HistoryGame } from '../_model/historyGame';
import { UserSendedBets } from '../_model/userSendedBets';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users');
  }

  getUser(username: string): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + username);
  }

  getStatus(): Observable<number[]> {
    return this.http.get<number[]>(this.baseUrl + 'status');
  }

  get5BestPlayers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'server');
  }

  getLast5Xp(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'xp');
  }

  getHistoryGameUser(username: string): Observable<HistoryGame[]> {
    return this.http.get<HistoryGame[]>(this.baseUrl + 'history/' + username);
  }

  getUserSendedBets(username: string): Observable<UserSendedBets[]> {
    return this.http.get<UserSendedBets[]>(this.baseUrl + 'sended-bets/' + username);
  }

}
