import { HistoryGame } from '../_model/historyGame';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { UserService } from '../_service/user.service';
import { AuthService } from '../_service/auth.service';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class UserHistoryResolver implements Resolve<HistoryGame[]> {

    constructor(private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<HistoryGame[]> {

        return this.userService.getHistoryGameUser(this.authService.decodedToken.unique_name).pipe(
            catchError(error => {
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
