import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { User } from '../_model/user';
import { UserService } from '../_service/user.service';
import { AuthService } from '../_service/auth.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserSendedBets } from '../_model/userSendedBets';

@Injectable()
export class UserSendedBetsResolver implements Resolve<UserSendedBets[]> {

    constructor(private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<UserSendedBets[]> {
        return this.userService.getUserSendedBets(this.authService.decodedToken.unique_name).pipe(
            catchError(error => {
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
