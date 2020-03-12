import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class ServerService {
    baseUrl = 'http://localhost:5000/api/';

    constructor(private http: HttpClient) { }

    getLatestLottoNumbers(): Observable<number[]> {
        return this.http.get<number[]>(this.baseUrl + 'latest-numbers');
    }
}
