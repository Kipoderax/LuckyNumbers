import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_model/user';

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
}
